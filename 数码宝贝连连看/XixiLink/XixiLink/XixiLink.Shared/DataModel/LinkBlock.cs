using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace XixiLink.DataModel
{
    public class LinkBlock : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private bool _isAlive;
        private int _imgType;
        private string _tag;

        public string Tag
        {
            get { return this.X.ToString() + this.Y.ToString(); }
            set
            {
                _tag = value;//this.X.ToString()+this.Y.ToString();
            FirePropertyChanged("Tag");
            }
        }

        public int ImgType
        {
            get { return _imgType; }
            set
            {
                _imgType = value;
                FirePropertyChanged("ImgType");
            }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;
                FirePropertyChanged("IsAlive");
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                FirePropertyChanged("Y");
            }
        }

        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                FirePropertyChanged("X");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void FirePropertyChanged(string prop)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(prop));
            }

        }
    }
}
