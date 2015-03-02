using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.SmartMove
{
    public static class SmartMove
    {
        public static bool checkWay(CCPoint position, List <Cell> openCell, List <Cell> closeCell, CCPoint finishPos, bool checkDark)
        {
            if (checkDark)
                if (GameLogic.darkCell.Find(sp => GameLogic.tileCoordForPosition(sp.Position) == GameLogic.tileCoordForPosition(position)) == null)
                    return false;
            if (openCell.Find(cell => cell.Position == position) != null)
                return false;
            if ( closeCell.Find ( cell => cell.Position == position ) != null)
                return false;
            if (GameLogic.getUnitFromCoord(position) != null)
            {
                if (GameLogic.tileCoordForPosition(position) == GameLogic.tileCoordForPosition(finishPos)
                    && GameLogic.isEnemy(GameLogic.getUnitFromCoord(position)))
                    return true;
                return false;
            }
            return true;
        }
        public static void addNewCell ( Cell parent, List <Cell> openCell, List <Cell> closeCell, CCPoint finishPos, bool checkDark )
        {
            var pos = parent.Position;
            var mapSize = GameLogic.map.TileSize;
            pos.X -= mapSize.Width;
            if (checkWay(pos, openCell, closeCell, finishPos, checkDark))
                openCell.Add(new Cell(parent, pos, 10, finishPos));
            pos = parent.Position;
            pos.X += mapSize.Width;
            if (checkWay(pos, openCell, closeCell, finishPos, checkDark))
                openCell.Add(new Cell(parent, pos, 10, finishPos));
            pos = parent.Position;
            pos.Y -= mapSize.Height;
            if (checkWay(pos, openCell, closeCell, finishPos, checkDark))
                openCell.Add(new Cell(parent, pos, 10, finishPos));
            pos = parent.Position;
            pos.Y += mapSize.Height;
            if (checkWay(pos, openCell, closeCell, finishPos, checkDark))
                openCell.Add(new Cell(parent, pos, 10, finishPos));
        }
        public static List<CCPoint> findWay( Cell parent, CCPoint finishPosition, bool checkDark )
        {
            var openCell = new List<Cell>();
            var closeCell = new List<Cell>();
            var path = new List<CCPoint>();
            if (GameLogic.tileCoordForPosition(parent.Position) == GameLogic.tileCoordForPosition(finishPosition))
                return path;
            closeCell.Add(parent);
            addNewCell(parent, openCell, closeCell, finishPosition, checkDark);
            Cell min;
            for (; ; )
            {
                if (openCell.Count == 0)
                    return new List<CCPoint>();
                min = openCell.Find ( cell => openCell.Min ( c => c.F ) == cell.F );
                if (GameLogic.tileCoordForPosition(min.Position) == GameLogic.tileCoordForPosition(finishPosition))
                    break;
                openCell.Remove(min);
                closeCell.Add(min);
                addNewCell(min, openCell, closeCell, finishPosition, checkDark);
            }
            for (; ; )
            {
                path.Add(min.Position);
                min = min.Parent;
                if (min == null)
                    break;
            }
            path.Reverse();
            return path;
        }
    }
}
