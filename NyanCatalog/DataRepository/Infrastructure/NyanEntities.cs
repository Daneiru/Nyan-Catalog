namespace DataRepository.Models 
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class NyanEntities : DbContext
    {
        public NyanEntities() : base("name=NyanEntities") { }

        public virtual DbSet<Nyan> Nyans { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }    
}
