using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [Timestamp]
        public byte[] TStamp { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
