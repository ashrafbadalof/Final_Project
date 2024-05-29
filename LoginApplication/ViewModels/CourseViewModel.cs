using System.ComponentModel.DataAnnotations;

namespace LoginApplication.ViewModels;

public class CourseViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public string Description { get; set; }

    public IFormFile? Image { get; set; } = null!;
}
