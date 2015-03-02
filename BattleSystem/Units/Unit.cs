using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Units
{
    public class Unit
    {
        public enum TypeOfUnit { Infighting, Ranged };
        public TypeOfUnit UnitType = TypeOfUnit.Infighting;
        protected int m_number;
        protected int m_attack;
        protected int m_defence;
        protected int m_initiative;
        protected int m_speed;
        protected int m_critical;
        protected int m_dammage;
        protected int m_health;
        protected int m_currentHealth;
        public int Attack { get { return m_attack; } }
        public int Defence { get { return m_defence; } }
        public int CurrentHealth { get { return m_currentHealth; } set { m_currentHealth = value; } }
        public int Speed { get { return m_speed; } }
        public int Initiative { get { return m_initiative; } }
        public int Health { get { return m_health; } }
        public int Number { get { return m_number; } }
        public bool isAlreadyFight { get; set; }
        public AnimationUnit Animation { get; set; }
        protected CCSprite m_sprite;
        protected CCSprite m_standSprite;
        protected CCLabel m_label;
        public CCSprite StandSprite { get { return m_standSprite; } }
        public CCSprite Sprite { get { return m_sprite; } }
        public CCLabel Label 
        { 
            get { return m_label; }
        }
        public virtual void Hit ( double dammage ) 
        {
            CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
            float health = m_health;
            var hp = m_currentHealth - dammage;
            if (hp <= 0)
            {
                m_number--;
                dammage = -hp;
                m_currentHealth = Health;
            }
            else
                m_currentHealth = Convert.ToInt32(hp);
            health += health * (Convert.ToSingle(m_defence) / 100.0f);
            int number = (int)(dammage / health);
            m_currentHealth = Convert.ToInt32(m_currentHealth - (dammage - (number * health)));
            m_number -= number;
            Label.Text = m_number.ToString();
            StandSprite.RunAction(Animation.Hit);
            if (m_number <= 0)
            {
                GameLogic.Player.Army.Remove(this);
                GameLogic.Enemy.Army.Remove(this);
                GameLogic.Layer.RemoveChild(this.StandSprite);
                GameLogic.Layer.RemoveChild(this.Label);
            }
            else
                GameLogic.Layer.ScheduleOnce(ft => StandSprite.RunAction(new CCRepeatForever(Animation.Stand)), Animation.Hit.Duration);
        }
        public virtual float doAttack(Unit target) 
        {
            var path = SmartMove.SmartMove.findWay(new SmartMove.Cell(StandSprite.Position), target.StandSprite.Position, true);
            if (GameLogic.Speed < path.Count - 1)
                return 0.0f;
            checkFinishPosition(target.StandSprite.Position);
            List<CCFiniteTimeAction> arr = new List<CCFiniteTimeAction>();
            var count = path.Count - 1 > Speed ? Speed : path.Count - 1;
            for (int i = 1; i < count; i++)
                arr.Add(new CCMoveTo(0.5f, path[i]));
            Random r = new Random();
            double dammage = m_dammage;
            dammage += dammage * (m_attack / 100.0f);
            dammage *= m_number;
            var a = r.NextDouble() * 100;
            if (a < m_critical)
                dammage *= 2;
            var seq = new CCSequence(new CCMoveTo(0.001f, StandSprite.Position));
            if (arr.Count > 0)
            {
                seq = new CCSequence(arr.ToArray());
                doWalk(path[path.Count - 2]);
            }
            GameLogic.Speed = 0;
            GameLogic.Layer.ScheduleOnce(ft =>
                        {
                            CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
                            StandSprite.RunAction(Animation.Attack); 
                            target.Hit(dammage); 
                        }, seq.Duration);
            GameLogic.isMouseValid = false;
            return Animation.Attack.Duration + seq.Duration + 0.05f;
        }
        public virtual void OrientationChange() 
        {
            
        }
        public virtual void LoadFrontResourses() { }
        public virtual void LoadBackResourses() { }
        public virtual float doSpecialAttack(Unit target) { return 0.0f; }
        public virtual void checkFinishPosition(CCPoint pos)
        {
            Orientation orient = UnitOrientation;
            if (GameLogic.tileCoordForPosition(pos).X > GameLogic.tileCoordForPosition(StandSprite.Position).X)
                orient = Orientation.Front;
            if (GameLogic.tileCoordForPosition(pos).X < GameLogic.tileCoordForPosition(StandSprite.Position).X)
                orient = Orientation.Back;
            if (UnitOrientation != orient)
                UnitOrientation = orient;
        }
        public virtual float doWalk(CCPoint finishPosition) 
        {
            if (GameLogic.tileCoordForPosition(GameLogic.CurrentUnit.StandSprite.Position) == GameLogic.tileCoordForPosition(finishPosition))
                return 0.0f;
            var path = SmartMove.SmartMove.findWay(new SmartMove.Cell(StandSprite.Position), finishPosition, true);
            List<CCFiniteTimeAction> arr = new List<CCFiniteTimeAction>();
            var count = path.Count > GameLogic.Speed + 1 ? GameLogic.Speed + 1 : path.Count;
            for (int i = 1; i < count; i++)
                arr.Add(new CCMoveTo(0.5f, path[i]));
            if (arr.Count == 0)
                return 0.0f;
            CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(StandSprite);
            checkFinishPosition(finishPosition);
            var seq = new CCSequence(arr.ToArray());
            GameLogic.Speed -= count-1;
            GameLogic.removeCell();
            StandSprite.RunAction(seq);
            StandSprite.RunAction(new CCRepeatForever(Animation.Walk));
            var number = Convert.ToUInt16(seq.Duration / 0.01f);
            number += 200;
            GameLogic.Layer.Schedule(ft => Label.Position = new CCPoint(StandSprite.Position.X + 32, StandSprite.Position.Y - 50), 0.01f, number, 0.0f);
            GameLogic.isMouseValid = false;
            return seq.Duration + 0.05f;
        }
        public enum Orientation { Front, Back, Up, Down }
        protected Orientation m_unitOrientation;
        public Orientation UnitOrientation
        {
            get { return m_unitOrientation; }
            set { m_unitOrientation = value; OrientationChange(); }
        }
        public bool IsLive() { return m_health > 0; }
        public virtual bool tryAttack(Unit target)
        {
            if (CCPoint.Distance(target.StandSprite.Position, StandSprite.Position) <= 128)
                return true;
            else return false;
        }
    }
}
