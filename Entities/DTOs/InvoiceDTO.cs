using Entities.Models;

namespace Entities.DTOs
{
    public class InvoiceDTO
    {

        public int PatientID { get; set; }
        public int InvoicePrice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ServiceDescription { get; set; }

        public List<Invoice>? Invoices{ get; set; }
        public IFormFile InvoiceFile { get; set; }


    }
}