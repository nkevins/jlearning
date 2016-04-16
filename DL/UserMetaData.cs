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
        [DisplayName("NRIC")]
        public string NRIC { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Obsolete")]
        public string ObsInd { get; set; }
    }

   
}
