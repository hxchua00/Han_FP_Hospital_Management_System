namespace Han_FP_Hospital_Management_System
{
    //Interface for UserManager Class
    interface IUserManager
    {
        User CurrentUser { get; }
        void Initialize();
        User GetUser(int userID);
        bool LogOn(int userId, string password);
    }
}
