//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        public int docId { get; set; }
        public Nullable<int> courseId { get; set; }
        public string teacherId { get; set; }
        public string docUrl { get; set; }
        public string description { get; set; }
    
        public virtual Teacher Teacher { get; set; }
    }
}
