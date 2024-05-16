using Dental_Clinic.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Models
{
    public class DentalHistory
    {
        public int Id {  get; set; }
        public string[] Up_Right_Details{ get; set; } = new string[8];
        public string[] Up_Left_Details { get; set; } = new string[8];
        public string[] Down_Right_Details { get; set; } = new string[8];
        public string[] Down_Left_Details { get; set; } = new string[8];

        public bool[] Up_Right_IsWorkingOn { get; set; } = new bool[8];
        public bool[] Up_Left_IsWorkingOn { get; set; } = new bool[8];
        public bool[] Down_Right_IsWorkingOn { get; set; } = new bool[8];
        public bool[] Down_Left_IsWorkingOn { get; set; } = new bool[8];

        public List<Image>? Images {  get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("Patient")]
        public int PatId { get; set; }
    }
}
