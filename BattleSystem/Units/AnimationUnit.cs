using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cocos2D;

namespace BattleSystem.Units
{
    public class AnimationUnit
    {
        public CCAnimate Walk { get; set; }
        public CCAnimate Attack { get; set; }
        public CCAnimate Stand { get; set; }
        public CCAnimate Dizzy { get; set; }
        public CCAnimate Hit { get; set; }
        public CCAnimate Block { get; set; }
        public CCAnimate SpecialMoves { get; set; }
    }
}
