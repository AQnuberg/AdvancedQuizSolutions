using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;


    public class SpelleiderEvenementVraagModels
    {
        public int EvenementID { get; set; }  
        public string Evenement_Naam { get; set; }
        public int Rondenummer { get; set; }
        public string Thema_Naam { get; set; }
        public string Vraag { get; set; }
    }
