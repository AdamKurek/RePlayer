   M S B A M L   `   `   `  ����  RePlayer    RePlayer.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5        
���   �  J� 	AllowDrop$
  True��5      $	��800��5	   I   $
��1150��6V    � Title 6	   �� *
FileName+ �� UpdateSourceTrigger$ PropertyChanged=�.��5      � -   5        $
��grid��5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $��50 �6   �� 5      $
��733* �6   �� 5      $
��249* �6   �� 5      $
��118* �6   .+�5   
   r� -   5        $��player��5       r� LoadedBehavior$ Manual=�6%   $��2��6>   $��4��6O   $��0��6c    � IsSharedSizeScope$
 True��6p   $	��100��5      $	��100��6'    r� ScrubbingEnabled$
 True��5      5      �� 5!   
   -   5         ��Name$ slider��5!      $��Bottom=�5"      $|�False��6,   	 �� IsMoveToPointEnabled$
	 true��5#      $��18��6-   $��0��69   
 � Minimum$
 0Q�6F   $��4��6R   $
��10,0q�5%       � Value 5$      �� *
TimeSlider+$ PropertyChanged=�5%      �� 5'   
   -   5         ��Name$ soundbtn��5'      $|�False��6#   $��1��65   $
��Left=�6C   ���	#55F5F5F5$��10,5,0,5q�6�   .��6�   Sound6�   �� 5)   
   -   5         ��Name$ panel��5)      $��1��6$   $��4��61   $��	10,5,10,5q�6E   .+�5*      � $��Center=�6   .+�5+      �� -   5        $��btnPrevFrame��5+      $|�False��6I   $��<��6[   6l   �� 5,      -   5        $��btnNextFrame��5,      $|�False��6I   $��>��6[   6m   �� 5-      -	   5        $��btnVideosList��5-      $|�False��6J   .��6]   files6d   �� 5.      -
   5        $��
btnPrevVid��5.      $|�False��6D   .��6V   prev6\   �� 5/      -   5        $��
btnNextVid��5/      $|�False��6D   .��6V   next6\   �� 50      -   5        $��btnPlay��50      $|�False��6>   .��6P   Play6V   �� 51      -   5        $��btnSlow��51      $|�False��6>   .��6P   Slow6V   �� 52      -   5        $��btnSpeed��52      $|�False��6@   .��6R   Speed6Y   �� 53      -   5        $��lblSpeed��53      $��	Condensed&�6)   $|�False��6A   $��25��6S   
�������6}   54      55      �� 56   
   -   5         ��Name$ Status��56      $��1��6    $
��Left=�6.   ���	#55F5F5F5$��	15,0,10,5q�6`   $��4��6t   �� 58   
   -   5        $ volumeSlider��58      $��0��6'   $��0��64    �� Orientation$ Vertical=�59      $y�Hidden=�6A   $
	 True��6U   $|�False��6q   $
 0Q�5:       � Maximum$	 100Q�6    J� Opacity$	 0.7Q�6+   $	��500��69   $	��100��6I   $��15,100,0,100q�6Y    59   (   �� *Volume+5:   Y   ]� 5>   
   -   5         ]�Name$ videosListLayout��5>      $
��true��6.   $y�Hidden=�6D   $��1��5?      $��2��6   $��3��6/   $��	733,0,0,0q�6C   .+�5@      ]� $	��Top=�6   .+�5A      �� $
��auto��6`   ��6   �� *DirectoryPath+$ PropertyChanged=�6q   �� 5B      -   5        $	��dir��5B      6A   5C      �� 5D      -   5         ��Name$ 
videosList��5D      $��0q�6�   ��6+   �� *Files+$ PropertyChanged=� �� SelectedIndex 6m   �� *	FileIndex+ �� SelectedItem 6�   �� *SelectedFile+ �� View 5E      �� 5F      .P�5G      �� $
��Name��6)    �� Width$	 200��6]    �� DisplayMemberBinding 67   �� *Name+6]   �� 5H      $��Created��6)   $ 70��6h    6:   �� *CreationTime+6h   5I      5K      5M      5N      5P      