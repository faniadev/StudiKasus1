using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public double Price { get; set; }
    }
}