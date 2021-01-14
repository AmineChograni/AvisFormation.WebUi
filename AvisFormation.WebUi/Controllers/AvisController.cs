using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult AjouterUnAvis(string nomSeo)
        {
            var vm = new AjouterUnAvisViewModel();
            vm.NomSeo = nomSeo;
            using (var context = new AvisEntities())
            {
                var formationEntity = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);
                if (formationEntity == null)
                    return RedirectToAction("Accueil", "Home");
                vm.FormationName = formationEntity.Nom;
            }
            return View(vm) ;
        }
        [HttpPost]
        //public ActionResult PostAjouterUnAvis(string nom, string note, string description, string nomSeo)
        public ActionResult PostAjouterUnAvis(SaveCommentViewModel comment)
        {
            Avis nouvelAvis = new Avis();
            nouvelAvis.DateAvis = DateTime.Now;
            nouvelAvis.Description = comment.description;
            nouvelAvis.Nom = comment.nom;
            double dNote = 0;
            if (!double.TryParse(comment.note, NumberStyles.Any, CultureInfo.InvariantCulture, out dNote))
            {
                throw new Exception("Impossible de parser la note " + comment.note);
            }

            nouvelAvis.Note = dNote;
            using (var context = new AvisEntities())
            {
                var formationEntity = context.Formation.FirstOrDefault(f => f.NomSeo == comment.nomSeo);
                if (formationEntity == null)
                    return RedirectToAction("Accueil", "Home");

                nouvelAvis.IdFormation = formationEntity.Id;
                nouvelAvis.Formation = formationEntity;
                nouvelAvis.UserId = "";
                context.Avis.Add(nouvelAvis);
                context.SaveChanges();
            }
            TempData["AvisSuccess"] = "Avis ajouté avec succés!";
            return RedirectToAction("DetailsFormation", "Formation", new {nomSeo=comment.nomSeo}) ;
        }

    }

    
}