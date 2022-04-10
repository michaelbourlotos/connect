//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mongoose.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Internship
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Internship()
        {
            this.Internship_Major = new HashSet<Internship_Major>();
            this.Student_Internship = new HashSet<Student_Internship>();
            this.Saved_Internship = new HashSet<Saved_Internship>();
        }
    
        public int InternshipId { get; set; }
        public int EmployerId { get; set; }
        [Display(Name = "Title")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        [Display(Name = "Pay Rate")]
        public Nullable<decimal> Rate { get; set; }
        public string Location { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Display(Name = "Date Posted")]
        [DataType(DataType.Date)]
        public System.DateTime PostDate { get; set; }
        public payMe Paid { get; set; }
    
        public virtual Employer Employer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Display(Name = "MAJOR")]
        public virtual ICollection<Internship_Major> Internship_Major { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Internship> Student_Internship { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Saved_Internship> Saved_Internship { get; set; }
    }
}
