using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleSystem.Units;
using BattleSystem.Spells;
using BattleSystem.Skills;
using Cocos2D;

namespace BattleSystem
{
    public class Player
    {
        public bool m_isUsedSpellBook = false;
        public CCSprite Picture { get; private set; }
        public List<Unit> Army { get; set; }
        public List<Spell> SpellsBook { get; set; }
        public int Attack { get; set; }
        public int Deffence { get; set; }
        public int Intelligence { get; set; }
        public int Mana { get; set; }
        public List<Skill> Skills { get; set; }
        public bool Move { get; set; }
        public event Action onChangeMove = delegate { };
        public Player()
        {
            Intelligence = 20;
            Attack = 15;
            Deffence = 20;
            Mana = 50;
            Army = new List<Unit>();
            SpellsBook = new List<Spell>();
            Skills = new List<Skill>();
        }
        public virtual void doMove ( Unit currentUnit ) 
        {
            if (SpellsBook.Count == 0)
                m_isUsedSpellBook = true;
            currentUnit.isAlreadyFight = true;
            Move = true;
            onChangeMove();
        }
        public Unit chooseUnit ()
        {
            Unit unit = new Unit ();
            foreach (var i in Army)
                if (i.Initiative > unit.Initiative && !i.isAlreadyFight)
                    unit = i;
            return unit;
        }
        public void newMove()
        {
            if ( Mana > 0 && SpellsBook.Count != 0 )
                m_isUsedSpellBook = false;
            foreach (var i in Army)
                i.isAlreadyFight = false;
        }
        public void LocateUnits( int i, CCTMXTiledMap map, IntroLayer layer )
        {
            for (int j = 0; j < Army.Count; j++ )
            {
                var point = new CCPoint(map.LayerNamed("Background").TileAt(new CCPoint(i, j)).Position);
                Army [ j ].StandSprite.Position = new CCPoint(point.X + 120, point.Y + 140);
                Army[j].Label.Position = new CCPoint(Army[j].StandSprite.Position.X + 32, Army[j].StandSprite.Position.Y - 50);
                Army[j].StandSprite.RunAction(new CCRepeatForever(Army[j].Animation.Stand));
                layer.AddChild(Army[j].Label, 1200 );
            }
        }
        public void useSpell(string spellName, CCPoint target)
        {
            if (m_isUsedSpellBook)
                return;
            if (Mana <= 0)
            {
                m_isUsedSpellBook = true;
                return;
            }
            var spell = SpellsBook.Find(sp => sp.ToString() == spellName);
            if (spell == null)
                return;
            spell.doSpell(target, this);
            m_isUsedSpellBook = true;
        }

    }
}
