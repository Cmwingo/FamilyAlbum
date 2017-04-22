using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public bool Read { get; set; }
        [Timestamp]
        public byte[] PostTime { get; set; }
        public ApplicationUser Sender { get; set; }
        public ICollection<ApplicationUserMessage> Recipients { get; set; }
    }
}
