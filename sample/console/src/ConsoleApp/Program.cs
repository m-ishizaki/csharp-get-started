using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            int input = default;
            Console.WriteLine("数字あて");
            while(input != answer)
            {
                Console.Write("数字を入力: ");
                var line = Console.ReadLine();
                if(!int.TryParse(line, out input))
                {
                    Console.WriteLine("数字を入力してください");
                    continue;
                }
                if(input < 1 || input > 9)
                {
                    Console.WriteLine("1-9 の数字を 1 文字入力してください");
                    continue;
                }
                if(input > answer)
                {
                    Console.WriteLine("答えはもっと小さい値です");
                    continue;
                }
                if(input < answer)
                {
                    Console.WriteLine("答えはもっと大きい値です");
                    continue;
                }
                break;
            }
            Console.Write("正解！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }
    }
}
