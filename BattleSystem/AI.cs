using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;
using BattleSystem.Units;

namespace BattleSystem
{
    public sealed class AI
    {
        private Player m_ownPlayer;
        public AI(Player player)
        {
            m_ownPlayer = player;
        }
        public void Attack( Unit target )
        {
            if (target == null)
            {
                GameLogic.endMove(0.0f);
                return;
            }
            if (GameLogic.CurrentUnit.tryAttack(target))
                GameLogic.doMove(GameLogic.CurrentUnit.doAttack(target));
            else
            {
                var forAttack = SmartMove.SmartMove.findWay(new SmartMove.Cell(GameLogic.CurrentUnit.StandSprite.Position), target.StandSprite.Position, false);
                if (forAttack.Count == 0)
                {
                    GameLogic.endMove(0.0f);
                    return;
                }
                if (forAttack.Count - 1 > GameLogic.Speed)
                    GameLogic.doMove(GameLogic.CurrentUnit.doWalk(forAttack[GameLogic.Speed]));
                else
                    GameLogic.doMove(GameLogic.CurrentUnit.doAttack(target));
            }
        }
        public void myMove()
        {
            var target = chooseTarget();
            if (target == null)
            {
                GameLogic.endMove(0.0f);
                return;
            }
            if (!m_ownPlayer.m_isUsedSpellBook)
            {
                m_ownPlayer.useSpell("Fireball", target.StandSprite.Position);
                GameLogic.Layer.ScheduleOnce(ft => Attack( chooseTarget () ), 1.0f);
            }
            else
                Attack( target );
        }
        private Unit chooseTarget()
        {
            if (GameLogic.enemy().Army.Count == 0)
                return null;
            var distance = GameLogic.enemy().Army.Min(u => CCPoint.Distance(u.StandSprite.Position, GameLogic.CurrentUnit.StandSprite.Position));
            var unit = GameLogic.enemy().Army.Find(u => distance == CCPoint.Distance(u.StandSprite.Position, GameLogic.CurrentUnit.StandSprite.Position));
            return unit;
        }
    }
}
