using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    [MetadataType(typeof(CourseMetaData))]
    public partial class Course
    {

    } 

    public class CourseMetaData
    {
        public int CourseID { get; set; }

       [DisplayName("Course Code")]
       [Required(ErrorMessage = "Course Code is required")]
        public string CourseCode { get; set; }

       [DisplayName("Course Name")]
       [Required(ErrorMessage = "Course Name is required")]
        public string CourseName { get; set; }
        public string ObsInd { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Course Description is required")]
        public string Description { get; set; }
    }
}
