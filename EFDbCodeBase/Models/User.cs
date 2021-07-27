using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFDbCodeBase.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string UserName { get; set; }
    }
}
