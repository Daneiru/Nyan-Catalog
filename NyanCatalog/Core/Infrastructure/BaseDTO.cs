namespace Core.Models 
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public abstract class BaseDTO 
    {        
        //[Display(Name = "LastModifiedByUserID", ResourceType = typeof(MainResources))]
        public int LastModifiedByUserID { get; set; }
        
        //[Display(Name = "Deleted", ResourceType = typeof(MainResources))]
        public bool Deleted { get; set; }

        [Timestamp]
        [SuppressMessageAttribute("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")] // Intended as byte[].
        public byte[] RowVersion { get; set; }

        public string RowVersionString {
            get {
                if (this.RowVersion != null)
                    return Convert.ToBase64String(this.RowVersion);
                else
                    return string.Empty;
            }
            set {
                if (string.IsNullOrEmpty(value))
                    this.RowVersion = null;
                else {
                    try {
                        this.RowVersion = Convert.FromBase64String(value);
                    }
                    catch (FormatException) { this.RowVersion = null; }
                }
            }
        }
        
        //[Display(Name = "RowVersionTimestamp", ResourceType = typeof(MainResources))]
        public DateTime RowVersionTimestamp { get; set; }
    }
}
