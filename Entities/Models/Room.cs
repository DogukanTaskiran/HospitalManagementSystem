using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Room : BaseEntity
    {
        public int RoomID { get; set; }
        public DateTime AdmissionDate { get; set; }
        
        //relations below

        public int PatientID { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
