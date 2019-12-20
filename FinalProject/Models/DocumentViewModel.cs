using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class DocumentViewModel
    {

        public int docId { get; set; }
        public Nullable<int> courseId { get; set; }
        public string teacherId { get; set; }
        public string docUrl { get; set; }
        public string description { get; set; }

        public DocumentViewModel() { }

        public DocumentViewModel(int docId, Nullable<int> courseId,
            string teacherId, string docUrl, string description)
        {
            this.docId = docId;
            this.courseId = courseId;
            this.teacherId = teacherId;
            this.docUrl = docUrl;
            this.description = description;
        }

        public void UpdateDocument(Document doc)
        {
            doc.courseId = this.courseId;
            doc.teacherId = this.teacherId;
            doc.docUrl = this.docUrl;
            doc.description = this.description;
            doc.docId = this.docId;
        }
    }
}