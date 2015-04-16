namespace Core.Infrastructure 
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DataRepository.Models;
    using Models;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;

    public abstract class Base<TDTO, TDBO>
        where TDBO : BaseDBO
        where TDTO : BaseDTO 
    {
        NyanEntities _entities;

        public Base(NyanEntities entitiesValue) {
            _entities = entitiesValue;
            SetupDbSet();
        }

        public NyanEntities Entities { get { return _entities; } }

        internal DbSet<TDBO> EntityDbSet { get; set; }
        private void SetupDbSet() {
            try {
                Type entType = typeof(NyanEntities);
                PropertyInfo entProp = entType.GetProperties().Single(pr => pr.PropertyType == typeof(DbSet<TDBO>));

                EntityDbSet = (DbSet<TDBO>)entProp.GetValue(_entities);
            }
            catch (InvalidOperationException ex) { throw new MyException(ex, "CODING ERROR: Make sure you have a valid DbSet<TDBO> setup in DataRepository.Models!"); }
        }

        #region Read
        /// <summary>
        /// Returns a query of all TDTO
        /// Will not include Deleted records.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TDTO> Read() {
            try {
                // Projection Query for TDBO >> TDTO:                
                var queryResult = EntityDbSet.Where(dbo => !dbo.Deleted).Project().To<TDTO>();
                return queryResult;
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public virtual TDTO Read(int id) {
            try {
                TDBO queryResult = EntityDbSet.Find(id);

                if (queryResult == null) // Ensure we found a result
                    throw new MyException(ExceptionResources.RecordNotFound);

                // TODO: Automapper language parameter??
                return Mapper.Map<TDTO>(queryResult);
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }
        #endregion

        #region Helpers (Reflection)

        private static Type _typeTDBO = typeof(TDBO);
        private static Type _typeTDTO = typeof(TDTO);

        #region KeyIDs
        private static PropertyInfo DBOPrimaryKeyProperty {
            get {
                var quickResult = _typeTDBO.GetProperties().FirstOrDefault(prop => prop.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(KeyAttribute)) != null);

                // If [Key] not set, try lookup by 'TypeNameID'
                if (quickResult == null) {
                    string keyName = _typeTDBO.Name + "ID";
                    return _typeTDBO.GetProperty(keyName);
                } else {
                    return quickResult;
                }
            }
        }
        internal object GetPrimaryKeyValue(TDBO dbo) {
            return DBOPrimaryKeyProperty.GetValue(dbo);
        }

        public static PropertyInfo DTOPrimaryKeyProperty {
            get {
                var quickResult = _typeTDTO.GetProperties().FirstOrDefault(prop => prop.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(KeyAttribute)) != null);

                // If [Key] not set, try lookup by 'TypeNameID'
                if (quickResult == null) {
                    string keyName = _typeTDTO.Name + "ID";
                    return _typeTDTO.GetProperty(keyName);
                } else {
                    return quickResult;
                }
            }
        }
        public object GetPrimaryKeyValue(TDTO dto) {
            return DTOPrimaryKeyProperty.GetValue(dto);
        }
        #endregion

        #region Row Versions
        private static PropertyInfo DBORowVersionProperty {
            get {
                return _typeTDBO.GetProperty("RowVersion");
            }
        }

        private object GetRowVersion(TDBO dbo) {
            return DBORowVersionProperty.GetValue(dbo);
        }

        /// <summary>
        /// Uses reflection to compare the RowVersion of two DBOs
        /// </summary>
        /// <param name="dbo1"></param>
        /// <param name="dbo2"></param>
        /// <returns>True if row versions match</returns>
        internal bool CompareRowVersions(TDBO dbo1, TDBO dbo2) {
            byte[] dboRV1 = (byte[])GetRowVersion(dbo1);
            byte[] dboRV2 = (byte[])GetRowVersion(dbo2);

            return Convert.ToBase64String(dboRV1) == Convert.ToBase64String(dboRV2);
        }
        #endregion

        #region Timestamps
        private static PropertyInfo DBOTimestampProperty {
            get {
                return _typeTDBO.GetProperty("RowVersionTimestamp");
            }
        }
        private static PropertyInfo DBOLastModifiedByProperty {
            get {
                return _typeTDBO.GetProperty("LastModifiedByUserID");
            }
        }
        internal TDBO UpdateLastEdited(TDBO dbo) {
            DBOTimestampProperty.SetValue(dbo, DateTime.UtcNow);
            //DBOLastModifiedByProperty.SetValue(dbo, CurrentUser.UserID);

            return dbo;
        }
        internal IEnumerable<TDBO> UpdateLastEdited(IEnumerable<TDBO> dbos) {
            foreach (var item in dbos) {
                UpdateLastEdited(item); 
            }

            return dbos;
        }
        #endregion

        #endregion
    }
}
