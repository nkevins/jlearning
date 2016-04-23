using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    [MetadataType(typeof(DocumentMetaData))]
    public partial class Document
    {
        public enum DocumentType
        {
            File = 1,
            Video = 2
        }
    }

    public class DocumentMetaData
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string FileName { get; set; }
    }
}
