using System;
using System.Collections.Generic;
using System.Text;
using To_Do_CLI.Enum;

namespace To_Do_CLI.Model
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public TodoTask()
        {
            CreatedAt = DateTime.Now;
            IsCompleted = false;
        }
        public TodoTask(string title, string description, TaskPriority priority): this()
        {
            Title = title;
            Description = description;
            Priority = priority;
        }
        public void ToggleComplete()
        {
            IsCompleted = !IsCompleted;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "DONE" : "IN PROCESS";
            return $"{status} #{Id} {Title} ({Priority.ToString()})";
        }
    }
}
