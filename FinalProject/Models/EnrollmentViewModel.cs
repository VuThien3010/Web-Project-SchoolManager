using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class EnrollmentViewModel
    {
        public int courseId { get; set; }
        public string studentId { get; set; }
        public Nullable<int> Grade { get; set; }

        public string StudentName{ get; set; }
        public EnrollmentViewModel()
        {

        }
        public EnrollmentViewModel(int courseId, string studentId, Nullable<int> Grade, string StudentName)
        {
            this.courseId = courseId;
            this.studentId = studentId;
                this.Grade = Grade;
            this.StudentName = StudentName;
        }
    }
}