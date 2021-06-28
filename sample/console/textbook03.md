# コンソールアプリを作ってみよう

### 入力された文字列が 1 桁の数値であるかを確認する

```if (...){ } else { }``` で条件によって実行されるコードを分岐できることを学びました。  
今度はこの機能をうまく使って、入力された文字列が 1 桁の数値であるかを確認してみましょう。
```cs
if (input < 1 || input > 9)
{
    Console.WriteLine("1-9 の数字を 1 文字入力してください");
}
```
入力された文字列を数値にした ```input``` が、1 より小さいまたは 9 より大きい時だけ **1-9 の数字を 1 文字入力してください** と表示するコードです。 ```input < 1``` といった書き方で、「1 より小さい場合」といった条件を表します。 ```||``` は or 条件です。「1 より小さい」「or」「9 より大きい」場合にだけ ```{ }``` の中のコードが実行されます。  
すなわち、1 ～ 9 の数値 **でない** だけ実行されます。これによりアプリが求める入力の書式に合わないプレイヤーの入力に対して、**「違うよ」** というメッセージをプレイヤーに伝えることができます。  
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            Console.WriteLine(answer);
            Console.WriteLine("数字あて");
            var line = Console.ReadLine();
            Console.WriteLine(line);
            if (!int.TryParse(line, out var input))
            {
                Console.WriteLine("数字を入力してください");
            } else {
                Console.WriteLine("数値が入力されました");
            }
            if (input < 1 || input > 9)
            {
                Console.WriteLine("1-9 の数字を 1 文字入力してください");
            }
            Console.ReadKey();
        }
    }
}
```
実行し、キーボードで **12** と打って Enter キーを押してみましょう。
```
12
数値が入力されました
1-9 の数字を 1 文字入力してください
```
と表示されます。再度実行してこんどは **3** と打って Enter キーを押してみましょう。  
```
3
数値が入力されました
```
と表示されます。

### プレイヤーが正解を入力できたかを確認する

```if(...){ } else { }``` を使えばアプリが様々な判断をして動作を変えられることがわかりました。プレイヤーが入力した答えが正解かどうかを判断する機能もこの ```if(...){ } else { }``` で作れます。作っていきましょう。  
```cs
if (input > answer)
{
    Console.WriteLine("答えはもっと小さい値です");
}
if (input < answer)
{
    Console.WriteLine("答えはもっと大きい値です");
}
if (input == answer)
{
    Console.Write("正解！\n終了するには何かキーを押してください... ");
}
```
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            Console.WriteLine(answer);
            Console.WriteLine("数字あて");
            var line = Console.ReadLine();
            Console.WriteLine(line);
            if (!int.TryParse(line, out var input))
            {
                Console.WriteLine("数字を入力してください");
            } else {
                Console.WriteLine("数値が入力されました");
            }
            if (input < 1 || input > 9)
            {
                Console.WriteLine("1-9 の数字を 1 文字入力してください");
            }
            if (input > answer)
            {
                Console.WriteLine("答えはもっと小さい値です");
            }
            if (input < answer)
            {
                Console.WriteLine("答えはもっと大きい値です");
            }
            if (input == answer)
            {
                Console.Write("正解！\n終了するには何かキーを押してください... ");
            }
            Console.ReadKey();
        }
    }
}
```
実行してみましょう。正解を入力できると
```
正解！\n終了するには何かキーを押してください... 
```
と表示されます。正解できない場合は
```
答えはもっと小さい値です
```
または
```
答えはもっと大きい値です
```
と表示されます。ゲームらしさが出てきましたね。

### 正解が入力されるまで繰り返す

今のままではプレイヤーに与えられたチャンスは 1 回だけです。数を当てられなかった場合に「もっと小さい」「もっと大きい」というヒントを得られますが、それを活かす機会は与えられません。完全にエスパー養成アプリになってしまっています。そこでアプリに正解するまで繰り返す処理を追加します。  
C# には繰り返し処理を行う機能は複数ありますが、今回は ```while (...) {  }``` を使います。この機能は ```while``` の中の ```...``` が条件として正しい間 ```{  }``` の中の処理を繰り返し実行します。 

今回はプレイヤーの入力が正解と同じ **でない** 間、繰り返しプレーヤーからの入力を受け付けるようにしてみます。C# では値が **同じでない** ことを ```!=``` と書いて判断します。条件の部分に書くコードは次の用にあります。これで ```input``` と ```answer``` が **同じでない** 場合、という条件になります。  
```cs
input != answer
```
プログラムコードの ```Console.WriteLine("数字あて");``` の次の行に繰り返し処理の ```while (...) {``` を追加してみましょう。
```cs
while(input != answer)
{
```
すると、```input``` の下に赤い波線が表示されます。これはプログラムコードに構文エラーがあることを教えてくれています。今回は ```input``` という名前がコードで使えるようになっていないので、ここでは ```input``` を使えないと教えてくれています。  
この ```while (input != answer)``` より前に ```input``` という名前を使えるようにするコードを追加します。```var answer = new System.Random().Next(1, 9);``` の次の行に次のコードを追加します。このコードで、```input``` という名前の数値を使えるようにして、初期値 (数値の場合は 0) を持った状態にするという意味になります。
```cs
int input = default;
```
ここでまた、今度は ```if (!int.TryParse(line, out var input))``` の ```input``` に赤い波線が表示れます。この一行の中にも ```input``` を使えるようにしている部分があるので、今度は ```input``` という名前が既に使えるようになっているので、もう一度使えるようにするコードはエラーですと教えてくれています。  
このエラーが表示されている行の ```var``` という部分が名前を使えるようにする機能です。この行から ```var``` を消します。  
**変更後**
```cs
if(!int.TryParse(line, out input))
```
まだ別の場所でエラーが表示され続けるはずです。先ほど ```while (input != answer) {``` で ```{  }``` の開始の ```{``` を書きましたが、終了の ```}``` を書いていません。``` Console.ReadKey();``` の **一つ上の行** に ```}``` を追加します。
```cs
}
```
**変更後 Program.cs 例**
```cs
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new System.Random().Next(1, 9);
            int input = default;
            Console.WriteLine(answer);
            Console.WriteLine("数字あて");
            while (input != answer)
            {
                var line = Console.ReadLine();
                Console.WriteLine(line);
                if (!int.TryParse(line, out input))
                {
                    Console.WriteLine("数字を入力してください");
                }
                else
                {
                    Console.WriteLine("数値が入力されました");
                }
                if (input < 1 || input > 9)
                {
                    Console.WriteLine("1-9 の数字を 1 文字入力してください");
                }
                if (input > answer)
                {
                    Console.WriteLine("答えはもっと小さい値です");
                }
                if (input < answer)
                {
                    Console.WriteLine("答えはもっと大きい値です");
                }
                if (input == answer)
                {
                    Console.Write("正解！\n終了するには何かキーを押してください... ");
                }
            }
            Console.ReadKey();
        }
    }
}
```
これで、プレイヤーが正解を入力するまで繰り返す処理が完成です。ゲームとしても完成したといってもよいでしょう。  
しかし、少し処理をわかりやすくするためのコードの改善をしてみましょう。

<hr />

[< 前へ](./textbook02.md) | [次へ >](./textbook04.md)  

[C# でアプリを作るへ](../../textbook/practice.md)