   M S B A M L   `   `   `  ���� H  DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PersianEditor.UserControlsPersianEditor.UserControls  0    *PersianEditor.UserControls.GamePadSyncerUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     p lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f	 bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
  <UCs(clr-namespace:PersianEditor.UserControls    	 
  Dd2http://schemas.microsoft.com/expression/blend/2008    	 
    � 	Resources     	Animation g� AutoReverse �� 
TargetName �� TargetProperty W� From	 W� To g� Duration  ShowKeyAnimation  ShowXBoxAnimation��%L  &	        &	 �     &	 a    ��  .  .��W� $ Lighter��$ Radius�$ 15X�$	 150X�$ 0:0:1D�W� $ BorderLighting��$ Opacity�$	 0.7X�$ 0X�$ 0:0:1D���  .  .��W� $ KeyboardPic��$ Opacity�$ 0X�$ 1X�$ 0:0:1D�W� $ XBoxPic��$ Opacity�$ 1X�$ 0X�$ 0:0:1D�W� $ 
XBoxLights��$ Opacity�$ 1X�$ 0X�$ 0:0:1D���  .  .��W� $ KeyboardPic��$ Opacity�$ 1X�$ 0X�$ 0:0:1D�W� $ XBoxPic��$ Opacity�$ 0X�$ 1X�$ 0:0:1D�W� $ 
XBoxLights��$ Opacity�$ 0X�$ 1X�$ 0:0:1D�.���  � RowDefinitions �� �� .+�� $��0��.+��� -    ��Name$ KeyboardPic��$<��6/PersianEditor;component/Resources/Images/Keyboard.jpg��$��Uniform=�$��1q��� -   	 ��Name$	 BorderLighting��
����� �$��30��$��30��
 �� CornerRadius$
 5��$��Center=�$
��Left=�
����� �$��
110,-4,0,0q� J� Opacity$ 0Q� J� Effect -  'System.Windows.Media.Effects.BlurEffect  -   Lighter��   Radius$ 15Q��� -   $ XBoxPic��$8��2/PersianEditor;component/Resources/Images/XBox.png��$��Uniform=�$��1q�$ 0Q�� -    �Name$ 
XBoxLights��$ 0Q�.+�'   !PersianEditor.UserControls.CtrlUC  -     Name$ LeftCtrl��'   !PersianEditor.UserControls.CtrlUC   OnBorderClicked$ LeftCtrlOnClicked��$��45��$��45��$��-210,-115,0,0q�
 � Tag$ 0��  -   $ 	LeftStick��$ LeftStickOnClicked��   	LeftColor
 ��  �   
RightColor
 ��  �   TopColor
 ��  �   	DownColor
 ��  �$��50��$��50��$��-110,12,0,0q�$ 1��  -   $ 	RightCtrl��$ RightCtrlOnClicked��
 �����
 �����
 �����
 �����$��45��$��45��$��
102,15,0,0q�$ 2��  -	   $ Buttons��$ ButtonsOnClicked��
 ��  �
 �  ��
 � ���
 � � �$��65��$��65��$��215,-110,0,0q�$ 3��� $��1�� �� $	��115 ��� $	��115 � � ColumnDefinitions �� $��50 ��� $
��Auto ��� $
��Auto ��� $	��100 ��� $
��Auto ��� $
��Auto ��� $��50 �.+��� $��0��$��1��$��Select Controller��
�������$
��20,5q�$��Center=�$��Center=�* 
 $Telerik.Windows.Controls.RadComboBox  -
     Name$ SelectCtrlCombo��$��0��$��2��$	��150��$��30��$��5q�$��Center=�$��Center=�$��Center=�.@�. 
 (Telerik.Windows.Controls.RadComboBoxItem   �� 
IsSelected . .��
Keybaord  .��XBox�� $��0��$��4��$��Select Animation��
�������$
��20,5q�$��Center=�$��Center=�  -   $ AnimationsCombo��$��0��$��5��$	��150��$��30��$��5q�$��Center=�$��Center=�$��Center=��� $��1��$��1��$��	Active by��
�������$
��20,5q�$��Center=�$��Center=�( 	 "Telerik.Windows.Controls.RadButton  -     Name$ 
Controller��$��1��$��2��$��Center=�$��Center=�$	��150��$��30��
����� � � ToolTip$
 tool��