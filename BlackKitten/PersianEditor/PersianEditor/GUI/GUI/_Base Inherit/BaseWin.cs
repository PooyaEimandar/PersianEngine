﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : BaseWin.cs
 * File Description : Base window
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 9/23/2013
 * Comment          : 
 */

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace PersianEditor.Windows
{
    /// <summary>
    /// Base window of all editor's windows
    /// </summary>
    public partial class BaseWin : Window
    {
        #region Fields & Properties

        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }

        Grid _RootGrid;
        public UIElement RootGrid
        {
            get
            {
                return this._RootGrid.Children[0];
            }
            set
            {
                this._RootGrid.Children.Clear();
                this._RootGrid.Children.Add(value);
            }
        }

        StackPanel _MenuPanel;
        public UIElement MenuPanel
        {
            get
            {
                return this._MenuPanel.Children[0];
            }
            set
            {
                this._MenuPanel.Children.Clear();
                this._MenuPanel.Children.Add(value);
            }
        }

        TextBlock title;
        public string WindowTitle
        {
            get
            {
                if (title != null)
                {
                    return this.title.Text;
                }
                return string.Empty;
            }
            set
            {
                if (title != null)
                {
                    this.title.Text = value;
                }
            }
        }
        
        #endregion

        #region Constructor

        public BaseWin()
        {
            InitializeFields();
            InitializeCtrls();
            InitializeEvents();
        }

        #endregion

        #region Initialize

        private void InitializeFields()
        {
            this.AllowsTransparency = true;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.BorderBrush = new SolidColorBrush(Colors.White);
            this.BorderThickness = new Thickness(1);
            this.Background = new LinearGradientBrush()
            {
                Opacity = 0.7d,
                GradientStops = new GradientStopCollection()
                {
                    new GradientStop(Color.FromRgb(45, 46, 50), 0.0d),
                    new GradientStop(Colors.Black, 1.0d),
                }
            };
        }
                
        private void InitializeCtrls()
        {
            var BaseBtnStyle = new Style()
            {
                Setters =
                {
                    new Setter(MarginProperty, new Thickness(5.0d)),
                    new Setter(WidthProperty, 40.0d),
                    new Setter(HeightProperty, 20.0d),
                    new Setter(ForegroundProperty, new SolidColorBrush(){ Color= Colors.Red}),
                    new Setter(FontWeightProperty, FontWeights.Bold),
                    new Setter(BorderBrushProperty, new SolidColorBrush(){ Color=Colors.Cyan}),
                    new Setter(BorderThicknessProperty, new Thickness(1.0d)),
                    new Setter(Telerik.Windows.Controls.RadButton.CornerRadiusProperty, new CornerRadius(10.0d)),
                }
            };
            _RootGrid = new Grid()
            {
                Name = "RootGrid",
                Margin = new Thickness(2),
            };
            _MenuPanel = new StackPanel()
            {
                Orientation =  Orientation.Horizontal,
            };
            var MinimizeBtn = new RadButton()
            {
                Content = "-",
                ToolTip = "Minimize",
                Style = BaseBtnStyle,
            };
            MinimizeBtn.Click += new RoutedEventHandler(Minimize_Click);
            var ExitBtn = new RadButton()
            {
                Content = "X",
                ToolTip="Close",                             
                Style = BaseBtnStyle,
            };
            ExitBtn.Click += new RoutedEventHandler(Base_Exit);

            this.title = new TextBlock()
            {
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                Text = "Window",
                Foreground = new SolidColorBrush(Colors.White),
            };
            var stackPanel = new StackPanel()
            {
                Children = 
                {
                    new Grid()
                    {
                        Children = 
                        {
                            //Buttons
                            new StackPanel()
                            {
                                Orientation = Orientation.Horizontal,
                                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                                Children = 
                                {
                                    MinimizeBtn,
                                    ExitBtn
                                }
                            },
                            //Middle Title
                            title,
                            //Left Menu
                            new StackPanel()
                            {
                                Orientation = Orientation.Horizontal,
                                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                                Children = 
                                {
                                    _MenuPanel,
                                }
                            },
                        },
                    },
                    _RootGrid,
                }
            };

            this.AddChild(stackPanel);
        }

        private void InitializeEvents()
        {
            this.Loaded += new RoutedEventHandler(Base_Loaded);
            this.Unloaded += new RoutedEventHandler(Base_Unloaded);
            this.Activated += new EventHandler(Base_Activated);
            this.Deactivated += new EventHandler(Base_Deactivated);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Base_MouseLeftButtonDown);
        }
                
        #endregion

        #region Events

        protected virtual void Base_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void Base_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void Base_Activated(object sender, EventArgs e)
        {
            //Deactive ShellWin and focus on this window
            PersianEditor.Windows.ShellWin.isBusy = true;
            this.WindowState = System.Windows.WindowState.Normal;
        }

        protected virtual void Base_Deactivated(object sender, EventArgs e)
        {
            //Focus on ShellWin and close this window
            PersianEditor.Windows.ShellWin.isBusy = false;
            if (this.WindowState != System.Windows.WindowState.Minimized)
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }

        protected virtual void Base_Exit(object sender, RoutedEventArgs e)
        {
            PersianEditor.Windows.ShellWin.isBusy = false;
            this.Close();
        }

        protected virtual void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        protected virtual void Base_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Focus on this Window
            PersianEditor.Windows.ShellWin.isBusy = true;
            this.DragMove();
        }

        #endregion
    }
}
