using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaverecny_projekt
{
    internal class Priest : Mage
    {
        public Priest(int maxHealth, int curentHealth, int baseDamadge, int level, int maxXp, int curentXp, int baseDefence, Equipment helm, Equipment armor, Equipment weapon, int maxMana, int curentMana) : base(maxHealth, curentHealth, baseDamadge, level, maxXp, curentXp, baseDefence, helm, armor, weapon, maxMana, curentMana)
        {
            this.typ = TypClasy.Priest;
        }
    }
}