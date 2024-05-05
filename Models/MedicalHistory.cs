using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Models
{
    public class MedicalHistory
    {
        public int Id {  get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("Patient")]
        public int PatId {  get; set; }
    }
}
