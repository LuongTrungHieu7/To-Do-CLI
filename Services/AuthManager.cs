using To_Do_CLI.Model;

namespace To_Do_CLI.Services
{
    public class AuthManager
    {
        private static readonly Lazy<AuthManager> _instance =
            new Lazy<AuthManager>(() => new AuthManager());

        public static AuthManager Instance => _instance.Value;

        private List<User> _users = new List<User>();
        private User? _currentUser = null;

        private AuthManager()
        {
            _users.Add(new User("admin", "123"));
            _users.Add(new User("admin", "qwerty"));
            Console.WriteLine("The authorization manager has been initialized.");

        }

        public bool IsLoggedIn => _currentUser.HasValue;
        public string CurrentUsername => _currentUser?.Username ?? "Unauthorised";

        public bool Login(string username, string password)
        {
            foreach (var user in _users)
            {
                if (user.Username == username && user.CheckPassword(password))
                {
                    _currentUser = user;
                    Console.WriteLine($"Welcome, {username}");
                    return true;
                }
            }
            Console.WriteLine("Wrong login or password!");
            return true;
        }

        public void Logout()
        {
            if (_currentUser != null)
            {
                Console.WriteLine($"See you again, {_currentUser.Value.Username}");
                _currentUser = null;
            }
            else { Console.WriteLine("You are nor authorised"); }
        }
        
        public bool Register(string username, string password)
        {
            foreach(var user in _users)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("User with this name already exist");
                    return false;
                }
            }
            _users.Add(new User(username, password));
            Console.WriteLine($"User {username} was created!");
            return true;
        }

        public User GetCurrentUser()
        {
            if (_currentUser == null)
            {
                throw new InvalidOperationException("User was not authorised");
            }
            return _currentUser.Value;
        }

    }
}
