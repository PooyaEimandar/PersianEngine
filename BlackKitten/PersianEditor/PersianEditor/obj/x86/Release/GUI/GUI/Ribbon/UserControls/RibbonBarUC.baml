   M S B A M L   `   `   `  ���� H  DPersianEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:PersianEditor.UserControlsPersianEditor.UserControls  i9clr-namespace:PersianEditor.UserControls.RibbonComponents+PersianEditor.UserControls.RibbonComponents  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem ,    &PersianEditor.UserControls.RibbonBarUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     p lTelerik.Windows.Controls.RibbonBar, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Docking, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Data, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f
 bTelerik.Windows.Controls, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2010.3.1110.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
   <UCs(clr-namespace:PersianEditor.UserControls   	 
   OComps9clr-namespace:PersianEditor.UserControls.RibbonComponents   	 
   :sys&clr-namespace:System;assembly=mscorlib   	 
     � 	Resources     TranslateImage  Logo �� 	UriSource  Base  FontBtn �� Property �� Value  	ThisStyle  	MaskStyle/  )Telerik.Windows.Controls.RadMaskedTextBox��%Y  &	        &	 =     &	 �     &	 �     &	 %    &	 v    �� $7��1/PersianEditor;component/Resources/Images/New.png���� $? 9/PersianEditor;component/Resources/Images/PersianLogo.png?��� $D >/PersianEditor;component/Resources/Images/Leaves/BaseLeave.png?��� .���  � ��$ Times New Roman)���  � ��$ 15(���  � ��$
 Bold"��� .���  � ��$ Center=���  � ��$
 Left=���  � ��$	 5,1q��� .���  � ��$ Center=���  � ��$ Center=���  � ��$	 5,1q���  � ��$ 70����  �@ Mask$ d����  �@ MaskType$ Numeric=�.��� .+�+  %Telerik.Windows.Controls.RadRibbonBar  -     Name$ 	RibbonBar��   Title$ PersianEngine��   ApplicationName$ Editor��#   ApplicationButtonImageSource# [    ApplicationMenu .  (Telerik.Windows.Controls.ApplicationMenu  -   	  Name$	 AppMenu��
   Content
 �� -    ��Name$ AppContentMenu��$	��300��.+�-  'Telerik.Windows.Controls.RadGroupHeader  $��Recent Projects��.  (Telerik.Windows.Controls.RadRibbonButton  $��1 The Land.PES��$��Stretch=�  $��2 AncientFight.PES��$��Stretch=�  $��
3 Test.PES��$��Stretch=�   FooterContent �� $��25�� �� Orientation$ 
Horizontal=�.+�( 
 "Telerik.Windows.Controls.RadButton  -     Name$
 Exit��
����� �$	��100��$��20��$��3,0,2,0q�   CornerRadius$ 5��$
��Exit��#��[ .@�3  -Telerik.Windows.Controls.RadRibbonSplitButton  -     Name$ NewSplitBtn��   Text$	 New��   
LargeImage# [ - 
 'Telerik.Windows.Controls.RadSplitButton   DropDownContent �� $��Stretch=�.+�  $	��New��)	   #PersianEditor.UserControls.HeaderUC	  -    	 Name$ NewScene��)
   #PersianEditor.UserControls.HeaderUC 
  MouseLeftButtonDown$ OnMouseLeftButtonDown�� 	  TextProperty$ 	New Scene�� � ToolTip$ Clear Render Surface��
�������	  -   $ 
NewProject��$ OnMouseLeftButtonDown��$ New Project��$ Remove All Contents��
�������  $
 Open��# [  �� $��Stretch=�.+�  $
��Open��	  -   $ 	OpenScene��$ OnMouseLeftButtonDown��$ 
Open Scene��$ Open Specific Scene��
�������	  -	   $ 
MergeScene��$ OnMouseLeftButtonDown��$ Merge Scene��$- 'Merge specific scene with current scene��
�������	  -
   $ OpenProject��$ OnMouseLeftButtonDown��$ Open Project��$ Open Specific Project��
�������(  "Telerik.Windows.Controls.Separator  
����� �$��1q�  -     Name$ 	SaveScene��   Text$ 
Save Scene��$ Save current scene��   
LargeImage# [   -   $ SaveAsScene��$ Save Scene as��$ Save current scene as��# [   
����� �$��1q�  -   $ SaveProject��$ Save project��$ Save current project��# [   -   $ SaveAsProject��$ Save project as��$ Save specific project as��# [   
����� �$��1q�  -   $ Import��$ Import��# [   $ Export��# [   
����� �$��1q�  $ Recent Projects��# [  �� $	��300��.+�  $��Recent Projects��  $��1 The Land.PES��$��Stretch=�  $��2 AncientFight.PES��$��Stretch=�  $��
3 Test.PES��$��Stretch=�  $ Recent Scenes��# [  �� $	��300��.+�  $��Recent Projects��  $��1 The Land.PES��$��Stretch=�  $��2 AncientFight.PES��$��Stretch=�  $��
3 Test.PES��$��Stretch=�  
����� �$��1q�  $ 
Prefrences��# [   
����� �$��1q�  $ About��# [  �� $��Stretch=�.+�  $��About��  $��1,0,2,0q�$	��242��.���� $ 
Horizontal=�$��3,0,5,0q�.+��� $��Print ��$
��Bold"��� $
��Wrap=�$	��180��$9��3Select a printer, number of copies before printing.��   QuickAccessToolBar 1  +Telerik.Windows.Controls.QuickAccessToolBar  $��35��.@�  $
 Undo��   
SmallImage# [    Size$ Large=�  $
 Redo��# [ $ Large=�  $ Show History��# [ $ Large=�.@�+  %Telerik.Windows.Controls.RadRibbonTab  $
��Home��.@�-  'Telerik.Windows.Controls.RadRibbonGroup  $��	Selection��.@�  -   $ Explorer��$ Explorer��# [ $ Large=�$ Show explorer window��  $ Select��   
SmallImage# [    Size$ Large=�(  "Telerik.Windows.Controls.ScreenTip    Title$  	Selection��!   Description$! Selecting object�� �� .+�  $��Selection options��-  'Telerik.Windows.Controls.RadContextMenu  $��1q�.@�*  $Telerik.Windows.Controls.RadMenuItem  -   "  Name$" 	SelectAll��$��
Select all��$+ %Select all object of scene (Ctrl + A)��  -   $" InvertSelection��$��Invert selection��$ Invert selection (I)��  -   $" Delete��$��Delete��$% Delete selected object(s) (Del)��  $
��Edit��.@�  -   $ 	Translate��# [ $ Large=�$ Translate Tool (V)��  -   $ Rotate��# [ $ Large=�$ Rotate Tool (C)��  -   $ Scale��# [ $ Large=�$ Scale Tool (X)��  $ 	Transform��# [ $ Large=�$! Transforming Options�� =   7PersianEditor.UserControls.RibbonComponents.TransformUC    -   $ 	Duplicate��$ 	Duplicate��# [ $ Large=�$ Duplicate Tool (V)��  $ BoundingBox��# [ $ Large=�$! BoundingBox Options�� �� .+�  $��Selection options��  $��1q�.@�  -   $" ShowAllBoundingBoxes��$��Show all��$ Show all bounding boxes��  -   $" HideAllBoundingBoxes��$��Hide all��$ Hide all bounding boxes��  -   $" ResetBoundingBox��$��Reset��$ Reset bounding box��  -   $" SetMaxBoundingBox��$��Identity��$) #Set identity value for bounding box��  $��Settings��.@�  $ Camera��# [ $ Large=�$1! +Edit camera's performance in render surface�� A   ;PersianEditor.UserControls.RibbonComponents.CameraSettingUC  -   #  Name$# CameraSetting��  $ Bind to��# [ $ Large=�$! Pick Object for binding�� �� .+��� $��5q�
����� �._��� .+��� -   $ ��Name$$ BindToObjectChkBox��$��Bind to Object��$��Center=�$��Center=�
�������% d� 	IsChecked$
% TrueY�
& � Tag$& 0���� � ' � ColumnDefinitions' �� �� .+��� $��0��$��2q�$��Property��
��������� -   ( ��Name$( BindProperty��$��1��$��2q�$��Position��$	��100��$��25��$% Specify the property of binding��.���� $��5q�
����� �._��� .+��� -   $$ BindToBoneChkBox��$��Bind to Bone��$��Center=�$��Center=�
�������$% FalseY�$& 1���� � ' �� �� .+��� $��0��$��2q�$��	Bone Name��
��������� -    $( BoneName��$��1��$��2q�$��RHand1��$	��100��$��25��$ Specify the name of bone��.���� $��Center=�$ 
Horizontal=�.+�  -!   $ PickSourceObjectForBind��$ 	Pick from��$��Center=�$��Center=�# [ $ Large=�$ Pick the source object��  -"   $ PickToBindObject��$ Bind to��$��Center=�$��Center=�# [ $ Large=�$ Select object to bind it��  -#   $ 
RemoveBind��$ Remove bind��$��Center=�$��Center=�# [ $ Large=�$ Remove binding��  -$   $ ApplyBindChanges��$ Apply change��$��Center=�$��Center=�# [ $ Large=�$ Apply change to binding��  $��	Animation��.@�  $��Animation Editor��.@��� $ 
Horizontal=�.+��� .+��� $��1��$��	Show Bone��#��[ *  $Telerik.Windows.Controls.RadComboBox  -%   )  Name$) ShowBone��$��2��$��Center=�$
��Left=�$	��5,1q�$��50��.@�.  (Telerik.Windows.Controls.RadComboBoxItem  $��False��  $
��True��  $ 
Swap Bones��# [ $ Large=�$  Swap Bones Values��$! 
Swap Bones�� �� .+�  $��Selection options��  $��1q�.@�  -&   $" 	SwapBones��$��All Transforms��$&  Swap all transforms of two bones��  -'   $" RemoveSwapped��$��
Remove All��$/ )Remove all swapped bones of selected mesh��  -(   $ DefaultPose��$ Default Pose��# [ $ Large=�$( "All skinned models play animations��  -)   $ ShowAllBones��$ Show All Bones��# [ $ Large=�$* $Build physic for selected object (V)��  -*   $ HiddenAllBones��$ Hidden All Bones��# [ $ Large=�$* $Build physic for selected object (V)��  -+   $ SyncGamePad��$ Sync GamePad��# [ $ Large=�$ Merge with gamePad��  $��Animation Player��.@��� $ 
Horizontal=�.+�  -,   $ PlayAnimation��$
 Play��# [ $ Large=�$
 Play��  --   $ PauseAnimation��$ Pause��# [ $ Large=�$ Pause��  -.   $ StopAnimation��$
 Stop��# [ $ Large=�$
 Stop��  -/   $ PlayNextFrame��$
 Next��# [ $ Large=�$ 
Next Frame��  -0   $ PlayPreviousFrame��$ Previous��# [ $ Large=�$ Previous Frame��  -1   $ PlayInverse��$ Inverse��# [ $ Large=�$ Inverse��� $��5q�$��Center=�$��Center=�* � RowDefinitions* �� �� .+��� -2   + ��Name$+ CurrentAnimationFrame��$��0��$��Center=�$��Center=�$ Current animation Frame��$
��Bold"�$��12(�
�������$	��NaN���� $��1��$��Center=�$��Center=�$��Animation time��$��0,12,0,5q�$��11(�  $��Animation Mixer��.@�� * �� �� ' �� �� .+��� $��0��$��0��$��Source Clips��#��[   -3   $) SourceClips��$��1��$��0��$	��100��$��Center=�$
��Left=�$	��5,1q��� $��0��$��1��$��Inherient Tracks��#��[   -4   $) InherientTracks��$��1��$��1��$	��100��$��Center=�$
��Left=�$	��5,1q�  -5   $ 	OpenMixer��$ 
Open Mixer��# [ $ Large=�$ Build All objects��  -6   $ ApplyInherit��$ Apply Changes��# [ $ Large=�$ Apply Changes��  $��	CutScenes��.@��� $ 
Horizontal=�.+�  -7   $ PlayCutScene��$
 Play��# [ $ Large=�$ Play the cutScene��  -8   $ PauseCutScene��$ Pause��# [ $ Large=�$  Pause the current cutScene��  -9   $ StopCutScene��$
 Stop��# [ $ Large=�$ Stop the current CutScene��� * �� �� ' �� �� .+��� $��0��$��0��$��
Cut Scenes��#��[   -:   $) 	CutScenes��$��1��$��0��$	��100��$��Center=�$
��Left=�$	��5,1q��� $��0��$��1��$��Animated Camera��#��[   -;   $) AnimatedCameras��$��1��$��1��$	��100��$��Center=�$
��Left=�$	��5,1q�  -<   $ CreateCutScene��$ Create��# [ $ Large=�$ Create a new cutScene��  -=   $  AddRemoveToCurrentCutScene��$ Add Animation��# [ $ Large=�$? 9Add/remove current animation to/from the current cutScene��  $��Build��.@�  $��Lights and Shadows��.@�  ->   $ AddLight��$ 	Add Light��# [ $ Large=�$ Build new light��  -?   $ RemoveLight��$ Remove Light��# [ $ Large=�$ Remove selected light��  -@   $ Shadows��$ Enable/Disable Shadows��# [ $ Large=�$ Enable/Disable Shadows���� $��Center=�.+��� Shadow Resolution  -A   $) ShadowsResolution��,   Text$, Shadows Resolution��$ Shadows Resolution��$	��100��$��25��.@�  - �� 
IsSelected- .  .��256  - . .��512  - .  .��1024  $��	Particles��.@�  -B   $ AddParticleSyS��$ Add ParticleSyS��# [ $ Large=�$ Build new particle system��  -C   $ RemoveParticleSyS��$ Remove ParticleSyS��# [ $ Large=�$% Remove selected particle system��  $��PostProcess��.@�� ' �� $
��auto ��� $
��auto �� $��0��.+�� * �� �� .+�  -D   $) ChangePostProcess��$��0��$ , Change type of PostProcess��$	��100��$��25��$  Change type of PostProcess��.@�  - . .��	Disable  - .  .��GodRay  - .  .��Toon  - .  .��Bloom  - .  .��Blur  - .  .��Glow  -E   $��1��$��70��$��25��$��Add to list��� $��0��.+�� * �� �� .+��� -F   . ��Name$. PostProcessList��$	��Top=�$��37��$X RRepresents list of all post process you can Add/Remove postprocess or reOrder them���� $��1��$ 
Horizontal=�.+�  -G   $��70��$��25��$��Remove��$��3q�$& 0��  -H   $��70��$��25��$��Move Up��$��3q�$& 1��  -I   $��70��$��25��$��	Move Down��$��3q�$& 2��  .@�  -J   $ 	PlayScene��$
 Play��# [ $ Large=�$ Play scene from editor��  -K   $ 	Build All��# [ $ Large=�$ Build All objects��  -L   $ Cancel��# [ $ Large=�$ Cancel building operation���� $ 
Horizontal=�$	��Top=�$��Right=�.+�  -M   $ Minimize��$��5q�$��40��$��20��
���  ��$
��Bold"�
����� �$��1q�$ 10��$��-��$ Minimize��  -N   $ ExitShortcut��$��5q�$��40��$��20��$��X��
���  ��$
��Bold"�
����� �$��1q�$ 10��$ Close��