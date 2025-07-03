using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTanks
{
    internal class GameLogic
    {

        public int row = 15;
        public int col = 20;

        public void PrintClearMap(Map map, int x, int y) // Отрисовка удаления объектов (очищение предыдущего места нахождения объекта)
        {
            map.Add(x, y, map.grid);
            Console.SetCursorPosition(y, x);
            Console.Write(map.grid);
        }
        public void PrintUploadMap(Map map, char visual, int x, int y, ConsoleColor color = ConsoleColor.Green) // Обновление на карте местанахождения объекта (отрисовка)
        {
            map.Add(x,y,visual);
            Console.SetCursorPosition(y, x);
            Console.ForegroundColor = color;  // COLOR; 
            Console.Write(visual);
            Console.ResetColor();
        }
        public char Rotates(char key, char validDirection) // Проверка валидности нажатия клавиш и обрадотка раскладки и CAPS Lock
        {
            char newKey = key;
            if (key == 'w' || key == 'W' || key == 'ц' || key == 'Ц')
            {
                newKey = 'w';
            }
            else if (key == 's' || key == 'S' || key == 'ы' || key == 'Ы')
            {
                newKey = 's';
            }
            else if (key == 'd' || key == 'D' || key == 'в' || key == 'В')
            {
                newKey = 'd';
            }
            else if (key == 'a' || key == 'A' || key == 'ф' || key == 'Ф')
            {
                newKey = 'a';
            }
            else
            {
                newKey = validDirection;
            }
            return newKey;
        }
    }
}
