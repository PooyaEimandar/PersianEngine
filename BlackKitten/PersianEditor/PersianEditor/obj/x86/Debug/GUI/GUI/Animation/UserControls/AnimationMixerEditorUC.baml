   M S B A M L   `   `   `  ����f  bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7{Sclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls#Telerik.Windows.Controls.QuickStart  5       PersianEditor7   1PersianEditor.UserControls.AnimationMixerEditorUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      p lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b ^Telerik.Windows.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ktelerik1http://schemas.telerik.com/2008/xaml/presentation   	   
   5      wtelerikQuickStartSclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=Telerik.Windows.Controls   	   
   5      Fd2http://schemas.microsoft.com/expression/blend/2008   	   
   5        � 	Resources  5         DefaultColor � Color � Offset  SelectedColor  BtnStyle �� Property �� Value��%  &	        &	      &	 �     7   �� 5   
   .��5      � $ Silver��6   $ 0Q�6*   67   � 5      $ Black��6   $	 0.7Q�6)   68   5      �� 5   
   .��5      � $ Yellow��6   $ 0Q�6*   67   � 5      $ Black��6   $	 0.7Q�6)   68   5      �� 5   
   .�5      ��  � ��$	 5,3q�6/   6>   �� 5       � ��$ 20��6/   6=   �� 5       �@��CornerRadius$ 5��64   6A   �� 5       � ��8   6[   �� 5       �@�Tag$ 0��6,   68   5      5      .��5      �  � RowDefinitions 5    
   �� 5!      6   �� 5"      $
��Auto �6   6,   .+�5$   
   � $��0��5%       � ColumnDefinitions 5&      �� 5'      6%   �� 5(      $
��Auto �6#   62   .+�5*      *  $Telerik.Windows.Controls.RadPanelBar  -   5          Name$ 
MixerPanel��5*   "   $��0��5+       J� 	AllowDrop . {�. 	   
ExpandMode$	 Single=�50      F   @Telerik.Windows.Controls.QuickStart.ThemeAwareBackgroundBehavior
   	IsEnabled
 . .@�52      .  (Telerik.Windows.Controls.RadPanelBarItem  .  (Telerik.Windows.Controls.RadTreeViewItem   
IsExpanded . $��Mixer��54      $��Center=�55      
����� �.@�57      �� -   5         ��Name$ MixerContainer��57   !   6;   58      59      � 5:      $��1��5;      .+�5<      �� $��2q�5=      .+�5>      �� $��2,15,2,0q�5?      
�������$��Center=�5A      $��Type of Mixer��5B      5C      * 
 $Telerik.Windows.Controls.RadComboBox  5D      -   5          Name$ MixerTypeCombo��5D   *   $	��100��5E      $��30��5F      .@�5G      . 
 (Telerik.Windows.Controls.RadComboBoxItem   �� 
IsSelected . .��6D   
Parallel6N     5H       .  .��6E   
Sequential6Q     5I       .  .��6E   Sequential with loop at end6b   5J      �� 5K      $��2,15,2,0q�5L      
�������$��Center=�5N      $��Source Clips��5O      5P        5Q      -   5        $ SourceCombo��5Q   *   $	��100��5R      $��30��5S      5T      �� 5U      $��2,30,2,0q�5V      
�������$��Center=�5X      $��Mixed Clip Name��5Y      5Z      �� 5[      -   5         ��Name$ 	mixedName��5[      $	��100��5\      $��30��5]      .��6%   5^      5_      5`      5a      �� 5b   
   -   5         ��Name$ 	StatusTxt��5b      $��1��5c      $��2q�5d      
�������$��Ready��5f      5g      5h      5i      