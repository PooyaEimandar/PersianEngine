   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5      f bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7{Sclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls#Telerik.Windows.Controls.QuickStart 5	      H DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PersianEditor.UserControlsPersianEditor.UserControls 5
      $   PersianEditor.Windows.ShellWin   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      6Sys&clr-namespace:System;assembly=mscorlib     5      Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5      k	 gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n
 jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b ^Telerik.Windows.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation 	 
      5      wtelerikQuickStartSclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls 	 
      5	      >UCs(clr-namespace:PersianEditor.UserControls 	 
      5
      Fd2http://schemas.microsoft.com/expression/blend/2008 	 
      5      -   5          � AllowsTransparency  .   � WindowStyle$
 None=�5       � WindowStartupLocation$ CenterScreen=�5       � WindowState$ 	Maximized=�5      ���#2d2e32 � 	Resources 5         	DockStyle& 
  Telerik.Windows.Controls.RadPane �� Property �� Value��%�   &	        �� 5   
   .�5      ��  �@ CanFloat .  6I   �� 5       �@ CanUserClose .  6M   �� 5       �@ 
CanUserPin .  6K   �� 5       �@ CanDockInDocumentHost .  6V   5      5      .��5!      � -   5        $��	RootLayer��5"   	    � RowDefinitions 5#   
   �� 5$      $
��Auto �6   6,   �� 5%      6   .+�5'   
   ,  &PersianEditor.UserControls.RibbonBarUC  -   5          Name$ ribbonBarUC��5)      $��0��5(      5*      � 5+   
   $��1��5,      .+�5-      ) 
 #Telerik.Windows.Controls.RadDocking  F  @Telerik.Windows.Controls.QuickStart.ThemeAwareBackgroundBehavior	   	IsEnabled	 . 
   DocumentHost
 50      0 
 *Telerik.Windows.Controls.RadSplitContainer  51      .@�52      + 
 %Telerik.Windows.Controls.RadPaneGroup  -   5          Name$ MainRenderDock��52   /   .@�53        $��Render Surface��54   !   #��[  55   !   .��56   "   7  1System.Windows.Forms.Integration.WindowsFormsHost  -   5          Name$ WinFormsHost��56   3   $��7q�57   %      
Background �#2d2e32   Child. 59   &      System.Windows.Forms.Panel  -   5        Render_Layer��5>   '   5?   #   5@      5A      5B      .@�5E        $	��250��5F      $	��250��5G      $	��250��5H         InitialPosition$ DockedRight=�5I      .@�5J        .@�5K           CanFloat .     CanUserClose .     
CanUserPin .     CanDockInDocumentHost .     IsPinned . )	  #Telerik.Windows.Controls.RadTabItem 	  
IsSelected .    Title$ 
Properties��5R      ��5S      )
  #PersianEditor.UserControls.HeaderUC
  5T   "    
  TextProperty$ 
Properties��5U   %   
���   �5W   #   .��5Y        $��1q�5Z   !   . 5[   "   '  !System.Windows.Forms.PropertyGrid  -   5        propertyGrid��   ViewBackColorU QSystem.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a#  System.Drawing.ColorConverter$ #2d2e32 5]   %      ViewForeColor$ White 5^   %      	LineColor$ DarkGray 5_   %      	BackColor$ #2d2e32 5`   %   5a   #   5b      5c        5d       .   .   .   .   . $ Content of Engine��5j      ��5k      
  5l   "   $ Content��5m   %   
���   �5o   #   .��5q      .  (PersianEditor.UserControls.ContentTreeUC  -   5          Name$ EngineContentTree��5q   0      contextType$ EngineContext=�5r   !      
SearchType 5t   &   ��	*.xnb��68   5v      5w        5x       .   .   .   .   . $ Content of Editor��5~      ��5      
  5�   "   $ Textures��5�   %   
���   �5�   #   .��5�      2  ,PersianEditor.UserControls.TexturesManagerUC  -	   5          Name$ TexturesManager��5�   4   5�      5�      5�      5�        5�      -
   5           Name$  BottomContainer��5�   ,   $ DockedBottom=�5�      $	��150��5�      $	��150��5�      $	��150��5�      .@�5�        .@�5�        -   5        !  Name$! 
ErrorPanel��5�      #��[  5�       .   . $ Errors��5�      ��5�      
  5�   "   $ Errors��6/   6G   .��5�      -  'PersianEditor.UserControls.NotifyListUC  -   5        "  Name$" Errors��5�   /   6A   5�        5�      #��[  5�       .   .  $ Warnings��5�      ��5�      
  5�   "   $ Warnings��6/   6I   .��5�        61   5�      5�      5�      5�      5�      5�      5�      