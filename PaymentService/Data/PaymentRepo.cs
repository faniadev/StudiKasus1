using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateEnrollment(Enrollment enrol)
        {
            if(enrol==null)
                throw new ArgumentNullException(nameof(enrol));
            _context.Enrollments.Add(enrol); 
        }

        public async Task<Payment> CreatePayment(int enrollmentId, Payment payment)
        {
            // if(payment==null)
            //     throw new ArgumentNullException(nameof(payment));
            // payment.EnrollmentID = enrollmentId;
            // _context.Payments.Add(payment);

            try
            {
                payment.EnrollmentID = enrollmentId;
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return payment;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public bool EnrollmentExist(int enrollmentid)
        {
            return _context.Enrollments.Any(p=>p.EnrollmentID==enrollmentid);
        }

        public bool ExternalEnrollmentExist(int externalEnrollmentId)
        {
            return _context.Enrollments.Any(p=>p.ExternalID==externalEnrollmentId);
        }

        public IEnumerable<Enrollment> GetAllEnrollment()
        {
            return _context.Enrollments.ToList();
        }

        public Payment GetPayment(int enrollmentId, int paymentId)
        {
            return _context.Payments
            .Where(c=>c.EnrollmentID==enrollmentId && c.PaymentID == paymentId)
            .FirstOrDefault();
        }

        public IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid)
        {
            return _context.Payments
            .Where(c=>c.EnrollmentID==enrollmentid)
            .OrderBy(c=>c.Enrollment.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }
    }
}