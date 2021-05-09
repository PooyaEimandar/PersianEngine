using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PersianEditor.UserControls
{
    public partial class AnimationMixerEditorUC : UserControl
    {
        #region Fields & Proeprties

        public MixerInfo mixerInfo;
        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }
        
        public event EventHandler ForceClose;

        public ItemCollection SourceItems
        {
            set
            {
                foreach (string iter in value)
                {
                    var textBlock = new TextBlock()
                    {
                        Text = iter.ToString(),
                        Width = 100.0d,
                    };
                    textBlock.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(OnMouseLeftButtonDown);
                    this.SourceCombo.Items.Add(textBlock);
                    if (this.SourceCombo.Items.Count > 0)
                    {
                        this.SourceCombo.SelectedIndex = 0;
                    }
                }
            }
        }
        
        #endregion

        #region Constructor

        public AnimationMixerEditorUC()
        {
            if (!IsInDesignMode)
            {
                InitializeComponent();
            }
        }

        #endregion

        #region Methods
        
        private void DeleteTrack()
        {
            for (int i = 0; i < this.MixerContainer.Children.Count; i++)
            {
                var border = (this.MixerContainer.Children[i] as Border);
                if (border.Tag.ToString() == "1")
                {
                    this.MixerContainer.Children.RemoveAt(i);
                    this.StatusTxt.Text = "Track(s) deleted successfully";
                    i--;
                }
            }
        }

        private void SelectAll()
        {
            foreach (Border iter in this.MixerContainer.Children)
            {
                iter.Tag = "1";
                iter.Background = this.Resources["SelectedColor"] as LinearGradientBrush;
            }
        }

        public bool CreateMixerInfo()
        {
            string Name = Check4Name();
            if (Name == null)
            {
                this.StatusTxt.Text = "Same name already existed";
                return false;
            }

            var size = this.MixerContainer.Children.Count;
            var Tracks = new List<string>(size);
            var TracksInverse = new List<bool>(size);

            for (int i = 0; i < this.MixerContainer.Children.Count; i++)
            {
                var grid = (this.MixerContainer.Children[i] as Border).Child as Grid;
                Tracks.Add((grid.Children[0] as TextBlock).Text);
                TracksInverse.Add((grid.Children[1] as CheckBox).IsChecked.Value);
            }

            //Create struct here
            this.mixerInfo = new MixerInfo(
                (byte)this.MixerTypeCombo.SelectedIndex,
                Name, 
                Tracks, 
                TracksInverse,
                new List<float>(),
                new List<long>(),
                new Dictionary<int,int>(),
                new List<long>());

            return true;
        }

        private string Check4Name()
        {
            bool SimilarName = false;
            string Name = string.IsNullOrWhiteSpace(this.mixedName.Text) ? Persian.GetUniqueName("Mixed_") : "Mixed_" + this.mixedName.Text;
            foreach (Border border in this.MixerContainer.Children)
            {
                var grid = border.Child as Grid;
                if ((grid.Children[0] as TextBlock).Text == Name)
                {
                    SimilarName = true;
                    break;
                }
            }
            if (SimilarName) return null;
            return Name;
        }
        
        public void CreateTrack(string TrackName)
        {
            if (TrackName.Contains("Mixed_"))
            {
                this.StatusTxt.Text = "This track already contains mixing data";
                return;
            }
            var btn = new Border()
            {
                Child = new Grid()
                {
                    Children = 
                    {
                        new TextBlock()
                        {
                            Text = TrackName,
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Center
                        },
                        new CheckBox()
                        {
                            Content = "Inverse",
                            Foreground = Brushes.Cyan,
                            Margin=new Thickness(0,0,50,0),
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                            IsChecked = false,
                        }
                    }
                },
                Style = this.Resources["BtnStyle"] as Style
            };
            btn.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(btn_MouseLeftButtonDown);
            this.MixerContainer.Children.Add(btn);
        }

        #endregion

        #region Events

        private void OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop((sender as TextBlock), (sender as TextBlock).Text, DragDropEffects.Copy);
        }

        private void Dest_Drop(object sender, DragEventArgs e)
        {
            var TrackName = e.Data.GetData(DataFormats.Text, true).ToString();
            CreateTrack(TrackName);
        }
                
        private void btn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var btn = (sender as Border);
            if (btn.Tag.ToString() == "0")
            {
                //Is Default
                btn.Tag = "1";
                btn.Background = this.Resources["SelectedColor"] as LinearGradientBrush;
            }
            else
            {
                btn.Tag = "0";
                btn.Background = this.Resources["DefaultColor"] as LinearGradientBrush;
            }
        }

        private void Dest_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text, true))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public void OnMenuClicked(object sender, CmdEventArgs e)
        {
            var Command = e.Data.ToString();
            if(Command.StartsWith("Save"))
            {
                CreateMixerInfo();
            }
            else if(Command.StartsWith("SelectAll"))
            {
                SelectAll();
            }
            else if (Command.StartsWith("Delete"))
            {
                DeleteTrack();
            }
            else
            {
                //Absolutely Command is close
                ForceClose(this, new EventArgs());
            }
        }

        #endregion
    }
}
