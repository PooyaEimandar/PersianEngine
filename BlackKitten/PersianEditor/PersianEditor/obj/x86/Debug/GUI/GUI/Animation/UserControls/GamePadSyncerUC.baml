   M S B A M L   `   `   `  ����H  DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PersianEditor.UserControlsPersianEditor.UserControls  5      0    *PersianEditor.UserControls.GamePadSyncerUC   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      k gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b
 ^Telerik.Windows.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
   5      >UCs(clr-namespace:PersianEditor.UserControls    	 
   5      Fd2http://schemas.microsoft.com/expression/blend/2008    	 
   5        � 	Resources  5         	Animation g� AutoReverse �� 
TargetName �� TargetProperty W� From	 W� To g� Duration  ShowKeyAnimation  ShowXBoxAnimation��%z  &	        &	 $    &	 �    �� 5   
    .  .��5      W� $ Lighter��5      $ Radius�5      $ 15X�5      $	 150X�5      $ 0:0:1D�5      5      W� 5      $ BorderLighting��5      $ Opacity�5      $	 0.7X�5      $ 0X�5      $ 0:0:1D�5      5      5      �� 5   
    .  .��5!      W� $ KeyboardPic��5"      $ Opacity�5#      $ 0X�5$      $ 1X�5%      $ 0:0:1D�5&      5'      W� 5(      $ XBoxPic��5)      $ Opacity�5*      $ 1X�5+      $ 0X�5,      $ 0:0:1D�5-      5.      W� 5/      $ 
XBoxLights��50      $ Opacity�51      $ 1X�52      $ 0X�53      $ 0:0:1D�54      55      56      �� 57   
    .  .��59      W� $ KeyboardPic��5:      $ Opacity�5;      $ 1X�5<      $ 0X�5=      $ 0:0:1D�5>      5?      W� 5@      $ XBoxPic��5A      $ Opacity�5B      $ 0X�5C      $ 1X�5D      $ 0:0:1D�5E      5F      W� 5G      $ 
XBoxLights��5H      $ Opacity�5I      $ 0X�5J      $ 1X�5K      $ 0:0:1D�5L      5M      5N      5O      .��5P      �  � RowDefinitions 5Q   
   �� 5R      6   �� 5S      6   .+�5U   
   � $��0��5V      .+�5W      �� -   5         ��Name$ KeyboardPic��5W      $<��6/PersianEditor;component/Resources/Images/Keyboard.jpg��5X      $��Uniform=�5Y      $��1q�5Z      5[      �� 5\      -   5        	 ��Name$	 BorderLighting��5]      
����� �$��30��5_      $��30��5`      
 �� CornerRadius$
 5��5a      $��Center=�5b      $
��Left=�5c      
����� �$��
110,-4,0,0q�5e       J� Opacity$ 0Q�5f       J� Effect 5g      -  'System.Windows.Media.Effects.BlurEffect  5h      -   5        Lighter��   Radius$ 15Q�5h   2   6@   5j      �� 5k      -   5        $ XBoxPic��5k      $8��2/PersianEditor;component/Resources/Images/XBox.png��5l      $��Uniform=�5m      $��1q�5n      $ 0Q�5o      5p      � 5q      -   5         �Name$ 
XBoxLights��5q      $ 0Q�5r      .+�5s      '   !PersianEditor.UserControls.CtrlUC  -   5          Name$ LeftCtrl��5s      '   !PersianEditor.UserControls.CtrlUC   OnBorderClicked$ LeftCtrlOnClicked��5x      $��45��5t      $��45��5u      $��-210,-115,0,0q�5v      
 � Tag$ 0��5w      5y        5z      -   5        $ 	LeftStick��5z      $ LeftStickOnClicked��5�         	LeftColor
 ��  �   
RightColor
 ��  �   TopColor
 ��  �   	DownColor
 ��  �$��50��5      $��50��5�      $��-110,12,0,0q�5�      $ 1��5�      5�        5�      -   5        $ 	RightCtrl��5�      $ RightCtrlOnClicked��5�      
 �����
 �����
 �����
 �����$��45��5�      $��45��5�      $��
102,15,0,0q�5�      $ 2��5�      5�        5�      -	   5        $ Buttons��5�      $ ButtonsOnClicked��5�      
 ��  �
 �  ��
 � ���
 � � �$��65��5�      $��65��5�      $��215,-110,0,0q�5�      $ 3��5�      5�      5�      5�      � 5�   
   $��1��5�       5�      �� 5�      $	��115 �6    6/   �� 5�      $	��115 �6    6/    � ColumnDefinitions 5�      �� 5�      $��50 �6#   60   �� 5�      $
��Auto �6#   62   �� 5�      $
��Auto �6#   62   �� 5�      $	��100 �6#   61   �� 5�      $
��Auto �6#   62   �� 5�      $
��Auto �6#   62   �� 5�      $��50 �6#   60   .+�5�      �� $��0��5�      $��1��5�      $��Select Controller��5�      
�������$
��20,5q�5�      $��Center=�5�      $��Center=�5�      5�      *  $Telerik.Windows.Controls.RadComboBox  5�      -
   5          Name$ SelectCtrlCombo��5�   "   $��0��5�      $��2��5�      $	��150��5�      $��30��5�      $��5q�5�      $��Center=�5�      $��Center=�5�      $��Center=�5�      .@�5�      .  (Telerik.Windows.Controls.RadComboBoxItem   �� 
IsSelected . .��6<   
Keybaord6F     5�      .��6*   XBox60   5�      �� 5�      $��0��5�      $��4��5�      $��Select Animation��5�      
�������$
��20,5q�5�      $��Center=�5�      $��Center=�5�      5�        5�      -   5        $ AnimationsCombo��5�   "   $��0��5�      $��5��5�      $	��150��5�      $��30��5�      $��5q�5�      $��Center=�5�      $��Center=�5�      $��Center=�5�      5�      �� 5�      $��1��5�      $��1��5�      $��	Active by��5�      
�������$
��20,5q�5�      $��Center=�5�      $��Center=�5�      5�      (  "Telerik.Windows.Controls.RadButton  5�      -   5          Name$ 
Controller��5�       $��1��5�      $��2��5�      $��Center=�5�      $��Center=�5�      $	��150��5�      $��30��5�      
����� � � ToolTip$
 tool��5�      5�      5�      5�      5�      