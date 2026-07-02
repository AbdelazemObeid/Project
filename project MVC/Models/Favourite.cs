using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_MVC.Models
{
    public class Favourite
    {
        public int favourite_id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public virtual User user { get; set; }
        public virtual Product product { get; set; }
    }
}
