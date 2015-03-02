using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Spells
{
    public class Healing: Spell
    {
        private float healedHP = 30;
        public override string ToString()
        {
            return "Healing";
        }
        public Healing(int intelligence)
        {
            Path = "Spells/Hill.png";
            Picture = new CCSprite(Path);
            healedHP += healedHP * (Convert.ToSingle(intelligence) / 100.0f);
            Mana = 5;
        }
        public override void doSpell(CCPoint target, Player player)
        {
            var effect = new CCParticleFlower();
            var tar = GameLogic.getUnitFromCoord(target);
            if (GameLogic.isEnemy (tar))
                return;
            if (player.Mana >= Mana)
                player.Mana -= Mana;
            else return;
            if (healedHP > tar.CurrentHealth)
                tar.CurrentHealth = tar.Health;
            else
                tar.CurrentHealth += (int)healedHP;
            effect.Position = tar.StandSprite.Position;
            GameLogic.Layer.AddChild(effect, 1500);
            GameLogic.Layer.ScheduleOnce(ft => GameLogic.Layer.RemoveChild(effect), 1.0f);
        }
    }
}
