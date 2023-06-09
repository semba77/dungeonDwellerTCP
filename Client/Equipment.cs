using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaverecny_projekt
{
    public enum TypVyz
    {
        Helm,
        Armor,
        Weapon
    }
    internal class Equipment
    {
        public string nazev { get; set; }
        public TypVyz typ { get; set; }
        public int stat { get; set; }

        public Equipment(string nazev, TypVyz typ, int stat)
        {
            this.nazev = nazev;
            this.typ = typ;
            this.stat = stat;
        }
    }
}
