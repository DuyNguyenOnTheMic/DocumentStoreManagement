using DocumentStoreManagement.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.DTOs
{
    public class OrderDTO : BaseEntity
    {
        public OrderDTO()
        {
            OrderDetailsDTOs = new HashSet<OrderDetailsDTO>();
        }

        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public int Status { get; set; }
        public virtual ICollection<OrderDetailsDTO> OrderDetailsDTOs { get; set; }
    }
}
