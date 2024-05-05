using Dental_Clinic.Models;

namespace Dental_Clinic.Interfaces
{
    public interface IInvoiceRepo
    {
        public Task<ICollection<Invoice>> GetAll();
        public Task<Invoice> GetById(int id);
        public Task<Invoice> GetByPatMobile(string mobile);
        public Task<Invoice> GetByPatId(int patId);
        public bool Create(Invoice invoice);
        public bool Update(Invoice invoice);
        public bool Delete(Invoice invoice);
        public bool InvoiceExists(int id);
        public bool Save();
    }
}

