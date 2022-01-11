using System;
using System.Threading.Tasks;
using EnrollmentServices.Dtos;

namespace EnrollmentServices.SyncDataServices.Http
{
    public interface IPaymentDataClient
    {
        Task CreateEnrollmentInPayment(EnrollmentForCreateDto plat);
    }
}
