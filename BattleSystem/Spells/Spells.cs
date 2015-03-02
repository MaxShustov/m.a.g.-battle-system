using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleSystem.Units;
using Cocos2D;

namespace BattleSystem.Spells
{
    public class Spell
    {
        public string Path { get; set; }
        public CCSprite Picture { get; set; }
        public int Mana { get; set; }
        public virtual void doSpell(CCPoint target, Player player) {}
        public override string ToString()
        {
            return "Base";
        }
    }
}
