using System;

namespace CFNC1
{
    internal class Program
    {
        public static int Hight = 28;
        public static int Weight = 119;

        public static int StartCoutn = 300;
        public static int StepTime = 50;

        public static int MinForLife = 3;
        public static int MaxForLife = 4;

        public static int MinForSpawn = 3;
        public static int MaxForSpawn = 3;

        public static char BorederChar = '#';
        public static char LifeChar = '0';
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int Centery = 1;
            char[,] Zone = new char[Hight, Weight];
            CreateBorder(ref Zone);
            CreateLife(StartCoutn, ref Zone);
            while (true)
            {
                render(Zone);
                Console.WriteLine("Поколение " + Centery);
                update(ref Zone);
                Centery++;
                System.Threading.Thread.Sleep(StepTime);
            }
        }

        public static void CreateLife(int Count, ref char[,] Zone)
        {
            Random rnd = new Random();
            for (int i = 0; i < Count; i++)
            {
                int x = 0, y = 0;
                do
                {
                    x = rnd.Next(1, Weight - 2);
                    y = rnd.Next(1, Hight - 2);
                } while (Zone[y,x] == LifeChar);
                Zone[y,x] = LifeChar;
            }
        }
        public static void CreateBorder(ref char[,] Zone)
        {
            for (int y = 0; y < Hight; y++)
            {
                for (int x = 0; x < Weight; x++)
                {
                    if (x == 0 || x == Weight - 1 || y == 0 || y == Hight - 1)
                        Zone[y, x] = BorederChar;
                }
            }
        }
        public static void update(ref char[,] Zone)
        {
            char[,] Buffer = new char[Hight,Weight];
            CreateBorder(ref Buffer);
            for (int y = 1; y < Hight-1; y++)
            {
                for (int x = 1; x < Weight-1; x++)
                {
                    if (Zone[y, x] == LifeChar)
                    {
                        int Count = CharCount(x,y, LifeChar, Zone);
                        if (Count >= MinForLife && Count <= MaxForLife)
                        {
                            Buffer[y,x] = LifeChar;
                        }
                    }
                    if(Zone[y, x] != LifeChar)
                    {
                        int Count = CharCount(x, y, LifeChar, Zone);
                        if (Count >= MinForSpawn && Count <= MaxForSpawn)
                        {
                            Buffer[y, x] = LifeChar;
                        }
                    }
                }
            }
            Zone = Buffer;
        }

        public static void render(char[,] Zone)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < Hight; y++)
            {
                for(int x = 0; x < Weight; x++)
                {
                    Console.Write(Zone[y,x]);
                }
                Console.WriteLine();
            }
        }
        
        public static int CharCount(int x, int y, char simb, char[,] Zone)
        {
            int Count = 0;
            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (Zone[i, j] == simb) Count++;
                }
            }
            return Count;
        }
    }
}
