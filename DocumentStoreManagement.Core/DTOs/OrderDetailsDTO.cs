using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.DTOs
{
    public class OrderDetailsDTO
    {
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string DocumentId { get; set; }
    }
}
