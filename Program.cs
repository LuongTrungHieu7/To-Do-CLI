using To_Do_CLI.Enum;
using To_Do_CLI.Helpers;
using To_Do_CLI.Model;
using To_Do_CLI.Services;

namespace To_Do_CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ToDo List";
            ConsoleHelper.PrintHeader("Welcome to the Task Manager");

            TaskManager taskManager = TaskManager.Instance;
            taskManager.TaskAdded += (task) =>
                ConsoleHelper.WriteLineColored($"Task added: {task.Title}", ConsoleColor.Green);

            taskManager.TaskCompleted += (task) =>
                ConsoleHelper.WriteLineColored($"Task updated: {task.Title}", ConsoleColor.Cyan);


            taskManager.TaskRemoved += (task) =>
                ConsoleHelper.WriteLineColored($"Task deleted: {task.Title}", ConsoleColor.Red);

            bool exit = false;
            while (!exit)
            {
                ConsoleHelper.PrintHeader("MAIN MENU");
                Console.WriteLine("1. Log in");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Work with tasks (require Log in)");
                Console.WriteLine("4. Exit");

                int choice = ConsoleHelper.ReadInt("\nChoose action: ", 1, 4);

                switch (choice)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Register();
                        break;
                    case 3:
                        if (AuthManager.Instance.IsLoggedIn)
                        {
                            TaskMenu();
                        }
                        else
                        {
                            ConsoleHelper.WriteLineColored("Log in first!", ConsoleColor.Yellow);
                            ConsoleHelper.WaitForKey();
                        }
                        break;
                    case 4:
                        ConsoleHelper.WriteLineColored("Good bye!", ConsoleColor.Green);
                        exit = true;
                        break;
                }
                
            }

        }
        static void Login()
        {
            ConsoleHelper.PrintHeader("LOG IN");
            string username = ConsoleHelper.ReadString("Enter the login:");
            string password = ConsoleHelper.ReadString("Enter the password:");
            AuthManager.Instance.Login(username, password);
            ConsoleHelper.WaitForKey();
        }

        static void Register()
        {
            ConsoleHelper.PrintHeader("Registration");
            string username = ConsoleHelper.ReadString("Enter the login:");
            string password = ConsoleHelper.ReadString("Enter the password:");
            AuthManager.Instance.Register(username, password);
            ConsoleHelper.WaitForKey();
        }

        static void TaskMenu()
        {
            TaskManager taskManager = TaskManager.Instance;
            bool back = false;

            while (!back)
            {
                ConsoleHelper.PrintHeader($"Tasks | User {AuthManager.Instance.CurrentUsername}");
                Console.WriteLine("1. Add task");
                Console.WriteLine("2. Show all tasks");
                Console.WriteLine("3. Find task");
                Console.WriteLine("4. Mark the task as completed");
                Console.WriteLine("5. Delete task");
                Console.WriteLine("6. Back to Main menu");
                int choice = ConsoleHelper.ReadInt("\nChoose action: ", 1, 6);

                switch (choice)
                {
                    case 1:
                        AddTask();
                        break;
                    case 2:
                        taskManager.PrintAllTask();
                        ConsoleHelper.WaitForKey();
                        break;
                    case 3:
                        SearchTasks();
                        break;
                    case 4:
                        CompleteTask();
                        break;
                    case 5:
                        RemoveTask();
                        break;
                    case 6:
                        back = true;
                        break;
                }

            }

        }

        static void AddTask()
        {
            TaskManager taskManager = TaskManager.Instance;
            ConsoleHelper.PrintHeader("Adding task");
            string title = ConsoleHelper.ReadString("Title: ");
            string description = ConsoleHelper.ReadString("Description: ");
            TaskPriority priority = ConsoleHelper.ReadPriority();

            TodoTask newTask = new TodoTask(title, description, priority);
            taskManager.AddTask(newTask);

            ConsoleHelper.WaitForKey();
        }

        static void SearchTasks()
        {
            TaskManager taskManager = TaskManager.Instance;
            ConsoleHelper.PrintHeader("FIND TASKS");

            string keyword = ConsoleHelper.ReadString("Enter the keyword (or press Enter to cancel): ", true);
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("The search has been cancelled.");
                ConsoleHelper.WaitForKey();
                return;
            }

            Predicate<TodoTask> searchPredicate = (task) =>
            task.Title.ToLower().Contains(keyword.ToLower()) || task.Description.ToLower().Contains(keyword.ToLower());

            List<TodoTask> result = taskManager.FindTask(searchPredicate);

            if (result.Count == 0)
            {
                Console.WriteLine("Nothing found.");
            }
            else
            {
                Console.WriteLine($"Task found: {result.Count}");
                foreach (TodoTask task in result)
                {
                    string status = task.IsCompleted ? "DONE" : "IN PROCESS";
                    Console.WriteLine($"{status} \n#{task.Id} \n{task.Priority} priority \n Title:{task.Title}");
                }
            }
            ConsoleHelper.WaitForKey();

        }

        static void CompleteTask()
        {
            TaskManager taskManager = TaskManager.Instance;
            ConsoleHelper.PrintHeader("TASK MARK");

            int id = ConsoleHelper.ReadInt("Enter the task ID:", 1);

            try
            {
                taskManager.ToggleTask(id);
            }
            catch(ArgumentException ex)
            {
                ConsoleHelper.WriteLineColored($"{ex.Message}", ConsoleColor.Red);
            }
            ConsoleHelper.WaitForKey();
        }

        static void RemoveTask()
        {
            TaskManager taskManager = TaskManager.Instance;
            ConsoleHelper.PrintHeader("TASK DELETION");

            int id = ConsoleHelper.ReadInt("Enter the task ID to delete:", 1);
            try
            {
                taskManager.RemoveTask(id);
            }
            catch (ArgumentException ex)
            {
                ConsoleHelper.WriteLineColored($"{ex.Message}", ConsoleColor.Red);
            }
            ConsoleHelper.WaitForKey();
        }


    }
}
