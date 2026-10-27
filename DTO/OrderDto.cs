using System;
using System.Collections.Generic;
using System.Text;

namespace vis3.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public string Material { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; } 
    }
}
