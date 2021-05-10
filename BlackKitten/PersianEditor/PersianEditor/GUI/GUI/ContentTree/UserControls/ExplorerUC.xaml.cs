﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ExplorerUC.cs
 * File Description : Explorer user control
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 9/23/2013
 * Comment          : 
 */

using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PersianEditor.UserControls
{
    public partial class ExplorerUC : UserControl
    {
        #region Fields & Properties

        bool DesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }
        SynchronizationContext UIContext { get; set; }
        public event EventHandler OnSelectingObject;
        public PersianCore.CoreFrameWork coreFrameWork { get; set; }

        Node SelectedNodeTree { get; set; }
        StackPanel FolderMenu, NodeMenu;
        int GroupLayerIndex;
        Brush WhiteBrush, LastBrush;

        #endregion

        #region Constructor

        public ExplorerUC()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.UIContext = SynchronizationContext.Current;
            }
        }

        #endregion
        
        #region Events

        private void OnTreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            if (OnSelectingObject != null)
            {
                if (sender is TreeViewItem)
                {
                    var tag = (sender as TreeViewItem).Tag;
                    if (tag != null)
                    {
                        OnSelectingObject(tag, null);       
                    }
                }
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.WhiteBrush = Brushes.White;
            CreateContextMenuPanels();
            OnComboBoxSelectionChanged(sender, null);
        }

        #region ItemsPanel's Events

        private void OnItemsClicked(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void OnItemsMouseEnter(object sender, MouseEventArgs e)
        {
            var ctrl = sender as Border;
            LastBrush = ctrl.Background;
            ctrl.Background = this.Resources["Hover"] as Brush;
        }

        private void OnItemsMouseLeave(object sender, MouseEventArgs e)
        {
            var ctrl = sender as Border;
            ctrl.Background = LastBrush;
        }

        #endregion

        private void OnRefreshing(object sender, MouseButtonEventArgs e)
        {
            OnComboBoxSelectionChanged(sender, null);
        }

        private void OnAddingEvent(object sender, MouseButtonEventArgs e)
        {
            if (this.AddItemsPanel.Visibility == Visibility.Collapsed)
            {
                this.AddItemsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.AddItemsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void OnComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxFilter != null)
            {
                //Close any menu that was opened
               // OnDataTemplateMouseLeftClicked(sender, null);
                if (this.ComboBoxFilter.SelectedIndex == 0)
                {
                    UpdateTree();
                }
            }
        }

        private void OnDataTemplateMouseRightClicked(object sender, MouseButtonEventArgs e)
        {
            var parent = (e.OriginalSource as FrameworkElement).Parent;
            if (parent is Grid)
            {
                this.SelectedNodeTree = ((parent as Grid).Parent as StackPanel).Tag as Node;
            }
            else
            {
                //is StackPanel
                this.SelectedNodeTree = (parent as StackPanel).Tag as Node;
            }

            CreateContextMenuCanvas(e.GetPosition(this.treeView as UIElement));
        }

        private void OnDataTemplateMouseLeftClicked(object sender, MouseButtonEventArgs e)
        {
            //if (e != null && e.ClickCount == 2)
            //{
            //    OnTreeViewItemDoubleClicked(this.SelectedNodeTree, e);
            //}
            //if (this.ContextMenuCanvas.Visibility == Visibility.Visible)
            //{
            //    this.ContextMenuCanvas.Visibility = Visibility.Collapsed;
            //}
            //if (MenuHasJustOpened)
            //{
            //    MenuHasJustOpened = false;
            //}
            //else
            //{
            //    if (this.AddItemsPanel.Visibility == Visibility.Visible)
            //    {
            //        this.AddItemsPanel.Visibility = Visibility.Collapsed;
            //    }
            //}
        }

        #region Context Menu Item's Events

        private void OnContextMenuItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if is reserved node so leave it
            //if (this.SelectedNodeTree.GetType().GetCustomAttributes(typeof(ReservedOrder), true).Length > 0)
            //{
            //    return;
            //}

            //var text = (sender as TextBlock).Text;
            //if (this.SelectedNodeTree is FakeFilterNode)
            //{
            //    if (text.StartsWith("H"))//Hide
            //    {
            //        HideHierarchy(this.SelectedNodeTree);
            //    }
            //    else if (text.StartsWith("F"))//Freeze
            //    {
            //        FreezeHierarchy(this.SelectedNodeTree);
            //    }
            //    else if (text.StartsWith("D"))//Delete
            //    {
            //        DeleteHierarchy(this.SelectedNodeTree);
            //    }
            //}
            //else
            //{
            //    if (text.StartsWith("H"))
            //    {
            //        (this.SelectedNodeTree as Client.Libraries.Core.Framework.Physics.PhysicNode).Visibility = !(this.SelectedNodeTree as Client.Libraries.Core.Framework.Physics.PhysicNode).Visibility;
            //    }
            //    else if (text.StartsWith("F"))
            //    {
            //        (this.SelectedNodeTree as Client.Libraries.Core.Framework.Physics.PhysicNode).IsFreeze = !(this.SelectedNodeTree as Client.Libraries.Core.Framework.Physics.PhysicNode).IsFreeze;
            //    }
            //    else
            //    {
            //        //Delete
            //        this.SelectedNodeTree.Remove();
            //        //Request for refreshing
            //        OnComboBoxSelectionChanged(sender, null);
            //    }
            //}
        }

        private void OnContextMenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            //var ctrl = (sender as TextBlock).Parent as Border;
            //ctrl.Background = LastBrush;
        }

        private void OnContextMenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //var ctrl = (sender as TextBlock).Parent as Border;
            //LastBrush = ctrl.Background;
            //ctrl.Background = this.Resources["Hover"] as Brush;
        }

        #endregion

        #endregion

        #region Methods

        private void UpdateTree()
        {
            if (this.treeView == null) return;

            //Update cameras
            var item = this.treeView.Items[0] as TreeViewItem;
            item.Items.Clear();
            var freeItem = new TreeViewItem()
            {
                Foreground = this.WhiteBrush,
                Header = "Free Camera",
                Tag = Persian.Camera.freeCamera
            };
            freeItem.Selected += OnTreeViewItemSelected;
            item.Items.Add(freeItem);

            var chaseItem = new TreeViewItem()
            {
                Foreground = this.WhiteBrush,
                Header = "Chase Camera",
                Tag = Persian.Camera.freeCamera
            };
            chaseItem.Selected += OnTreeViewItemSelected;
            item.Items.Add(chaseItem);
            

            //Update Meshes
            item = this.treeView.Items[1] as TreeViewItem;
            item.Items.Clear();
            foreach (var iter in this.coreFrameWork.ObjectsManager.Meshes)
            {
                var tItem = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = iter.ID,
                    Tag = iter
                };
                tItem.Selected += OnTreeViewItemSelected;
                item.Items.Add(tItem);
            }

            //Update Particles
            item = this.treeView.Items[2] as TreeViewItem;
            item.Items.Clear();
            foreach (var iter in this.coreFrameWork.ObjectsManager.ParticlesManager.ParticleSystems)
            {
                var tItem = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = iter.ID,
                    Tag = iter
                };
                tItem.Selected += OnTreeViewItemSelected;
                item.Items.Add(tItem);
            }

            //Update Lights
            item = this.treeView.Items[3] as TreeViewItem;
            item.Items.Clear();
            foreach (var iter in this.coreFrameWork.RenderManager.Lights)
            {
                var tItem = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = iter.Name,
                    Tag = iter
                };
                tItem.Selected += OnTreeViewItemSelected;
                item.Items.Add(tItem);
            }

            //Update PostProcess
            item = this.treeView.Items[4] as TreeViewItem;
            item.Items.Clear();

            #region Add Post Process

            if (this.coreFrameWork.Post.GodRay != null)
            {
                var godRay = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = "GodRay",
                    Tag = this.coreFrameWork.Post.GodRay
                };
                godRay.Selected += OnTreeViewItemSelected;
                item.Items.Add(godRay);
            }
            if (this.coreFrameWork.Post.Toon != null)
            {
                var toon = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = "Toon",
                    Tag = this.coreFrameWork.Post.Toon
                };
                toon.Selected += OnTreeViewItemSelected;
                item.Items.Add(toon);
            }
            if (this.coreFrameWork.Post.Bloom != null)
            {
                var bloom = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = "Bloom",
                    Tag = this.coreFrameWork.Post.Bloom
                };
                bloom.Selected += OnTreeViewItemSelected;
                item.Items.Add(bloom);
            }
            if (this.coreFrameWork.Post.GaussianBlur != null)
            {
                var blur = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = "Blur",
                    Tag = this.coreFrameWork.Post.GaussianBlur
                };
                blur.Selected += OnTreeViewItemSelected;
                item.Items.Add(blur);
            }
            if (this.coreFrameWork.Post.Glow != null)
            {
                var glow = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = "Glow",
                    Tag = this.coreFrameWork.Post.Glow
                };
                glow.Selected += OnTreeViewItemSelected;
                item.Items.Add(glow);
            }

            #endregion

            //Update CutScenes
            item = this.treeView.Items[5] as TreeViewItem;
            item.Items.Clear();
            foreach (var iter in Persian.CutScenes)
            {
                var ti = new TreeViewItem()
                {
                    Foreground = this.WhiteBrush,
                    Header = iter.Name,
                    Tag = iter
                };
                ti.Selected += OnTreeViewItemSelected;
                item.Items.Add(ti);
            }


            //Update Sky
            item = this.treeView.Items[6] as TreeViewItem;
            item.Items.Clear();
            var sky = new TreeViewItem()
            {
                Foreground = this.WhiteBrush,
                Header = "Sky",
                Tag = this.coreFrameWork.ObjectsManager.EnvManager.Sky
            };
            sky.Selected += OnTreeViewItemSelected;

            var rain = new TreeViewItem()
            {
                Foreground = this.WhiteBrush,
                Header = "Rains",
                Tag = this.coreFrameWork.ObjectsManager.EnvManager.Sky
            };
            rain.Selected += OnTreeViewItemSelected;

            item.Items.Add(sky);
            item.Items.Add(rain);

        }

        private void CreateContextMenuPanels()
        {
            #region Create menu for folder

            var Margin = new Thickness(1);
            this.FolderMenu = new StackPanel()
            {
                Width = 130,
                Height = 85
            };
            var style = Resources["TextBoxStyle"] as Style;
            this.FolderMenu.Children.Add(new Border() { Margin = Margin, Child = CreateMenuItem("Hide/UnHide all", style) });
            this.FolderMenu.Children.Add(new Border() { Margin = Margin, Child = CreateMenuItem("Freeze/DeFreeze all", style) });
            this.FolderMenu.Children.Add(new Border() { Margin = Margin, Child = CreateMenuItem("Delete all", style) });

            #endregion

            #region Create menu for node

            this.NodeMenu = new StackPanel()
            {
                Width = 115,
                Height = 63
            };
            this.NodeMenu.Children.Add(new Border() { Margin = Margin, Child = CreateMenuItem("Hide/UnHide", style) });
            this.NodeMenu.Children.Add(new Border() { Margin = Margin, Child = CreateMenuItem("Freeze/DeFreeze", style) });
            this.NodeMenu.Children.Add(new Border() { Margin = Margin, Child = CreateMenuItem("Delete", style) });

            #endregion
        }

        private void GroupLayerIndexTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (this.GroupLayerIndexTxt.Text != string.Empty)
            //{
            //    var name = (this.SelectedNodeTree as FakeFilterNode).Name;
            //    if (name != "Layers" && name != "Events")
            //    {
            //        IndexHierarchy(this.SelectedNodeTree);
            //        (this.SelectedNodeTree as FakeFilterNode).Name = "Layer" + this.GroupLayerIndex;
            //    }
            //}
        }

        private void LayerIndexTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as TextBox).Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                this.GroupLayerIndex = Convert.ToInt32(text);
            }
        }

        /// <summary>
        /// Check for disable characters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerIndexTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    var name = (this.SelectedNodeTree as FakeFilterNode).Name;
            //    if (name != "Layers" && name != "Events")
            //    {
            //        IndexHierarchy(this.SelectedNodeTree);
            //        (this.SelectedNodeTree as FakeFilterNode).Name = "Layer" + this.GroupLayerIndex;
            //    }
            //}
            //else if (!((e.PlatformKeyCode >= 48 && e.PlatformKeyCode <= 57) || (e.PlatformKeyCode >= 96 && e.PlatformKeyCode <= 105)))
            //{
            //    e.Handled = true;
            //}
        }

        private TextBlock CreateMenuItem(string text, Style style)
        {
            var textBoxMenuItem = new TextBlock()
            {
                Style = style,
                Text = text
            };
            textBoxMenuItem.MouseEnter += OnContextMenuItem_MouseEnter;
            textBoxMenuItem.MouseLeave += OnContextMenuItem_MouseLeave;
            textBoxMenuItem.MouseLeftButtonDown += OnContextMenuItem_MouseLeftButtonDown;
            return textBoxMenuItem;
        }

        private void CreateContextMenuCanvas(Point pos)
        {
            //Create context menu for filter mode
            if (this.SelectedNodeTree == null)
            {
                //is folder
                if (this.ContextMenuCanvas.Children.Count == 0 || this.ContextMenuCanvas.Children[0] != this.FolderMenu)
                {
                    this.ContextMenuCanvas.Children.Clear();
                    this.ContextMenuCanvas.Children.Add(this.FolderMenu);
                    this.ContextMenuCanvas.Width = this.FolderMenu.Width;
                    this.ContextMenuCanvas.Height = this.FolderMenu.Height;
                }
            }
            else
            {
                //is Node
                if (this.ContextMenuCanvas.Children.Count == 0 || this.ContextMenuCanvas.Children[0] != this.NodeMenu)
                {
                    this.ContextMenuCanvas.Children.Clear();
                    this.ContextMenuCanvas.Children.Add(this.NodeMenu);
                    this.ContextMenuCanvas.Width = this.NodeMenu.Width;
                    this.ContextMenuCanvas.Height = this.NodeMenu.Height;
                }
            }
            this.ContextMenuCanvas.Margin = new Thickness(pos.X, pos.Y, 0, 0);
            this.ContextMenuCanvas.Visibility = Visibility.Visible;
        }

        private void HideHierarchy(Node Node)
        {
            //Node.GetChilds().ForEach(delegate(Node node)
            //{
            //    if (node.GetChilds().Count != 0)
            //    {
            //        HideHierarchy(node);
            //    }
            //    //Do freezing
            //    if (!(node is FakeFilterNode))
            //    {
            //        (node as Client.Libraries.Core.Framework.Physics.PhysicNode).Visibility = !(node as Client.Libraries.Core.Framework.Physics.PhysicNode).Visibility;
            //    }
            //});
        }

        private void IndexHierarchy(Node Node)
        {
            //Node.GetChilds().ForEach(delegate(Node node)
            //{
            //    if (node.GetChilds().Count != 0)
            //    {
            //        IndexHierarchy(node);
            //    }
            //    //Do freezing
            //    if (!(node is FakeFilterNode))
            //    {
            //        node.LayerIndex = this.GroupLayerIndex;
            //    }
            //});
        }

        private void DeleteHierarchy(Node Node)
        {
            //Node.GetChilds().ForEach(delegate(Node node)
            //{
            //    if (node.GetChilds().Count != 0)
            //    {
            //        DeleteHierarchy(node);
            //    }
            //    //Do freezing
            //    if (!(node is FakeFilterNode))
            //    {
            //        node.Remove();
            //    }
            //});
        }

        #endregion
    }
}