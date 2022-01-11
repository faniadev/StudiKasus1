using System;
using System.Collections.Generic;
using PaymentService.Models;

namespace PaymentService.Data
{
    public interface IEnrollment 
    {
        bool SaveChanges();
        IEnumerable<Enrollment> GetAllEnrollment();
        Enrollment GetEnrollmentById(int id);
        void CreateEnrollment(Enrollment enrol);

    }
}
