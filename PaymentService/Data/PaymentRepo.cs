using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class PaymentRepo : IPaymentRepo<Payment>
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            try
            {
                var result = await _context.Payments.AddAsync(payment);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<Payment> GetPaymentByID(int id)
        {
            var result = await _context.Payments.Where(e => e.PaymentID == id).AsNoTracking().SingleAsync();
            return result;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsForEnrollment()
        {
            var result = await _context.Payments.AsNoTracking().ToListAsync();
            return result;
        }
    }
}