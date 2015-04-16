namespace DataRepository.Initializers 
{
    using Models;
    using System.Collections.Generic;

    public class GenreInitializer 
    {
        public List<Genre> IniList = new List<Genre> {
            new Genre { Name = "Nyan!" },
            new Genre { Name = "Ron Swanson" },
            new Genre { Name = "Other" }
        };

        public void Seed(NyanEntities context) {
            IniList.ForEach(seedItem => context.Genres.Add(seedItem));
            context.SaveChanges();
        }
    }
}
