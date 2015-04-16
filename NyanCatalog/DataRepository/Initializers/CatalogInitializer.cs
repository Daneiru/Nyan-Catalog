namespace DataRepository.Initializers 
{
    using Models;
    using System.Collections.Generic;

    public class CatalogInitializer 
    {
        public List<Catalog> IniList = new List<Catalog> {
            new Catalog { Name = "Boots and Cats and.." }
        };

        public void Seed(NyanEntities context) {
            IniList.ForEach(seedItem => context.Catalogs.Add(seedItem));
            context.SaveChanges();
        }
    }
}
