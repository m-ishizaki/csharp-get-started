using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var game1 = new Game(1);
            var game2 = new Game(2);
            Console.WriteLine("数字あて");
            while(!game1.Cleared || !game2.Cleared)
            {
                Console.Write("数字を入力: ");
                var line = Console.ReadLine();
                if(!int.TryParse(line, out var input))
                {
                    Console.WriteLine("数字を入力してください");
                    continue;
                }
                if(input < 1 || input > 9)
                {
                    Console.WriteLine("1-9 の数字を 1 文字入力してください");
                    continue;
                }
                game1.Proceed(input);
                game2.Proceed(input);
            }
            Console.Write("ゲームクリア！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }
}
