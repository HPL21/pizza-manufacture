using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        public long PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; } = null!;

        public long OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public double ItemAmount { get; set; }
    }
}
