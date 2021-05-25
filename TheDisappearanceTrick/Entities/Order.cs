using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDisappearanceTrick.Entities
{
    public class Order //Заказы
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
    }
}
