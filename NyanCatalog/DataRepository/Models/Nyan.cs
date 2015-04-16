namespace DataRepository.Models 
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Nyan")]
    public partial class Nyan : BaseDBO 
    {
        public Nyan() {
            //Users = new HashSet<User>();
        }

        [Key]
        public int NyanID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
        
        public string Note { get; set; }

        public bool IsURL { get; set; }

        public string ImageURL { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImageData { get; set; }

        [Column(TypeName = "image")]
        public byte[] ImageThumbnail { get; set; }

        #region 1 : m Nyans
        [ForeignKey("CatalogID")]
        public virtual Catalog Catalog { get; set; }
        public int CatalogID { get; set; }

        [ForeignKey("GenreID")]
        public virtual Genre Genre { get; set; }
        public int GenreID { get; set; }
        #endregion

        #region 1 Nyan : m
        //[ForeignKey("NyanID")]
        //public virtual ICollection<User> Users { get; set; }
        #endregion
    }
}
