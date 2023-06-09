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

        public int maxHealth { get; set; }
        public int curentHealth { get; set; }
        public int baseDamadge { get; set; }
        public int level { get; set; }
        public int maxXp { get; set; }
        public int curentXp { get; set; }
        public int baseDefence { get; set; }
        public Equipment helm { get; set; }
        public Equipment armor { get; set; }
        public Equipment weapon { get; set; }
        public TypClasy typ { get; set; }

        public Player(int maxHealth, int curentHealth, int baseDamadge, int level, int maxXp, int curentXp, int baseDefence, Equipment helm, Equipment armor, Equipment weapon)
        {
            this.maxHealth = maxHealth;
            this.curentHealth = curentHealth;
            this.baseDamadge = baseDamadge;
            this.level = level;
            this.maxXp = maxXp;
            this.curentXp = curentXp;
            this.baseDefence = baseDefence;
            this.armor = armor;
            this.weapon = weapon;
            this.helm = helm;
            this.typ = TypClasy.Warrior;
        }
        public void LevelUp()
        {
            this.level += 1;
            this.maxXp += 10;
            this.curentXp = 0;
        }
    }
}
