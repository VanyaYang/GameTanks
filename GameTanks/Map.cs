using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTanks
{
    internal class Map
    {
        private int row;
        private int column;
        public char grid;
        public char wall;
        public char[][] dermo;
        public Map(int row, int column)
        {
            grid = '∙';
            wall = '+';
            this.row = row;
            this.column = column;
            dermo = new char[row][];

        }
        public void CreateMap() // создание карты
        {
            dermo = new char[row][];
            for (int i = 0; i < row; i++)
            {
                dermo[i] = new char[column];
                for (int j = 0; j < column; j++)
                {
                    if (i == 0 || i == row - 1 || j == 0 || j == column - 1)
                    {                                                           // ДЕЛАЕМ СТЕНЫ
                        dermo[i][j] = wall;
                    }
                    else
                        dermo[i][j] = grid;
                }
            }
        }
        public void Print(Map map) // отрисовка карты
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (map.dermo[i][j] == wall) // Рисуем и меняем цвет стен
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(map.dermo[i][j]);
                        Console.ResetColor();
                        continue;
                    }
                    Console.Write(map.dermo[i][j]);
                }
                Console.WriteLine();
            }
        }
        public void Add(int x, int y, char sumbol)
        {
            dermo[x][y] = sumbol;
        }
        public int Column
        {
            get
            {
                return column;
            }
        }
        public int Row
        {
            get
            {
                return row;
            }
        }
    }
}
