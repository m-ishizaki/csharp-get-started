# WPF アプリを作ってみよう

### 改善ポイント
1. ```void OnAnswerCommand(object parameter) { }``` の中の ```if (...) {  }``` はどれか一つの場所が実行されれば、後続の処理は実行する必要がありません。例えば ```if (input > answer)``` であれば ```if (input < answer)``` のことは考える必要はありません。そういった場合は、```if (...) {  }``` の中に ```return;``` と書くと、その場で処理が完了します。そういった形で、後続の処理の実行が不要だと分かったタイミングで、後続の処理を行わない機能を使うことが良いコードとされます。  
1. プレイヤーが正解を入力した場合の ```input == answe``` ですが、実はこれはその前の ```if (input > _answer)``` や ```if (input < _answer)``` のでなかった場合と考えることもできます。それ以前のどの```if (...) {  }``` でもなかった場合に **「正解！\n終了するには右上の × ボタンを押してください... 」** と表示するコードを書けば同じことになります。プログラムコードはより短く、より ```if (...)``` などが少ない方が良いコードとされます。  

**変更後 MainWindow.xaml.cs 例**
```cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    class Command : ICommand
    {
        public Action<object> Action;
        bool _canExecute = true;
        public event EventHandler CanExecuteChanged;

        public Command(Action<object> execute)
        {
            Action = execute;
        }

        public void SetCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            CanExecuteChanged?.Invoke(null, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            Action?.Invoke(parameter);
        }
    }

    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(property, value))
            {
                return false;
            }
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }

    class MainWindowViewModel : BaseViewModel
    {
        public Command AnswerCommand { get; }
        public string Input { get; set; }
        int _answer;
        string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainWindowViewModel()
        {
            AnswerCommand = new Command(OnAnswerCommand);
            _answer = new System.Random().Next(1, 9);
        }

        void OnAnswerCommand(object parameter)
        {
            if (!int.TryParse(Input, out var input))
            {
                Message = $"{Input} → 数字を入力してください";
                return;
            }
            if (input < 1 || input > 9)
            {
                Message = $"{input} → 1-9 の数字を 1 文字入力してください";
                return;
            }
            if (input > _answer)
            {
                Message = $"{input} → 答えはもっと小さい値です";
                return;
            }
            if (input < _answer)
            {
                Message = $"{input} → 答えはもっと大きい値です";
                return;
            }
            Message = "正解！\n終了するには右上の × ボタンを押してください... ";
            AnswerCommand.SetCanExecute(false);
        }
    }
}
```
おめでとうございます！ これで数当てゲームの完成です。  
簡単なものではありますが、立派なアプリです。**C#** でアプリを作った経験者になりました。これから素晴らしい **C#** エンジニア生活を送ってください！

## 自習 - アプリの改善
今回作った数当てゲームはまだ不親切な部分があります。**C#** の学習としてぜひ自分で機能を追加してみてください。  

### 機能の例

- 正解を当てるまでにかかった回数を数えて表示する機能
- 1 ゲームごとに終了するのではなく、連続で遊べる機能
- 複数の答えを同時に当てていく機能
- 数字でなく単語を当てる機能

**あなたに、素晴らしい C# エンジニアライフを！**

<hr />

[< 前へ](./textbook05.md) |

[[ C# でアプリを作る ] へ](../../textbook/practice.md)
<hr />
