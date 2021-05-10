﻿/*
 * Copyright (c) BaziPardaz.Co Ltd. All rights reserved.
 * 
 * File Name        : ContentTreeUC.cs
 * File Description : Content tree user control
 * Generated by     : Pooya Eimandar
 * Last modified by : Pooya Eimandar on 9/23/2013
 * Comment          : 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace PersianEditor.UserControls
{
    public partial class ContentTreeUC : UserControl
    {
        #region Fields & Properties

        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(this);
            }
        }

        //Just for Spedding up...when user creating new folder there is no need for seraching and renaming it's assets and it's subfolders
        enum EditState
        {
            FromUser,
            FromEngine
        };

        static BitmapImage AssetIcon, TextureIcon, FolderIcon;
        RadTreeViewItem Nodes = new RadTreeViewItem();

        string root;
        public string Root 
        {
            get
            {
                return this.root;
            }
            set
            {
                this.root = value;
                if (!IsInDesignMode)
                {
                    Initialize();
                }
            }
        }

        List<string> searchType;
        public List<string> SearchType
        {
            get
            {
                return this.searchType;
            }
            set
            {
                this.searchType = value;
            }
        }

        public ContextType contextType
        {
            get;
            set;
        }

        Windows.TextureViewerWin Viewer;
        SynchronizationContext synchronizationContext;

        Thread worker;
        
        #endregion

        #region Constructor/Destructor

        public ContentTreeUC()
        {
            InitializeComponent();
            FolderIcon = new BitmapImage(new Uri("/PersianEditor;component/Resources/Images/Folder.png", UriKind.Relative));
            AssetIcon = new BitmapImage(new Uri("/PersianEditor;component/Resources/Images/Asset.png", UriKind.Relative));
            TextureIcon = new BitmapImage(new Uri("/PersianEditor;component/Resources/Images/Texture.png", UriKind.Relative));
            this.searchType = new List<string>();
            this.Loaded += (s, e) =>
                {
                    synchronizationContext = SynchronizationContext.Current;
                };
        }
        
        ~ContentTreeUC()
        {

        }

        #endregion

        #region Initialize

        private void Initialize()
        {
            //Start On New Thread...this is for seperating editor's operation and xna 's operation
            this.worker = new Thread(new ParameterizedThreadStart(
            delegate
            {
                if (this.Dispatcher.CheckAccess())
                {
                    InitializeUp();
                }
                else
                {
                    this.Dispatcher.Invoke(new RefreshDelegate(
                        delegate
                        {
                            InitializeUp();
                        }),
                        new object[] { });
                }
            }));
            worker.Start();
        }

        private void InitializeUp()
        {
            SwitchContextType();
            RefreshTreeView();
            if (this.treeView.Items.Count != 0)
            {
                this.treeView.SelectedItem = this.treeView.Items[0];
            }
        }

        #endregion

        #region Method

        private void SwitchContextType()
        {
            switch (this.contextType)
            {
                case ContextType.EngineContext:
                    this.TreeContextMenu.Items.Add(CreateContext("NewFolder"));
                    this.TreeContextMenu.Items.Add(CreateContext("Rename"));
                    this.TreeContextMenu.Items.Add(CreateContext("Delete"));
                    this.TreeContextMenu.Items.Add(CreateContext("Import"));
                    break;
                case ContextType.EditorContext:
                    this.TreeContextMenu.Items.Add(CreateContext("View"));

                    RadMenuItem item = CreateContext("Sort by");
                    item.Items.Add(CreateContext("Name"));
                    item.Items.Add(CreateContext("Type"));
                    this.TreeContextMenu.Items.Add(item);

                    this.TreeContextMenu.Items.Add(CreateContext("NewFolder"));
                    this.TreeContextMenu.Items.Add(CreateContext("Rename"));
                    this.TreeContextMenu.Items.Add(CreateContext("Delete"));
                    this.TreeContextMenu.Items.Add(CreateContext("Import"));
                    break;
            }
        }

        private RadMenuItem CreateContext(string Name)
        {
            var item = new RadMenuItem()
            {
                Header = Name,
            };
            item.Click += new Telerik.Windows.RadRoutedEventHandler(ContexMenuItem_Click);
            return item;
        }

        internal void ClearAllBindings(RadTreeViewItem Node)
        {
            if (Node == null)
            {
                Node = this.treeView.Items[0] as RadTreeViewItem;
            }
            foreach (RadTreeViewItem item in Node.Items)
            {
                BindingOperations.ClearBinding(item, RadTreeViewItem.TagProperty);
                ClearAllBindings(item);
            }
        }

        delegate void RefreshDelegate();
        internal void RefreshTreeView()
        {
            this.synchronizationContext.Post((p) =>
                {
                    this.treeView.Items.Clear();
                    CreatingAssets(this.root, null, EditState.FromEngine);
                }, null);
        }
        internal void RefreshTreeView(List<object> Objs)
        {
            this.synchronizationContext.Post((p) =>
            {
                this.treeView.Items.Clear();
                CreatingAssets(this.root, null, EditState.FromEngine);
                foreach (object Obj in Objs)
                {
                    BindToTag(Obj);
                }
            }, null);
        }
        
        private void CreatingAssets(string path, RadTreeViewItem Parent, EditState editState)
        {
            #region Adding Node

            string NodeName = string.Empty;
            if (editState == EditState.FromEngine)
            {
                NodeName = Path.GetFileName(path);
            }
            else
            {
                NodeName = "New Folder";

                #region Create Real folder in System

                try
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    else
                    {
                        Windows.ShellWin.Notify(this, new NotifyEventArgs(NotifyType.Error, NotifyAction.ClearThenAdd, "The destination already contains same name"));
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Windows.ShellWin.Notify(this, new NotifyEventArgs(NotifyType.Error, NotifyAction.ClearThenAdd, ex.Message));
                    return;
                }

                #endregion
            }

            var node = new RadTreeViewItem()
            {
                Header = NodeName,
                DefaultImageSrc = FolderIcon,
                Tag = ToLogicalPath(path),
            };

            if (Parent == null)
            {
                this.treeView.Items.Add(node);
            }
            else
            {
                //Expand parent then add new node
                (Parent as RadTreeViewItem).Items.Add(node);
                (Parent as RadTreeViewItem).IsExpanded = true;
            }

            #endregion

            if (editState == EditState.FromEngine)
            {
                #region Get All Folder inside This Folder

                string[] Dirs = Directory.GetDirectories(path);
                foreach (string dir in Dirs)
                {
                    CreatingAssets(dir, node, editState);
                }

                #endregion

                #region Get All Files inside This Folder

                BitmapImage AsssetImage = AssetIcon;
                foreach (string search in this.searchType)
                {
                    if (search.Contains("jpg") || search.Contains("png") || search.Contains("dds"))
                    {
                        AsssetImage = TextureIcon;
                    }

                    string[] Files = Directory.GetFiles(path, search);
                    foreach (string file in Files)
                    {
                        (node as RadTreeViewItem).Items.Add(
                            new RadTreeViewItem()
                            {
                                Header = Path.GetFileNameWithoutExtension(file),
                                DefaultImageSrc = AsssetImage,
                                Tag = ToLogicalPath(file),
                            });
                    }
                }

                #endregion
            }
        }

        private void UpdateNodesTags(RadTreeViewItem SelectedNode, string OldPath, string NewPath)
        {
            foreach (RadTreeViewItem node in SelectedNode.Items)
            {
                node.Tag = this.ToLogicalPath(this.ToPhysicalPath(node.Tag.ToString()).Replace(OldPath, NewPath));
                UpdateNodesTags(node, OldPath, NewPath);
            }
        }

        /// <summary>
        /// return logical path from content
        /// </summary>
        /// <returns></returns>
        private string ToLogicalPath(string path)
        {
            //Split it to get logical path from content
            string[] LogicalPath = path.Split(new string[] { this.root + @"\" }, StringSplitOptions.None);
            if (LogicalPath.Length == 1)
            {
                return string.Empty;
            }
            return LogicalPath[1];
        }

        private string ToPhysicalPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return this.root;
            }
            return this.root + @"\" + path;
        }

        private void DeleteProcess(RadTreeViewItem Node)
        {
            try
            {
                string _path = ToPhysicalPath(Node.Tag.ToString());

                #region First Delete Childs

                while (Node.Items.Count != 0)
                {
                    DeleteProcess(Node.Items[0] as RadTreeViewItem);
                }

                #endregion

                #region Then Delete itSelf

                if (Path.GetExtension(_path).Equals(string.Empty))
                {
                    Directory.Delete(_path);
                }
                else
                {
                    File.Delete(_path);
                }

                (Node.Parent as RadTreeViewItem).Items.Remove(Node);

                #endregion
            }
            catch (Exception ex)
            {
                Windows.ShellWin.Notify(this, new NotifyEventArgs(NotifyType.Error, NotifyAction.ClearThenAdd, ex.Message));
            }
        }

        internal void BindToTag(object Obj)
        {
            string PropertyName = "ModelPath";
            string tag = (string)Obj.GetType().GetProperty(PropertyName).GetValue(Obj, new object[] { });

            #region Search for Item

            RadTreeViewItem Founded = null;
            string TagBeforeBind = string.Empty;

            //Search from Content
            Founded = FindTag(this.treeView.Items[0] as RadTreeViewItem, tag);
 
            #endregion

            if (Founded != null)
            {
                TagBeforeBind = Founded.Tag.ToString();
                Binding binding = new Binding();
                binding.Source = Obj;
                binding.Path = new System.Windows.PropertyPath(PropertyName);
                binding.Mode = BindingMode.OneWayToSource;
                Founded.SetBinding(RadTreeViewItem.TagProperty, binding);
                Founded.Tag = TagBeforeBind;
            }
        }
        
        private RadTreeViewItem FindTag(RadTreeViewItem item, string TagToSearch)
        {
            RadTreeViewItem Founded = null;

            if (item.Tag != null && item.Tag.ToString().Equals(TagToSearch))
            {
                return item;
            }

            #region Search Nodes

            foreach (RadTreeViewItem i in item.Items)
            {
                Founded = FindTag(i, TagToSearch);
                if (Founded != null)
                {
                    return Founded;
                }
            }

            #endregion

            return Founded;
        }

        private void ImportAsset(string DestinationPath)
        {
            Windows.ShellWin parent = this.GetVisualParent<Windows.ShellWin>();
            parent.ImportProcess("Import Assets", "*.fbx;*.x)|*.fbx;*.x|All Files (*.*)|*.*", false, false, null);
        }

        private void ImportTexture(string DestinationPath)
        {
            Windows.ShellWin parent = this.GetVisualParent<Windows.ShellWin>();
            string[] Paths = parent.ImportProcess("Import Textures",
                string.Format("Texture Files ({0};{1};{2})|{0};{1};{2}|" + "All Files (*.*)|*.*", this.searchType[0], this.searchType[1], this.searchType[2]), true, true, ToPhysicalPath(DestinationPath));
            if (Paths != null)
            {
                this.RefreshTreeView();
            }
        }

        private void ShowTexture(string path)
        {
            this.Viewer = new Windows.TextureViewerWin()
            {
                Width = Persian.GDevice.DisplayMode.Width / 2,
                Height = 2 * (Persian.GDevice.DisplayMode.Height / 3),
            };
            this.Viewer.Ctrl.ImagePath = ToPhysicalPath(path);
            this.Viewer.Show();
        }

        #endregion

        #region Events

        private void Btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Image).Name.StartsWith("Expand"))
            {
                this.treeView.ExpandAll();
            }
            else if ((sender as Image).Name.StartsWith("Collapse"))
            {
                this.treeView.CollapseAll();
            }
            else
            {
                this.RefreshTreeView();
                EditorShared.RefreshBindingTags = true;
            }
        }

        private void ContentTreeView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            RadTreeViewItem OnRightClicked = e.Source as RadTreeViewItem;
            if (OnRightClicked != null)
            {
                this.treeView.SelectedItems.Clear();
                this.treeView.SelectedItem = OnRightClicked;
            }
        }

        private void ContexMenuItem_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadTreeViewItem SelectedItem = this.treeView.SelectedItem as RadTreeViewItem;
            string ItemName = (sender as RadMenuItem).Header.ToString();
            if (ItemName.StartsWith("New"))//New Folder
            {
                CreatingAssets(ToPhysicalPath(SelectedItem.Tag.ToString()) + @"\New Folder", SelectedItem, EditState.FromUser);
            }
            else if (ItemName.StartsWith("Re"))//Rename
            {
                //Rename Clicked
                if (!(ToPhysicalPath(SelectedItem.Tag.ToString()).Equals(this.root)))
                {
                    SelectedItem.BeginEdit();
                }
            }
            else if (ItemName.StartsWith("De"))//Delete
            {
                if (SelectedItem.Tag!= null && !SelectedItem.Tag.ToString().Equals(this.root))
                {
                    DeleteProcess(SelectedItem as RadTreeViewItem);
                }
            }
            else if (ItemName.StartsWith("Vi"))//View
            {
                ShowTexture(SelectedItem.Tag.ToString());
            }
            else if (ItemName.StartsWith("Im"))//Import
            {
                if (this.contextType == ContextType.EngineContext)
                {
                    ImportAsset(SelectedItem.Tag.ToString());
                }
                else
                {
                    ImportTexture(SelectedItem.Tag.ToString());
                }
            }
        }
        
        private void ContentTreeView_Edited(object sender, RadTreeViewItemEditedEventArgs e)
        {
            //Check 
            if (!e.OldValue.ToString().Equals(e.NewValue.ToString()))
            {
                string Before = e.OldValue.ToString();
                string After = e.NewValue.ToString();
                RadTreeViewItem source = e.Source as RadTreeViewItem;
                string path = ToPhysicalPath((source.Parent as RadTreeViewItem).Tag.ToString());

                string OldPath = path + @"\" + Before;
                string NewPath = path + @"\" + After;
                try
                {
                    string extension = Path.GetExtension(source.Tag.ToString());
                    if (String.IsNullOrEmpty(extension))
                    {
                        Directory.Move(OldPath, NewPath);

                        #region Change All Child's Tag

                        source.Tag = this.ToLogicalPath(NewPath);
                        UpdateNodesTags(source, OldPath, NewPath);

                        #endregion
                    }
                    else
                    {
                        OldPath += extension;
                        NewPath += extension;
                        File.Move(OldPath, NewPath);
                    }
                }
                catch (Exception ex)
                {
                    Windows.ShellWin.Notify(this, new NotifyEventArgs(NotifyType.Error, NotifyAction.ClearThenAdd, ex.Message));
                }
            }
        }

        private void ContentTreeView_PreviewDragEnded(object sender, RadTreeViewDragEndedEventArgs e)
        {
            foreach (RadTreeViewItem node in e.DraggedItems)
            {
                try
                {
                    #region Set Path

                    string TargetNodeTag = (e.TargetDropItem.Item as RadTreeViewItem).Tag.ToString();
                    string OldPath = ToPhysicalPath(node.Tag.ToString());
                    string NewPath = ToPhysicalPath(TargetNodeTag) + @"\" + node.Header;

                    #endregion

                    #region Moving from OS

                    string extension = Path.GetExtension(OldPath);
                    if (extension.Equals(string.Empty))
                    {
                        //is directory
                        Directory.Move(OldPath, NewPath);
                        UpdateNodesTags(node, OldPath, NewPath);
                    }
                    else
                    {
                        //is file
                        NewPath += extension;
                        File.Move(OldPath, NewPath);
                    }

                    node.Tag = this.ToLogicalPath(NewPath);
                    
                    #endregion
                }
                catch (Exception ex)
                {
                    Windows.ShellWin.Notify(this,  new NotifyEventArgs(NotifyType.Error, NotifyAction.ClearThenAdd, ex.Message));
                    Telerik.Windows.Controls.DragDrop.RadDragAndDropManager.CancelDrag();
                }
            }
        }

        private void TreeContextMenu_Opened(object sender, System.Windows.RoutedEventArgs e)
        {
            RadContextMenu context = sender as RadContextMenu;

            if ((this.treeView.SelectedItem as RadTreeViewItem).Tag == null) return;
            if (this.contextType == ContextType.EngineContext)
            {
                if (String.IsNullOrEmpty((this.treeView.SelectedItem as RadTreeViewItem).Tag.ToString()))
                {
                    //Root
                    (context.Items[0] as RadMenuItem).IsEnabled = true;
                    (context.Items[1] as RadMenuItem).IsEnabled = false;
                    (context.Items[2] as RadMenuItem).IsEnabled = false;
                    (context.Items[3] as RadMenuItem).IsEnabled = true;
                }
                else if (Path.GetExtension((this.treeView.SelectedItem as RadTreeViewItem).Tag.ToString()).Equals(".xnb"))
                {
                    //Files
                    (context.Items[0] as RadMenuItem).IsEnabled = false;
                    (context.Items[1] as RadMenuItem).IsEnabled = true;
                    (context.Items[2] as RadMenuItem).IsEnabled = true;
                    (context.Items[2] as RadMenuItem).IsEnabled = true;
                }
                else
                {
                    //Folders
                    (context.Items[0] as RadMenuItem).IsEnabled = true;
                    (context.Items[1] as RadMenuItem).IsEnabled = true;
                    (context.Items[2] as RadMenuItem).IsEnabled = true;
                    (context.Items[2] as RadMenuItem).IsEnabled = true;
                }
            }
            else
            {
                if (String.IsNullOrEmpty((this.treeView.SelectedItem as RadTreeViewItem).Tag.ToString()))
                {
                    //Root
                    (context.Items[0] as RadMenuItem).IsEnabled = false;//View
                    (context.Items[1] as RadMenuItem).IsEnabled = true;//Sort
                    (context.Items[2] as RadMenuItem).IsEnabled = true;//NewFolder
                    (context.Items[3] as RadMenuItem).IsEnabled = false;//Rename
                    (context.Items[4] as RadMenuItem).IsEnabled = false;//Delete
                    (context.Items[5] as RadMenuItem).IsEnabled = true;//Import
                }
                else if (!String.IsNullOrEmpty(Path.GetExtension((this.treeView.SelectedItem as RadTreeViewItem).Tag.ToString())))
                {
                    //Files
                    (context.Items[0] as RadMenuItem).IsEnabled = true;
                    (context.Items[1] as RadMenuItem).IsEnabled = false;
                    (context.Items[2] as RadMenuItem).IsEnabled = false;
                    (context.Items[3] as RadMenuItem).IsEnabled = true;
                    (context.Items[4] as RadMenuItem).IsEnabled = true;
                    (context.Items[5] as RadMenuItem).IsEnabled = false;
                }
                else
                {
                    //Folders
                    (context.Items[0] as RadMenuItem).IsEnabled = false;
                    (context.Items[1] as RadMenuItem).IsEnabled = true;
                    (context.Items[2] as RadMenuItem).IsEnabled = true;
                    (context.Items[3] as RadMenuItem).IsEnabled = true;
                    (context.Items[4] as RadMenuItem).IsEnabled = true;
                    (context.Items[5] as RadMenuItem).IsEnabled = true;
                }
            }
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            lock (this.worker)
            {
                this.worker.Abort();
            }
        }

        #endregion
    }
}