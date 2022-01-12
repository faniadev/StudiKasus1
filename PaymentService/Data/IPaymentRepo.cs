using System;
using System.Collections.Generic;
using PaymentService.Models;

namespace PaymentService.Data
{
    public interface IPaymentRepo
    {
        bool SaveChanges();

        //enrollment
        IEnumerable<Enrollment> GetAllEnrollment();
        void CreateEnrollment(Enrollment enrol);
        bool EnrollmentExist(int enrollmentid);
        bool ExternalEnrollmentExist(int externalEnrollmentId);

        //payment
        IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid);
        Payment GetPayment(int enrollmentId,int paymentId);
        void CreatePayment(int enrollmentId, Payment payment);

    }
}
