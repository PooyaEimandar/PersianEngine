   M S B A M L   `   `   `  ���� f  bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7{Sclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls#Telerik.Windows.Controls.QuickStart   PersianEditor7   1PersianEditor.UserControls.AnimationMixerEditorUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     p lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation   	   
  utelerikQuickStartSclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls   	   
  Dd2http://schemas.microsoft.com/expression/blend/2008   	   
    � 	Resources     DefaultColor � Color � Offset  SelectedColor  BtnStyle �� Property �� Value��%  &	        &	 =     &	 z     7   �� .��� $ Silver��$ 0Q�� $ Black��$	 0.7Q��� .��� $ Yellow��$ 0Q�� $ Black��$	 0.7Q��� .���  � ��$	 5,3q���  � ��$ 20����  �@��CornerRadius$ 5����  � ��8   ��  �@�Tag$ 0��.���  � RowDefinitions �� �� $
��Auto �.+�� $��0�� � ColumnDefinitions �� �� $
��Auto �.+�*  $Telerik.Windows.Controls.RadPanelBar  -     Name$ 
MixerPanel��$��0�� J� 	AllowDrop . {�. 	   
ExpandMode$	 Single=�F   @Telerik.Windows.Controls.QuickStart.ThemeAwareBackgroundBehavior
   	IsEnabled
 . .@�.  (Telerik.Windows.Controls.RadPanelBarItem  .  (Telerik.Windows.Controls.RadTreeViewItem   
IsExpanded . $��Mixer��$��Center=�
����� �.@��� -    ��Name$ MixerContainer��� $��1��.+��� $��2q�.+��� $��2,15,2,0q�
�������$��Center=�$��Type of Mixer��* 
 $Telerik.Windows.Controls.RadComboBox  -     Name$ MixerTypeCombo��$	��100��$��30��.@�. 
 (Telerik.Windows.Controls.RadComboBoxItem   �� 
IsSelected . .��
Parallel   .  .��
Sequential   .  .��Sequential with loop at end�� $��2,15,2,0q�
�������$��Center=�$��Source Clips��  -   $ SourceCombo��$	��100��$��30���� $��2,30,2,0q�
�������$��Center=�$��Mixed Clip Name���� -    ��Name$ 	mixedName��$	��100��$��30��.���� -    ��Name$ 	StatusTxt��$��1��$��2q�
�������$��Ready��