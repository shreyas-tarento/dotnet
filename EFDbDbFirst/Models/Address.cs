using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EFDbDbFirst.Models
{
    [Table("Address")]
    public partial class Address
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(250)]
        public string AddressLine1 { get; set; }
        [StringLength(250)]
        public string AddressLine2 { get; set; }
        [StringLength(50)]
        public string Pincode { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Addresses")]
        public virtual User User { get; set; }
    }
}
