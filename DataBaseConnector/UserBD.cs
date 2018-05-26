using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBaseConnector
{
    public class UserBD
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Second")]
        public string SecondName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Education")]
        public string Education { get; set; }
        [Display(Name = "Theme")]
        public OrderTheme InterestedTheme { get; set; }
        [Display(Name = "Information")]
        public string Information { get; set; }
        [Display(Name = "Photolink")]
        public string PhotoLink { get; set; }
        [Display(Name = "WorksPerformed")]
        public int WorksPerformed { get; set; }
        public int WorksOrdered { get; set; }
    }
}
