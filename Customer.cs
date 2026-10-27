using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class Customer
    {
        private static int _idCounter = 0;

        public int CustomerId { get; private set; }
        string Name { get; set; }
        string Email { get; set; }
        List<Order> Orders { get; set; } = new List<Order>();

        public Customer(string name, string email)
        {
            _idCounter++;
            CustomerId = _idCounter;
            Name = name;
            Email = email;
        }

        public Order CreateOrder(OrderSpecification specification)
        {
            Order o = new Order(CustomerId, specification);
            Orders.Add(o);
            return o;
        }

        public void ListOrders()
        {
            foreach (Order o in Orders)
            {
                Console.WriteLine(o);
            }
        }
    }
}
