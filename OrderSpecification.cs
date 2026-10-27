using System;
using System.Collections.Generic;
using System.Text;

namespace vis3
{
    public class OrderSpecification
    {
        int Width { get; set; }
        int Height { get; set; }
        int Depth { get; set; }
        string Material { get; set; }
        string DesignNotes { get; set; }

        public OrderSpecification(
            int width,
            int height,
            int depth,
            string material,
            string note
            )
        {
            Width = width;
            Height = height;
            Depth = depth;
            Material = material;
            DesignNotes = note;
        }

        public override string ToString()
        {
            return $"W:{Width} H:{Height} D:{Depth} M:{Material} Notes: {DesignNotes}";
        }
    }
}
