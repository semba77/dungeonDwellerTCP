using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesengerClient
{
    internal class Item
    {
        public Item(string name, string popis, int id)
        {
            this.name = name;
            this.popis = popis;
            this.id = id;
        }

        public string name { get; set; }
        public string popis { get; set; }
        public int id { get; set; }

        public void Efekt()
        {

        }
    }
}
