using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Repositories
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppDbContext _context;

        public AppointmentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Appointment>> GetAll()
        {
            return await _context.Appointments.Include(a => a.Patient).OrderBy(a=>a.Date).ToListAsync();
        }
        public async Task<ICollection<Appointment>> GetByDate(DateOnly date)
        {
            return await _context.Appointments.Include(a => a.Patient).Where(a=>a.Date==date).ToListAsync();
        }

        public async Task<Appointment> GetById(int id)
        {
            return await _context.Appointments.Include(a => a.Patient).OrderBy(a => a.Date)
                .Where(a=>a.Id==id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Appointment>> GetByPatId(int patId)
        {
            return await _context.Appointments.Include(a => a.Patient).OrderBy(a => a.Date)
                .Where(a => a.PatId == patId).ToListAsync();
        }
        public bool Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            return Save();
        }
        public bool Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return Save();
        }
        public bool Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            return Save();
        }
        public bool Save()
        {
            var saves = _context.SaveChanges();
            return saves > 0 ? true : false;
        }
        public bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(a => a.Id == id);
        }

        public async Task<Appointment> GetByPatMobile(string mobile)
        {
            return await _context.Appointments.Include(a => a.Patient).OrderBy(a => a.Date)
                .Where(a => a.Patient.Mobile == mobile).FirstOrDefaultAsync();
        }

        
    }
}
