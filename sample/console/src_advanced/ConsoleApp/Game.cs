using System;

internal class Game
{
    int _no { get; init; }
    int _answer { get; init; }
    public bool Cleared { get; private set; }

    public Game(int no)
    {
        _no = no;
        _answer = new System.Random().Next(1, 9);
    }

    public void Proceed(int input)
    {
        Console.Write($"ゲーム{_no}: ");
        if (Cleared)
        {
            Console.WriteLine("クリア済みです");
            return;
        }
        if (input > _answer)
        {
            Console.WriteLine("答えはもっと小さい値です");
            return;
        }
        if (input < _answer)
        {
            Console.WriteLine("答えはもっと大きい値です");
            return;
        }
        Console.WriteLine("正解！ クリアです");
        Cleared = true;
    }
}
