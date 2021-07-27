using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EFDbDbFirst.Models
{
    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(50)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
        [StringLength(50)]
        public string Alternative { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Contacts")]
        public virtual User User { get; set; }
    }
}
