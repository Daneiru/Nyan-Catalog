namespace DataRepository.Models 
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseDBO 
    {
        public BaseDBO() {
            RowVersion = BitConverter.GetBytes(0x0);
            RowVersionTimestamp = DateTime.UtcNow;
            Deleted = false;
        }

        public bool Deleted { get; set; }
        
        [Timestamp]
        [MaxLength(8)]
        [Column(TypeName = "timestamp")]
        public byte[] RowVersion { get; set; }

        public DateTime RowVersionTimestamp { get; set; }

        //public int LastModifiedByUserID { get; set; }
    }
}
