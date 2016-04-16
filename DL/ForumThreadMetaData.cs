using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    [MetadataType(typeof(ForumThreadMetaData))]
    public partial class ForumThread
    {
        public ForumPost GetLatestPost()
        {
            return this.ForumPosts.Where(x => x.ObsInd == "N").OrderByDescending(x => x.CreatedDate).Take(1).SingleOrDefault();
        }
    }

    public class ForumThreadMetaData
    {
        [Required(ErrorMessage = "Topic title is required.")]
        public string Title { get; set; }
    }
}
