   M S B A M L   `   `   `  ����   TEProtectorV1!    TEProtectorV1.Windows.Login   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes    � Title$  	FormClose��$	��550��$	��500�� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � WindowStyle$
 None=�
������  � AllowsTransparency .    MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignFont#���  .���  � RowDefinitions �� $��40 ��� �� $��30 � J� OpacityMask !� 	 !� Visual	 �� 
 �� ElementName$
 Border1��.+��� -    ��Name$ Border1�� �� CornerRadius$ 15�����	#FFF2F7EC$��0��$��3��� $��0�� � ColumnDefinitions �� $��40 ��� �� $��40 �.+��� $>��8/TEProtectorV1;component/Images/Logo/LogoWithoutText.png��$��0��$��Uniform=�$��30��$��30���� $��Login��$��Center=�$��Center=�$��24(����	#FF8DC060$��1���� $��2�� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ btnClose��$��5 0 0 0q� / +MaterialDesignFloatingActionMiniLightButton#��[ $��20��$��20�����	#FFB8D698���	#FFDDEBCE.��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Close=�$��12��$��12��� $��1�����	#FF8DC060 �� $��20 ��� $
��1.4* ��� $
��1.4* ��� $
��1.4* ��� $
��1.4* ��� $��30 � �� $��30 ��� �� �� $��30 �.+�� ���	#FFF2F7EC$��2��$��1��$
��Left=�$��40��$��	20,57,0,0q�$��4��$	��Top=�$	��400��.+��� $ 
Horizontal=�$��Center=�.+��� $��Italic$�
�������$��0��$��Don't have account?��$��Center=��� 
��� � �$��Italic$�$��0��$��Sign In��$
��Auto��$��Center=�$��10 0 0 0q�����  �� 
TargetType" ��.R��� � $��2�� �� $	��100 ��� �� $	��100 �.+��� $7��1/TEProtectorV1;component/Images/Logo/OnlyText.png��$��Uniform=�$��0���� $��Version 1.0.0��$��2��$��Center=�$��Center=�$��15(�$��Light"����	#FF57AB27� ���	#FFF2F7EC$��Center=�$	��338��$��Center=�$	��400��$��1��$��0 0 0 90q� �� $��20 ��� $��75 ��� $��75 ��� $��40 ��� $��75 ��� $��20 � �� $��20 ��� �� �� $��20 �.+��� -   $ btnLogin�����	#FF8DC060$��4��$��2�����	#FFF2F7EC  MaterialDesignFlatButton#��[ $��50��$��1��$��Center=�.��LOGIN�� $ 
Horizontal=�$��1��$��1��$��2��.+�  $ Email=�$��1�� J� RenderTransformOrigin$ 0.833,2.542&�$��Center=�$��Center=�$��15���� -    ��Name$ EmailBox��$��1��$��1��$��2��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Email��$��Center=�$	��330��$��10 0 0 0q�.������  �� Path$ Email� �� UpdateSourceTrigger$ PropertyChanged=�.���� $ 
Horizontal=�$��1��$��2��$��2��.+�  $ 
AccountKey=�$��2��$ 1.667,3.354&�$��16��$��15��$��Center=�$��Center=�G� -    G�Name$ PasswordBox��$��2��$	��330��$��Right=�$��Center=�$��10 0 0 0q�$ Password��$��1��$��2���� $��Italic$�
�������$��3��$��2��$��Forgot Password?��$
��Left=�$��0,3,0,21q��� 
��� � �$��Italic$�$��3��$��2��$��
Click Here��$��Right=�$
��Auto��$ 0.494,0.793&�$��0 2 10 0q����� " ��.R��� �� $��3��$��
Remeber me��$
��Left=�$	��Top=�$��20��$	��150��$��1��