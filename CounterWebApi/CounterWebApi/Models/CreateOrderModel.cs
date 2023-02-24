namespace CounterWebApi.Models
{
    public class CreateOrderModel
    {
        public string Name { get; set; } = null!;
        public int Customer { get; set; }
    }
}
