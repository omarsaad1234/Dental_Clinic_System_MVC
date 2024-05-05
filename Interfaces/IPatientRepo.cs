using Dental_Clinic.Models;

namespace Dental_Clinic.Interfaces
{
    public interface IPatientRepo
    {
        public Task<ICollection<Patient>> GetAll();
        public Task<Patient> GetById(int id);
        public Task<Patient> GetByMobile(string mobile);
        public bool Create(Patient patient);
        public bool Update(Patient patient);
        public bool Delete(Patient patient);
        public bool PatientExists(int id);
        public bool Save();
    }
}
