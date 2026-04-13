using System;
using System.Collections.Generic;
using System.Linq;
using SmartProductivityManager.Models;

namespace SmartProductivityManager.Data
{
    public class TaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
        }

        public List<TaskItem> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public TaskItem? GetById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Add(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = GetById(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public List<TaskItem> GetByStatus(Status status)
        {
            return _context.Tasks
                .Where(t => t.Status == status)
                .ToList();
        }

        public List<TaskItem> GetOverdue()
        {
            return _context.Tasks
                .Where(t => t.Status != Status.Completed && t.DueDate < DateTime.Now)
                .ToList();
        }

        public List<TaskItem> Search(string keyword)
        {
            return _context.Tasks
                .Where(t => t.Title.Contains(keyword) || t.Category.Contains(keyword))
                .ToList();
        }
    }
}