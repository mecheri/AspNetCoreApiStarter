namespace AspNetCoreApiStarter.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }

        /// <summary>
        /// Obtient ou définit le timestamp
        /// </summary>
        public TimeStamp Ts { get; set; }
    }
}