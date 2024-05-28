using Dental_Clinic.Models;

namespace Dental_Clinic.Interfaces
{
    public interface IImageRepo
    {
        public Task<ICollection<Image>> GetAll();
        public Task<ICollection<Image>> GetByDentalHistId(int denId);
        public Task<Image> GetById(int id);
        public Task<Image> GetByFileName(string fileName);
        public void Create(Image image);
        public void CreateRange(List<Image> images);
        public void Update(Image image);
        public void Delete(Image image);
        public void DeleteRange(List<Image> images);
    }
}
