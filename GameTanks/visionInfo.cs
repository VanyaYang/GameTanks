using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTanks
{
    internal struct visionInfo
    {
        public char ch;
        public ConsoleColor color;
        public visionInfo(char symbol, ConsoleColor color)
        {
            this.ch = symbol;
            this.color = color;
        }
    }
}
