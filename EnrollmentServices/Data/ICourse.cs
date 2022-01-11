using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public interface ICourse : ICrud <Course>
    {
        Task<IEnumerable<Course>> GetByTitle(string title);
    }
}
