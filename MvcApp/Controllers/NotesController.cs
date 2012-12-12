using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers
{   
    public class NotesController : Controller
    {
		private readonly ITaskRepository taskRepository;
		private readonly INoteRepository noteRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public NotesController() : this(new TaskRepository(), new NoteRepository())
        {
        }

        public NotesController(ITaskRepository taskRepository, INoteRepository noteRepository)
        {
			this.taskRepository = taskRepository;
			this.noteRepository = noteRepository;
        }

        //
        // GET: /Notes/

        public ViewResult Index()
        {
            return View(noteRepository.All);
        }

        //
        // GET: /Notes/Details/5

        public ViewResult Details(int id)
        {
            return View(noteRepository.Find(id));
        }

        //
        // GET: /Notes/Create

        public ActionResult Create()
        {
			ViewBag.PossibleTasks = taskRepository.All.ToList();
            return View();
        } 

        //
        // POST: /Notes/Create

        [HttpPost]
        public ActionResult Create(Note note)
        {
            if (ModelState.IsValid) {
                noteRepository.InsertOrUpdate(note);
                noteRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTasks = taskRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Notes/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleTasks = taskRepository.All.ToList();
             return View(noteRepository.Find(id));
        }

        //
        // POST: /Notes/Edit/5

        [HttpPost]
        public ActionResult Edit(Note note)
        {
            if (ModelState.IsValid) {
                noteRepository.InsertOrUpdate(note);
                noteRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTasks = taskRepository.All;
				return View();
			}
        }

        //
        // GET: /Notes/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(noteRepository.Find(id));
        }

        //
        // POST: /Notes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            noteRepository.Delete(id);
            noteRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                taskRepository.Dispose();
                noteRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

