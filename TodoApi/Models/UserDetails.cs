using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
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
        [StringLength(50)]
        public string Number { get; set; }
        [StringLength(50)]
        public string HouseNumber { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string Town { get; set; }
        [StringLength(50)]
        public string State { get; set; }
        [StringLength(50)]
        public string Pincode { get; set; }
        [StringLength(50)]
        public string AddressType { get; set; }
    }
}
