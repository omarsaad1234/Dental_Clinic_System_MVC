using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Repositories
{
    public class MedicalHistoryRepo : IMedicalHistoryRepo
    {
        private readonly AppDbContext _context;

        public MedicalHistoryRepo(AppDbContext context)
        {
            _context = context;
        }
        

        public async Task<ICollection<MedicalHistory>> GetAll()
        {
            return await _context.MedicalHistories.Include(m => m.Patient).ToListAsync();
        }

        public async Task<MedicalHistory> GetById(int id)
        {
            return await _context.MedicalHistories.Include(m => m.Patient).Where(m=>m.Id==id).FirstOrDefaultAsync();
        }

        public async Task<MedicalHistory> GetByPatId(int patId)
        {
            return await _context.MedicalHistories.Include(m => m.Patient).Where(m => m.PatId == patId).FirstOrDefaultAsync();
        }

        public bool Create(MedicalHistory medHistory)
        {
            _context.MedicalHistories.Add(medHistory);
            return Save();
        }
        public bool Update(MedicalHistory medHistory)
        {
            _context.MedicalHistories.Update(medHistory);
            return Save();
        }
        public bool Delete(MedicalHistory medHistory)
        {
            _context.MedicalHistories.Remove(medHistory);
            return Save();
        }
        public bool MedicalHistoryExists(int id)
        {
            return _context.MedicalHistories.Any(m => m.Id == id);
        }

        public bool Save()
        {
            var saves = _context.SaveChanges();
            return saves > 0 ? true : false;
        }

        public async Task<MedicalHistory> GetByPatMobile(string mobile)
        {
            return await _context.MedicalHistories.Include(m => m.Patient).Where(m => m.Patient.Mobile == mobile).FirstOrDefaultAsync();
        }
    }
}
