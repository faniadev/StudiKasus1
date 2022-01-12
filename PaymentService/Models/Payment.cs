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
        //[Required]
        public int EnrollmentID { get; set; }
        public double TotalPrice { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}