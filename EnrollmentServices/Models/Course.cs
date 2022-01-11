using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentServices.Models
{
    //[Table("Course")]
    public class Course
    {
        //[Column("Course_id")]
        public int CourseID { get; set; }

        //[Column("Judul")]
        //[MaxLength(255)]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        //[Column(TypeName ="decimal(5,2)")]
        [Required]
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}