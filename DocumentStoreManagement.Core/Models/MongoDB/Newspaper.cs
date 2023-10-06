using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models.MongoDB
{
    /// <summary>
    /// Newspaper collection - a document type
    /// </summary>
    public class Newspaper : Document
    {
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
