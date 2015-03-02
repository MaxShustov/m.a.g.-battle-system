using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleSystem.Units;
using Cocos2D;

namespace BattleSystem.Units
{
    public class Ermac: Unit
    {
        public override string ToString()
        {
             return "Ermac";
        }
        public Ermac(Orientation or, int number)
        {
            UnitType = TypeOfUnit.Ranged;
            m_health = 30;
            m_attack = 30;
            m_defence = 10;
            m_dammage = 15;
            m_currentHealth = m_health;
            m_critical = 30;
            Animation = new AnimationUnit();
            UnitOrientation = or;
            m_number = number;
            m_initiative = 4;
            m_speed = 4;
            m_label = new CCLabel(m_number.ToString(), "Times New Roman", 20.0f);
        }

        public override float doAttack(Unit target)
        {
            if (base.tryAttack(target))
                return base.doAttack(target);
            else
                return distanceAttack(target);
        }

        public float distanceAttack(Unit target)
        {
            base.checkFinishPosition(target.StandSprite.Position);
            Random r = new Random();
            double dammage = m_dammage;
            dammage += dammage * (m_attack / 100.0f);
            dammage *= m_number;
            var a = r.NextDouble() * 100;
            if (a < m_critical)
                dammage *= 2;
            GameLogic.Speed = 0;
            CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
            StandSprite.RunAction(Animation.SpecialMoves);
            var particle = new CCParticleFlower();
            particle.Position = StandSprite.Position;
            GameLogic.Layer.ScheduleOnce(ft => { GameLogic.Layer.AddChild(particle, 1100); particle.RunAction(new CCMoveTo(Animation.SpecialMoves.Duration/2.0f, target.StandSprite.Position)); }, 0.5f);
            GameLogic.Layer.ScheduleOnce(ft => { target.Hit(dammage); GameLogic.Layer.RemoveChild(particle, true); }, Animation.SpecialMoves.Duration);
            GameLogic.isMouseValid = false;
            return Animation.SpecialMoves.Duration + 0.05f + target.Animation.Hit.Duration/2.0f;
        }
        public override bool tryAttack(Unit target)
        {
            return true;
        }
        public override void OrientationChange()
        {
            var pos = new CCPoint();
            switch (UnitOrientation)
            {
                case Orientation.Back:
                    CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
                    if (m_standSprite != null)
                        pos = m_standSprite.Position;
                    GameLogic.Layer.RemoveChild(m_standSprite);
                    m_standSprite = new CCSprite("Units/Ermac/Ermac Stand Back.png");
                    m_standSprite.Position = pos;
                    GameLogic.Layer.AddChild(m_standSprite, 1000);
                    LoadBackResourses();
                    break;
                case Orientation.Front:
                    CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
                    if (m_standSprite != null)
                        pos = m_standSprite.Position;
                    GameLogic.Layer.RemoveChild(m_standSprite);
                    m_standSprite = new CCSprite("Units/Ermac/Ermac Stand.png");
                    m_standSprite.Position = pos;
                    GameLogic.Layer.AddChild(m_standSprite, 1000);
                    LoadFrontResourses();
                    break;
            }
        }
        public override void LoadFrontResourses()
        {
            #region Load Attack
            CCAnimation attack = new CCAnimation();
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 1.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 2.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 3.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 4.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 5.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 6.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 7.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 8.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 9.png");
            attack.DelayPerUnit = 0.1f;
            attack.RestoreOriginalFrame = true;
            Animation.Attack = new CCAnimate(attack);
            #endregion
            #region Load Walk
            CCAnimation walk = new CCAnimation();
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 1.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 2.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 3.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 4.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 5.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 6.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 7.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 8.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 9.png");
            walk.DelayPerUnit = 0.1f;
            walk.RestoreOriginalFrame = true;
            Animation.Walk = new CCAnimate(walk);
            #endregion
            #region Load Stand
            CCAnimation stand = new CCAnimation();
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 1.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 2.png"); 
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 3.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 4.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 5.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 6.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 7.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 8.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 9.png");
            stand.DelayPerUnit = 0.1f;
            stand.RestoreOriginalFrame = true;
            Animation.Stand = new CCAnimate(stand);
            #endregion
            #region Load Hit
            CCAnimation hit = new CCAnimation();
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 1.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 2.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 3.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 4.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 5.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 6.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 7.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 8.png");
            hit.DelayPerUnit = 0.1f;
            hit.RestoreOriginalFrame = true;
            Animation.Hit = new CCAnimate(hit);
            #endregion
            #region Load Special Attack
            CCAnimation specialAttack = new CCAnimation();
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 1.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 2.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 3.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 4.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 5.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 6.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 7.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 8.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 9.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 10.png");
            specialAttack.DelayPerUnit = 0.1f;
            specialAttack.RestoreOriginalFrame = true;
            Animation.SpecialMoves = new CCAnimate(specialAttack);
            #endregion
        }
        public override void LoadBackResourses()
        {
            #region Load Attack
            CCAnimation attack = new CCAnimation();
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 1 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 2 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 3 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 4 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 5 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 6 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 7 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 8 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Ermac/Attack/Ermac Attack 9 Back.png");
            attack.DelayPerUnit = 0.1f;
            attack.RestoreOriginalFrame = true;
            Animation.Attack = new CCAnimate(attack);
            #endregion
            #region Load Walk
            CCAnimation walk = new CCAnimation();
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 1 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 2 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 3 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 4 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 5 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 6 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 7 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 8 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Ermac/Walk/Ermac Walk 9 Back.png");
            walk.DelayPerUnit = 0.1f;
            walk.RestoreOriginalFrame = true;
            Animation.Walk = new CCAnimate(walk);
            #endregion
            #region Load Stand
            CCAnimation stand = new CCAnimation();
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 1 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 2 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 3 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 4 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 5 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 6 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 7 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 8 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Ermac/Stand/Ermac Stand 9 Back.png");
            stand.DelayPerUnit = 0.1f;
            stand.RestoreOriginalFrame = true;
            Animation.Stand = new CCAnimate(stand);
            #endregion
            #region Load Hit
            CCAnimation hit = new CCAnimation();
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 1 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 2 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 3 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 4 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 5 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 6 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 7 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Ermac/Hit/Ermac Hit 8 Back.png");
            hit.DelayPerUnit = 0.1f;
            hit.RestoreOriginalFrame = true;
            Animation.Hit = new CCAnimate(hit);
            #endregion
            #region Load Special Attack
            CCAnimation specialAttack = new CCAnimation();
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 1 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 2 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 3 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 4 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 5 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 6 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 7 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 8 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 9 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Ermac/Distance Attack/Ermac Distance Attack 10 Back.png");
            specialAttack.DelayPerUnit = 0.1f;
            specialAttack.RestoreOriginalFrame = true;
            Animation.SpecialMoves = new CCAnimate(specialAttack);
            #endregion
        }
    }
}
