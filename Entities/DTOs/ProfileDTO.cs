using Entities.Models;

namespace Entities.DTOs
{
    public class ProfileDTO
    {
        public Patient Patient {get;set;}
        public List<Invoice> Invoices {get;set;}
        public Doctor Doctor { get;set;}
        public Receptionist Receptionist { get;set;}
    }
}