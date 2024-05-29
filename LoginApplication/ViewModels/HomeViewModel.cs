using LoginApplication.Entities;

namespace LoginApplication.ViewModels
{
    public class HomeViewModel
    {
        public List<Courses> Courses { get; set; }
        public List<Employees> Employees { get; set; }

        public HomeViewModel()
        {
            Employees = new List<Employees>();
            Courses = new List<Courses>();
        }
    }
}
