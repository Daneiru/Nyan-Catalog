namespace DataRepository.Models {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Genre")]
    public partial class Genre : BaseDBO {
        public Genre() {
            Nyans = new HashSet<Nyan>();
        }

        [Key]
        public int GenreID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        #region 1 : m Nyans
        //[ForeignKey("StatusCodeID")]
        //public virtual StatusCode StatusCode { get; set; }
        //public int StatusCodeID { get; set; }
        #endregion

        #region 1 Nyan : m
        [ForeignKey("GenreID")]
        public virtual ICollection<Nyan> Nyans { get; set; }
        #endregion
    }
}
