using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models
{
    public class PhotoAlbum
    {
        [Key]
        public int PhotoAlbumId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Family Family { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEnd { get; set; }
        [Timestamp]
        public virtual ICollection<Image> Images { get; set; }
    }
}
