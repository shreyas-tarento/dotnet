using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFDbCodeBase.Models
{
    public class Contact
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Mobile { get; set; }
        public string TelePhone { get; set; }
        public string Alternative { get; set; }
    }
}
