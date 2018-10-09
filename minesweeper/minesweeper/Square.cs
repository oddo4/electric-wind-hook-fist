using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    class Square
    {
        public bool Bomb = false;
        public int NearValue = 0;
        public bool Mark = false;

        public Square()
        {

        }

        public void AddValue()
        {
            NearValue += 1;
        }
    }
}
