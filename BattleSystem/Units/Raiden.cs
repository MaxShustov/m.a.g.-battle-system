using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Units
{
    public class Raiden: Unit
    {
        public override bool tryAttack(Unit target)
        {
            if (target == GameLogic.getUnitFromCoord ( new CCPoint ( StandSprite.Position.X + 384, StandSprite.Position.Y )))
                return true;
            else
                return base.tryAttack(target);
        }
        public Raiden ( Orientation or, int number )
        {
            m_attack = 15;
            m_defence = 10;
            m_critical = 60;
            m_initiative = 3;
            m_speed = 5;
            m_dammage = 5;
            m_health = 50;
            m_currentHealth = m_health;
            Animation = new AnimationUnit();
            UnitOrientation = or;
            m_number = number;
            m_label = new CCLabel(m_number.ToString(), "Times New Roman", 20.0f);
        }
        public override string ToString()
        {
            return "Raiden";
        }
        public override float doAttack(Unit target)
        {
            if (target == GameLogic.getUnitFromCoord(new CCPoint(StandSprite.Position.X + 384, StandSprite.Position.Y)))
                return doSpecialAttack(target);
            else
                return base.doAttack(target);
        }
        public override float doSpecialAttack(Unit target)
        {
            CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
            checkFinishPosition(target.StandSprite.Position);
            StandSprite.RunAction(Animation.SpecialMoves);
            float dammage = ((Convert.ToSingle (m_attack)/100.0f)*Convert.ToSingle(m_dammage))*m_number*5.0f;
            GameLogic.Layer.ScheduleOnce(ft => {
                StandSprite.RunAction(new CCMoveTo(0.5f, new CCPoint ( target.StandSprite.Position.X - GameLogic.map.TileSize.Width, target.StandSprite.Position.Y))); 
                GameLogic.isMouseValid = true; 
            }, 0.3f);
            GameLogic.Speed = 0;
            GameLogic.Layer.ScheduleOnce(ft => target.Hit(dammage), 0.7f);
            GameLogic.isMouseValid = false;
            return 0.5f + Animation.SpecialMoves.Duration;
        }
        public override void OrientationChange()
        {
            var pos = new CCPoint ();
            switch (UnitOrientation)
            { 
                case Orientation.Back:
                    CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
                    if ( m_standSprite != null )
                        pos = m_standSprite.Position;
                    GameLogic.Layer.RemoveChild(m_standSprite);
                    m_standSprite = new CCSprite("Units/Raiden/Raiden Stand Back.png");
                    m_standSprite.Position = pos;
                    GameLogic.Layer.AddChild(m_standSprite, 1000);
                    LoadBackResourses();
                    break;
                case Orientation.Front:
                    CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
                    if ( m_standSprite != null )
                        pos = m_standSprite.Position;
                    GameLogic.Layer.RemoveChild(m_standSprite);
                    m_standSprite = new CCSprite("Units/Raiden/Raiden Stand.png");
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
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 1.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 2.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 3.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 4.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 5.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 6.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 7.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 8.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 9.png");
            attack.DelayPerUnit = 0.1f;
            attack.RestoreOriginalFrame = true;
            Animation.Attack = new CCAnimate(attack);
            #endregion
            #region Load Walk
            CCAnimation walk = new CCAnimation();
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 1.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 2.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 3.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 4.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 5.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 6.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 7.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 8.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 9.png");
            walk.DelayPerUnit = 0.1f;
            walk.RestoreOriginalFrame = true;
            Animation.Walk = new CCAnimate(walk);
            #endregion
            #region Load Stand
            CCAnimation stand = new CCAnimation();
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 1.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 2.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 3.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 4.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 5.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 6.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 7.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 8.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 9.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 10.png");
            stand.DelayPerUnit = 0.1f;
            stand.RestoreOriginalFrame = true;
            Animation.Stand = new CCAnimate(stand);
            #endregion
            #region Load Hit
            CCAnimation hit = new CCAnimation();
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 1.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 2.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 3.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 4.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 5.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 6.png");
            hit.DelayPerUnit = 0.1f;
            hit.RestoreOriginalFrame = true;
            Animation.Hit = new CCAnimate(hit);
            #endregion
            #region Load Special Attack
            CCAnimation specialAttack = new CCAnimation();
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 1.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 2.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 3.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4.png");
            specialAttack.DelayPerUnit = 0.1f;
            specialAttack.RestoreOriginalFrame = true;
            Animation.SpecialMoves = new CCAnimate(specialAttack);
            #endregion
        }
        public override void LoadBackResourses()
        {
            #region Load Attack
            CCAnimation attack = new CCAnimation();
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 1 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 2 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 3 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 4 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 5 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 6 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 7 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 8 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Raiden/Attack/Attack 9 Back.png");
            attack.DelayPerUnit = 0.1f;
            attack.RestoreOriginalFrame = true;
            Animation.Attack = new CCAnimate(attack);
            #endregion
            #region Load Walk
            CCAnimation walk = new CCAnimation();
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 1 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 2 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 3 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 4 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 5 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 6 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 7 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 8 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Raiden/Walk/Raiden Walk forward 9 Back.png");
            walk.DelayPerUnit = 0.1f;
            walk.RestoreOriginalFrame = true;
            Animation.Walk = new CCAnimate(walk);
            #endregion
            #region Load Stand
            CCAnimation stand = new CCAnimation();
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 1 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 2 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 3 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 4 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 5 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 6 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 7 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 8 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 9 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Raiden/Stand/Raiden Stand 10 Back.png");
            stand.DelayPerUnit = 0.1f;
            stand.RestoreOriginalFrame = true;
            Animation.Stand = new CCAnimate(stand);
            #endregion
            #region Load Hit
            CCAnimation hit = new CCAnimation();
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 1 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 2 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 3 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 4 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 5 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Raiden/Hit/Raiden Hit 6 Back.png");
            hit.DelayPerUnit = 0.1f;
            hit.RestoreOriginalFrame = true;
            Animation.Hit = new CCAnimate(hit);
            #endregion
            #region Load Special Attack
            CCAnimation specialAttack = new CCAnimation();
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 1 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 2 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 3 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4 Back.png");
            specialAttack.AddSpriteFrameWithFileName("Units/Raiden/Special Attack/Raiden Fly 4 Back.png");
            specialAttack.DelayPerUnit = 0.1f;
            specialAttack.RestoreOriginalFrame = true;
            Animation.SpecialMoves = new CCAnimate(specialAttack);
            #endregion
        }
    }
}
