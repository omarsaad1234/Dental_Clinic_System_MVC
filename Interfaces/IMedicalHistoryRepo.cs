using Dental_Clinic.Models;

namespace Dental_Clinic.Interfaces
{
    public interface IMedicalHistoryRepo
    {
        public Task<ICollection<MedicalHistory>> GetAll();
        public Task<MedicalHistory> GetById(int id);
        public Task<MedicalHistory> GetByPatMobile(string mobile);
        public Task<MedicalHistory> GetByPatId(int patId);
        public bool Create(MedicalHistory medHistory);
        public bool Update(MedicalHistory medHistory);
        public bool Delete(MedicalHistory medHistory);
        public bool MedicalHistoryExists(int id);
        public bool Save();
    }
}
