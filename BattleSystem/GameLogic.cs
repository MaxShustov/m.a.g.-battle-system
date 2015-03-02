using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;
using BattleSystem.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BattleSystem
{
    public static class GameLogic
    {
        public static System.Threading.AutoResetEvent m_autoEvent = new System.Threading.AutoResetEvent(false);
        public static bool isMouseValid { get; set; }
        public static int Speed { get; set; }
        public static Player Player { get; set; }
        public static Player Enemy { get; set; }
        public static Unit CurrentUnit { get; set; }
        public static List<CCSprite> darkCell = new List<CCSprite>();
        public static List<CCSprite> lightPath = new List<CCSprite>();
        public static CCSprite unitCell = new CCSprite("Tile_For_Map_Unit.png");
        public static CCSprite Sword = new CCSprite();
        public static CCSprite MagicBlade = null;
        public static CCSprite Bow = null;
        public static CCLabel Information { get; set; }
        public static string CurrentSpell = null;
        public static CCTMXTiledMap map { get; set; }
        public static IntroLayer Layer { get; set; }
        public static void InitGameLogic(Player player, Player enemy, IntroLayer layer)
        {
            Information = null;
            isMouseValid = true;
            Sword = null;
            Layer = layer;
            var background = new CCSprite("Desert.jpg");
            Layer.AddChild(background);
            map = new CCTMXTiledMap("map.tmx");
            map.Position = CCDirector.SharedDirector.WinSize.Center;
            map.AnchorPoint = new CCPoint(0.5f, 0.5f);
            background.Position = CCDirector.SharedDirector.WinSize.Center;
            Player = player;
            Enemy = enemy;
            Layer.AddChild(map);
        }
        public static void chooseUnit()
        {
            if (Enemy.Army.Count == 0 || Player.Army.Count == 0)
            {
                new Microsoft.Xna.Framework.Game().Exit();
                return;
            }
            var unitPlayer = Player.chooseUnit();
            var unitEnemy = Enemy.chooseUnit();
            if (!unitEnemy.IsLive () && !unitPlayer.IsLive ())
            {
                Player.newMove();
                Enemy.newMove();
                unitPlayer = Player.chooseUnit();
                unitEnemy = Enemy.chooseUnit();
            }
            if (unitPlayer.Initiative <= unitEnemy.Initiative)
            {
                CurrentUnit = unitEnemy;
                Speed = CurrentUnit.Speed;
                Player.Move = false;
                showCell(CurrentUnit, Layer);
                Enemy.doMove(CurrentUnit);
            }
            else
            {
                CurrentUnit = unitPlayer;
                Speed = CurrentUnit.Speed;
                showCell(CurrentUnit, Layer);
                Player.doMove(CurrentUnit);
            }
        }
        public static void showCell(Unit unit, IntroLayer layer)
        {
            unitCell.Position = new CCPoint(CurrentUnit.StandSprite.Position.X - 10, CurrentUnit.StandSprite.Position.Y - 12);
            Layer.AddChild(unitCell, 999);
            var pos = tileCoordForPosition(unit.StandSprite.Position);
            var b = Speed > map.MapSize.Width ? map.MapSize.Width : Speed;
            pos.X--;
            for (int j = 0; j < b + 1; j++)
            {
                var a = Speed > map.MapSize.Height ? map.MapSize.Height : Speed;
                a -= j;
                for (int i = 0; i <= a; i++)
                {
                    var sprite = new CCSprite("simple_tile.png");
                    if (pos.X + j >= map.MapSize.Width || pos.Y + i >= map.MapSize.Height)
                        continue;
                    var point = new CCPoint(map.LayerNamed("Background").TileAt(new CCPoint(pos.X + j, pos.Y + i)).Position);
                    sprite.Position = new CCPoint(point.X + 107, point.Y + 128);
                    if (darkCell.Find(sp => sp.Position == sprite.Position) == null)
                    {
                        darkCell.Add(sprite);
                        layer.AddChild(sprite);
                    }
                }
                for (int i = 0; i <= a; i++)
                {
                    var sprite = new CCSprite("simple_tile.png");
                    if (pos.X + j >= map.MapSize.Width || pos.Y - i < 0)
                        continue;
                    var point = new CCPoint(map.LayerNamed("Background").TileAt(new CCPoint(pos.X + j, pos.Y - i)).Position);
                    sprite.Position = new CCPoint(point.X + 107, point.Y + 128);
                    if (darkCell.Find(sp => sp.Position == sprite.Position) == null)
                    {
                        darkCell.Add(sprite);
                        layer.AddChild(sprite);
                    }
                }
            }
            for (int j = 0; j < b + 1; j++)
            {
                var a = Speed > map.MapSize.Height ? map.MapSize.Height : Speed;
                a -= j;
                for (int i = 0; i <= a; i++)
                {
                    var sprite = new CCSprite("simple_tile.png");
                    if (pos.X - j < 0 || pos.Y + i >= map.MapSize.Height)
                        continue;
                    var point = new CCPoint(map.LayerNamed("Background").TileAt(new CCPoint(pos.X - j, pos.Y + i)).Position);
                    sprite.Position = new CCPoint(point.X + 107, point.Y + 128);
                    if (darkCell.Find(sp => sp.Position == sprite.Position) == null)
                    {
                        darkCell.Add(sprite);
                        layer.AddChild(sprite);
                    }
                }
                for (int i = 0; i <= a; i++)
                {
                    var sprite = new CCSprite("simple_tile.png");
                    if (pos.X - j < 0 || pos.Y - i < 0)
                        continue;
                    var point = new CCPoint(map.LayerNamed("Background").TileAt(new CCPoint(pos.X - j, pos.Y - i)).Position);
                    sprite.Position = new CCPoint(point.X + 107, point.Y + 128);
                    if (darkCell.Find(sp => sp.Position == sprite.Position) == null)
                    {
                        darkCell.Add(sprite);
                        layer.AddChild(sprite);
                    }
                }
            }
        }
        public static CCPoint tileCoordForPosition(CCPoint position)
        {
            var x = (position.X / map.TileSize.Width);
            var y = (((map.MapSize.Height * map.TileSize.Height) - position.Y) / map.TileSize.Height);
            y = Convert.ToInt32(y);
            x = Convert.ToInt32(x);
            return new CCPoint(x, y);
        }
        public static void removeCell()
        {
            foreach (var i in darkCell)
                Layer.RemoveChild(i);
            darkCell.Clear();
            Layer.RemoveChild(unitCell);
        }
        public static void endMove(float delay = 0.0f)
        {
            removeCell();
            if (delay == 0.0f)
                chooseUnit();
            else
                Layer.ScheduleOnce(ft => chooseUnit(), delay);
        }

        public static void chooseUnitForSpell( string nameOfSpell )
        {
            var tPoint = new CCPoint();
            tPoint.X = Mouse.GetState().Position.X ;
            tPoint.Y = CCDirector.SharedDirector.WinSize.Height - Mouse.GetState().Position.Y;
            Layer.Parent.RemoveChild(Layer.m_spellsBook);
            CurrentSpell = nameOfSpell;
            MagicBlade = new CCSprite("MagicBlade.png");
            MagicBlade.Position = tPoint;
            Layer.AddChild(MagicBlade, 1500);
            Layer.m_spellsBook = null;
            Layer.Schedule(Layer.Update);
            Player.Move = true;
        }

        public static void doMove(float duraction)
        {
            var task = new System.Threading.Tasks.Task(obj =>
            {
                float? dur = obj as float?;
                if (dur != null)
                    System.Threading.Thread.Sleep(Convert.ToInt32(dur * 1000));
                CCDirector.SharedDirector.ActionManager.RemoveAllActionsFromTarget(CurrentUnit.StandSprite);
                CurrentUnit.StandSprite.RunAction(new CCRepeatForever(CurrentUnit.Animation.Stand));
                isMouseValid = true;
                if (Speed <= 0)
                    endMove();
                else
                    showCell(CurrentUnit, Layer);
            }, duraction);
            task.Start();
            Layer.ScheduleOnce(ft => task.Wait(), duraction - 0.05f);
        }
        public static Player enemy()
        {
            var player = Player.Army.Find((Unit unit) => { return CurrentUnit == unit; });
            var enemy = Enemy.Army.Find((Unit unit) => { return CurrentUnit == unit; });
            return player == null ? Player : Enemy;
        }
        public static Unit getUnitFromCoord(CCPoint coord)
        {
            var fromPlayerArmy = Player.Army.Find((Unit unit) => { return GameLogic.tileCoordForPosition(coord) == GameLogic.tileCoordForPosition(unit.StandSprite.Position); });
            var fromEnemyArmy = Enemy.Army.Find((Unit unit) => { return GameLogic.tileCoordForPosition(coord) == GameLogic.tileCoordForPosition(unit.StandSprite.Position); });
            return fromEnemyArmy == null ? fromPlayerArmy : fromEnemyArmy;
        }
        public static bool isEnemy(Unit unit)
        {
            if ( enemy ().Army.Find ( u => u == unit ) != null )
                return true;
            return false;
        }
    }
}
