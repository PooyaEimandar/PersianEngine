using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;

namespace PersianEditor.UserControls
{
    public partial class GamePadSyncerUC : UserControl
    {
        #region Fields & Properties

        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }
        
        bool isLoaded, isCombo;
        Thickness pos;
        Stopwatch stopWatch;
        Dictionary<Key, Thickness> KeyPosition = new Dictionary<Key, Thickness>()
        {
            {Key.Escape , new Thickness(20, -162, 0, 0)},
            {Key.Oem3 , new Thickness(70, -162, 0, 0)},
            {Key.D1 , new Thickness(110, -162, 0, 0)},
            {Key.D2 , new Thickness(155, -162, 0, 0)},
            {Key.D3 , new Thickness(195, -162, 0, 0)},
            {Key.D4 , new Thickness(235, -162, 0, 0)},
            {Key.D5 , new Thickness(275, -162, 0, 0)},
            {Key.D6 , new Thickness(320, -162, 0, 0)},
            {Key.D7 , new Thickness(355, -162, 0, 0)},
            {Key.D8 , new Thickness(400, -162, 0, 0)},
            {Key.D9 , new Thickness(440, -162, 0, 0)},
            {Key.D0 , new Thickness(480, -162, 0, 0)},
            {Key.OemMinus , new Thickness(520, -162, 0, 0)},
            {Key.OemPlus  , new Thickness(560, -162, 0, 0)},
            {Key.Back  , new Thickness(620, -162, 0, 0)},
            {Key.Home  , new Thickness(680, -162, 0, 0)},
            {Key.PageUp  , new Thickness(750, -162, 0, 0)},

            
            {Key.Tab , new Thickness(33, -82, 0, 0)},
            {Key.Q , new Thickness(93, -82, 0, 0)},
            {Key.W , new Thickness(133, -82, 0, 0)},
            {Key.E , new Thickness(173, -82, 0, 0)},
            {Key.R , new Thickness(213, -82, 0, 0)},
            {Key.T , new Thickness(255, -82, 0, 0)},
            {Key.Y , new Thickness(295, -82, 0, 0)},
            {Key.U , new Thickness(340, -82, 0, 0)},
            {Key.I , new Thickness(380, -82, 0, 0)},
            {Key.O , new Thickness(420, -82, 0, 0)},
            {Key.P , new Thickness(460, -82, 0, 0)},
            {Key.Oem4 , new Thickness(505, -82, 0, 0)},
            {Key.Oem6 , new Thickness(545, -82, 0, 0)},
            {Key.Oem5 , new Thickness(585, -82, 0, 0)},
            {Key.Delete , new Thickness(625, -82, 0, 0)},
            {Key.End , new Thickness(680, -82, 0, 0)},
            {Key.PageDown , new Thickness(750, -82, 0, 0)},

            {Key.A , new Thickness(110, -4, 0, 0)},
            {Key.S , new Thickness(150, -4, 0, 0)},
            {Key.D , new Thickness(195, -4, 0, 0)},
            {Key.F , new Thickness(235, -4, 0, 0)},
            {Key.G , new Thickness(275, -4, 0, 0)},
            {Key.H , new Thickness(320, -4, 0, 0)},
            {Key.J , new Thickness(355, -4, 0, 0)},
            {Key.K , new Thickness(400, -4, 0, 0)},
            {Key.L , new Thickness(440, -4, 0, 0)},
            {Key.Oem1 , new Thickness(480, -4, 0, 0)},
            {Key.Oem7 , new Thickness(520, -4, 0, 0)},
            {Key.Enter , new Thickness(600, -4, 0, 0)},
            {Key.Insert , new Thickness(680, -4, 0, 0)},
            {Key.Pause , new Thickness(750, -4, 0, 0)},

            {Key.LeftShift , new Thickness(50, 75, 0, 0)},
            {Key.Z , new Thickness(130, 75, 0, 0)},
            {Key.X , new Thickness(175, 75, 0, 0)},
            {Key.C , new Thickness(215, 75, 0, 0)},
            {Key.V , new Thickness(255, 75, 0, 0)},
            {Key.B , new Thickness(295, 75, 0, 0)},
            {Key.N , new Thickness(335, 75, 0, 0)},
            {Key.M , new Thickness(380, 75, 0, 0)},
            {Key.OemComma , new Thickness(420, 75, 0, 0)},
            {Key.OemPeriod , new Thickness(460, 75, 0, 0)},
            {Key.Oem2 , new Thickness(500, 75, 0, 0)},
            {Key.Up , new Thickness(545, 75, 0, 0)},
            {Key.RightShift , new Thickness(605, 75, 0, 0)},

            {Key.LeftCtrl , new Thickness(20, 145, 0, 0)},
            {Key.Space , new Thickness(235, 145, 0, 0)},
            {Key.RightCtrl , new Thickness(455, 145, 0, 0)},
            {Key.Left , new Thickness(503, 145, 0, 0)},
            {Key.Down , new Thickness(543, 145, 0, 0)},
            {Key.Right , new Thickness(583, 145, 0, 0)},
            
        };

        #endregion

        #region Constructor

        public GamePadSyncerUC()
        {
            if (!IsInDesignMode)
            {
                InitializeComponent();
                Initialize();
            }
        }

        #endregion

        #region Initialize

        private void Initialize()
        {
            this.pos = new Thickness();
            this.isLoaded = true;
            this.stopWatch = new Stopwatch();
        }

        #endregion

        #region Methods

        private void GetMargin(Key key)
        {
            if (!this.KeyPosition.TryGetValue(key, out pos))
            {
                pos.Left = Double.MaxValue;
            }
        }

        #endregion

        #region Events

        public void OnKeyDownPressed(object sender, KeyEventArgs e)
        {
            if (this.stopWatch.Elapsed < TimeSpan.FromSeconds(1.0d))
            {
                isCombo = true;
            }
            else
            {
                isCombo = false;
            }
            this.stopWatch.Restart();
            if (this.SelectCtrlCombo.SelectedIndex == 0)
            {
                GetMargin(e.Key);
                if (pos.Left != Double.MaxValue)
                {
                    if (this.isCombo)
                    {
                        string Plus;
                        if (this.Controller.Content == null || this.Controller.Content.ToString() == string.Empty)
                        {
                            //No need plus
                            Plus = string.Empty;
                        }
                        else
                        {
                            Plus = "+";
                        }
                        this.Controller.Content = this.Controller.Content + Plus + e.Key.ToString();
                    }
                    else
                    {
                        this.Controller.Content = e.Key.ToString();
                    }
                    ApplyAnimation();
                }
            }
        }

        private void SelectCtrlCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.isLoaded)
            {
                if ((sender as RadComboBox).SelectedIndex == 0)
                {
                    Storyboard KeyAnimation = this.Resources["ShowKeyAnimation"] as Storyboard;
                    KeyAnimation.Stop();
                    KeyAnimation.Begin();
                }
                else
                {
                    Storyboard KeyAnimation = this.Resources["ShowXBoxAnimation"] as Storyboard;
                    KeyAnimation.Stop();
                    KeyAnimation.Begin();
                }
            }
        }

        private void Controller_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectCtrlCombo.SelectedIndex == 0)
            {
                GetMargin(ENUMS.StringToEnum<Key>(this.Controller.Content.ToString()));
                if (pos.Left != Double.MaxValue)
                {
                    ApplyAnimation();
                }
            }
            else
            {

            }
        }

        private void ApplyAnimation()
        {
            this.BorderLighting.Margin = pos;
            Storyboard animation = (this.Resources["Animation"] as Storyboard);
            animation.Stop();
            animation.Begin();
        }

        private void LeftCtrlOnClicked(object sender, CmdEventArgs e)
        {
            switch ((Direction)e.Data)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
            }
        }

        private void LeftStickOnClicked(object sender, CmdEventArgs e)
        {
            switch ((Direction)e.Data)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
            }
        }

        private void RightCtrlOnClicked(object sender, CmdEventArgs e)
        {
            switch ((Direction)e.Data)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
            }
        }

        private void ButtonsOnClicked(object sender, CmdEventArgs e)
        {
            switch ((Direction)e.Data)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
            }
        }


        private void Controller_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            Controller.ToolTip = String.Format("Active by {0}", Controller.Content);
        }

        #endregion
    }
}
