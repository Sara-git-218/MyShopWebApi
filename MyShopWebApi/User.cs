namespace MyShopWebApi
{
    public class User
    {

        public int UserId { get; set; }
        public String user_name { get; set; }
        public String password { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }

        public User(String user_name, String password, String first_name, String last_name)
        {
            this.user_name = user_name;
            this.password = password;
            this.first_name = first_name;
            this.last_name = last_name;
        }

    }
}
