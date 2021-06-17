namespace BaseProjectAPI.Domain.ViewModels
{
    public class ItemViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string StockStatus { get; set; }
    }
}
