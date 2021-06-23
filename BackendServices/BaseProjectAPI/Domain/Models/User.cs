using BaseProjectAPI.Domain.Models.BaseModels;

namespace BaseProjectAPI.Domain.Models
{
    public class User: BaseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
