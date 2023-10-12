using DocumentStoreManagement.Core.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.DTOs
{
    public class OrderDetailsDTO : BaseEntity
    {
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [DefaultValue(null)]
        public string DocumentId { get; set; }
    }
}
