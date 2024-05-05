using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Models
{
    public class DentalHistory
    {
        public int Id {  get; set; }
        public string[]? Up_Right { get; set; } = new string[8];
        public string[]? Up_Left { get; set; } = new string[8];
        public string[]? Down_Right { get; set; } = new string[8];
        public string[]? Down_Left { get; set; } = new string[8];
        public Patient Patient { get; set; }
        [ForeignKey("Patient")]
        public int PatId { get; set; }
    }
}
