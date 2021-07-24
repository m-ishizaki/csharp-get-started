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
