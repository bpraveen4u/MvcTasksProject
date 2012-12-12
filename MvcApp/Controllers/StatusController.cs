using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{   
    public class StatusController : Controller
    {
		private readonly IStatusRepository statusRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatusController() : this(new StatusRepository())
        {
        }

        public StatusController(IStatusRepository statusRepository)
        {
			this.statusRepository = statusRepository;
        }

        //
        // GET: /Status/

        public ViewResult Index()
        {
            return View(statusRepository.All);
        }

        //
        // GET: /Status/Details/5

        public ViewResult Details(int id)
        {
            return View(statusRepository.Find(id));
        }

        //
        // GET: /Status/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Status/Create

        [HttpPost]
        public ActionResult Create(Status status)
        {
            if (ModelState.IsValid) {
                statusRepository.InsertOrUpdate(status);
                statusRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Status/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(statusRepository.Find(id));
        }

        //
        // POST: /Status/Edit/5

        [HttpPost]
        public ActionResult Edit(Status status)
        {
            if (ModelState.IsValid) {
                statusRepository.InsertOrUpdate(status);
                statusRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Status/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(statusRepository.Find(id));
        }

        //
        // POST: /Status/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            statusRepository.Delete(id);
            statusRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                statusRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

