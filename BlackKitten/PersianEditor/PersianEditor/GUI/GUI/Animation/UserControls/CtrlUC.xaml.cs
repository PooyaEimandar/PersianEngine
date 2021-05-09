using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Telerik.Windows.Controls;

namespace PersianEditor.UserControls
{
    public partial class CtrlUC : UserControl
    {
        #region Fields & Properties

        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }

        Storyboard storyBoard;
        DoubleAnimation DAnimation;
        public Brush LeftColor
        {
            get
            {
                return this.Left.Background;
            }
            set
            {
                this.Left.Background = value;
            }
        }
        public Brush RightColor
        {
            get
            {
                return this.Right.Background;
            }
            set
            {
                this.Right.Background = value;
            }
        }
        public Brush TopColor
        {
            get
            {
                return this.Top.Background;
            }
            set
            {
                this.Top.Background = value;
            }
        }
        public Brush DownColor
        {
            get
            {
                return this.Down.Background;
            }
            set
            {
                this.Down.Background = value;
            }
        }

        public event EventHandler<CmdEventArgs> OnBorderClicked;
        
        #endregion

        #region Constructor

        public CtrlUC()
        {
            InitializeComponent();
            if (!IsInDesignMode)
            {
                Initialize();
            }
        }

        #endregion

        #region Initialize

        private void Initialize()
        {
            this.storyBoard = new Storyboard();
            this.DAnimation = new DoubleAnimation(0.0d, 1.0d, new Duration(TimeSpan.FromSeconds(1)));
            Storyboard.SetTargetProperty(DAnimation, new PropertyPath(OpacityProperty));
            this.Left.Tag = Direction.Left;
            this.Right.Tag = Direction.Right;
            this.Top.Tag = Direction.Up;
            this.Down.Tag = Direction.Down;
        }

        #endregion

        #region Events

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.OnBorderClicked(sender, new CmdEventArgs((sender as Border).Tag));
            Storyboard.SetTarget(DAnimation, sender as UIElement);
            this.storyBoard.Children.Clear();
            this.storyBoard.Children.Add(DAnimation);
            this.storyBoard.Begin();
        }

        #endregion
    }
}
