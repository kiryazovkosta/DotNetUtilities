using System.ComponentModel.DataAnnotations;

namespace CounterWebApi.Models
{
    public class Order
    {
        [Key]
        public long OrderId { get; set; }
        public int Customer { get; set; }
        public string Name { get; set; } = null!;
        public int Code { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
