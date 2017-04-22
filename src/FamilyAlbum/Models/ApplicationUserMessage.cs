using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models
{
    public class ApplicationUserMessage
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
