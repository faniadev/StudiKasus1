using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Dtos
{
    public class PaymentCreateDto
    {
        //[Required]
        public int EnrollmentID { get; set; }
        //[Required]
        public double TotalPrice { get; set; }
        
    }
}