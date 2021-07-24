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

    public string Proceed(int input)
    {
        var no = $"ゲーム{_no}: ";
        if (Cleared)
        {
            return $"{input} → {no}クリア済みです";
        }
        if (input > _answer)
        {
            return $"{input} → {no}答えはもっと小さい値です";
        }
        if (input < _answer)
        {
            return $"{input} → {no}答えはもっと大きい値です";
        }
        Cleared = true;
        return $"{input} → {no}正解！ クリアです";
    }
}