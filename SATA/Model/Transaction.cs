using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SATA.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime TransactionDateTime { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(3)")]
        public string CurrencyCode { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public TransactionStatus Status { get; set; }
    }
}
