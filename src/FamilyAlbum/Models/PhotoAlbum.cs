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
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        [Timestamp]
        public byte[] DateCreate { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
