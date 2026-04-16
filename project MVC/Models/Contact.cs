namespace project_MVC.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string massage { get; set; }
        public int user_id { get; set; }
        public User user { get; set; }
    }
}
