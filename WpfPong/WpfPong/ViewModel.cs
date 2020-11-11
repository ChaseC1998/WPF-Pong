//Chase Christiansen
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfPong
{
    class ViewModel : INotifyPropertyChanged
    {
        private int leftPadPosition = 180;
        private int rightPadPosition = 180;
        private int leftResult = 0;
        private int rightResult = 0;

        public int LeftPadPosition
        {
            get {return leftPadPosition;}
            set
            {
                leftPadPosition = value;
                OnPropertyChanged("LeftPadPosition");
            }
        }

        public int RightPadPosition
        {
            get {return rightPadPosition;}
            set
            {
                rightPadPosition = value;
                OnPropertyChanged("RightPadPosition");
            }
        }

        public int LeftResult
        {
            get {return leftResult;}
            set
            {
                leftResult = value;
                OnPropertyChanged("LeftResult");
            }
        }

        public int RightResult
        {
            get {return rightResult;}
            set
            {
                rightResult = value;
                OnPropertyChanged("RightResult");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
