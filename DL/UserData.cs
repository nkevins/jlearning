using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DL
{
  public  class UserData
    {
        [Required(ErrorMessage = "NRIC is required")]
        public string NRIC { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DisplayName("Obsolete")]
        public string ObsInd { get; set; }

        
        public string Salt { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public int UserID { get; set; }

        [DisplayName("Role")]
        public List<SelectListItem> userRole { get; set; }
        public string selectedRole { get; set; }
    }
}
