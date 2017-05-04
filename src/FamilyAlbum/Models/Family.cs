using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FamilyAlbum.Models
{
    public class Family
    {
        [Key]
        public int FamilyId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Motto { get; set; }
        public string PhotoURL { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
    }
}
