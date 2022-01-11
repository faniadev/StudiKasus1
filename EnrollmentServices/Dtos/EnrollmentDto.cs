using System;
using EnrollmentServices.Models;

namespace EnrollmentServices.Dtos
{
    public class EnrollmentDto
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Grade? Grade { get; set; }
    }
}

