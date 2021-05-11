using System.ComponentModel;

namespace PersianEditor.Classes
{
    public class INotifyMessage : INotifyPropertyChanged
    {
        #region Fields

        string message;
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                NotifyPropertyChanged("Message");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public INotifyMessage()
            : this(string.Empty)
        {
        }

        public INotifyMessage(string Message)
        {
            this.message = Message;
            this.PropertyChanged += new PropertyChangedEventHandler(OnPropertyChanged);
        }
        
        #endregion

        #region Events

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return this.message.ToString();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        #endregion
    }
}