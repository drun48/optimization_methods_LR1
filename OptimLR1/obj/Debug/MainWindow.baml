   M S B A M L   `   `   `  ����C  ?OptimLR1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:OptimLR1OptimLR1  5   	       OptimLR1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:OptimLR1     5   	     � Title$  Решение СЛАУ��5   	   
�������$��Stretch=�5
   	   $��Stretch=�5   	   $	��600��6'   .��5      � .+�5   
   �� .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� $
��n = ��6   $��Center=�6(   $��16(�6C   $��0,0,0,4q�6Q   �� 5      -   5        $
��txtN��5      $��30��6&   $��5q�61    �� 	MaxLength$ 3��6<   ��6J   �� *	RowsCount+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $�� m = ��6   $��Center=�6)   $��16(�6D   $��0,0,0,4q�6R   �� 5      -   5        $
��txtM��5      $��30��6&   $��5q�61   $ 3��6<   ��6J   �� *ColumnsCount+$ PropertyChanged=��� 5      -   5        $��Применить��5      $��5q�6-   ��68   �� *SetSizeCommand+6[   5      �� 5      $ 
Horizontal=�6   $��Center=�62   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��Data_A��5      $��5q�6)   $��1��64      SelectionMode$ Single=�6a      SelectionUnit$
 Cell=�6x    �� AlternationCount$
 9999��6�      CanUserAddRows .     CanUserDeleteRows .  	   CanUserReorderColumns	 .  
   CanUserSortColumns
 .  ��6D   �� *Data+   RowHeaderTemplate 5      5  -System.Windows.Controls.ItemContainerTemplate�  5      .R�5      �� ��6(   �� *AlternationIndex+ �� RelativeSource ��  �� AncestorType$ DataGridRowL�5      5      �� 5       -   5        $��txtbl1��5       $��42(�6*   $��Center=�68   × X =6Z     5!      -   5        $��Data_B��5!      $��5q�6)   $��1��64   $ Single=�6b   $
 Cell=�6y   $
 9999��6�    .   .  	 .  
 .  ��6D   �� *Datab+ 5"        5#      .R�5$      �� ��6(   �� *AlternationIndex+ �� $ DataGridRowL�5%      5'      5(      �� 5)      -   5         ��Name$ ButtonCalculate��5)      $��30��6.   $��10q�6:   $9��3Найти решение СЛАУ (вектор X)��6F   $��20(�6n   6|   �� 5*      $ 
Horizontal=�6   $��Center=�62   .+�5+      �� -   5        $��txtbl2��5+      $��42(�6*   $��Center=�68   $��Center=�6S   X =6u   �� 5,      -	   5        $
��LW_X��5,      ��6'   �� *ItemSource_X+ �� View 5-      �� 5.      -
   5        	GridWiew1��.P�5/      ��  �� DisplayMemberBinding 6-   �� *[1]+6T   �� 50       6-   �� *[0]+.��6R   06U   51      53      �� 54      -   5        $��Table��54   4   $ Vertical=�6   $
��15pxq�6A   $��Center=�6O   55      �� 56      -   5        $��txtbl3��56      $��24(�6*   $��Center=�68   $��Center=�6S   $��10q�6p   Норма невязки:6�   57      58      59      