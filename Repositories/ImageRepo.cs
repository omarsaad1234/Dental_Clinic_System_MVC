using Dental_Clinic.Data;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace Dental_Clinic.Repositories
{
    public class ImageRepo : IImageRepo
    {
        private readonly AppDbContext _context;

        public ImageRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Image>> GetAll()
        {
            return await _context.Images.Include(i => i.DentalHistory).ToListAsync();
        }

        public async Task<ICollection<Image>> GetByDentalHistId(int denId)
        {
            return await _context.Images.Include(i => i.DentalHistory).Where(i=>i.DentalHistory.Id==denId).ToListAsync();
        }

        public async Task<Image> GetById(int id)
        {
            return await _context.Images.Include(i => i.DentalHistory).Where(i=>i.Id==id).FirstOrDefaultAsync();
        }

        public void Create(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public void CreateRange(List<Image> images)
        {
            _context.Images.AddRange(images);
            _context.SaveChanges();
        }

        
        public void Update(Image image)
        {
            _context.Images.Update(image);
            _context.SaveChanges();
        }
        public void Delete(Image image)
        {
            _context.Images.Remove(image);
            _context.SaveChanges();
        }

        public async Task<Image> GetByFileName(string fileName)
        {
            return await _context.Images.Where(i => i.Url == fileName).FirstOrDefaultAsync();
        }
    }
}
