using Dental_Clinic.Models;

namespace Dental_Clinic.Interfaces
{
    public interface IAppointmentRepo
    {
        public Task<ICollection<Appointment>> GetAll();
        public Task<ICollection<Appointment>> GetByDate(DateOnly date);
        public Task<Appointment> GetById(int id);
        public Task<Appointment> GetByPatMobile(string mobile);
        public Task<ICollection<Appointment>> GetByPatId(int patId);
        public bool Create(Appointment appointment);
        public bool Update(Appointment appointment);
        public bool Delete(Appointment appointment);
        public bool AppointmentExists(int id);
        public bool Save();
    }
}
