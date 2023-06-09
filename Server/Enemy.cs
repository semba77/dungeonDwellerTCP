using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaverecny_projekt
{
    internal class Enemy
    {
        public string species { get; set; }
        public int health { get; set; }
        public int damadge { get; set; }
        public int level { get; set; }
        public int defence { get; set; }

        public Enemy(string species, int health, int damadge, int level, int defence)
        {
            this.species = species;
            this.health = health;
            this.damadge = damadge;
            this.level = level;
            this.defence = defence;
        }
    }
}
