#pragma checksum "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7AED94ACA3D6F3658E37F169FE88A3C5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.Primitives;
using Telerik.Windows.Controls.RibbonBar;
using Telerik.Windows.Controls.TransitionEffects;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.Shapes;


namespace PersianEditor.UserControls {
    
    
    /// <summary>
    /// MenuUC
    /// </summary>
    public partial class MenuUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadMenu Menu;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadMenuItem FileItemMenu;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadMenuItem Close;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadMenuItem HelpSetting;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PersianEditor;component/gui/gui/_base%20inherit/menuuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Menu = ((Telerik.Windows.Controls.RadMenu)(target));
            
            #line 22 "..\..\..\..\..\..\GUI\GUI\_Base Inherit\MenuUC.xaml"
            this.Menu.ItemClick += new Telerik.Windows.RadRoutedEventHandler(this.OnMenuItemClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FileItemMenu = ((Telerik.Windows.Controls.RadMenuItem)(target));
            return;
            case 3:
            this.Close = ((Telerik.Windows.Controls.RadMenuItem)(target));
            return;
            case 4:
            this.HelpSetting = ((Telerik.Windows.Controls.RadMenuItem)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

