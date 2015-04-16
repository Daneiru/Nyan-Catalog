namespace DataRepository.Models {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Catalog")]
    public partial class Catalog : BaseDBO {
        public Catalog() {
            Nyans = new HashSet<Nyan>();
        }

        [Key]
        public int CatalogID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        #region 1 : m Catalogs
        //[ForeignKey("StatusCodeID")]
        //public virtual StatusCode StatusCode { get; set; }
        //public int StatusCodeID { get; set; }
        #endregion

        #region 1 Catalog : m
        [ForeignKey("CatalogID")]
        public virtual ICollection<Nyan> Nyans { get; set; }
        #endregion
    }
}
