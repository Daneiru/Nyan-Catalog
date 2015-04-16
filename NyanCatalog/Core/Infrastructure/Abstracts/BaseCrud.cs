namespace Core.Infrastructure 
{
    using AutoMapper;
    using Models;
    using DataRepository.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public abstract class BaseCrud<TDTO, TDBO> : Base<TDTO, TDBO>, IBaseCrud<TDTO, TDBO>
        where TDBO : BaseDBO
        where TDTO : BaseDTO 
    {
        public BaseCrud(NyanEntities entities) : base(entities) { }

        #region Create
        internal virtual TDBO Create(TDBO createObject) {
            try {
                // Update timestamp & last modified by
                createObject = UpdateLastEdited(createObject);

                // Add & Return
                return EntityDbSet.Add(createObject);
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public virtual TDTO Create(TDTO createObject) {
            try {
                TDBO dbo = Mapper.Map<TDBO>(createObject); // Convert DTO >> DBO
                TDBO resultDBO = Create(dbo);

                Entities.SaveChanges(); // Save.

                return Mapper.Map<TDTO>(resultDBO); // Convert & Return result: DBO >> DTO
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        internal virtual IEnumerable<TDBO> Create(IEnumerable<TDBO> createObjects) {
            try {
                IEnumerable<TDBO> dbos = UpdateLastEdited(createObjects);

                return EntityDbSet.AddRange(dbos); // Create & Return.
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public IEnumerable<TDTO> Create(IEnumerable<TDTO> createObjects) {
            try {
                IEnumerable<TDBO> dbos = Mapper.Map<IEnumerable<TDBO>>(createObjects);  // Convert DTO >> DBO
                IEnumerable<TDBO> resultDBOs = Create(dbos);                            // Create new DBOs

                Entities.SaveChanges(); // Save.

                return Mapper.Map<IEnumerable<TDTO>>(resultDBOs); // Convert & Return result: DBO >> DTO
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }
        #endregion

        #region Update
        // An override can call back to this at the end AFTER applying and buissness logic to the DBO.
        internal virtual TDBO Update(TDBO updateObject) {
            try {
                // Get originalEntity based on PrimaryKey value:  
                var primaryKey = GetPrimaryKeyValue(updateObject);
                TDBO originalEntity = EntityDbSet.Find(primaryKey);

                // Validate concurrency
                if (!CompareRowVersions(originalEntity, updateObject))
                    throw new MyException(Resources.ExceptionResources.ConcurrencyException);

                // Update Last Edit:
                updateObject = UpdateLastEdited(updateObject);

                // Fix issue with Moqs..
                Entities.Entry(originalEntity).State = EntityState.Added;
                Entities.Entry(originalEntity).State = EntityState.Unchanged;

                // Update record
                Entities.Entry(originalEntity).CurrentValues.SetValues(updateObject);

                return updateObject; // Return.
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public virtual TDTO Update(TDTO updateObject) {
            try {
                TDBO dbo = Mapper.Map<TDBO>(updateObject);
                TDBO result = Update(dbo);

                Entities.SaveChanges(); // SAVE

                return Mapper.Map<TDTO>(result);
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }


        public virtual IEnumerable<TDTO> Update(IEnumerable<TDTO> updateObjects) {
            try {
                var resultList = new LinkedList<TDBO>();

                for (int i = 0; updateObjects.Count() > i; i++) {
                    TDBO dbo = Mapper.Map<TDBO>(updateObjects.ElementAt(i));
                    resultList.AddLast(Update(dbo));
                }

                Entities.SaveChanges(); // SAVE

                return Mapper.Map<IEnumerable<TDTO>>(resultList.AsEnumerable());
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public virtual IEnumerable<TDBO> Update(IEnumerable<TDBO> updateObjects) {
            try {
                LinkedList<TDBO> result = new LinkedList<TDBO>();
                foreach (var obj in updateObjects) {
                    result.AddLast(Update(obj));
                }

                return result;
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }
        #endregion

        #region Delete
        internal virtual TDBO Delete(TDBO deleteObject) {
            try {
                deleteObject.Deleted = true; // Soft delete flag.
                return Update(deleteObject);
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public TDTO Delete(TDTO deleteObject) {
            try {
                TDBO dbo = Mapper.Map<TDBO>(deleteObject);
                var result = Delete(dbo);

                Entities.SaveChanges();

                return Mapper.Map<TDTO>(result);
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }

        public void Delete(IEnumerable<TDTO> deleteObjects) {
            try {
                foreach (var item in deleteObjects) {
                    TDBO dbo = Mapper.Map<TDBO>(item);
                    Delete(dbo);
                }

                Entities.SaveChanges();
            }
            catch (MyException) { throw; }
            catch (Exception ex) { throw new MyException(ex); }
        }
        #endregion

        //#region Recover
        //internal virtual TDBO Recover(TDBO recoverObject);
        //public TDTO Recover(TDTO recoverObject);
        //public void Recover(IEnumerable<TDTO> recoverObjects) {
        //    foreach (var item in recoverObjects) {
        //        TDBO dbo = Mapper.Map<TDBO>(item);
        //        Recover(dbo);
        //    }
        //}
        //#endregion

        #region Temp Delete/Recover Placeholders
        internal void Delete(IEnumerable<TDBO> deleteObjects) {
            throw new NotImplementedException();
        }

        internal IEnumerable<TDBO> Recover(IEnumerable<TDBO> recoverObjects) {
            throw new NotImplementedException();
        }

        public IEnumerable<TDTO> Recover(IEnumerable<TDTO> recoverObjects) {
            throw new NotImplementedException();
        }

        internal TDBO Recover(TDBO recoverObject) {
            throw new NotImplementedException();
        }

        public TDTO Recover(TDTO recoverObject) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
