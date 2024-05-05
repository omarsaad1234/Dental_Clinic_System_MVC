using Dental_Clinic.Models;

namespace Dental_Clinic.Interfaces
{
    public interface IDentalHistoryRepo
    {
        public Task<ICollection<DentalHistory>> GetAll();
        public Task<DentalHistory> GetById(int id);
        public Task<DentalHistory> GetByPatMobile(string mobile);
        public Task<DentalHistory> GetByPatId(int patId);
        public bool Create(DentalHistory denHistory);
        public bool Update(DentalHistory denHistory);
        public bool Delete(DentalHistory denHistory);
        public bool DentalHistoryExists(int id);
        public bool Save();
    }
}
