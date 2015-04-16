namespace DataRepository.Initializers 
{
    using Models;
    using System.Collections.Generic;

    public class NyanInitializer 
    {
        public List<Nyan> IniList = new List<Nyan> {
            new Nyan { CatalogID = 1, Title = "Nyaaaaaaaaaaa", GenreID = 1, IsURL = true, ImageURL = "http://upload.wikimedia.org/wikipedia/en/thumb/e/ed/Nyan_cat_250px_frame.PNG/220px-Nyan_cat_250px_frame.PNG" },
            new Nyan { CatalogID = 1, Title = "Nyaan!", GenreID = 1, IsURL = true, ImageURL = "http://38.media.tumblr.com/8210fd413c5ce209678ef82d65731443/tumblr_mjphnqLpNy1s5jjtzo1_400.gif" }
        };

        public void Seed(NyanEntities context) {
            IniList.ForEach(seedItem => context.Nyans.Add(seedItem));
            context.SaveChanges();
        }
    }
}
