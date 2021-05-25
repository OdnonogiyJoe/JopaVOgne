using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDisappearanceTrick.Entities
{
    public class Product //Изделия, которые мы вообще производим (их список)
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Price { get; set; }
        public int Weight { get; set; }
        public string Name1 { get; set; }

        public Workshop Workshops { get; set; }
        public Material Materials { get; set; }
    }
}
