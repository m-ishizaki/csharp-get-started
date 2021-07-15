# 進歩したコンソールアプリを作ってみよう

## メソッドを使ったプログラムコード

ひとまず、**メソッド** を使ったプログラムコードが出来上がりました。現在のプログラムコードは次のようになっています。  

```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer1 = new System.Random().Next(1, 9);
            var answer2 = new System.Random().Next(1, 9);
            var cleared1 = false;
            var cleared2 = false;
            int input = default;
            Console.WriteLine("数字あて");
            while (!cleared1 || !cleared2)
            {
                Console.Write("数字を入力: ");
                var line = Console.ReadLine();
                if (!int.TryParse(line, out input))
                {
                    Console.WriteLine("数字を入力してください");
                    continue;
                }
                if (input < 1 || input > 9)
                {
                    Console.WriteLine("1-9 の数字を 1 文字入力してください");
                    continue;
                }
                cleared1 = Proceed(1, cleared1, answer1, input);
                cleared2 = Proceed(2, cleared2, answer2, input);
            }
            Console.Write("正解！\n終了するには何かキーを押してください... ");
            Console.ReadKey();
        }

        static bool Proceed(int gameNo, bool cleared, int answer, int input)
        {
            Console.Write($"ゲーム{gameNo}: ");
            if (cleared)
            {
                Console.WriteLine("クリア済みです");
                return true;
            }
            if (input > answer)
            {
                Console.WriteLine("答えはもっと小さい値です");
                return false;
            }
            if (input < answer)
            {
                Console.WriteLine("答えはもっと大きい値です");
                return false;
            }
            Console.WriteLine("正解！ クリアです");
            return true;
        }
    }
}
```

正解の判定処理や、正解したか。クリア済みかなどの表示のプログラムコードが一回だけ書かれた状態になりました。これには次のようなメリットがあります。  

* クリア済みの表示を変更したいと思ったとします。これまでは 2 箇所を変更しなければなりませんでした。これからは 1 箇所の変更で済みます。これは変更の手間が少ないのみならず間違いにくいという効果もあります。  
* これまでのプログラムコードでは同じ変更を 2 箇所に対して行うので、どちらか一方で書き間違えをするリスクがありました。これからは 1 箇所の変更なので書き間違えをするリスクは劇的に減少します。  

この書き間違えリスクを減らすという考えはプログラミングにおいて最重要とも言える最優先事項です。覚えておきましょう。  
また、ゲームを 3 つに増やす場合にも、長いコードをコピー＆ペーストする必要がなくなります。

## まだ大きな書き間違えリスクが存在

**メソッド** でコードを整理しましたが、まだ書き換えやゲームを増やす際にミスをする大きなリスクが残っています。それが、```cleared1 = Proceed(1, cleared1, answer1, input);``` の ```cleared1 =```、```1, cleared1, answer1,``` の部分です。  
ゲームを 3 つに増やすそうと思ったら
```cs
cleared1 = Proceed(1, cleared1, answer1, input);
cleared2 = Proceed(2, cleared2, answer2, input);
```
となっている部分を 1 行コピー＆ペーストして
```cs
cleared1 = Proceed(1, cleared1, answer1, input);
cleared2 = Proceed(2, cleared2, answer2, input);
cleared2 = Proceed(2, cleared2, answer2, input);
```
続いて 3 行目の ```2``` をすべて ```3``` と書き換えることになりますが、ここで書き換え漏れが発生します。どんなに注意深くプログラムコードを書いていても、撲滅はできないミスです。必ずどこかでミスをします。  

そこで **クラス** を使ってミスをする確率を減らしていきましょう。

## クラスを使ってみる

**クラス** を使ってプログラムコードを改善してみましょう。  
**※このコンテンツは入門者にプログラムコードを書いて動かしてもらうことを目的としているので、クラス の詳しいことは別途学習してください。**  

**クラス** を使うと複数のデータとプログラムの処理をひとまとめにして扱うことができるようになります。例えば今回のコードでは、```gameNo```、```cleared```、``` answer``` がゲーム 1 とゲーム 2 用で 2 セット必要になっていますし、それをセットでメソッドに渡すプログラムコードを書く際にミスも起こります。これを **クラス** を使って改善しましょう。





<hr />

[< 前へ](./textbook_advanced03.md) | [次へ >](./textbook_advanced05.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)