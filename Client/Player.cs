using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaverecny_projekt
{
    public enum TypClasy
    {
        Warrior,
        Mage,
        Priest
    }
    internal class Player
    {

        private int maxHealth { get; set; }
        private int curentHealth { get; set; }
        private int baseDamadge { get; set; }
        private int level { get; set; }
        private int baseDefence { get; set; }
        private Equipment helm { get; set; }
        private Equipment armor { get; set; }
        private Equipment weapon { get; set; }
        protected TypClasy typ { get; set; }

        public Player(int maxHealth, int curentHealth, int baseDamadge, int level, int baseDefence, Equipment helm, Equipment armor, Equipment weapon)
        {
            this.maxHealth = maxHealth;
            this.curentHealth = curentHealth;
            this.baseDamadge = baseDamadge;
            this.level = level;
            this.baseDefence = baseDefence;
            this.armor = armor;
            this.weapon = weapon;
            this.helm = helm;
            this.typ = TypClasy.Warrior;
        }
    }
}
