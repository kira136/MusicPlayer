   M S B A M L   `   `   `  ����   WpfApp1    WpfApp1.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     c _MaterialDesignThemes.MahApps, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     � 
ResizeMode$  NoResize=� � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
������  � Title$ MusicPlayer��$	��700��$
��1000��   MaterialDesignWindow#��[  .���� 
������� �� CornerRadius$ 10��._��  � ColumnDefinitions �� $��65 ��� $	��619 ��� $	��315 �.+��� -   
������$��25��$	��Top=�$ 	10,10,0,0��$��3��$��0,0,-1,0q��� -   $��Right=�$��Bottom=�$��18��$��18��$��
0,0,22,680q�$��2������  �� 
TargetType" ��.R���  ��Name$ PART_Border��
���  ��$ 50,50,50,50��
���  ��._��� $��Center=�$��Center=�	 �� Triggers	 P� 
 P� Property
 �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ PART_Border�� �� Property � �� �� Value
 �  ��.���� 
�������$
��Bold"�$��30(��� -   $��Right=�$��18��$��18��$��
0,2,45,680q�$��2������ " ��.R��� $ PART_Border��
��� ���
��� ���$ 50,50,50,50��._��� $��Center=�$	��Top=�	 P� 
 �@J�IsMouseOver . . ��� $ PART_Border�� � ��
 �/����� $��0��$ 0,0,0,10�����#F5F5F5$��0,25,0,0q�._���  �� Orientation$ Vertical=�.+��� $��Center=�.+��� -    & "MaterialDesignIconForegroundButton#��[  � ToolTip$( "MaterialDesignIconForegroundButton�� �� Left$ 5��
 �� Top$ 20��{��� *DataContext.ControlsEnabled+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ WindowL�.��'  !MaterialDesignThemes.Wpf.PackIcon  -     Name$ 
HomeButton��   Kind$ HomeOutline=��� $��Center=�.+��� -    ��Name$ DirectoryButton��#��[ $( "MaterialDesignIconForegroundButton��$ 5��$ 70��$��Center=�$	��Top=�{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ FolderOutline=��� .+��� -   $ PlaylistButton��#��[ $( "MaterialDesignIconForegroundButton��$ 5��$	 120��$
��Left=�$��Center=�{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ ListBoxOutline=�$��Center=�$��Center=��� .+��� -   $ FocusButton��#��[ $( "MaterialDesignIconForegroundButton��$ 5��$	 170��$
��Left=�$��Center=�{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ TargetArrow=�$��Center=�$��Center=��� .+��� -	   #��[ $( "MaterialDesignIconForegroundButton��$ 5��$	 620��{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  -
   $ SettingButton��$ SettingsOutline=�$��Center=�� -    �Name$ SettingsPopup��$��Right=� � 	StaysOpen .   � HorizontalOffset$ 25�� � VerticalOffset$	 -90�� � PlacementTarget ��  �� ElementName$ SettingButton��.���� 
�������$��1q� �� Padding$ 10q�._��� .+��� $��Settings��$
��Bold"�$��0,0,0,10q��� $��Enable Notifications��$	��0,5q��� $��	Dark Mode��$	��0,5q��� $��Auto Update��$	��0,5q�� $��1�� �� $
��322* ��� $
��297* � � RowDefinitions �� $	��17* ��� $
��158* �.+��� -     ��Name$  ContentControl��$��1��$��2��� $��0��$��2��.+��� )  #MaterialDesignThemes.Wpf.HintAssist!   Hint$! Search��"   
IsFloating" . $	��200��$��30��$��
419,32,0,2q�.  (MaterialDesignThemes.Wpf.TextFieldAssist#   UnderlineBrush# �#A8E6CF$   
Foreground
$ �   �$��Roboto)�� $��2��$��10,25,10,10q�.+��� $ Vertical=����#F5F5F5.+��� -   $ Frame1��$ 10��$ 20q�$��10q�$	��443��$	��276������ -   GradientBrush1��% �� 
StartPoint$	% 0,0&�& �� EndPoint$	& 1,1&�.��� -   GradientStop1��' � Color$' #A8E6CF��( � Offset$	( 0.0Q�� -   GradientStop2��$' #F5F5F5��$	( 1.0Q�) � Triggers) :� * :� RoutedEvent$* 
MouseEnter��.[��� .���� .���� + �� 
TargetName$+ GradientStop1��, �� TargetProperty$, Color�	- �� To$- #81C784X�. g� Duration$. 0:0:0.3D��� $+ GradientStop2��$, Color�$- #A8E6CFX�$. 0:0:0.3D�:� $* 
MouseLeave��.[��� .���� .���� $+ GradientStop1��$, Color�$- #A8E6CFX�$. 0:0:0.3D��� $+ GradientStop2��$, Color�$- #F5F5F5X�$. 0:0:0.3D�._�� $��-10,-10,-10,-10q� �� $
��341* ��� $	��37* ��� $	��45* �.+��� -   / ��Name$/ SongProgress_Slider��$��2��$��6,0,4,0q�$��Center=�$��20���� .+��� -   $ PlaySong_Button��#��[ $( "MaterialDesignIconForegroundButton��$	 104��$	 340��{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ PlayOutline=��� .+��� -   $ PauseSong_Button��#��[ $( "MaterialDesignIconForegroundButton��$	 104��$	 340��{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ Pause=��� .+��� -   $ NextSong_Button��#��[ $( "MaterialDesignIconForegroundButton��$	 154��$	 340��$
��Left=�$��Center=�{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ SkipNextOutline=��� .+��� -   $ PrevSong_Button��#��[ $( "MaterialDesignIconForegroundButton��$ 54��$	 340��$
��Left=�$��Center=�{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ SkipPreviousOutline=��� .+��� -   $ ReplaySong_Button��#��[ $( "MaterialDesignIconForegroundButton��$ 4��$	 340��$
��Left=�$��Center=�{��� *DataContext.ControlsEnabled+ �� *FindAncestor+$ WindowL�.��  $ Repeat=��� �� -   $ Frame2��$ 10��$ 20q�$��10q�$	��182������ -   GradientBrush2��$	% 0,0&�$	& 1,1&�.��� -   GradientStop3��$' #A8E6CF��$	( 0.0Q�� -   GradientStop4��$' #F5F5F5��$	( 1.0Q�) :� $* 
MouseEnter��.[��� .���� .���� $+ GradientStop3��$, Color�$- #81C784X�$. 0:0:0.3D��� $+ GradientStop4��$, Color�$- #A8E6CFX�$. 0:0:0.3D�:� $* 
MouseLeave��.[��� .���� .���� $+ GradientStop3��$, Color�$- #A8E6CFX�$. 0:0:0.3D��� $+ GradientStop4��$, Color�$- #F5F5F5X�$. 0:0:0.3D�