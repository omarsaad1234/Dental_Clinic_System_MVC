namespace Dental_Clinic.Dtos.CreateAndEditRequests
{
    public class InvoiceDtoEdit
    {
        public int Id {  get; set; }
        public int TotalSessions { get; set; }
        public int DoneSessions { get; set; }
        public decimal TotalCost { get; set; }
        public decimal PaidAmount { get; set; }
        public int PatId { get; set; }
    }
}
