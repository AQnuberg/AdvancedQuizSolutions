//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MeerkeuzeAntwoord
    {
        public int MeerkeuzeAntwoordID { get; set; }
        public string Meerkeuze_Antwoord { get; set; }
        public int QuizVraagID { get; set; }
        public bool Is_Juist { get; set; }
    
        public virtual QuizVraag QuizVraag { get; set; }
    }
}
