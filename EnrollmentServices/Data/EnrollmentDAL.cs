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

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _db.Enrollments.Include(e => e.Course).Include(e => e.Student).AsNoTracking().ToListAsync();
            return results;
        }


        public async Task<Enrollment> GetById(string id)
        {
            var result = await (from c in _db.Enrollments
                                where c.EnrollmentID == Convert.ToInt32(id)
                                select c).SingleOrDefaultAsync();
            if (result == null) throw new Exception($"data id {id} tidak ditemukan");

            return result;
        }

        public Task<IEnumerable<Enrollment>> GetByName(string title)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            try
            {
                _db.Enrollments.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public bool SaveChanges()
        {
            return (_db.SaveChanges()>=0);
        }

        public Task<Enrollment> Update(string id, Enrollment obj)
        {
            throw new NotImplementedException();
        }
    }
}