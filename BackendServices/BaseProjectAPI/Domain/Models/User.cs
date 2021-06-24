using BaseProjectAPI.Domain.Models.BaseModels;

namespace BaseProjectAPI.Domain.Models
{
    public class User: BaseModelLevel1
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
