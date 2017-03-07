using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public virtual PhotoAlbum Album { get; set; }
        [Required]
        public string Name { get; set; }
        public string Caption { get; set; }
        public string FilePath { get; set; }
        public DateTime Date { get; set; }
        [Timestamp]
        public byte[] UploadTime { get; set; }
    }
}
