using System;
using System.Collections.Generic;
using ProjectOther.Common.Models;

namespace ProjectOther.Common.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int IdDeliverer { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public float Total { get; set; }
        public DateTime DateTimeOfDelivery { get; set; }
        public Enums.OrderStatus OrderStatus { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
