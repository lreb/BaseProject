using BaseProjectAPI.Domain.Models.BaseModels;

namespace BaseProjectAPI.Domain.Models
{
    public class Item : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
