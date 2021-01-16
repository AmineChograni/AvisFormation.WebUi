using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
        {
            var listFormation = new List<Formation>();
            using (var context =new AvisEntities())
            {
                listFormation = context.Formation.ToList();
            }
            //redirection vers une eutre action
            //RedirectToAction("Index");
            return View(listFormation);
        }
        public ActionResult DetailsFormation(string nomSeo=null)
        {
            
            var vm = new FormationAvecAvisViewModel();
            using (var context = new AvisEntities())
            {
                var formationEntity = context.Formation.FirstOrDefault(f => f.NomSeo == nomSeo);
                if (formationEntity == null)
                {
                    RedirectToAction("Index","Home");
                }
                vm.FName = formationEntity.Nom;
                vm.FDescription = formationEntity.Description;
                vm.FNomSEO = formationEntity.NomSeo;
                vm.FUrl = formationEntity.Url;
                if (formationEntity.Avis.Count != 0)
                {
                    vm.FNote = Math.Round(formationEntity.Avis.Average(a => a.Note),1);
                    vm.FNbrAvis = formationEntity.Avis.Count();
                    vm.Avis = formationEntity.Avis.ToList();
                }
            }

            return View(vm);
        }


    }
}