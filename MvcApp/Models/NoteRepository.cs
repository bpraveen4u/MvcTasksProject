using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcApp.Models
{ 
    public class NoteRepository : INoteRepository
    {
        MvcAppContext context = new MvcAppContext();

        public IQueryable<Note> All
        {
            get { return context.Notes; }
        }

        public IQueryable<Note> AllIncluding(params Expression<Func<Note, object>>[] includeProperties)
        {
            IQueryable<Note> query = context.Notes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Note Find(int id)
        {
            return context.Notes.Find(id);
        }

        public void InsertOrUpdate(Note note)
        {
            if (note.NoteId == default(int)) {
                // New entity
                note.CreatedOn = note.ModifiedOn = DateTime.UtcNow;
                context.Notes.Add(note);
            } else {
                // Existing entity
                note.ModifiedOn = DateTime.UtcNow;
                context.Entry(note).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var note = context.Notes.Find(id);
            context.Notes.Remove(note);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface INoteRepository : IDisposable
    {
        IQueryable<Note> All { get; }
        IQueryable<Note> AllIncluding(params Expression<Func<Note, object>>[] includeProperties);
        Note Find(int id);
        void InsertOrUpdate(Note note);
        void Delete(int id);
        void Save();
    }
}