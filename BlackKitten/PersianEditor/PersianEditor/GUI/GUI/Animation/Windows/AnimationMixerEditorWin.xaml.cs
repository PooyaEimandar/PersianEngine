using System;
using PersianEditor.UserControls;
using Telerik.Windows.Controls;

namespace PersianEditor.Windows
{
    public partial class AnimationMixerEditorWin : BaseWin
    {
        #region Fields & Properties

        public event EventHandler<CmdEventArgs> OnCommand;
        event EventHandler<WindowClosedEventArgs> ConfirmClose;
        bool? dialogResult;

        #endregion

        #region Constructor

        public AnimationMixerEditorWin()
        {
            InitializeComponent();
            if (!this.IsInDesignMode)
            {
                Initialize();
            }
        }

        #endregion

        #region Initialize

        private void Initialize()
        {
            InitializeMenu();
            InitializeEvent();
        }

        private void InitializeMenu()
        {
            MenuUC Mixer = new MenuUC();
            int index = 0;//We want to add menu items to before close item

            Mixer.FileItemMenu.Items.Insert(index++, new RadMenuItem()
            {
                Name = "Save",
                Header = "Save",
            });
            Mixer.FileItemMenu.Items.Insert(index++, new RadMenuItem()
            {
                Name = "SelectAll",
                Header = "Select all tracks",
            });
            Mixer.FileItemMenu.Items.Insert(index++, new RadMenuItem()
            {
                Name = "Delete",
                Header = "Delete tracks",
            });

            //Bind menu click event to Mixer click event
            Mixer.OnMenuClicked += new System.EventHandler<CmdEventArgs>(this.Ctrl.OnMenuClicked);
            this.MenuPanel = Mixer;
        }
        
        private void InitializeEvent()
        {
            this.Ctrl.ForceClose += new System.EventHandler(Ctrl_ForceClose);
            this.ConfirmClose+=new EventHandler<WindowClosedEventArgs>(OnConfirmClose);
        }

        #endregion

        #region Events

        private void Ctrl_ForceClose(object sender, System.EventArgs e)
        {
            base.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (this.Ctrl.MixerContainer.Children.Count != 0)
            {
                if (this.Ctrl.mixerInfo == null)
                {
                    RadWindow.Confirm("Would you like save changes before exit?", ConfirmClose);
                    if (dialogResult == null)
                    {
                        e.Cancel = true;
                    }
                    else if (dialogResult == true)
                    {
                        e.Cancel = !this.Ctrl.CreateMixerInfo();
                    }
                }

                //if closing has not been canceled and Mixer Message is not still null , then send it
                if (!e.Cancel && this.Ctrl.mixerInfo != null)
                {
                    OnCommand(this, new CmdEventArgs(this.Ctrl.mixerInfo));
                }
            }
            base.OnClosing(e);
        }

        private void OnConfirmClose(object sender, WindowClosedEventArgs e)
        {
            dialogResult = e.DialogResult;   
        }

        #endregion
    }
}
