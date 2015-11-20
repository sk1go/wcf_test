using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Client
{
    public class ConnectionInfo : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void SetErrors(string propertyName, List<string> propertyErrors)
        { 
            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public ConnectionInfo()
        { 
            IPBlock_1 = "127"; 
            IPBlock_2 = "0"; 
            IPBlock_3 = "0"; 
            IPBlock_4 = "1";
            Port = "12345";
        }

        private string _ipBlock_1;
        public string IPBlock_1
        {
            get { return _ipBlock_1; }
            set
            {
                _ipBlock_1 = value;

                bool valid = true;
                foreach (char c in _ipBlock_1)
                {
                    if (!Char.IsDigit(c))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    List<string> errors = new List<string>();
                    errors.Add("IP адресс может состоять только из цифр.");
                    SetErrors("IPBlock_1", errors);
                }
                else
                    ClearErrors("IPBlock_1");

                OnPropertyChanged(new PropertyChangedEventArgs("IPBlock_1"));
            }
        }

        private string _ipBlock_2;
        public string IPBlock_2
        {
            get { return _ipBlock_2; }
            set
            {
                _ipBlock_2 = value;

                bool valid = true;
                foreach (char c in _ipBlock_2)
                {
                    if (!Char.IsDigit(c))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    List<string> errors = new List<string>();
                    errors.Add("IP адресс может состоять только из цифр.");
                    SetErrors("IPBlock_2", errors);
                }
                else
                    ClearErrors("IPBlock_2");

                OnPropertyChanged(new PropertyChangedEventArgs("IPBlock_2"));
            }
        }

        private string _ipBlock_3;
        public string IPBlock_3
        {
            get { return _ipBlock_3; }
            set
            {
                _ipBlock_3 = value;

                bool valid = true;
                foreach (char c in _ipBlock_3)
                {
                    if (!Char.IsDigit(c))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    List<string> errors = new List<string>();
                    errors.Add("IP адресс может состоять только из цифр.");
                    SetErrors("IPBlock_3", errors);
                }
                else
                    ClearErrors("IPBlock_1");

                OnPropertyChanged(new PropertyChangedEventArgs("IPBlock_3"));
            }
        }

        private string _ipBlock_4;
        public string IPBlock_4
        {
            get { return _ipBlock_4; }
            set
            {
                _ipBlock_4 = value;

                bool valid = true;
                foreach (char c in _ipBlock_4)
                {
                    if (!Char.IsDigit(c))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    List<string> errors = new List<string>();
                    errors.Add("IP адресс может состоять только из цифр.");
                    SetErrors("IPBlock_4", errors);
                }
                else
                    ClearErrors("IPBlock_1");

                OnPropertyChanged(new PropertyChangedEventArgs("IPBlock_4"));
            }
        }

        private string _port;
        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;

                bool valid = true;
                foreach (char c in _port)
                {
                    if (!Char.IsDigit(c))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid)
                {
                    List<string> errors = new List<string>();
                    errors.Add("Порт может состоять только из цифр.");
                    SetErrors("Port", errors);
                }
                else
                    ClearErrors("Port");

                OnPropertyChanged(new PropertyChangedEventArgs("Port"));
            }
        }

        public bool HasErrors
        {
            get { return (errors.Count > 0); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (errors.Values);
            }
            else 
            {
                if (errors.ContainsKey(propertyName))
                    return errors[propertyName];
                else
                    return null;
            }
        }
    }
}
