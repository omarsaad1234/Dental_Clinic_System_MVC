using Dental_Clinic.Data;

namespace Dental_Clinic.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url {  get; set; }
        public Position Position {  get; set; }
        public int Index {  get; set; }
        public DentalHistory DentalHistory {  get; set; }
    }
}
