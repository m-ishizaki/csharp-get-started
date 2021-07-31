# 進歩したコンソールアプリを作ってみよう

## クラスを別のファイルに書く

先ほど作った新しいファイル **Game.cs** にクラスのプログラムコードを移動しましょう。  
移動するのは ```Game``` クラスのコードです。  

**変更前**
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
        Game _game1 = new Game(1);
        Game _game2 = new Game(2);
        string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainWindowViewModel()
        {
            AnswerCommand = new Command(OnAnswerCommand);
        }

        void OnAnswerCommand(object parameter)
        {
            if (!int.TryParse(Input, out var input))
            {
                Message = "{Input} → 数字を入力してください";
                return;
            }
            if (input < 1 || input > 9)
            {
                Message = "{Input} → 1-9 の数字を 1 文字入力してください";
                return;
            }
            Message = $"{_game1.Proceed(input)}\n{_game2.Proceed(input)}";
            if (!_game1.Cleared || !_game2.Cleared)
            {
                return;
            }
            Message += "\nゲームクリア！\n終了するには右上の × ボタンを押してください... ";
            AnswerCommand.SetCanExecute(false);
        }
    }

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
}
```

**変更後 MainWindow.xaml.cs**
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
        Game _game1 = new Game(1);
        Game _game2 = new Game(2);
        string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public MainWindowViewModel()
        {
            AnswerCommand = new Command(OnAnswerCommand);
        }

        void OnAnswerCommand(object parameter)
        {
            if (!int.TryParse(Input, out var input))
            {
                Message = "{Input} → 数字を入力してください";
                return;
            }
            if (input < 1 || input > 9)
            {
                Message = "{Input} → 1-9 の数字を 1 文字入力してください";
                return;
            }
            Message = $"{_game1.Proceed(input)}\n{_game2.Proceed(input)}";
            if (!_game1.Cleared || !_game2.Cleared)
            {
                return;
            }
            Message += "\nゲームクリア！\n終了するには右上の × ボタンを押してください... ";
            AnswerCommand.SetCanExecute(false);
        }
    }
}
```

**変更後 Game.cs**
```cs
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
```

<hr />

[< 前へ](./textbook_advanced06.md) | [次へ >](./textbook_advanced08.md)  

[[ C# でアプリを作る ] へ](../../textbook/practice.md)