using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace PersianEditor.UserControls
{
    public partial class MenuUC : UserControl
    {
        #region Fields & Properties

        public event EventHandler<CmdEventArgs> OnMenuClicked;
        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }

        #endregion

        #region Constructor

        public MenuUC()
        {
            InitializeComponent();
            if (!this.IsInDesignMode)
            {
            }
        }

        #endregion

        #region Events

        private void OnMenuItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string Name = e.OriginalSource.GetType().GetProperty("Name").GetValue(e.OriginalSource, null).ToString();
            OnMenuClicked(this, new CmdEventArgs(Name));
        }

        #endregion
    }
}
