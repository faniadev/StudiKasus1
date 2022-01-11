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
        private ApplicationDbContext _db;
        public EnrollmentDAL(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            //var results = await _db.Enrollments.Include(e => e.Student)
            //    .Include(e => e.Course).AsNoTracking().ToListAsync();

            var results = await _db.Enrollments.OrderBy(c => c.EnrollmentID).Include(e => e.Student)
                .Include(e => e.Course).AsNoTracking().ToListAsync();

            return results;
        }

        public async Task<Enrollment> GetById(string id)
        {
            //var result = await _db.Enrollments.Where(s => s.EnrollmentID == Convert.ToInt32(id)).SingleOrDefaultAsync<Enrollment>();
            //if (result != null)
            //    return result;
            //else
            //    throw new Exception("Data tidak ditemukan !");

            var result = await (from c in _db.Enrollments
                                where c.EnrollmentID == Convert.ToInt32(id)
                                select c).SingleOrDefaultAsync();
            if (result == null) throw new Exception($"data id {id} tidak ditemukan");

            return result;
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

        public async Task<Enrollment> Update(string id, Enrollment obj)
        {
            try
            {
                var result = await GetById(id);
                result.CourseID = obj.CourseID;
                result.StudentID = obj.StudentID;
                result.Grade = obj.Grade;
                await _db.SaveChangesAsync();
                obj.EnrollmentID = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        Task<IEnumerable<Enrollment>> ICrud<Enrollment>.GetByName(string title)
        {
            throw new NotImplementedException();
        }
    }
}
