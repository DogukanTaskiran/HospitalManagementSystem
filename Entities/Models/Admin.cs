using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    
    public class Admin : ApplicationUser // base-class 
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminID {get;set;}
  
        //one to one relationship with user

        public int ApplicationUserID {get; set;}
        public ApplicationUser ApplicationUser {get; set;}

    
    }
}
