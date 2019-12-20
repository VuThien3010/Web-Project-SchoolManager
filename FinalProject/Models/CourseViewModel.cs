using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class CourseViewModel
    {
        public string SubjectName { get; set; }
        public string TeacherId { get; set; }
        public int CourseId { get; set; }

        public CourseViewModel()
        {
           
        }

        public CourseViewModel(string SubjectName, string TeacherId, int CourseId)
        {
            this.SubjectName = SubjectName;
            this.TeacherId = TeacherId;
            this.CourseId = CourseId;
        }
    }
}