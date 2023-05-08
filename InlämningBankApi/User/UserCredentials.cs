namespace InlämningBankApi.User
{
    public class UserCredentials
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() // Full read/write access
            {
                UserName = "fredrik_admin",
                EmailAddress = "fredrik_admin@email.se",
                Password = "passwordAdmin",
                GivenName = "Fredrik",
                SurName = "Thomasson",
                Role = "Admin",
            },
            new UserModel() // Can only Read
            {
                UserName = "fredrik_user",
                EmailAddress = "fredrik_user@email.se",
                Password = "passwordUser",
                GivenName = "Fredrik",
                SurName = "Thomasson",
                Role = "User",
            }
        };
    }
}
