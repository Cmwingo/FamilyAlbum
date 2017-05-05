using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models.ReplyViewModel
{
    public class CreateReplyViewModel
    {
        [Required]
        public string Body { get; set; }
        public int PostId { get; set; }
    }
}
