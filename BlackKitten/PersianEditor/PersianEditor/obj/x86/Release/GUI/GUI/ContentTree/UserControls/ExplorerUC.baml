   M S B A M L   `   `   `  ����   PersianEditor+    %PersianEditor.UserControls.ExplorerUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Msdk=http://schemas.microsoft.com/winfx/2006/xaml/presentation/sdk     -     � 	Resources     TextBoxStyle �� 
TargetType �� Property �� Value  dataTemplate �� ItemsSource �� Orientation
 � Tag ^� ToolTip 	 Hover �� EndPoint	 �� 
StartPoint
 � Color � Offset��%]  &	        &	 T     &	 b    �� " ��.���  � ��$	 1,2q���  � ��
 �   ���  � ��$
 Bold"���  �� *Childs+.R��� -   $ 
Horizontal=� �� *
INodePtr+.+�� .+��� $��32��$��32������ *
IconPath+ �� *ToolTipDesc+�� $��Center=�$��Center=�$
��Bold"�
����������� *	IconTitle+�� $��Center=����� *Name+�� $ 0.5,1&�$	 0.5,0&�.��� $
 	#FFE4C56F��$ 0Q�� $
 	#FFE3C56F��$ 1Q�� $
 	#FFFFFBEF��$ 0.078Q�� $
 	#FFFFF3CF��$ 0.474Q�� $
 	#FFFFECB5��$
 0.53Q�� $
 	#FFFFECB5��$ 0.922Q�.��� -    �Name$ 
LayoutRoot�� � RowDefinitions �� $
��Auto ��� .+�� $��0�� � ColumnDefinitions �� �� �� $
��Auto ��� $
��Auto �.+��� -    ��Name$ 	SearchTxt��$��0��$y�Hidden=�$
��Left=�$	��2,1q�$	��110��$��25��$��Search��.���� -    ��Name$ ComboBoxFilter��$��1��$y�Hidden=�$
��Left=�$
��10,1q� �� SelectedIndex$ 0��$��90��$��25��.@��� .��
Filter All�� .��
Scene Root� -   $ AddItemsRoot��$��2��$y�Hidden=�$ 	Add Items��$��Right=�$��2q�.+��� $<��6/PersianEditor;component/Resources/Images/BaseBlue.png���� $
��None=�$<��6/PersianEditor;component/Resources/Images/AddItems.png��� -   $��3��$ Refresh��$��Right=�$��2q�.+��� $<��6/PersianEditor;component/Resources/Images/BaseBlue.png���� $
��None=�$;��5/PersianEditor;component/Resources/Images/Refresh.png��� $��1��.+�R� -    R�Name$ treeView��
������ 
�������$
��Bold"�#��[ .@�Q� $��Cameras��
�������Q� $��Meshes��
�������Q� $��	Particles��
�������Q� $��Lights��
�������Q� $��PostProcessing��
�������Q� $��	CutScenes��
�������Q� $��Environment��
��������� -	    ��Name$ ContextMenuCanvas��$	��Top=�$
��Left=�$y�	Collapsed=����#DBE0E7�� -
    ��Name$ AddItemsPanel��$y�Hidden=�.+�� $��45,-10q�$��Right=�$	��Top=�
���   ����#DBE0E7 � Points 8�  8� X$ 0Q� 8� Y$ 14Q�8� $ 14Q�$ 14Q�8� $ 7Q�$ 0Q��� $	��Top=�$��Right=�$	��0,0q�$y�Visible=����#DBE0E7
���   �$��1q� �� CornerRadius$ 5��._��� $��2q�.+��� -   $��1q�$ 2E��._��� $
��Bold"�	2x1 Event�� -   $��1q�$ KP��._��� $
��Bold"�On Key Pressed�� -   $��1q�$ KH��._��� $
��Bold"�On Key Holded�� -   $��1q�$ MC��._��� $
��Bold"�On Mouse Clicked�� -   $��1q�$	 MDC��._��� $
��Bold"�On Mouse Double Clicked�� -   $��1q�$ MD��._��� $
��Bold"�On Mouse Draging