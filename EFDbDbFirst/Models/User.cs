using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EFDbDbFirst.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Contacts = new HashSet<Contact>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(150)]
        public string LastName { get; set; }
        [Required]
        [StringLength(150)]
        public string UserName { get; set; }

        [InverseProperty(nameof(Address.User))]
        public virtual ICollection<Address> Addresses { get; set; }
        [InverseProperty(nameof(Contact.User))]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
