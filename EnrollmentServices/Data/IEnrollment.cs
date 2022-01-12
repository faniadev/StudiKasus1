using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public interface IEnrollment 
    {
        bool SaveChanges();
        IEnumerable<Enrollment> GetAllEnrollment();
        Enrollment GetEnrollmentById(int id);
        Task<Enrollment> CreateEnrollment(Enrollment enrol);

    }
}
