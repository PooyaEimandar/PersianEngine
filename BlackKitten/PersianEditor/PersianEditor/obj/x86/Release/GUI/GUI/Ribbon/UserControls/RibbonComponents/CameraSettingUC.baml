   M S B A M L   `   `   `  ���� H  DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:PersianEditor.UserControls.RibbonComponents+PersianEditor.UserControls.RibbonComponents  A    ;PersianEditor.UserControls.RibbonComponents.CameraSettingUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     p lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f	 bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
  Dd2http://schemas.microsoft.com/expression/blend/2008    	 
  OComps9clr-namespace:PersianEditor.UserControls.RibbonComponents    	 
    � 	Resources     	FontStyle �� Property �� Value��%c   &	        �� .���  � ��$ Times New Roman)���  � ��$ 12(���  � ��$
 Bold"�.���� .+�-  'Telerik.Windows.Controls.RadGroupHeader  $��Camera Settings���� $��Translation Speed��$��10q�$��Center=�#��[  �� -    ��Name$ MovementSpeed��$��Center=� � Maximum$ 20Q� � Minimum$ 1Q� � Value$ 10Q�$
��10,0q� �� TickFrequency$ 1Q� �� TickPlacement$ TopLeft=�
����� ��� $��Rotation Speed��$��10,20,10,10q�$��Center=�#��[  �� -   $ RotationSpeed��$��Center=�$ 20Q�$ 1Q�$ 10Q�$��
10,0,10,10q�$ 1Q�$ TopLeft=�
����� ��� 	 �� Orientation$	 
Horizontal=�$��	7,5,10,10q�$��Center=�.+�7   1PersianEditor.UserControls.RibbonComponents.XYZUC  -   
  Name$
 CameraPosCom��   UpperTextProperty$ Camera��   LowerTextProperty$ Position��$��Center=�#��[  ( 	 "Telerik.Windows.Controls.RadButton  -     Name$ GoToPositionBtn��$��Go to��   CornerRadius$ 5��$��5q�$��Center=�#��[  �� $	 
Horizontal=�$��	7,5,10,10q�$��Center=�.+��� $��Assign Camera to��$��Center=�#��[    -   $ Pick4AssigningBtn��$
��Pick��$ 10��#��[  $��50��$��25��$��Center=�$��2q��� $	 
Horizontal=�$��	7,5,10,10q�$��Center=�.+��� $��Active Camera��$��Center=�#��[  * 
 $Telerik.Windows.Controls.RadComboBox  -     Name$ ActiveCamCombo��#��[  $	��150��$��25��$��Center=�$��2q�.@�. 
 (Telerik.Windows.Controls.RadComboBoxItem   �� 
IsSelected . .��
FreeCamera  .��ChaseCamera�� $	 
Horizontal=�$��	7,5,10,10q�$��Center=�.+�  -   $ ResetCameraBtn��$��Reset Camera��$��Center=�#��[  $	��100��$��30��$ 10��