using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly AppDbContext _context;

        public PatientRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Patient>> GetAll()
        {
            
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetById(int id)
        {
            return await _context.Patients.Where(p=>p.Id==id).FirstOrDefaultAsync();
        }

        public async Task<Patient> GetByMobile(string mobile)
        {
            return await _context.Patients.Where(p => p.Mobile == mobile).FirstOrDefaultAsync();
        }

        public bool PatientExists(int id)
        {
            return _context.Patients.Any(p => p.Id == id);
        }
        
        public bool Create(Patient patient)
        {
            _context.Patients.Add(patient);

            return Save();
        }
        public bool Update(Patient patient)
        {
            _context.Patients.Update(patient);
            return Save();
        }
        public bool Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
            return Save();
        }
        public bool Save()
        {
            try
            {
                var saves = _context.SaveChanges();
                return saves > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
