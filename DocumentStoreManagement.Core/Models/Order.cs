using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// Order collection - to store customer orders
    /// </summary>
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }
        [Required]
        [Unicode(false)]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public int Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
