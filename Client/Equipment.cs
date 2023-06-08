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
        private string nazev { get; set; }
        private TypVyz typ { get; set; }
        private int stat { get; set; }

        public Equipment(string nazev, TypVyz typ, int stat)
        {
            this.nazev = nazev;
            this.typ = typ;
            this.stat = stat;
        }
    }
}
