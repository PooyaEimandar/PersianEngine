   M S B A M L   `   `   `  ����  PersianEditor&     PersianEditor.Windows.LoadingWin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � Title$  WELLCOME��5      $
��Auto��5      $
��Auto��6   $y�Visible=�5	       � WindowStyle$
 None=�5
       � AllowsTransparency .  � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterScreen=�5      ��5      
� 5   
   .��5   
   �  � Color$ White��6    � Offset$
 0.03Q�6%   65   � 5   
   $ 	LightBlue��6   $
 0.07Q�6)   69   � 5   
   $ #082A4E��6   $
 0.57Q�6'   67   � 5   
   $ Transparent��6   $ 1Q�6+   68   5       � 	Resources 5         
SampleText  OpeningAnim g� 	BeginTime	 �� 
TargetName
 �� TargetProperty O� KeyTime O� Value g� Duration R� KeyTime  System.Windows.Visibility   	Collapsed R� Value��%�	  &	        &	      �� 5   
   6   �� 5   
   $ 
00:00:00.7X�5      .��5!      U� $ 00:00:00X�5"      $	 shadow��5#      $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5$      .\�5&      �� $ 00:00:00��5'      $ 2Q�5(      �� 5*      $ 00:00:00.4000000��5+      $ 1Q�5,      5.      U� 50      $ 00:00:00X�6,   $	 
xamFeature��6A   $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6d   .\�51      �� $ 00:00:00��6'   $ 2Q�6:   �� 52      $ 00:00:00.4000000��6'   $ 1Q�6B   53      U� 54      $ 00:00:00X�6,   $	 shadow��6A   $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6`   .\�55      �� $ 00:00:00��6'   $ 2Q�6:   �� 56      $ 00:00:00.4000000��6'   $ 1Q�6B   57      U� 58      $ 00:00:00X�6,   $	 
xamFeature��6A   $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6d   .\�59      �� $ 00:00:00��6'   $ 2Q�6:   �� 5:      $ 00:00:00.4000000��6'   $ 1Q�6B   5;      U� 5<      $ 00:00:00X�6,   $	 shadow��6A   $
 (UIElement.Opacity)�6`   .\�5=      �� $ 00:00:00��6'   $ 0Q�6:   �� 5>      $ 00:00:00.4000000��6'   $ 1Q�6B   �� 5?      $ 00:00:00.7000000��6'   $ 0Q�6B   5@      U� 5A      $ 00:00:00X�6,   $	 
xamFeature��6A   $
 (UIElement.Opacity)�6d   .\�5B      �� $ 00:00:00��6'   $ 0Q�6:   �� 5C      $ 00:00:00.4000000��6'   $ 1Q�6B   5D      U� 5E      $ 00:00:00X�6,   $	 shadow��6A   $U
 O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6`   .\�5F      �� $ 00:00:00��6'   $ 0Q�6:   �� 5G      $ 00:00:00.4000000��6'   $ 0Q�6B   5H      T� 5I      $ 00:00:00X�6,   $	 xamBroswerClose��6A   $
 (UIElement.Visibility)�6i   $ 00:00:00.0010000D�6�   ..�5J      i� $ 00:00:00��6)   # Z 6<   5K      U� 5L      $ 00:00:00X�6,   $	 xamBroswerClose��6A   $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�6i   $ 00:00:00.0010000D�6�   .\�5M      �� $ 00:00:00��6'   $ 1Q�6:   5N      U� 5O      $ 00:00:00X�6,   $	 xamBroswerClose��6A   $V
 P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�6i   $ 00:00:00.0010000D�6�   .\�5P      �� $ 00:00:00��6'   $ 1Q�6:   5Q      5R      5T       � Triggers 5V      :� 5W   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5X      �� #��[ 6   5Y      .��5\      � -   5         �Name$ 
LayoutRoot��5\      .+�5^   
   (� $	��650��6   .��5`      � .+�5b      �� -   5         ��Name$ shadow��5d       J� RenderTransformOrigin$ 0.5,0.5&�5c      $
��None=�5e      $��Center=�5f      $��Center=�5g      $��	0,0,0,162q�5h       J� Opacity$ 0Q�5i      $?��9/PersianEditor;component/Resources/Images/PersianLogo.png��5j      z�5m      V� 5n      .~�5p      ��  �� ScaleX$ 1Q�5q   !    �� ScaleY$ 1Q�5r   !   �� 5t       �� AngleX$ 0Q�5u   !    �� AngleY$ 0Q�5v   !   �� 5x       �� Angle$ 0Q�5y   !   T� 5{       T� X$ 0Q�5|   !    T� Y$ 0Q�5}   !   5      5�      �� 5�      -   5        $ 
xamFeature��5�      $ 0.5,0.5&�5�      $��Center=�5�      $��Center=�5�      $
��None=�5�      $ 0Q�5�      $?��9/PersianEditor;component/Resources/Images/PersianLogo.png��5�      z�5�      V� 5�      .~�5�      �� $ 1Q�5�   !   $ 1Q�5�   !   �� 5�      $ 0Q�5�   !   $ 0Q�5�   !   �� 5�      $ 0Q�5�   !   T� 5�      $ 0Q�5�   !   $ 0Q�5�   !   5�      5�      �� 5�      -   5        $ xamBroswerClose��5�      $ 0.5,0.5&�5�      $��Center=�5�      $��Center=�5�      $
��Auto��5�      $
��Auto��6"   $ 0Q�5�      $?��9/PersianEditor;component/Resources/Images/PersianLogo.png��5�      z�5�      V� 5�      .~�5�      �� $ 1Q�5�   !   $ 1Q�5�   !   �� 5�      $ 0Q�5�   !   $ 0Q�5�   !   �� 5�      $ 0Q�5�   !   T� 5�      $ 0Q�5�   !   $ 0Q�5�   !   5�      5�      5�      5�      5�      5�      