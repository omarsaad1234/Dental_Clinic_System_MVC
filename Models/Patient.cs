using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Patient
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        [MaxLength(15)]
        [MinLength(11,ErrorMessage ="Mobile Number Isn't Complete")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile must be numeric")]
        public string Mobile {  get; set; }
        [Range(1,100,ErrorMessage ="Age Must Be Between 1 , 100")]
        public int Age {  get; set; }
        public string? ChiefComplain {  get; set; }
        public string? Procedure {  get; set; }
        public DentalHistory? DentalHistory { get; set; }
        public MedicalHistory? MedicalHistory {  get; set; }
        public Invoice? Invoice { get; set; }
        public List<Appointment>? Appointments {  get; set; }
    }
}
