using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models
{
    public class Reply
    {
        [Key]
        public int ReplyId { get; set; }
        public string Body { get; set; }
        public DateTime ReplyTime { get; set; }
        public virtual Post Post { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
