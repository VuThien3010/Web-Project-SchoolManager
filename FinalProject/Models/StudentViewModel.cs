using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class StudentViewModel
    {

        public string studentId { get; set; }
        public string studentName { get; set; }
        public Nullable<int> grade { get; set; }
        public string birthDate { get; set; }

        public StudentViewModel() { }

        public StudentViewModel(string studentId, string studentName
            , Nullable<int> grade, string birthDate)
        {
            this.studentId = studentId;
            this.studentName = studentName;
            this.grade = grade;
            this.birthDate = birthDate;
        }
    }
}