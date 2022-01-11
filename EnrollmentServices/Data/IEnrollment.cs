using System;
using System.Collections.Generic;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public interface IEnrollment 
    {
        bool SaveChanges();
        IEnumerable<Enrollment> GetAllEnrollment();
        Enrollment GetEnrollmentById(int id);
        void CreateEnrollment(Enrollment enrol);

    }
}
