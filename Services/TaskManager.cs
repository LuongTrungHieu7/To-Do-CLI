using Microsoft.VisualBasic;
using System.Collections;
using To_Do_CLI.Enum;
using To_Do_CLI.Model;

namespace To_Do_CLI.Services
{
    public class TaskManager : IEnumerable<TodoTask>
    {
        private static readonly Lazy<TaskManager> _instance =
            new Lazy<TaskManager>(() => new TaskManager());
        public static TaskManager Instance => _instance.Value;

        private TaskManager()
        {
            Console.WriteLine("Task manager initialized");
        }

        private Dictionary<int, TodoTask> _tasks = new Dictionary<int, TodoTask>();
        private int _nextId = 1;

        public const int MaxTasks = 100;

        public event Action<TodoTask> TaskAdded;
        public event Action<TodoTask> TaskRemoved;
        public event Action<TodoTask> TaskCompleted;

        public void AddTask(TodoTask task)
        {
            if (_tasks.Count >= MaxTasks)
            {
                throw new Exception($"You reached max number of tasks {MaxTasks}");
            }
            task.Id = _nextId++;
            _tasks[task.Id] = task;

            TaskAdded?.Invoke(task);
            Console.WriteLine($"Task #{task.Id} added!");
        }

        public bool RemoveTask(int id)
        {
            if (!_tasks.ContainsKey(id))
            {
                throw new ArgumentException($"task with ID {id} not found");
            }
            TodoTask task = _tasks[id];
            _tasks.Remove(id);
            TaskRemoved?.Invoke(task);
            Console.WriteLine($"Task #{id} deleted");
            return true;
        }

        public void ToggleTask(int id)
        {
            if (!_tasks.ContainsKey(id))
            {
                throw new ArgumentException($"Task with ID {id} not found");
            }

            TodoTask task = _tasks[id];
            task.ToggleComplete();

            TaskCompleted?.Invoke(task);
            string status = task.IsCompleted ? "DONE" : "IN PROCESS";

            Console.WriteLine($"Task #{id} now {status}");

        }

        public TodoTask GetTask(int id)
        {
            if (_tasks.TryGetValue(id, out TodoTask task))
            {
                return task;
            }
            return null;
        }

        public List<TodoTask> FindTask(Predicate<TodoTask> match)
        {
            List<TodoTask> result = new List<TodoTask>();
            foreach (TodoTask task in _tasks.Values)
            {
                if (match(task))
                {
                    result.Add(task);
                }
            }
            return result;
        }

        public void PrintAllTask()
        {
            PrintTasks(null);
        }
        public void PrintTasks(params TaskPriority[] priorities)
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine("List is empty");
                return;
            }

            Console.WriteLine("\nToDo list");
            Console.WriteLine("===================");

            List<TodoTask> taskToShow = new List<TodoTask>(_tasks.Values);

            if (priorities != null && priorities.Length > 0)
            {
                taskToShow = taskToShow.FindAll(t =>
                {
                    foreach (var p in priorities)
                    {
                        if (t.Priority == p) return true;
                    }
                    return false;
                });
            }

            if (taskToShow.Count == 0)
            {
                Console.WriteLine("Task with these priorities does not exist");
                return;
            }

            taskToShow.Sort((a, b) => b.Priority.CompareTo(a.Priority));

            foreach (var task in taskToShow)
            {
                string status = task.IsCompleted ? "DONE" : "IN PROCESS";
                Console.WriteLine($"#{task.Id} \n{status} \n{task.Priority} priority \nTitle:{task.Title}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Created at: {task.CreatedAt:dd.MM.yyyy HH:mm}");
                Console.WriteLine("----------------------------------");
            }

        }
        public IEnumerator<TodoTask> GetEnumerator()
        {
            foreach(var task in _tasks.Values)
            yield return task;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count=> _tasks.Count;

    }
}
