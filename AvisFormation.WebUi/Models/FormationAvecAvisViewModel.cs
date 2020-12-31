using AvisFormation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvisFormation.WebUi.Models
{
    public class FormationAvecAvisViewModel
    {
        public string FName { get; internal set; }
        public string FDescription { get; internal set; }
        public string FNomSEO { get; internal set; }
        public string FUrl { get; internal set; }
        public double FNote { get; internal set; }
        public int FNbrAvis { get; internal set; }
        public List<Avis> Avis { get; internal set; }
    }
}