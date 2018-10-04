using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesweeper
{
    class Square
    {
        public bool Bomb { get; set; }
        public Mark MarkStatus { get; set; }

        public Square()
        {
            
        }
    }
}
