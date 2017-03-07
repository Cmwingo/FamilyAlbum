using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyAlbum.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public DateTime Birthday { get; set; }
        public Image AvatarImg { get; set; }
        public virtual Family Family { get; set; }
        public virtual ICollection<PhotoAlbum> PhotoAlbums { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        [InverseProperty("Sender")]
        public virtual ICollection<Message> OutgoingMessages { get; set; }
        [InverseProperty("Recipients")]
        public virtual ICollection<Message> IncomingMessages { get; set; }
    }
}
