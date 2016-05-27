//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuizRonde
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuizRonde()
        {
            this.VraagInQuizs = new HashSet<VraagInQuiz>();
        }
    
        public int QuizRondeID { get; set; }
        public int EvenementID { get; set; }
        public int Rondenummer { get; set; }
        [StringLength(50)]
        public string Thema_Naam { get; set; }
    
        public virtual Evenement Evenement { get; set; }
        public virtual Thema Thema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VraagInQuiz> VraagInQuizs { get; set; }
    }
}
