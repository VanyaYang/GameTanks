using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTanks
{
    internal class TankGame
    {
        public GameLogic gameLogic;
        public Tank tank;
        public Tank tank1;
        public List<Ammo> listAmmo;
        public List<Ammo> listAmmoBot;
        public List<Ammo> delete;
        char[] rates;
        public HashSet<char> shotingKey;
        public int row;
        public int column;
        Map map;
        char key;
        bool smartBot;
        bool isMoveble;

        public TankGame(bool smartBot)
        {
            this.gameLogic = new GameLogic();
            this.shotingKey = new HashSet<char> { 'e', 'E', 'у', 'У' };
            this.rates = ['a', 's', 'd', 'w'];
            this.row = gameLogic.row;
            this.column = gameLogic.col;
            this.tank = new Tank(row, column, StartPosition(1, row - 1), StartPosition(1, column - 1), key);
            this.tank1 = new Tank(row, column, StartPosition(1, row - 1), StartPosition(1, column - 1), key);
            this.listAmmo = new List<Ammo>();
            this.listAmmoBot = new List<Ammo>();
            this.delete = new List<Ammo>();
            tank1.visual = 'B';
            map = new Map(row, column);
            this.smartBot = smartBot;
            Console.CursorVisible = false;
            Console.Clear();
            
        }

        public void TanksGameRun() //Тело игры в Танчики с ботом
        {
            InitGame();
            map.Print(map);

            while (true)
            {
                EventKeyWait(map, isMoveble, 'e');

                gameLogic.PrintUploadMap(map, tank1.visual, tank1.X, tank1.Y, ConsoleColor.Blue); // рисуем бота 
                gameLogic.PrintUploadMap(map, tank.visual, tank.X, tank.Y, ConsoleColor.Green);   //рисуем себя

                AmmoFlyingPosition(listAmmo, delete, map); // полет пули и удаление\отрисовка на карте
                AmmoFlyingPosition(listAmmoBot, delete, map);

                ConflictAmmoFlying(listAmmo, listAmmoBot, delete, map);

                DamadeRaiting(listAmmo, tank1, delete, map);
                DamadeRaiting(listAmmoBot, tank, delete, map);

                LookAndShotBot(tank, tank1, map, listAmmoBot, rates); // хотьба и стрельба бота

                if (isMoveble)
                {
                    gameLogic.PrintUploadMap(map, tank.visual, tank.X, tank.Y, ConsoleColor.Green); // если было движение отрисовываем танк (фикс пропадания на карте при стрельбе в стену)
                }

                DamadeRaiting(listAmmo, tank1, delete, map);
                DamadeRaiting(listAmmoBot, tank, delete, map);

                ConflictAmmoFlying(listAmmo, listAmmoBot, delete, map);

                delete.Clear();
                
                if (EndingGame(tank, tank1, map))
                {
                    break;
                }
                Thread.Sleep(150);
            }
        }
        private void InitGame()
        {
            map.CreateMap();
            gameLogic.PrintUploadMap(map, tank.visual, tank.X, tank.Y, ConsoleColor.Green);
            gameLogic.PrintUploadMap(map, tank1.visual, tank1.X, tank1.Y, ConsoleColor.Blue);
            Console.Clear();
            isMoveble = false;
        }
        private static int StartPosition(int from, int until) // Стартовые позиции появления танков
        {
            int start = 0;
            Random position = new Random();
            start = position.Next(from, until);
            return start;
        }
        private void EventKeyWait(Map map, bool isMoveble, char snakeDirection) // Ожидание нажатия кнопки
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
                key = gameLogic.Rotates(key, snakeDirection);
                gameLogic.PrintClearMap(map, tank.X, tank.Y);
                if (shotingKey.Contains(key))
                {
                    listAmmo.Add(tank.Shot('#', ConsoleColor.Yellow));
                }
                tank.Move(key);
                isMoveble = true;
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
            }
        }
        private static char RandomRate(char[] rates) // Рандомная хотьба второго танка бота
        {
            Random step = new Random();
            int keyRate = step.Next(rates.Length);
            return rates[keyRate];

        }
        private void LookAndShotBot(Tank tank, Tank tankBot, Map map, List<Ammo> listAmmoBot, char[] rates)// Стрельба в направлении врага
        {
            if (tank.x == tankBot.x || tank.y == tankBot.y)
            {
                if (tank.x == tankBot.x && tank.y > tankBot.y)
                {
                    LookingBot(map, tankBot, 'd');
                }
                else if (tank.x == tankBot.x && tank.y < tankBot.y)
                {
                    LookingBot(map, tankBot, 'a');
                }
                else if (tank.x > tankBot.x && tank.y == tankBot.y)
                {
                    LookingBot(map, tankBot, 's');
                }
                else if (tank.x < tankBot.x && tank.y == tankBot.y)
                {
                    LookingBot(map, tankBot, 'w');
                }
            }
            if (smartBot == true)
            {
                GoingSmartBot(map, tank, tankBot);
            }
            GoingStupidBot(map, tankBot);

        }
        private void LookingBot(Map map, Tank tankBot, char direction)
        {
            gameLogic.PrintClearMap(map, tankBot.X, tankBot.Y);
            tankBot.direction = direction;
            gameLogic.PrintUploadMap(map, tankBot.visual, tankBot.X, tankBot.Y, ConsoleColor.Blue);
            listAmmoBot.Add(tankBot.Shot(tankBot.visualBullit, ConsoleColor.Magenta));
        }
         private void GoingSmartBot(Map map, Tank tank, Tank tankBot)
        {
            int goX = tankBot.x - tank.x;
            int goY = tankBot.y - tank.y;
            bool directionTankBotLook = Math.Abs(goX) > Math.Abs(goY) ? true : false;
            if (tankBot.x != tank.x && directionTankBotLook == false)
            {
                if (goX > 0)
                {
                    gameLogic.PrintClearMap(map, tankBot.x, tankBot.y);
                    //if (tankBot.x + 1 != tank.X)
                    tankBot.Move('w');
                    gameLogic.PrintUploadMap(map, tankBot.visual, tankBot.x, tankBot.y, ConsoleColor.Blue);
                }
                else
                {
                    gameLogic.PrintClearMap(map, tankBot.x, tankBot.y);
                    //if (tankBot.x - 1 != tank.X)
                    tankBot.Move('s');
                    gameLogic.PrintUploadMap(map, tankBot.visual, tankBot.x, tankBot.y, ConsoleColor.Blue);
                }
            }
            if (tankBot.y != tank.y && directionTankBotLook == true)
            {
                if (goY > 0)
                {
                    gameLogic.PrintClearMap(map, tankBot.x, tankBot.y);
                    //if (tankBot.y - 1 != tank.Y)
                    tankBot.Move('a');
                    gameLogic.PrintUploadMap(map, tankBot.visual, tankBot.x, tankBot.y, ConsoleColor.Blue);
                }
                else
                {
                    gameLogic.PrintClearMap(map, tankBot.x, tankBot.y);
                    //if (tankBot.y + 1 != tank.Y)
                    tankBot.Move('d');
                    gameLogic.PrintUploadMap(map, tankBot.visual, tankBot.x, tankBot.y, ConsoleColor.Blue);
                }
            }
        }
        private void GoingStupidBot(Map map, Tank tankBot)
        {
            gameLogic.PrintClearMap(map, tankBot.X, tankBot.Y);
            tankBot.Move(RandomRate(rates));
            gameLogic.PrintUploadMap(map, tankBot.visual, tankBot.X, tankBot.Y, ConsoleColor.Blue);
        }
        private void AmmoFlyingPosition(List<Ammo> listAmmo, List<Ammo> delete, Map map) // Полет пули и удаление с карты
        {
            foreach (Ammo bullet in listAmmo)
            {
                int X_ = bullet.X;
                int Y_ = bullet.Y;
                if (map.dermo[X_][Y_] == bullet.vision)
                {
                    Console.SetCursorPosition(bullet.Y, bullet.X);
                    Console.Write(map.grid);
                }

                bullet.MoveBullit(delete);
                map.Add(bullet.X, bullet.Y, bullet.vision);
                if (map.dermo[X_][Y_] == bullet.vision)
                {
                    gameLogic.PrintUploadMap(map, bullet.vision, bullet.X, bullet.Y, bullet.displayInfo.color);
                    map.Add(X_,Y_,map.grid);
                }
            }
        }
        private void DamadeRaiting(List<Ammo> listAmmo, Tank tank, List<Ammo> delete, Map map) // Событие попадания пули по танку
        {
            for (int i = 0; i < listAmmo.Count; i++)
            {
                if (listAmmo[i].x == tank.x && listAmmo[i].y == tank.y)
                {
                    tank.Hp = 0;
                }
                if (delete.Contains(listAmmo[i]))
                {
                    gameLogic.PrintClearMap(map, listAmmo[i].X, listAmmo[i].Y);
                    listAmmo.Remove(listAmmo[i]);
                }
            }
        }
        private void ConflictAmmoFlying(List<Ammo> listAmmo, List<Ammo> listAmmoBot, List<Ammo> delete, Map map)
        {
            while (true)
            {
                foreach (Ammo item in listAmmo)
                {
                    foreach (Ammo bot in listAmmoBot)
                    {
                        if (item.x == bot.x && item.y == bot.y)
                        {
                            delete.Add(bot);
                            gameLogic.PrintClearMap(map, bot.x, bot.y);
                            delete.Add(item);
                            gameLogic.PrintClearMap(map, item.x, item.y);
                        }
                    }
                }
                break;
            }
        }
        private bool EndingGame(Tank tank, Tank tank1, Map map) // завершение игры при убийстве оппонента (игра в танки)
        {
            if (tank1.Hp == 0)
            {
                gameLogic.PrintClearMap(map, tank1.X, tank1.Y);
                gameLogic.PrintUploadMap(map, tank.visual, tank.X, tank.Y, ConsoleColor.Green);
                Console.SetCursorPosition(map.Column + 1, 0);
                Console.WriteLine("Ты Победил!");
                Console.SetCursorPosition(0, map.Column + 1);
                Console.ReadLine();
                
                return true;

            }
            if (tank.Hp == 0)
            {
                gameLogic.PrintClearMap(map, tank.X, tank.Y);
                gameLogic.PrintUploadMap(map, tank1.visual, tank1.X, tank1.Y, ConsoleColor.Blue);
                Console.SetCursorPosition(map.Column + 1, 0);
                Console.WriteLine("Победил Бот!");
                Console.SetCursorPosition(0, map.Column + 1);
                Console.ReadLine();
                return true;
            }
            return false;
        }
    }
}
