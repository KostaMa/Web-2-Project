using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOther.Common.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int IdDeliverer { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public float Total { get; set; }
        public DateTime DateTimeOfDelivery { get; set; }
        public Enums.OrderStatus OrderStatus { get; set; }
    }
}
