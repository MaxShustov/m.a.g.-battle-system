using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Spells
{
    public class FireballsRain: Spell
    {
        private float Dammage = 200;
        public static int num = 0;
        private List<CCParticleMeteor> m_meteors = new List<CCParticleMeteor>();
        public FireballsRain(int intelligence)
        {
            Path = "Spells/FireRain.png";
            Picture = new CCSprite(Path);
            Dammage += Dammage * (Convert.ToSingle(intelligence) / 100.0f);
            Mana = 30;

            for (int i = 0; i < 9; i++)
            {
                m_meteors.Add(new CCParticleMeteor());
                m_meteors[i].Position = new CCPoint(0.0f, 0.0f);
            }
        }
        public override void doSpell(CCPoint target, Player player)
        {
            List<CCPoint> targets = new List<CCPoint>();
            #region Targets for fireballs
            targets.Add(new CCPoint(target.X + 128, target.Y));
            targets.Add(new CCPoint(target.X - 128, target.Y));
            targets.Add(new CCPoint(target.X, target.Y + 128));
            targets.Add(new CCPoint(target.X, target.Y - 128));
            targets.Add(target);
            targets.Add(new CCPoint(target.X + 128, target.Y + 128));
            targets.Add(new CCPoint(target.X - 128, target.Y - 128));
            targets.Add(new CCPoint(target.X - 128, target.Y + 128));
            targets.Add(new CCPoint(target.X + 128, target.Y - 128));
            #endregion
            if (player.Mana >= Mana)
                player.Mana -= Mana;
            else return;
            for (int i = 0; i < m_meteors.Count; i++)
            {
                GameLogic.Layer.ScheduleOnce ( ft => runFireBall(targets), i/4.0f );
                //GameLogic.Layer.AddChild(m_meteors[i], 1500);
                //GameLogic.Layer.ScheduleOnce(ft => m_meteors[i].RunAction(new CCMoveTo(1.0f, targets[i])), i / 4.0f);
                //if (GameLogic.getUnitFromCoord(targets[i]) != null)
                //    GameLogic.Layer.ScheduleOnce(ft => GameLogic.getUnitFromCoord(targets[i]).Hit(Dammage), 1.0f + (i / 4.0f));
                //GameLogic.Layer.ScheduleOnce(ft => GameLogic.Layer.RemoveChild(m_meteors[i]), 1.0f+(i/4.0f));
            }
        }
        private void runFireBall(List <CCPoint> list)
        {
            GameLogic.Layer.AddChild(m_meteors[num], 1500);
            GameLogic.Layer.ScheduleOnce(ft => m_meteors[num].RunAction(new CCMoveTo(1.0f, list[num])), num / 4.0f);
            if (GameLogic.getUnitFromCoord(list[num]) != null)
                GameLogic.Layer.ScheduleOnce(ft => GameLogic.getUnitFromCoord(list[num]).Hit(Dammage), 1.0f + (num / 4.0f));
            GameLogic.Layer.ScheduleOnce(ft => GameLogic.Layer.RemoveChild(m_meteors[num]), 1.0f + (num / 4.0f));
            num++;
        }
        public override string ToString()
        {
            return "FireballsRain";
        }
    }
}