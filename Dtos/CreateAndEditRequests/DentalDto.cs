using Dental_Clinic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Dtos.CreateAndEditRequests
{
    public class DentalDto
    {
        public string[]? Up_Right { get; set; } = new string[8];
        public string[]? Up_Left { get; set; } = new string[8];
        public string[]? Down_Right { get; set; } = new string[8];
        public string[]? Down_Left { get; set; } = new string[8];
        public int PatId { get; set; }
    }
}
