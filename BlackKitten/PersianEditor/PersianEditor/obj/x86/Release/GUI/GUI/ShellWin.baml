   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms f bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7{Sclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls#Telerik.Windows.Controls.QuickStart H DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PersianEditor.UserControlsPersianEditor.UserControls $   PersianEditor.Windows.ShellWin   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6Sys&clr-namespace:System;assembly=mscorlib     Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     p	 lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n
 jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation 	 
     utelerikQuickStartSclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls 	 
     <UCs(clr-namespace:PersianEditor.UserControls 	 
     Dd2http://schemas.microsoft.com/expression/blend/2008 	 
     -     � AllowsTransparency  .   � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=����#2d2e32 � 	Resources    	DockStyle& 
  Telerik.Windows.Controls.RadPane �� Property �� Value��%�   &	        �� .���  �@ CanFloat .  ��  �@ CanUserClose .  ��  �@ 
CanUserPin .  ��  �@ CanDockInDocumentHost .  .��� -   $��	RootLayer�� � RowDefinitions �� $
��Auto ��� .+�,  &PersianEditor.UserControls.RibbonBarUC  -     Name$ ribbonBarUC��$��0��� $��1��.+�) 
 #Telerik.Windows.Controls.RadDocking  F  @Telerik.Windows.Controls.QuickStart.ThemeAwareBackgroundBehavior	   	IsEnabled	 . 
   DocumentHost
 0 
 *Telerik.Windows.Controls.RadSplitContainer  .@�+ 
 %Telerik.Windows.Controls.RadPaneGroup  -     Name$ MainRenderDock��.@�  $��Render Surface��#��[  .��7  1System.Windows.Forms.Integration.WindowsFormsHost  -     Name$ WinFormsHost��$��7q�   
Background �#2d2e32   Child.    System.Windows.Forms.Panel  -   Render_Layer��.@�  $	��250��$	��250��$	��250��   InitialPosition$ DockedRight=�.@�  .@�     CanFloat .     CanUserClose .     
CanUserPin .     CanDockInDocumentHost .     IsPinned . )	  #Telerik.Windows.Controls.RadTabItem 	  
IsSelected .    Title$ 
Properties����)
  #PersianEditor.UserControls.HeaderUC
   
  TextProperty$ 
Properties��
���   �.��  $��1q�. '  !System.Windows.Forms.PropertyGrid  -   propertyGrid��   ViewBackColorU QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a#  System.Drawing.ColorConverter$ #2d2e32    ViewForeColor$ White    	LineColor$ DarkGray    	BackColor$ #2d2e32    .   .   .   .   . $ Content of Engine����
  $ Content��
���   �.��.  (PersianEditor.UserControls.ContentTreeUC  -     Name$ EngineContentTree��   contextType$ EngineContext=�   
SearchType ��	*.xnb��   .   .   .   .   . $ Content of Editor����
  $ Textures��
���   �.��2  ,PersianEditor.UserControls.TexturesManagerUC  -	     Name$ TexturesManager��  -
      Name$  BottomContainer��$ DockedBottom=�$	��150��$	��150��$	��150��.@�  .@�  -   !  Name$! 
ErrorPanel��#��[   .   . $ Errors����
  $ Errors��.��-  'PersianEditor.UserControls.NotifyListUC  -   "  Name$" Errors��  #��[   .   .  $ Warnings����
  $ Warnings��.��  