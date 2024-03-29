﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace Entities.Models
{
    public class Hospital : BaseEntity
    {
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }


        // relations below
        public virtual List<Department> Departments { get; set; } // one hospital has many departments

        // constructor with parameters


        // default constructor
        public Hospital() { 
        }   
    }
}
