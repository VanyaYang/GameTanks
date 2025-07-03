using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameTanks
{
    internal class Tank 
    {
        public int Hp;
        public int x;
        public int y;
        public int row;
        public int col;
        public char visual;
        public char direction;
        public char visualBullit;

        public Tank(int row, int col, int x, int y, char key)
        {
            this.row = row - 1;
            this.col = col - 1;
            this.x = x;
            this.y = y;
            direction = key;
            visual = 'C';
            visualBullit = '#';
            Hp = 1;
        }
        public Ammo Shot(char sumb, ConsoleColor a) // создание пули
        {
            visionInfo visionInfo = new visionInfo(sumb, a);
            Ammo ammo = new Ammo(x, y, row, col, direction, visionInfo);
            return ammo;
        }
        public void Move(char key) // передвижение танка юзера
        {
            switch (key)
            {
                case 'w':
                    direction = key;
                    if (x > 1)
                        x -= 1;
                    break;
                case 's':
                    direction = key;
                    if (x < row - 1)
                        x += 1;
                    break;
                case 'd':
                    direction = key;
                    if (y < col - 1)
                        y += 1;
                    break;
                case 'a':
                    direction = key;
                    if (y > 1)
                        y -= 1;
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
        public int HP
        {
            get => Hp;
            set => Hp = value;
        }
    }
}
    

