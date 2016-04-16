using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
      
    } 

    class UserMetaData
    {
        [Required(ErrorMessage = "NRIC is required")]
        public string NRIC { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
         [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DisplayName("Obsolete")]
        public string ObsInd { get; set; }

        [Required(ErrorMessage = "Salt value is required to hash with password")]
        public string Salt { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

   
}
