namespace Dental_Clinic.Dtos.CreateAndEditRequests
{
    public class PatientDtoRequest
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string? ChiefComplain { get; set; }
        public string? Procedure { get; set; }
    }
}
