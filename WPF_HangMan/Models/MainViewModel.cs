using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPF_HangMan.ViewModels;

namespace WPF_HangMan.Models
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<char> _pushedButtons;
        private char _pushedbutton;
        private string _secretWord;
        public string _guessingWord;
        private int _lifesCount;

        public ParametrizedRelayCommand<string> ButtonPushed { get; set; }

        public MainViewModel()
        {
            _pushedButtons = new ObservableCollection<char>();
            _pushedButtons.Add('1');
            _pushedButtons.Add('#');
            _secretWord = "Heslo";
            _guessingWord = new string('-', _secretWord.Length);
            _lifesCount = 6;

            ButtonPushed = new ParametrizedRelayCommand<string>(
                (value) =>
                {
                    PushedButton = value;
                    ButtonPushed.RaiseCanExecureChanged();
                },
                (parameter) => { return !_pushedButtons.Contains(parameter[0]); }
                );
        }

        public ObservableCollection<char> PushedButtons
        {
            get { return _pushedButtons; }
            //set { return _pushedButtons.Add(); }
        }
        public string PushedButton
        {
            //get { return _pushedbutton; }
            set
            {
                _pushedbutton = value[0];
                NotifyPropertyChanged("PushedButton");
                _pushedButtons.Add(_pushedbutton);
                PlayTurn(_pushedbutton);
            }
        }

        public string GuessingWord
        {
            get { return _guessingWord; }
            set { _guessingWord = value; }
        }

        public void PlayTurn(char value)
        {
            var indexes = AllIndexesOf(_secretWord, value).ToArray();
            if (indexes.Length != -1)
            {
                var guessingWord = _guessingWord.ToCharArray();
                foreach (var index in indexes)
                {

                    guessingWord[index] = value;
                }
                _guessingWord = string.Join("", guessingWord);
            }            
            NotifyPropertyChanged("GuessingWord");

        }

        public IEnumerable<int> AllIndexesOf(string str, char searchstring)
        {
            str = str.ToLower();
            searchstring = Char.ToLower(searchstring);
            List<int> indexes = new List<int> { };
            int index;
            index = str.IndexOf(searchstring);
            while (index != -1)
            {
                indexes.Add(index);
                index = str.IndexOf(searchstring, index + 1);
            }
            indexes.Remove(-1);
            return indexes;
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    int top = 50;
        //    int left = 100;

        //    for (int i = 'A'; i <= 'Z'; ++i)
        //    {
        //        var b = new System.Windows.Button();
        //        b.Text = System.Convert.ToChar(i).ToString();
        //        b.Name = "btn" + b.Text;
        //        b.Left = left;
        //        b.Top = top;
        //        left += b.Width + 2;
        //        b.Click += Any_Click;
        //        this.Controls.Add(b);
        //    } // Next i 

        //} // End Sub Form1_Load
    }
}
