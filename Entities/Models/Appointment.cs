﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DocID { get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentTime { get; set; }
        public bool Status { get; set; }
        public Doctor Doctor { get; set; }          // reference to doctor 
        public Patient Patient { get; set; }        // reference to patient

        // constructor with parameters
        public Appointment(int docID, int patientID, DateTime appointmentDate, int appointmentTime, bool status)
        {
            DocID = docID;
            PatientID = patientID;
            AppointmentDate = DateTime.Now;
            AppointmentTime = appointmentTime;
            Status = status;
        }
        // default constructor
        public Appointment()
        {

        }
    }
}
