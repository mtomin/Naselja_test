using Naselja_test.DAL;
using Naselja_test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Naselja_test.Controllers
{
    [Authorize]
    public class NaseljaController : Controller
    {
        private INaseljeRepository naseljaRepository;
        private IDrzavaRepository drzavaRepository;
        private List<Drzava> _countryList;
        public int PageSize { get; set; }
        private List<Drzava> CountryList
        {
            get
            {
                if (_countryList == null)
                {
                    try
                    {
                        _countryList = drzavaRepository.GetDrzave();
                    }
                    catch
                    {
                        return new List<Drzava>() { new Drzava() { ID = 0, Naziv = "Trenutno nije moguće dohvatiti popis država iz baze" } };
                    }
                    if (_countryList==null)
                    {
                        return new List<Drzava>() { new Drzava() { ID = 0, Naziv = "Baza trenutno ne sadrži popis država" } };
                    }
                }

                return _countryList;
            }
        }

        public NaseljaController(INaseljeRepository _naseljaRepository, IDrzavaRepository _drzavaRepository)
        {
            naseljaRepository = _naseljaRepository;
            drzavaRepository = _drzavaRepository;
            PageSize = 10;
        }
        [HttpGet]
        public IActionResult Index(int id = 1)
        {
            var model = new List<Naselje>() { new Naselje() { Naziv = "Baza trenutno ne sadrži naselja" } };
            int brojNaselja = naseljaRepository.BrojNaselja();
            ViewData["brojNaselja"] = brojNaselja;
            ViewData["pageSize"] = PageSize;
            model = naseljaRepository.GetNaseljaPaged(id, 10);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditNaselje(Naselje naselje)
        {
            ViewData.Add("CountryList", CountryList);
            return PartialView(naselje);
        }

        [HttpPost]
        public ActionResult SaveChanges(Naselje naselje)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    naseljaRepository.UpdateNaselje(naselje);
                    return PartialView();
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return PartialView("ErrorSavingChanges");
                }
            }

            ViewData.Add("CountryList", CountryList);
            return PartialView("EditNaselje", naselje);
        }

        [HttpGet]
        public ActionResult DeleteNaselje(int naseljeID)
        {
            naseljaRepository.DeleteNaselje(naseljeID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddNaselje()
        {
            ViewData.Add("CountryList", CountryList);
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddNaselje(Naselje naselje)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    naseljaRepository.AddNaselje(naselje);
                    return PartialView("SaveChanges");
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return PartialView("ErrorSavingChanges");
                }
            }

            ViewData.Add("CountryList", CountryList);
            return PartialView("AddNaselje", naselje);
        }
    }
}
