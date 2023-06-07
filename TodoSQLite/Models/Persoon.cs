using SQLite;
using System.ComponentModel;

namespace TodoSQLite.Models
{
    public class Persoon : INotifyPropertyChanged
    {
        private int _id;
        private string _voorNaam;
        private string _achterNaam;
        private string _afbeelding;
        private string _telefoonNummer;
        private string _geslacht;
        private bool _isMan;
        private bool _isVrouw;
        private bool _isOnbekend;

        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        public string VoorNaam
        {
            get { return _voorNaam; }
            set
            {
                if (_voorNaam != value)
                {
                    _voorNaam = value;
                    OnPropertyChanged(nameof(VoorNaam));
                }
            }
        }

        public string AchterNaam
        {
            get { return _achterNaam; }
            set
            {
                if (_achterNaam != value)
                {
                    _achterNaam = value;
                    OnPropertyChanged(nameof(AchterNaam));
                }
            }
        }

        public string Afbeelding
        {
            get { return _afbeelding; }
            set
            {
                if (_afbeelding != value)
                {
                    _afbeelding = value;
                    OnPropertyChanged(nameof(Afbeelding));
                }
            }
        }

        public string TelefoonNummer
        {
            get { return _telefoonNummer; }
            set
            {
                if (_telefoonNummer != value)
                {
                    _telefoonNummer = value;
                    OnPropertyChanged(nameof(TelefoonNummer));
                }
            }
        }

        public string Geslacht
        {
            get { return _geslacht; }
            set
            {
                if (_geslacht != value)
                {
                    _geslacht = value;
                    OnPropertyChanged(nameof(Geslacht));
                }
            }
        }

        public bool IsMan
        {
            get { return _isMan; }
            set
            {
                if (_isMan != value)
                {
                    _isMan = value;
                    OnPropertyChanged(nameof(IsMan));
                }
            }
        }

        public bool IsVrouw
        {
            get { return _isVrouw; }
            set
            {
                if (_isVrouw != value)
                {
                    _isVrouw = value;
                    OnPropertyChanged(nameof(IsVrouw));
                }
            }
        }

        public bool IsOnbekend
        {
            get { return _isOnbekend; }
            set
            {
                if (_isOnbekend != value)
                {
                    _isOnbekend = value;
                    OnPropertyChanged(nameof(IsOnbekend));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
