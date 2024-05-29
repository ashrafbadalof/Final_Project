using System.ComponentModel.DataAnnotations;

namespace LoginApplication.Entities
{
    public class Employees
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}