using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class Order
    {
        private static int _idCounter = 0;

        public int OrderId { get; private set; }
        int Status { get; set; }
        OrderSpecification Specification { get; set; }

        public Order(
            int customerId,
            int width,
            int height,
            int depth,
            string material,
            string notes
            )
        {
            _idCounter++;
            OrderId = _idCounter;
            Status = 0;
            Specification = new OrderSpecification(
                width, height, depth, material, notes
                );
        }

        public Order(
            int customerId,
            OrderSpecification specification
            )
        {
            _idCounter++;
            OrderId = _idCounter;
            Status = 0;
            Specification = specification;
        }

        public void Confirm()
        {
            Status = 1;
        }

        public void PrintSummary()
        {
            Console.WriteLine("summary");
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            return $"ID:{OrderId} Status:{Status} Specification: {Specification}";
        }
    }
}
