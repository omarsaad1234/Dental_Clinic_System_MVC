using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Models
{
    public class Invoice
    {
        public int Id {  get; set; }
        public int TotalSessions {  get; set; }
        public int DoneSessions { get; set; }
        public int RemainSessions { get; set; }
        public decimal TotalCost {  get; set; }
        public decimal PaidAmount {  get; set; }
        public decimal RemainAmount {  get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("Patient")]
        public int PatId { get; set; }

    }
}
