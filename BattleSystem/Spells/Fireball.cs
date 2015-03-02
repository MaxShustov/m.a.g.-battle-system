using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleSystem.Units;
using Cocos2D;

namespace BattleSystem.Spells
{
    public class Fireball: Spell
    {
        private float Dammage = 60;
        public CCParticleMeteor m_meteor = new CCParticleMeteor();
        public Fireball ( int intelligence )
        {
            Path = "Spells/Fireball.png";
            Picture = new CCSprite(Path);
            Dammage += Dammage * (Convert.ToSingle(intelligence) / 100.0f);
            Mana = 10;
            m_meteor.Position = new CCPoint( 100, 700);
        }
        public override void doSpell(CCPoint target, Player player)
        {
            var unitTarget = GameLogic.getUnitFromCoord(target);
            if ( unitTarget == null )
                return;
            m_meteor = new CCParticleMeteor();
            this.m_meteor.Position = new CCPoint(0.0f, 770.0f);
            if (player.Mana >= Mana)
                player.Mana -= Mana;
            else return;
            this.m_meteor.AutoRemoveOnFinish = true;
            GameLogic.Layer.AddChild(this.m_meteor, 1100);
            this.m_meteor.RunAction(new CCMoveTo(1.0f, new CCPoint(unitTarget.StandSprite.Position)));
            GameLogic.Layer.ScheduleOnce(ft => { unitTarget.Hit(this.Dammage); GameLogic.isMouseValid = true; }, 0.8f);
            GameLogic.Layer.ScheduleOnce(ft => GameLogic.Layer.RemoveChild(this.m_meteor), 1.0f);
            GameLogic.isMouseValid = false;
        }
        public override string ToString()
        {
            return "Fireball";
        }
    }
}
