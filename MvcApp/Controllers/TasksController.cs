using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{   
    public class TasksController : Controller
    {
		private readonly IStatusRepository statusRepository;
		private readonly ITaskRepository taskRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TasksController() : this(new StatusRepository(), new TaskRepository())
        {
        }

        public TasksController(IStatusRepository statusRepository, ITaskRepository taskRepository)
        {
			this.statusRepository = statusRepository;
			this.taskRepository = taskRepository;
        }

        //
        // GET: /Tasks/

        public ViewResult Index()
        {
            return View(taskRepository.AllIncluding(task => task.Status, task => task.Notes));
        }

        //
        // GET: /Tasks/Details/5

        public ViewResult Details(int id)
        {
            return View(taskRepository.Find(id));
        }

        //
        // GET: /Tasks/Create

        public ActionResult Create()
        {
			ViewBag.PossibleStatus = statusRepository.All;
            return View();
        } 

        //
        // POST: /Tasks/Create

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid) {
                taskRepository.InsertOrUpdate(task);
                taskRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleStatus = statusRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Tasks/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleStatus = statusRepository.All;
             return View(taskRepository.Find(id));
        }

        //
        // POST: /Tasks/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid) {
                taskRepository.InsertOrUpdate(task);
                taskRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleStatus = statusRepository.All;
				return View();
			}
        }

        //
        // GET: /Tasks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(taskRepository.Find(id));
        }

        //
        // POST: /Tasks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            taskRepository.Delete(id);
            taskRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                statusRepository.Dispose();
                taskRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

