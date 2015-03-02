using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Spells
{
    public class SpellsBook: CCLayer
    {
        private List<CCPoint> m_points = new List<CCPoint>();
        private CCSprite m_backGround;
        private int m_firstSpellsOnPage = 0;
        private int m_lastSpellOnPage = 0;
        private List<Spell> m_currentPage = new List<Spell>();
        private Player m_player;
        private CCMenu m_menu;
        public SpellsBook(Player player)
        {
            m_player = player;
            var winSize = CCDirector.SharedDirector.WinSize.Center;

            #region Set Default Position
            m_points.Add(new CCPoint(winSize.X - 987, winSize.Y - 265)); //1 
            m_points.Add(new CCPoint(winSize.X - 810, winSize.Y - 265)); //2
            m_points.Add(new CCPoint(winSize.X - 987, winSize.Y - 365)); //3
            m_points.Add(new CCPoint(winSize.X - 810, winSize.Y - 365)); //4
            m_points.Add(new CCPoint(winSize.X - 987, winSize.Y - 470)); //5
            m_points.Add(new CCPoint(winSize.X - 810, winSize.Y - 470)); //6
            m_points.Add(new CCPoint(winSize.X - 553, winSize.Y - 265)); //7
            m_points.Add(new CCPoint(winSize.X - 376, winSize.Y - 265)); //8
            m_points.Add(new CCPoint(winSize.X - 553, winSize.Y - 365)); //9
            m_points.Add(new CCPoint(winSize.X - 376, winSize.Y - 365)); //10
            m_points.Add(new CCPoint(winSize.X - 553, winSize.Y - 470)); //11
            m_points.Add(new CCPoint(winSize.X - 376, winSize.Y - 470)); //12
            m_backGround = new CCSprite("Spells Book.png");
            m_backGround.Position = CCDirector.SharedDirector.WinSize.Center;
            #endregion

            foreach (var i in player.SpellsBook)
            {
                if (m_lastSpellOnPage < 12)
                {
                    m_currentPage.Add(i);
                    m_lastSpellOnPage++;
                }
            }
            List<CCMenuItemImage> page = new List<CCMenuItemImage>();
            for (int i = 0; i < m_currentPage.Count; i++)
            {
                var menu = new CCMenuItemImage(m_currentPage[i].Path, m_currentPage[i].Path, m_currentPage[i].Path, MenuCallBack);
                menu.Name = m_currentPage[i].ToString ();
                menu.Position = m_points[i];
                page.Add(menu);
            }
            m_menu = new CCMenu(page.ToArray ());
            this.AddChild(m_backGround, 2);
            this.AddChild(m_menu, 1500);
        }

        private void MenuCallBack ( object sender )
        {
            CCMenuItemImage selectedMenu = sender as CCMenuItemImage;
            GameLogic.chooseUnitForSpell(selectedMenu.Name);
        }
    }
}
