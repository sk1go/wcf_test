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
        }

        private string ipBlock_1;
        public string IPBlock_1
        {
            get { return ipBlock_1; }
            set
            {
                ipBlock_1 = value;

                bool valid = true;
                foreach (char c in ipBlock_1)
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

        private string ipBlock_2;
        public string IPBlock_2
        {
            get { return ipBlock_2; }
            set
            {
                ipBlock_2 = value;

                bool valid = true;
                foreach (char c in ipBlock_2)
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

        private string ipBlock_3;
        public string IPBlock_3
        {
            get { return ipBlock_3; }
            set
            {
                ipBlock_3 = value;

                bool valid = true;
                foreach (char c in ipBlock_3)
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

        private string ipBlock_4;
        public string IPBlock_4
        {
            get { return ipBlock_4; }
            set
            {
                ipBlock_4 = value;

                bool valid = true;
                foreach (char c in ipBlock_4)
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
