using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Models
{
    public class Appointment
    {
        public int Id {  get; set; }
        [DataType(DataType.Date)]
        public DateOnly Date {  get; set; }
        public TimeOnly Time { get; set; }
        public Patient Patient {  get; set; }
        [ForeignKey("Patient")]
        public int PatId {  get; set; }
    }
}
