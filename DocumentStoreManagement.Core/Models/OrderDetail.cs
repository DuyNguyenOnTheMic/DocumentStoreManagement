﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DocumentStoreManagement.Core.Models
{
    /// <summary>
    /// OrderDetail collection - to store order details of customer order
    /// </summary>
    public class OrderDetail
    {
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }
        [Key]
        [Required]
        [ForeignKey("Document")]
        [JsonIgnore]
        public string DocumentId { get; set; }
        [JsonIgnore]
        public virtual Document Document { get; set; }
        [Key]
        [Required]
        [ForeignKey("Order")]
        [JsonIgnore]
        public string OrderId { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
