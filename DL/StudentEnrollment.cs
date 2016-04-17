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
   public class StudentEnrollment
    {

        [DisplayName("Course Code")]
       public string courseCode { get; set; }

        [DisplayName("Course Name")]
       public string courseName { get; set; }

        [DisplayName("Lecturer")]
       public string lecturerName { get; set; }

        [Required(ErrorMessage = "Schedule start date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [DisplayName("Start Date")]
       public DateTime? startDate { get; set; }

        [Required(ErrorMessage = "Schedule end date is required")]
        [DataType(DataType.Date)]
      
         [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
         [DisplayName("End Date")]
       public DateTime? endDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (endDate < startDate)
            {
                yield return new ValidationResult("End Date must be greater than Start Date");
            }
        }

       [Key]
       [DisplayName("Course Schedule ID")]
       public int scheduleId { get; set; }

        [DisplayName("Description")]
       public string description { get; set; }

        public List<SelectListItem> lstCourse { get; set; }
        public Int16 courseSelected { get; set; }

        public List<SelectListItem> lstLecturer { get; set; }
        public Int16 lecturerSelected { get; set; }

        [DisplayName("User Id")]
        public int userId { get; set; }

        [DisplayName("Course ID")]
        public int courseId { get; set; }
    }
}
