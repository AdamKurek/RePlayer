   M S B A M L   `   `   `  ����   RePlayer    RePlayer.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   
���   �  J� 	AllowDrop$
  True��$	��800��$
��1150�� � Title �� *
FileName+ �� UpdateSourceTrigger$ PropertyChanged=�.��� -   $
��grid�� � RowDefinitions �� $��* ��� $
��Auto ��� $
��auto � � ColumnDefinitions �� $��50 ��� $
��733* ��� $
��249* ��� $
��118* �.+�r� -   $��player�� r� LoadedBehavior$ Manual=�$��2��$��4��$��0�� � IsSharedSizeScope$
 True��$	��100��$	��100�� r� ScrubbingEnabled$
 True���� -    ��Name$ slider��$��Bottom=�$|�False��	 �� IsMoveToPointEnabled$
	 true��$��18��$��0��
 � Minimum$
 0Q�$��4��$
��10,0q� � Value �� *
TimeSlider+$ PropertyChanged=��� -    ��Name$ soundbtn��$|�False��$��1��$
��Left=����	#55F5F5F5$��10,5,0,5q�.��Sound�� -    ��Name$ panel��$��1��$��4��$��	10,5,10,5q�.+�� $��Center=�.+��� -   $��btnPrevFrame��$|�False��$��<���� -   $��btnNextFrame��$|�False��$��>���� -	   $��btnVideosList��$|�False��.��files�� -
   $��
btnPrevVid��$|�False��.��prev�� -   $��
btnNextVid��$|�False��.��next�� -   $��btnPlay��$|�False��.��Play�� -   $��btnSlow��$|�False��.��Slow�� -   $��btnSpeed��$|�False��.��Speed�� -   $��lblSpeed��$��	Condensed&�$|�False��$��25��
��������� -    ��Name$ Status��$��1��$
��Left=����	#55F5F5F5$��	15,0,10,5q�$��4���� -   $ volumeSlider��$��0��$��0�� �� Orientation$ Vertical=�$y�Hidden=�$
	 True��$|�False��$
 0Q� � Maximum$	 100Q� J� Opacity$	 0.7Q�$	��500��$	��100��$��15,100,0,100q� �� *Volume+]� -    ]�Name$ videosListLayout��$
��true��$y�Hidden=�$��1��$��2��$��3��$��	733,0,0,0q�.+�]� $	��Top=�.+��� $
��auto������ *DirectoryPath+$ PropertyChanged=��� -   $	��dir���� -    ��Name$ 
videosList��$��0q����� *Files+$ PropertyChanged=� �� SelectedIndex �� *	FileIndex+ �� SelectedItem �� *SelectedFile+ �� View �� .P��� $
��Name�� �� Width$	 200�� �� DisplayMemberBinding �� *Name+�� $��Created��$ 70�� �� *CreationTime+