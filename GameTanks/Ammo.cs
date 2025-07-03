using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace GameTanks
{
    internal class Ammo 
    {
        public int x;
        public int y;
        public int row;
        public int col;
        public char vision;
        public char key;
        public visionInfo displayInfo;
        public Ammo(int x, int y, int row, int col, char key, visionInfo displayInfo1)
        {
            this.x = x;
            this.y = y;
            this.row = row;
            this.col = col;
            this.key = key;
            vision = '#';
            displayInfo = displayInfo1;
        }
        public void MoveBullit(List<Ammo> delete) // сторона передвижения пули
        {
            switch (key)
            {
                case 'w':
                    if (x > 1)
                        x -= 1;
                    else
                        delete.Add(this);
                    break;
                case 's':
                    if (x < row - 1)
                        x += 1;
                    else
                        delete.Add(this);
                    break;
                case 'd':
                    if (y < col - 1)
                        y += 1;
                    else
                        delete.Add(this);
                    break;
                case 'a':
                    if (y > 1)
                        y -= 1;
                    else
                        delete.Add(this);
                    break;
            }
        }


        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }
    }
}
