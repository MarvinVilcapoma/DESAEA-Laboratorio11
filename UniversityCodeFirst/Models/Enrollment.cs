﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityCodeFirst.Models
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText ="No Grade")]
        public Grade? Grade { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
       
    }
}