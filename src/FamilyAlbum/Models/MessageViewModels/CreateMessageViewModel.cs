using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyAlbum.Models.MessageViewModels
{
    public class CreateMessageViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Display(Name = "To")]
        public string RecipientIds { get; set; }
    }
}
