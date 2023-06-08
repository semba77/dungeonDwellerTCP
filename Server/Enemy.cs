using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaverecny_projekt
{
    internal class Enemy
    {
        private string species { get; set; }
        private int health { get; set; }
        private int damadge { get; set; }
        private int level { get; set; }
        private int defence { get; set; }

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
