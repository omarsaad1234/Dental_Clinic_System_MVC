namespace Dental_Clinic.Models
{
    public class Patient
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        
        public string Mobile {  get; set; }
        public int Age {  get; set; }
        public string? ChiefComplain {  get; set; }
        public string? Procedure {  get; set; }
        public DentalHistory? DentalHistory { get; set; }
        public MedicalHistory? MedicalHistory {  get; set; }
        public Invoice? Invoice { get; set; }
        public List<Appointment>? Appointments {  get; set; }
    }
}
