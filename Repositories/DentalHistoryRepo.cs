
using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Repositories
{
    public class DentalHistoryRepo : IDentalHistoryRepo
    {
        private readonly AppDbContext _context;

        public DentalHistoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<DentalHistory>> GetAll()
        {
            return await _context.DentalHistories.Include(d => d.Patient).ToListAsync();
        }

        public async Task<DentalHistory> GetById(int id)
        {
            return await _context.DentalHistories.Include(d => d.Patient).Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DentalHistory> GetByPatId(int patId)
        {
            return await _context.DentalHistories.Include(d => d.Patient).Where(d => d.PatId == patId).FirstOrDefaultAsync();
        }
        public bool Create(DentalHistory denHistory)
        {
            _context.DentalHistories.Add(denHistory);
            return Save();
        }
        public bool Update(DentalHistory denHistory)
        {
            _context.DentalHistories.Update(denHistory);
            return Save();
        }
        public bool Delete(DentalHistory denHistory)
        {
            _context.DentalHistories.Remove(denHistory);
            return Save();
        }

        public bool Save()
        {
            var saves = _context.SaveChanges();
            return saves > 0 ? true : false;
        }
        public bool DentalHistoryExists(int id)
        {
            return _context.DentalHistories.Any(d => d.Id == id);
        }

        public async Task<DentalHistory> GetByPatMobile(string mobile)
        {
            return await _context.DentalHistories.Include(d => d.Patient).Where(d => d.Patient.Mobile == mobile).FirstOrDefaultAsync();
        }

        public DentalHistory GetLast()
        {
            return _context.DentalHistories.OrderBy(d => d.Id).Last();
        }
    }
}
