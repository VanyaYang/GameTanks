using System.Runtime.CompilerServices;
using WindowsInput;


namespace GameTanks
{
    internal class Program 
    {
        static void Main(string[] args)
        {
        newCurcle:
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Выберите игру: 1 - танчики, 2 - змейка");
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            char selectGame;
            selectGame = consoleKey.KeyChar;
            if (selectGame == '1')
            {
            subMenu:
                Console.Clear();
                Console.WriteLine("Выберите сложность: 1 - Умный Бот, 2 - Легкий режим");
                ConsoleKeyInfo consoleKeyMode = Console.ReadKey(true);
                char selectMode;
                selectMode = consoleKeyMode.KeyChar;

                if (selectMode == '1')
                {
                    TankGame tankGame = new TankGame(true);
                    tankGame.TanksGameRun();
                }
                else if (selectMode == '2')
                {
                    TankGame tankGame = new TankGame(false);
                    tankGame.TanksGameRun();
                }
                else
                {
                    goto subMenu;
                }
            }
            else
            {
                goto newCurcle;
            }
        }
      
    }      
}

