using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Dtos.CreateAndEditRequests
{
    public class AppointmentDtoRequest
    {
        [DataType(DataType.Date)]
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int PatId { get; set; }
    }
}
