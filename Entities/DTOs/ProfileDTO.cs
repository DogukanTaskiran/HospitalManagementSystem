using Entities.Models;

namespace Entities.DTOs
{
    public class ProfileDTO
    {
        public Patient Patient {get;set;}
        public List<Invoice> Invoices {get;set;}
    }
}