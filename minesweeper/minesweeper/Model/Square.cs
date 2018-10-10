using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper.Model
{
    class Square
    {
        public bool Bomb = false;
        public int NearValue = 0;
        public bool Mark = false;
        public bool Covered = true;
        public int PosX { get; set; }
        public int PosY { get; set; }

        public void AddValue()
        {
            NearValue += 1;
        }

        public int SetMark(int Marked)
        {
            Mark = !Mark;

            if(Mark)
            {
                Marked++;
            }
            else
            {
                Marked--;
            }

            return Marked;
        }
    }
}
