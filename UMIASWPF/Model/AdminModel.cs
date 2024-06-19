
namespace UMIASWPF.Model
{
    public class AdminModel
    {
        public AdminModel()
        {

        }
        public int? IdAdmin { get; set; }

        public string SurnameAdmin { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public string EnterPassword { get; set; } = null!;
    }
}
