   M S B A M L   `   `   `  ����E  ACourseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VintageParts.ViewsVintageParts.Views  5      !    VintageParts.Views.HomeView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:VintageParts.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $��	Helvetica)�5	   7   .��5
      �   � RowDefinitions  5   
   �� 5      $	��46* �6   �� 5      $	��69* �6   �� 5      $��70 �6   �� 5      $
��265* �6   .+�5   
   � $��0��6   $��	0,0,0,0.4q�6   .+�5      #  MaterialDesignThemes.Wpf.Card  $��
10,2,10,10q�6"   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�66      UniformCornerRadius$ 5Q�6g   .��5      ��    Welcome#���   6   $��Center=�6=   $��Center=�6Z   $��25(�6u   5      5      � 5   
   $��1��6   .+�5        $��10,10,10,10q�6"   $ 5Q�67   $ Depth4=�6O   .��5      �� $��Center=�6   ��65   �� *
categories+ �� SelectedItem 6X   �� *SelectedCategory+��5      �� 5      .R�5      ��  �� CornerRadius$ 6��6%   $��1q�66   $	��Top=�6J   
������  J� InputBindings 5   "   d� 5   &   &   System.Windows.Input.MouseAction   LeftDoubleClick d� MouseAction# Z 63    �� CommandParameter 5   3   �� 	 �� RelativeSource	 �� 
 �� AncestorType$
 ListBoxL� �� Path$  DataContext.FindByCategory� �� Command 5   3   �� 	 �� $
 ListBoxL�$  DataContext.FindByCategory�._�5!   "   ��  �� Orientation$ Vertical=�6-   .+�5"   &     $ 10Q�6:   .��5#   *   � $��45��6/   $	��100��6;   .+�5$   .   �� $��Center=�6N   $��Center=�6k   ��68   �� *Name+6k   5%   +   5&   '   5'   #   5(      5)      ��5+      �� 5,      .R�5-      � 5.      50      51      52      � 53   
   $��2��6   .+�54        $��10,10,10,10q�6"   $ Depth4=�67   $ 5Q�6h   .��55      �� $ 
Horizontal=�6   $��Center=�62   .+�56      �� $��1q�6   $��Center=�6I   $��25��6k   $
�� 400��6w   $��Center=�6�     TextBoxBaseControlTemplate#��[ 6�    �� MaxLines$ 1��6�    �� 	MaxLength$	 100��6�   ��6*   �� *textForSearch+.��6�   .  (MaterialDesignThemes.Wpf.TextFieldAssist   CharacterCounterStyle 57      �� 58       �� 
TargetType" ��6$   .��59   P   5:      �� 5;      $��75��6   $��25��6(     RoundedButtonStyle#��[ 6S   ��64   �� *
FindByName+.��5<      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Search=�62   
�������6@   5=      5>      5?      5@      � 5A   
   $��3��6   .+�5B        $��10,10,10,10q�6"   $ Depth4=�67   $ 5Q�6h   .��5C      �� -   5         ��Name$ partList��5C   ^   $��244.4��6�   $	��400��6�    �� SelectionMode$ Extended=�6�   ��6   �� *Parts+ 68   �� *SelectedPart+��5D      �� 5E      .R�5F      �� $ 6��6!   $��1q�62   $	��Top=�6F   
������  5G   "   d� 5H   &   # Z 63    5I   3   �� 	 �� $
 ListBoxL�$ DataContext.OpenFullInfo� 5J   3   �� 	 �� $
 ListBoxL�$ DataContext.OpenFullInfo�._�5L   "   �� $ Vertical=�6-   .+�5M   "     $ 10Q�66   $	��150��6O   .��5N   "   �� $ Vertical=�6-   $
�� 230��6D   $	��130��6R   .+�5P   &   �� $	��130��6,   $	��110��68   $��5q�6E   ��6P   �� $ 
Media.Path��� 5Q   *   $ 
Horizontal=�65   .+�5R   .   �� #��� ��68   $��	Helvetica)�6V   �� 5S   .   $��	Helvetica)�6N   $��WrapWithOverflow=�6e   ��68   �� *Name+6e   5T   +   �� 5U   .   $ 
Horizontal=�69   .+�5V   2   ��   Quantity#���  6<   �� 5W   2   ��6<   �� *
Quantity+5X   /   �� 5Y   .   $ 
Horizontal=�69   .+�5Z   2   ��  	 Price#���  6<   �� 5[   2   ��6<   �� *Price+5\   /   �� 5]   .   $ 
Horizontal=�69   .+�5^   2   ��   Category#���  6<   �� 5_   2   ��6<   �� *Category.Name+5`   /   �� 5a   .   $ 
Horizontal=�69   .+�5b   2   ��  	 Brand#���  6<   �� 5c   2   ��6<   �� *
Brand.Name+5d   /   �� 5e   .   
���   �68   �� 5f   *   #��[ 6�     More#���  6�   ��61   �� 	 �� $
 ListBoxL�$ DataContext.OpenFullInfo�6�   5g   +   5h   #   5i      5j      5k      ��5m      �� 5n      " ��6     �� BasedOn# [B��5o       �� Triggers 5p      P� 5q   "    P� Property �@J�IsKeyboardFocusWithin P� Value . . �5r   &   ��  �� Property �@��
IsSelected �� Value . 6C   5s   #   5u      ��5w      �� 5x      .R�5y      � 5z      5|      5}      5~      5      5�      