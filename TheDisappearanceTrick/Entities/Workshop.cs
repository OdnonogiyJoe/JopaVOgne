using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDisappearanceTrick.Entities
{
    public class Workshop //Цех
    {
        public int Id { get; set; }
        public string Name1 { get; set; }

        public List<Product> Products { get; set; }
        public List<LostSoul> LostSouls { get; set; }
    }
}
