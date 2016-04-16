using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
   public class StudentEnrollment
    {

       public string courseCode { get; set; }
       public string courseName { get; set; }

        [DisplayName("Lecturer")]
       public string lecturerName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [DisplayName("Start Date")]
       public DateTime? startDate { get; set; }

         [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
         [DisplayName("End Date")]
       public DateTime? endDate { get; set; }
       public int scheduleId { get; set; }

        [DisplayName("Description")]
       public string description { get; set; }
    }
}
