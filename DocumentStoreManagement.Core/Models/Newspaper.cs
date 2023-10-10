using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Newspaper collection - a document type
    /// </summary>
    [Table("Newspapers")]
    public class Newspaper : Document
    {
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
