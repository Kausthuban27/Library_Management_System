namespace Library_WebApp.Services
{
    public class UserStateManagementService
    {
        public string username { get; set; } = null!;
        public bool isLoggedIn { get; set; }

        public void clear()
        {
            username = string.Empty;
            isLoggedIn = false;
        }
    }
}
