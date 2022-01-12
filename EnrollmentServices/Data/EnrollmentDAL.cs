using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnrollmentServices.Models;

namespace EnrollmentServices.Data
{
    public class EnrollmentDAL : IEnrollment
    {
        private readonly ApplicationDbContext _db;
        public EnrollmentDAL(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Enrollment> CreateEnrollment(Enrollment enrol)
        {
        
            try
            {
                _db.Enrollments.Add(enrol);
                await _db.SaveChangesAsync();
                return enrol;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
       
        }

        public IEnumerable<Enrollment> GetAllEnrollment()
        {
            return _db.Enrollments.ToList();
        }

        public Enrollment GetEnrollmentById(int id)
        {
            return _db.Enrollments.FirstOrDefault(p=>p.EnrollmentID==id);
        }

        public bool SaveChanges()
        {
            return (_db.SaveChanges()>=0);
        }
    }
}