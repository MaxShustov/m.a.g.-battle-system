using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Units
{
    public class Nightwolf: Unit
    {
        public override string ToString()
        {
            return "Nightwolf";
        }
        public Nightwolf(Orientation or, int number)
        {
            m_health = 60;
            m_attack = 15;
            m_defence = 10;
            m_dammage = 10;
            m_currentHealth = m_health;
            m_critical = 40;
            Animation = new AnimationUnit();
            UnitOrientation = or;
            m_number = number;
            m_initiative = 3;
            m_speed = 4;
            m_label = new CCLabel(m_number.ToString(), "Times New Roman", 20.0f);
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
                    m_standSprite = new CCSprite("Units/Nightwolf/Nightwolf Stand Back.png");
                    m_standSprite.Position = pos;
                    GameLogic.Layer.AddChild(m_standSprite, 1000);
                    LoadBackResourses();
                    break;
                case Orientation.Front:
                    CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
                    if (m_standSprite != null)
                        pos = m_standSprite.Position;
                    GameLogic.Layer.RemoveChild(m_standSprite);
                    m_standSprite = new CCSprite("Units/Nightwolf/Nightwolf Stand.png");
                    m_standSprite.Position = pos;
                    GameLogic.Layer.AddChild(m_standSprite, 1000);
                    LoadFrontResourses();
                    break;
            }
        }
        public override void LoadBackResourses()
        {
            #region Load Attack
            CCAnimation attack = new CCAnimation();
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 1 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 2 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 3 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 4 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 5 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 6 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 7 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 8 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 9 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 10 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 11 Back.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 12 Back.png");
            attack.DelayPerUnit = 0.1f;
            attack.RestoreOriginalFrame = true;
            Animation.Attack = new CCAnimate(attack);
            #endregion
            #region Load Walk
            CCAnimation walk = new CCAnimation();
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 1 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 2 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 3 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 4 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 5 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 6 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 7 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 8 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 9 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 10 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 11 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 12 Back.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 13 Back.png");
            walk.DelayPerUnit = 0.1f;
            walk.RestoreOriginalFrame = true;
            Animation.Walk = new CCAnimate(walk);
            #endregion
            #region Load Stand
            CCAnimation stand = new CCAnimation();
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 1 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 2 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 3 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 4 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 5 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 6 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 7 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 8 Back.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 9 Back.png");
            stand.DelayPerUnit = 0.07f;
            stand.RestoreOriginalFrame = true;
            Animation.Stand = new CCAnimate(stand);
            #endregion
            #region Load Hit
            CCAnimation hit = new CCAnimation();
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 1 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 2 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 3 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 4 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 5 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 6 Back.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 7 Back.png");
            hit.DelayPerUnit = 0.1f;
            hit.RestoreOriginalFrame = true;
            Animation.Hit = new CCAnimate(hit);
            #endregion
        }
        public override void LoadFrontResourses()
        {
            #region Load Attack
            CCAnimation attack = new CCAnimation();
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 1.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 2.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 3.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 4.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 5.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 6.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 7.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 8.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 9.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 10.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 11.png");
            attack.AddSpriteFrameWithFileName("Units/Nightwolf/Attack/Nightwolf Attack 12.png");
            attack.DelayPerUnit = 0.15f;
            attack.RestoreOriginalFrame = true;
            Animation.Attack = new CCAnimate(attack);
            #endregion
            #region Load Walk
            CCAnimation walk = new CCAnimation();
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 1.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 2.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 3.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 4.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 5.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 6.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 7.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 8.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 9.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 10.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 11.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 12.png");
            walk.AddSpriteFrameWithFileName("Units/Nightwolf/Walk/Nightwolf Walk 13.png");
            walk.DelayPerUnit = 0.1f;
            walk.RestoreOriginalFrame = true;
            Animation.Walk = new CCAnimate(walk);
            #endregion
            #region Load Stand
            CCAnimation stand = new CCAnimation();
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 1.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 2.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 3.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 4.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 5.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 6.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 7.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 8.png");
            stand.AddSpriteFrameWithFileName("Units/Nightwolf/Stand/Nightwolf Stand 9.png");
            stand.DelayPerUnit = 0.07f;
            stand.RestoreOriginalFrame = true;
            Animation.Stand = new CCAnimate(stand);
            #endregion
            #region Load Hit
            CCAnimation hit = new CCAnimation();
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 1.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 2.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 3.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 4.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 5.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 6.png");
            hit.AddSpriteFrameWithFileName("Units/Nightwolf/Hit/Nightwolf Hit 7.png");
            hit.DelayPerUnit = 0.1f;
            hit.RestoreOriginalFrame = true;
            Animation.Hit = new CCAnimate(hit);
            #endregion
        }
    }
}
