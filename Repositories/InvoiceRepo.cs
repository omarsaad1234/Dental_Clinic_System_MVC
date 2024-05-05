using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Repositories
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly AppDbContext _context;

        public InvoiceRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Invoice>> GetAll()
        {
            return await _context.Invoices.Include(i => i.Patient).ToListAsync();
        }

        public async Task<Invoice> GetById(int id)
        {
            return await _context.Invoices.Include(i=>i.Patient).Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Invoice> GetByPatId(int patId)
        {
            return await _context.Invoices.Include(i=>i.Patient).Where(i => i.PatId == patId).FirstOrDefaultAsync();
        }
        public bool Create(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            return Save();
        }
        public bool Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            return Save();
        }
        public bool Delete(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            return Save();
        }
        public bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(i => i.Id == id);
        }

        public bool Save()
        {
            var saves = _context.SaveChanges();
            return saves > 0 ? true : false;
        }

        public async Task<Invoice> GetByPatMobile(string mobile)
        {
            return await _context.Invoices.Include(i => i.Patient).Where(i => i.Patient.Mobile == mobile).FirstOrDefaultAsync();
        }
    }
}
