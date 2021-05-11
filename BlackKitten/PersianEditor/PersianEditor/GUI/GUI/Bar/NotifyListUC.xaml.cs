using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using PersianEditor.Classes;

namespace PersianEditor.UserControls
{
    public partial class NotifyListUC : UserControl
    {
        #region Fields & Properties

        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }

        ObservableCollection<INotifyMessage> NotiftyList;

        #endregion

        #region Constructor/Destructor

        public NotifyListUC()
        {
            InitializeComponent();
            if (!IsInDesignMode)
            {
                this.NotiftyList = new ObservableCollection<INotifyMessage>();
                this.listBox.ItemsSource = this.NotiftyList;
            }
        }

        ~NotifyListUC()
        {

        }

        #endregion

        #region Methods

        public void Clear()
        {
            this.NotiftyList.Clear();
        }

        public void Add(string Message)
        {
            this.NotiftyList.Add(new INotifyMessage(Message));
        }

        public void ClearThenAdd(string Message)
        {
            this.NotiftyList.Clear();
            this.NotiftyList.Add(new INotifyMessage(Message));
        }

        #endregion
    }
}
