using System.ComponentModel.DataAnnotations;

namespace MyAppModels.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public string Description { get; set; }
       public DateTime CreatedDate { get; set; }=DateTime.Now;
    }
}
