using System;
using System.Linq;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{FirstName="Spongebob",LastName="Squarepants",EnrollmentDate=DateTime.Parse("2022-01-01")},
                new Student{FirstName="Yohan",LastName="Kang",EnrollmentDate=DateTime.Parse("2022-01-01")},
                new Student{FirstName="Gaon",LastName="Kim",EnrollmentDate=DateTime.Parse("2022-01-01")},
                new Student{FirstName="Yikyung",LastName="Song",EnrollmentDate=DateTime.Parse("2022-01-01")},
            };

            foreach (var s in students)
            {
                context.Students.Add(s);
            }

            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{Title="Python Fundamental",Credits=3},
                new Course{Title="Data Science",Credits=3},
                new Course{Title="Data Analytics",Credits=3},
            };

            foreach (var c in courses)
            {
                context.Courses.Add(c);
            }

            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentID=1,CourseID=1,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=2,Grade=Grade.B},
                new Enrollment{StudentID=1,CourseID=3,Grade=Grade.C},
                new Enrollment{StudentID=2,CourseID=1,Grade=Grade.D},
                new Enrollment{StudentID=2,CourseID=2,Grade=Grade.E},
                new Enrollment{StudentID=2,CourseID=3,Grade=Grade.A},
                new Enrollment{StudentID=3,CourseID=1,Grade=Grade.B},
                new Enrollment{StudentID=4,CourseID=2,Grade=Grade.A},
                new Enrollment{StudentID=4,CourseID=3,Grade=Grade.C},
            };

            foreach (var e in enrollments)
            {
                context.Enrollments.Add(e);
            }

            context.SaveChanges();
        }
    }
}
