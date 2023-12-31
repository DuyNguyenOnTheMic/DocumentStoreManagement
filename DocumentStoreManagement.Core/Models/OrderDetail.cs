﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// OrderDetail collection - to store order details of customer order
    /// </summary>
    public class OrderDetail : BaseEntity
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        [Required]
        [ForeignKey("Document")]
        public string DocumentId { get; set; }
        [JsonIgnore]
        public Document Document { get; set; }
        [Required]
        [ForeignKey("Order")]
        public string OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
