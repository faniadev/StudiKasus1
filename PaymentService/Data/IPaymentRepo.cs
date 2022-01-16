using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Models;

namespace PaymentService.Data
{
    public interface IPaymentRepo<T>
    {
  
        //payment
        Task<IEnumerable<T>> GetPaymentsForEnrollment();
        Task<T> CreatePayment(T payment);
        Task<T> GetPaymentByID(int id);

    }
}
