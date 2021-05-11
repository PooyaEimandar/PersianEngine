using System.Windows.Input;

namespace PersianEditor.Windows
{
    public partial class GamePadSyncerWin : BaseWin
    {
        #region Constructor

        public GamePadSyncerWin()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void OnKeyDownPressed(object sender, KeyEventArgs e)
        {
            this.Ctrl.OnKeyDownPressed(sender, e);
        }

        #endregion
    }
}
