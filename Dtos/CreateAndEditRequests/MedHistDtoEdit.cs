namespace Dental_Clinic.Dtos.CreateAndEditRequests
{
    public class MedHistDtoEdit
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PatId { get; set; }
    }
}
