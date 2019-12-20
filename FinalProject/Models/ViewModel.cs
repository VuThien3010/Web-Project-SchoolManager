using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class ViewModel
    {
        public Course course { get; set; }
        public Subject subject { get; set; }
        public Enrollment enrollment { get; set; }
        public Student student { get; set; }
    }
}