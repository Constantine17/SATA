using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SATA.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string MobileNo { get; set; }

        public ICollection<Transaction> Transactions  { get; set; }
    }
}
