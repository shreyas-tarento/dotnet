using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFDbDbFirst.Models
{
    public class UserDetails
    {
        [Required]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(250)]
        public string AddressLine1 { get; set; }
        [StringLength(250)]
        public string AddressLine2 { get; set; }
        [StringLength(50)]
        public string Pincode { get; set; }
        [StringLength(50)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string Telephone { get; set; }
        [StringLength(50)]
        public string Alternative { get; set; }
    }
}
