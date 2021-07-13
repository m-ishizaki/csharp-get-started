# 進歩したコンソールアプリを作ってみよう

## 二つのゲームを同時に進行する
まずは、**クラス**、**メソッド** を使わずに二つのゲームを進行するように変更してみましょう。  

**重要**  
今回のプログラムはコードの書き間違えや中途半端な状態での実行があると無限に終了しないプログラムになってしまう可能性がかなりあります。そんな場合には、**Ctrl+C** または **Cntrol+C** (Mac の場合) でプログラムを停止できるので覚えておくと捗ります。  

### 答えを二つ作る
答えを二つ作るように変更します。  

**変更前**
```cs
var answer = new System.Random().Next(1, 9);
```
**変更後**
```cs
var answer1 = new System.Random().Next(1, 9);
var answer2 = new System.Random().Next(1, 9);
```

### ゲームのクリアを記録する
元のアプリではゲームをクリアしたらすぐに終了する処理になっていましたが、今度は二つともをクリアしなければ終了の処理にならないようにしなければなりません。また、ゲームを一つだけクリアした状態ではクリアした方の表示も異なります。  
つまり、ゲームそれぞれについてクリアをしたかどうかをアプリが記憶できるようにしなければなりません。  

そこで、ゲームクリアフラグを二つ追加します。  

**変更前**
```cs
var answer1 = new System.Random().Next(1, 9);
var answer2 = new System.Random().Next(1, 9);
```
**変更語**
```cs
var answer1 = new System.Random().Next(1, 9);
var answer2 = new System.Random().Next(1, 9);
var cleared1 = false;
var cleared2 = false;
```

### ゲームのクリア判定を変更する
ゲームのクリア判定を「二つのゲームがクリアされた場合」という条件に変更します。  

**変更前**
```cs
while (input != answer)
```
**変更後**
```cs
while (!cleared1 || !cleared2)
```
```while``` は ```( )``` の中が **true** か **false** かを見ます。そして **true** の場合だけ続く ```{ }``` の中身が実行されます。  
また、```!``` は否定を意味します。そして ```||``` は **OR** を意味します。つまりこのプログラムコードは「```cleared1``` が **true** でない、または、```cleared2``` が **true** でない場合、繰り返す」という意味になります。  
もう少し自然言語に意訳すると「両方のゲームがクリアされるまで繰り返す」です。プログラミングでは、このように自然言語で考えた場合を大きく意訳したプログラムコードを書くことになることが多くあります。柔軟に考える力が重要です。  

### ゲーム 1 がクリア済みの場合の処理
ゲームが片一方だけクリアされている状態の処理を作っていきます。まず、ゲーム 1 がクリア済みの場合の処理です。クリア済みの場合「**ゲーム1: クリア済みです**」と表示します。  

**変更前**
```cs
if (input > answer)
{
    Console.WriteLine("答えはもっと小さい値です");
    continue;
}
if (input < answer)
{
    Console.WriteLine("答えはもっと大きい値です");
    continue;
}
break;
```
**変更後**
```cs
if(cleared1)
{
    Console.WriteLine("ゲーム1: クリア済みです");
}
else
{
    if (input > answer)
    {
        Console.WriteLine("答えはもっと小さい値です");
        continue;
    }
    if (input < answer)
    {
        Console.WriteLine("答えはもっと大きい値です");
        continue;
    }
}
```

### ゲーム 1 の正解の判定を追加する

ゲーム 1 の正解の判定を追加します。具体的な処理としては、正解した場合に ```cleared1``` を **true** にして、正解しなかった場合に答えのヒントを表示するようにします。

**変更前**
```cs
else
{
    if (input > answer)
    {
        Console.WriteLine("答えはもっと小さい値です");
        continue;
    }
    if (input < answer)
    {
        Console.WriteLine("答えはもっと大きい値です");
        continue;
    }
}
```
**変更後**
```cs
else if(input == answer1)
{
    Console.WriteLine("ゲーム1: 正解！ クリアです");
    cleared1 = true;
}
else if (input > answer1)
{
    Console.WriteLine("ゲーム1: 答えはもっと小さい値です");
 }
else if (input < answer1)
{
    Console.WriteLine("ゲーム1: 答えはもっと大きい値です");
}
```

### ゲーム 2 の処理を追加する

ゲーム 1 と同様の処理を行う、ゲーム 2 用のプログラムコードを追加します。

**追加するコード**
```cs
if(cleared2)
{
    Console.WriteLine("ゲーム2: クリア済みです");
}
else if(input == answer2)
{
    Console.WriteLine("ゲーム2: 正解！ クリアです");
    cleared2 = true;
}
else if (input > answer2)
{
    Console.WriteLine("ゲーム2: 答えはもっと小さい値です");
 }
else if (input < answer2)
{
    Console.WriteLine("ゲーム2: 答えはもっと大きい値です");
}
```

<hr />

[< 前へ](./textbook_advanced02.md) | [次へ >](./textbook_advanced04.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)