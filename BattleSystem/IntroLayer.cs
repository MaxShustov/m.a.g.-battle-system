using System;
using System.Collections.Generic;
using System.Collections;
using Cocos2D;
using Microsoft.Xna.Framework.Input;
using BattleSystem.Units;
using System.Collections.Specialized;
using CocosDenshion;

namespace BattleSystem
{
    public class IntroLayer : CCLayer
    {
        Player player = new Player();
        Player enemy = new Player();
        public Spells.SpellsBook m_spellsBook;

        public IntroLayer()
        {
            player.SpellsBook.Add(new Spells.Healing(player.Intelligence));
            player.SpellsBook.Add(new Spells.Fireball(player.Intelligence));
            //player.SpellsBook.Add(new Spells.FireballsRain(player.Intelligence));
            AI ai = new AI(enemy);
            AI ai_player = new AI(player);
            //player.onChangeMove += ai_player.myMove;
            enemy.onChangeMove += ai.myMove;
            GameLogic.InitGameLogic(player, enemy, this);
            player.Army.Add(new Raiden( BattleSystem.Units.Unit.Orientation.Front, 20 ));
            //enemy.Army.Add(new Raiden(BattleSystem.Units.Unit.Orientation.Back, 45));
            enemy.Army.Add(new Nightwolf(Unit.Orientation.Back, 30));
            player.Army.Add(new Nightwolf (Unit.Orientation.Front, 100 ));
            player.Army.Add(new Ermac(BattleSystem.Units.Unit.Orientation.Front, 63));
            //enemy.Army.Add(new Ermac(BattleSystem.Units.Unit.Orientation.Back, 50));
            player.LocateUnits(0, GameLogic.map, this);
            enemy.LocateUnits(9, GameLogic.map, this);
            var passMove = new CCMenuItemImage("PassMove.png", "PassMove.png", "PassMove.png", EndMoveButton);
            var spellsBook = new CCMenuItemImage("SpellsBook.png", "SpellsBook.png", "SpellsBook.png", SpellsBookCome);
            passMove.Position = new CCPoint(630.0f, -350.0f);
            spellsBook.Position = new CCPoint(passMove.PositionX - 80, passMove.PositionY);
            var menu = new CCMenu(passMove, spellsBook);
            this.AddChild(menu, 1200);
            ScheduleOnce(ft => { GameLogic.chooseUnit(); Schedule(Update); }, 2.0f);
            CCDirector.SharedDirector.TouchDispatcher.AddTargetedDelegate(this, 0, true);
            CCDirector.SharedDirector.KeyboardDispatcher.AddDelegate(this);           
        }

        public void EndMoveButton(Object obj)
        {
            if (CCDirector.SharedDirector.ActionManager.NumberOfRunningActionsInTarget(GameLogic.CurrentUnit.StandSprite) > 1)
                return;
            var pl = GameLogic.enemy() == GameLogic.Player ? GameLogic.Enemy : GameLogic.Player;
            if ( pl == enemy )
                return;
            GameLogic.endMove();
        }

        public void SpellsBookCome(Object obj)
        {
            if (player.m_isUsedSpellBook)
                return;
            if (m_spellsBook != null)
            {
                this.Parent.RemoveChild(m_spellsBook);
                m_spellsBook = null;
                this.Schedule(Update);
                player.Move = true;
                return;
            }
            if (GameLogic.enemy() == player)
                return;
            m_spellsBook = new Spells.SpellsBook ( player );
            this.Unschedule(Update);
            player.Move = false;
            this.Parent.AddChild(m_spellsBook, 2);
        }

        public override bool TouchBegan(CCTouch touch)
        {
            return GameLogic.isMouseValid & player.Move;
        }

        public override void KeyPressed(Microsoft.Xna.Framework.Input.Keys key)
        {
        }

        public override void TouchEnded(CCTouch touch)
        {
            if (GameLogic.MagicBlade != null)
            {
                player.useSpell(GameLogic.CurrentSpell, touch.Location);
                RemoveChild(GameLogic.MagicBlade);
                GameLogic.CurrentSpell = null;
                GameLogic.MagicBlade = null;
            }
            if (GameLogic.Bow != null)
            {
                var target = GameLogic.getUnitFromCoord(touch.Location);
                GameLogic.doMove(GameLogic.CurrentUnit.doAttack(target));
            }
            if (GameLogic.Sword != null)
            {
                var target = GameLogic.getUnitFromCoord(touch.Location);
                if (target == null)
                    return;
                GameLogic.doMove(GameLogic.CurrentUnit.doAttack(target));
            }
            else
            {
                if (GameLogic.getUnitFromCoord(touch.Location) != null)
                    return;
                GameLogic.doMove(GameLogic.CurrentUnit.doWalk(touch.Location));
            }
        }

        public override void Update(float dt)
        {
            var mouse = Mouse.GetState ();
            var tPoint = new CCPoint();
            tPoint.X = mouse.Position.X ;
            tPoint.Y = CCDirector.SharedDirector.WinSize.Height - mouse.Position.Y;
            var is_onDark = false;
            if ( mouse.RightButton == ButtonState.Pressed )
            {
                if (GameLogic.getUnitFromCoord(tPoint) != null)
                {
                    var unit = GameLogic.getUnitFromCoord(tPoint);
                    GameLogic.Information = new CCLabel("", "Times New Roman", 20);
                    GameLogic.Information.Color = new CCColor3B(Microsoft.Xna.Framework.Color.AliceBlue);
                    AddChild(GameLogic.Information, 1500);
                    GameLogic.Information.Text = String.Format("Attack: {0}\nDefence: {1}\nHealth: {2}", unit.Attack, unit.Defence, unit.CurrentHealth);
                    var pos = unit.StandSprite.Position;
                    pos.X += 400;
                    pos.Y += 380;
                    GameLogic.Information.Position = pos;
                }
            }
            else
            {
                RemoveChild(GameLogic.Information);
                GameLogic.Information = null;
            }
            if (GameLogic.MagicBlade != null)
            {
                foreach (var sp in GameLogic.lightPath)
                    RemoveChild(sp);
                GameLogic.lightPath.Clear();
                RemoveChild(GameLogic.Sword);
                RemoveChild(GameLogic.Bow);
                GameLogic.Sword = null;
                GameLogic.Bow = null;
                GameLogic.MagicBlade.Position = tPoint;
            }
            else if (GameLogic.CurrentUnit.UnitType == Unit.TypeOfUnit.Ranged && GameLogic.getUnitFromCoord(tPoint) != null)
            {
                var bufUnit = GameLogic.getUnitFromCoord(tPoint);
                if (bufUnit != null)
                {
                    if (!GameLogic.isEnemy(bufUnit))
                    {
                        RemoveChild(GameLogic.Bow);
                        GameLogic.Bow = null;
                        return;
                    }
                    foreach (var sp in GameLogic.lightPath)
                        RemoveChild(sp);
                    GameLogic.lightPath.Clear();
                    RemoveChild(GameLogic.Sword);
                    GameLogic.Sword = null;
                    if (GameLogic.Bow == null)
                    {
                        GameLogic.Bow = new CCSprite("Bow.png");
                        this.AddChild(GameLogic.Bow, 1500);
                    }
                    GameLogic.Bow.Position = tPoint;
                }
                else
                {
                    RemoveChild(GameLogic.Bow);
                    GameLogic.Bow = null;
                }
            }
            else
            {
                foreach (var i in GameLogic.darkCell)
                {
                    if (GameLogic.tileCoordForPosition(tPoint) == GameLogic.tileCoordForPosition(i.Position))
                    {
                        var curArmy = GameLogic.enemy().Army.Find((Unit unit) => { return GameLogic.tileCoordForPosition(tPoint) == GameLogic.tileCoordForPosition(unit.StandSprite.Position); });
                        if (curArmy != null)
                        {
                            foreach (var sp in GameLogic.lightPath)
                                GameLogic.Layer.RemoveChild(sp);
                            GameLogic.lightPath.Clear();
                            RemoveChild(GameLogic.Bow);
                            GameLogic.Bow = null;
                            var path = SmartMove.SmartMove.findWay(new SmartMove.Cell(GameLogic.CurrentUnit.StandSprite.Position), i.Position, true);
                            var count = path.Count > GameLogic.Speed + 1 ? GameLogic.Speed + 1 : path.Count;
                            for (int ip = 0; ip < count - 1; ip++)
                            {
                                GameLogic.lightPath.Add(new CCSprite("Tile_For_Map_Selected.png"));
                                GameLogic.lightPath[ip].Position = new CCPoint(path[ip].X - 10, path[ip].Y - 10); ;
                                GameLogic.Layer.AddChild(GameLogic.lightPath[ip], 999);
                            }
                            RemoveChild(GameLogic.Sword);
                            GameLogic.Sword = new CCSprite("Sword.png");
                            GameLogic.Sword.Position = tPoint;
                            AddChild(GameLogic.Sword, curArmy.StandSprite.ZOrder + 1);
                            is_onDark = true;
                            break;
                        }
                        else
                        {
                            RemoveChild(GameLogic.Sword);
                            GameLogic.Sword = null;
                            RemoveChild(GameLogic.Bow);
                            GameLogic.Bow = null;
                            is_onDark = true;
                            foreach (var sp in GameLogic.lightPath)
                                GameLogic.Layer.RemoveChild(sp);
                            GameLogic.lightPath.Clear();
                            var path = SmartMove.SmartMove.findWay(new SmartMove.Cell(GameLogic.CurrentUnit.StandSprite.Position), i.Position, true);
                            var count = path.Count > GameLogic.Speed + 1 ? GameLogic.Speed + 1 : path.Count;
                            for (int ip = 0; ip < count; ip++)
                            {
                                GameLogic.lightPath.Add(new CCSprite("Tile_For_Map_Selected.png"));
                                GameLogic.lightPath[ip].Position = new CCPoint(path[ip].X - 10, path[ip].Y - 10); ;
                                GameLogic.Layer.AddChild(GameLogic.lightPath[ip], 999);
                            }
                            break;
                        }
                    }

                }
                if (!is_onDark)
                {
                    foreach (var sp in GameLogic.lightPath)
                        GameLogic.Layer.RemoveChild(sp);
                    GameLogic.lightPath.Clear();
                    RemoveChild(GameLogic.Sword);
                    RemoveChild(GameLogic.Bow);
                }
            }
        }

        public static CCScene Scene
        {
            get
            {
                // 'scene' is an autorelease object.
                var scene = new CCScene();

                // 'layer' is an autorelease object.
                var layer = new IntroLayer();

                // add layer as a child to scene
                scene.AddChild(layer);

                // return the scene
                return scene;

            }

        }

    }
}

