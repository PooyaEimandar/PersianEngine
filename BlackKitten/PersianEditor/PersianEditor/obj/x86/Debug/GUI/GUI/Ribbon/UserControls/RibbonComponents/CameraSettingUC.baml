   M S B A M L   `   `   `  ����H  DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:PersianEditor.UserControls.RibbonComponents+PersianEditor.UserControls.RibbonComponents  5      A    ;PersianEditor.UserControls.RibbonComponents.CameraSettingUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      k gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b
 ^Telerik.Windows.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
   5      Fd2http://schemas.microsoft.com/expression/blend/2008    	 
   5      QComps9clr-namespace:PersianEditor.UserControls.RibbonComponents    	 
   5        � 	Resources  5         	FontStyle �� Property �� Value��%�   &	        �� 5   
   .�5      ��  � ��$ Times New Roman)�63   6M   �� 5       � ��$ 12(�61   6>   �� 5       � ��$
 Bold"�63   6B   5      5      .��5      �� .+�5   
   - 	 'Telerik.Windows.Controls.RadGroupHeader  $��Camera Settings��6!   6=   �� 5   
   $��Translation Speed��5      $��10q�5      $��Center=�5      #��[  5      5      �� 5   
   -   5         ��Name$ MovementSpeed��5      $��Center=�5       � Maximum$ 20Q�5       � Minimum$ 1Q�5        � Value$ 10Q�5!      $
��10,0q�5"       �� TickFrequency$ 1Q�5#       �� TickPlacement$ TopLeft=�5$      
����� �5'      �� 5(   
   $��Rotation Speed��5)      $��10,20,10,10q�5*      $��Center=�5+      #��[  5,      5-      �� 5.   
   -   5        $ RotationSpeed��5/      $��Center=�50      $ 20Q�51      $ 1Q�52      $ 10Q�53      $��
10,0,10,10q�54      $ 1Q�55      $ TopLeft=�56      
����� �59      �� 5:   
   	 �� Orientation$	 
Horizontal=�5;      $��	7,5,10,10q�5<      $��Center=�5=      .+�5>      7   1PersianEditor.UserControls.RibbonComponents.XYZUC  -   5        
  Name$
 CameraPosCom��5>         UpperTextProperty$ Camera��5?         LowerTextProperty$ Position��5@      $��Center=�5A      #��[  5B      5C      (  "Telerik.Windows.Controls.RadButton  5D      -   5          Name$ GoToPositionBtn��5D       $��Go to��5E         CornerRadius$ 5��5F      $��5q�5G      $��Center=�5H      #��[  5I      5K      5L      �� 5M   
   $	 
Horizontal=�5N      $��	7,5,10,10q�5O      $��Center=�5P      .+�5Q      �� $��Assign Camera to��5R      $��Center=�5S      #��[  5T      5U        5V      -   5        $ Pick4AssigningBtn��5V       $
��Pick��5W      $ 10��5X      #��[  5Y      $��50��5Z      $��25��5[      $��Center=�5\      $��2q�5]      5_      5`      �� 5a   
   $	 
Horizontal=�5b      $��	7,5,10,10q�5c      $��Center=�5d      .+�5e      �� $��Active Camera��5f      $��Center=�5g      #��[  5h      5i      *  $Telerik.Windows.Controls.RadComboBox  5j      -   5          Name$ ActiveCamCombo��5j   "   #��[  5k      $	��150��5l      $��25��5m      $��Center=�5n      $��2q�5o      .@�5q      .  (Telerik.Windows.Controls.RadComboBoxItem   �� 
IsSelected . .��6<   
FreeCamera6H     5r      .��6*   ChaseCamera67   5s      5t      �� 5u   
   $	 
Horizontal=�5v      $��	7,5,10,10q�5w      $��Center=�5x      .+�5y        -   5        $ ResetCameraBtn��5y       $��Reset Camera��5z      $��Center=�5{      #��[  5|      $	��100��5}      $��30��5~      $ 10��5      5�      5�      5�      5�      