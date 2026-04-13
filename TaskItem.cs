using System;

namespace SmartProductivityManager.Models
{
    public enum Priority { Low, Medium, High }
    public enum Status { Pending, InProgress, Completed }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime DueDate { get; set; }
        public string Category { get; set; } = "General";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsOverdue => Status != Status.Completed && DueDate < DateTime.Now;

        public override string ToString()
        {
            return $"[{Id}] {Title} | {Category} | {Priority} | {Status} | Due: {DueDate:dd-MM-yyyy}";
        }
    }
}