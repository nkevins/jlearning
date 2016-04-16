using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    [MetadataType(typeof(ForumPostMetaData))]
    public partial class ForumPost
    {
    }

    public class ForumPostMetaData
    {
        [Required]
        public string Description { get; set; }
    }
}
