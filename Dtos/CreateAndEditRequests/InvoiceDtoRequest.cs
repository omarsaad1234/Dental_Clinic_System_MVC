using Dental_Clinic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dental_Clinic.Dtos.CreateAndEditRequests
{
    public class InvoiceDtoRequest
    {
        public int TotalSessions { get; set; }
        public int DoneSessions { get; set; }
        public decimal TotalCost { get; set; }
        public decimal PaidAmount { get; set; }
        public int PatId { get; set; }
    }
}
