   M S B A M L   `   `   `  ����E  ACourseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VintageParts.ViewsVintageParts.Views  5      #    VintageParts.Views.SearchView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      ,local clr-namespace:VintageParts.Views  5      
�������.��5
      � ���#341e7c  � RowDefinitions  5   
   �� 5      $	��70* �6   �� 5      $
��380* �6    � ColumnDefinitions 5   
   �� 5      $
��580* �6   �� 5      $	��210 �6   .+�5   
   � $��0��6   $��2��6   .+�5      #  MaterialDesignThemes.Wpf.Card  $��
10,10,10,0q�6"      UniformCornerRadius$ 5Q�66   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�6N   .��5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5      �� $	��250��6=   $��25��6I   $��Center=�6U   $��1q�6p   
���   �   TextBoxBaseControlTemplate#��[  6�    �� MaxLines$ 1��6�    �� 	MaxLength$	 100��6�   ��6   �� *textForSearch+.��6�   .  (MaterialDesignThemes.Wpf.TextFieldAssist   CharacterCounterStyle 5      �� 5       �� 
TargetType" ��6$   .��5   P   5      �� 5      $��10,0,0,0q�6<     RoundedButtonStyle#��[ 6N   $��40��6z   $��40��6�   ��6   �� *
FindByName+.��5      '  !MaterialDesignThemes.Wpf.PackIcon  	   Kind$	 Search=�62   $��20��6@   $��20��6L   5      5      5      5       � 5!   
   $��1��6   $��1��6   .+�5"        $ Depth4=�6"   $��0,10,9.6,10q�6S   $ 5Q�6h   $��3��6�   .��5#      �   5$      �� 5%      $��2* �6(   �� 5&      $��4* �6(   �� 5'      $��4* �6(   �� 5(      $��2* �6(   �� 5)      $��2* �6(   �� 5*      $��2* �6(   �� 5+      $��4* �6(   �� 5,      $��2* �6(   .+�5.      � $��0��6   .+�5/      ��   DetailedSearch#���  6$   $��Center=�6L   $��Center=�6i   $��	Helvetica)�6�   $��14(�6�   50      � 51      $��1��6   .+�52      �� $ Vertical=�6%   .+�53      �� $ 
Horizontal=�6%   $��0,20,0,0q�6>   .+�54      �� $��10,0,0,0q�6(     from#���  6:   $��Center=�6X   $��	Helvetica)�6s   �� 55      $��Center=�6@   $��10,0,0,0q�6[   $��80��6m   
���   �$��1q�6�   $��80��6�   $ 1��6�   ��6&   �� *
LowValue+6�   56      �� 57      $ Vertical=�6%   .+�58      �� ���#341e7c$	��185��6:   $
��Left=�6F   
 � Maximum$

 9999Q�6|    � Minimum$ 0Q�6�    � Value 6a   �� *
LowValue+6�   59      5:      5;      � 5<      $��2��6   .+�5=      �� $ Vertical=�6%   .+�5>      �� $ 
Horizontal=�6%   $��0,20,0,0q�6>   .+�5?      �� $��10,0,0,0q�6(     to#���  6:   $��Center=�6V   $��	Helvetica)�6q   �� 5@   "   $��Center=�6D   $��10,0,0,0q�6_   $��80��6q   
���   �$��1q�6�   $��80��6�   $ 1��6�   ��6*   �� *
MaxValue+6�   5A      �� 5B      $ Vertical=�6%   .+�5C      �� ���#341e7c$	��185��6:   $
��Left=�6F   $

 9999Q�6�    6a   �� *
MaxValue+ 6|   �� *
LowValue+6�   5D      5E      5F      � 5G      $��3��6   .+�5H      �� $ 
Horizontal=�6%   $
��Left=�6>   $��Center=�6Y   .+�5I      ��   Category#���  6(   $��	Helvetica)�6J   $��Center=�6a   $��10,0,0,0q�6|   �� 5J      $	��100��6t     	Category2)  #MaterialDesignThemes.Wpf.HintAssist   Hint# �  6�   $��2q�6�   
���   �$	��150��6�   ��6'   �� *
Categories+ �� SelectedItem 6J   �� *SelectedCategory+��5K      �� 5L   "   .R�5M   &   �� $��0q�6F   $��Bottom=�6Q   $��Center=�5N   (   ��5M   0   �� *Name+5N   (   5O   #   5Q      5R      5S      � 5T      $��6��6   .+�5U      ��  
 Search#���  6!   #��[ 6g   $��40��6�   $	��150��6�   ��6D   �� *DetailedSearch+6�   5V      5W      5X      5Y      � 5Z   
   $��1��6   $��0��6   .+�5[        $��10,10,10,10q�6"   $ Depth4=�67   $ 5Q�6h   .��5\      �� -   5         ��Name$ partList��5\   ^   $	��360��6p   $	��500��6�    �� SelectionMode$ Extended=�6�   ��6   �� *Parts+ 68   �� *SelectedPart+��5]      �� 5^      .R�5_      ��  �� CornerRadius$ 6��6%   $��1q�66   $	��Top=�6J   
������  J� InputBindings 5`   "   d� 5a   &   &   System.Windows.Input.MouseAction   LeftDoubleClick d� MouseAction# Z 63    �� CommandParameter 5b   3   ��  �� RelativeSource ��  �� AncestorType$ ListBoxL� �� Path$ DataContext.OpenFullInfo� �� Command 5c   4   ��  �� $ ListBoxL�$ DataContext.OpenFullInfo�._�5e   "   �� $ Vertical=�6-   .+�5f   &     $ 10Q�6:   .��5g   *   �� $ Vertical=�65   $
�� 230��6L   $	��130��6Z   .+�5i   .   �� $	��130��64   $	��110��6@   $��5q�6M   ��6X   �� *
Media.Path+�� 5j   .   $ 
Horizontal=�69   .+�5k   2   �� #��� ��6<   $��	Helvetica)�6Z   �� 5l   2   $��	Helvetica)�6R   ��6<   �� *Name+6R   5m   /   �� 5n   .   $ 
Horizontal=�69   .+�5o   2   ��   Quantity#���  6<   �� 5p   2   ��6<   �� *
Quantity+5q   /   �� 5r   .   $ 
Horizontal=�69   .+�5s   2   ��  		 Price#��� 	 6<   �� 5t   2   ��6<   �� *Price+5u   /   �� 5v   .   $ 
Horizontal=�69   .+�5w   2   �� #���  6<   �� 5x   2   ��6<   �� *Category.Name+5y   /   �� 5z   .   $ 
Horizontal=�69   .+�5{   2   ��  	
 Brand#��� 
 6<   �� 5|   2   ��6<   �� *
Brand.Name+5}   /   �� 5~   .   
���   �68   �� 5   .   #��[ 65     More#���  6a   ��5�   5   ��  �� $ ListBoxL�$ DataContext.OpenFullInfo�5�   +   5�   '   5�   #   5�      5�      ��5�      �� 5�      " ��6     �� BasedOn# [B��5�       �� Triggers 5�      P� 5�   "    P� Property �@J�IsKeyboardFocusWithin P� Value . . �5�   &   ��  �� Property �@��
IsSelected �� Value . 6C   5�   #   5�      ��5�      �� 5�      .R�5�      � 5�      5�      5�      5�      5�      5�      