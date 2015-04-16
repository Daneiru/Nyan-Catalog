namespace Core.Infrastructure 
{
    using System.Collections.Generic;
    using System.Linq;

    interface IBaseCrud<TDTO, TDBO> 
    {
        //IEnumerable<TDBO> Create(IEnumerable<TDBO> createObjects);
        IEnumerable<TDTO> Create(IEnumerable<TDTO> createObjects);
        //TDBO Create(TDBO createObject);
        TDTO Create(TDTO createObject);

        IQueryable<TDTO> Read();
        TDTO Read(int id);

        //IEnumerable<TDBO> Update(IEnumerable<TDBO> updateObjects);
        IEnumerable<TDTO> Update(IEnumerable<TDTO> updateObjects);
        //TDBO Update(TDBO updateObject);
        TDTO Update(TDTO updateObject);

        //void Delete(IEnumerable<TDBO> deleteObjects);
        void Delete(IEnumerable<TDTO> deleteObjects);
        //void Delete(TDBO deleteObject);
        TDTO Delete(TDTO deleteObject);

        //IEnumerable<TDBO> Recover(IEnumerable<TDBO> recoverObjects);
        IEnumerable<TDTO> Recover(IEnumerable<TDTO> recoverObjects);
        //TDBO Recover(TDBO recoverObject);
        TDTO Recover(TDTO recoverObject);
    }
}
