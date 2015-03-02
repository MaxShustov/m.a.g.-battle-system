using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.SmartMove
{
    public class Cell
    {
        private int m_H;
        private int m_D;
        private int m_F;
        public int F { get { return m_F; } }
        public int D { get { return m_D; } }
        private Cell m_parent;
        public Cell Parent { get { return m_parent; } }
        public CCPoint Position { get; set; }
        public Cell ( CCPoint position )
        {
            m_parent = null;
            m_D = 0;
            m_F = 0;
            m_H = 0;
            Position = position;
        }
        public int calcH(CCPoint finishPosition)
        {
            var sPosition = Position;
            var eTile = new CCPoint(GameLogic.tileCoordForPosition(finishPosition));
            var sTile = new CCPoint(GameLogic.tileCoordForPosition(sPosition));
            var mapSize = GameLogic.map.TileSize;
            int count = 0;
            while (eTile != sTile)
            {
                var diff = eTile - sTile;
                if (diff.X < 0) // left
                    sPosition.X -= mapSize.Width;
                else if (diff.X > 0) //right
                    sPosition.X += mapSize.Width;
                else if (diff.Y > 0) //down
                    sPosition.Y -= mapSize.Height;
                else if (diff.Y < 0) //up
                    sPosition.Y += mapSize.Height;
                count++;
                sTile = new CCPoint(GameLogic.tileCoordForPosition(sPosition));
            }
            return count;
        }
        public Cell(Cell parent, CCPoint position, int d, CCPoint finishPosition)
        {
            m_parent = parent;
            Position = position;
            m_D = parent.D + d;
            m_H = calcH ( finishPosition );
            m_F = m_D + m_H;
        }
    }
}
