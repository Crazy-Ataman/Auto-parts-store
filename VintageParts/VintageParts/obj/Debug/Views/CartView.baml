   M S B A M L   `   `   `  ����E  ACourseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:VintageParts.ViewsVintageParts.Views  5      !    VintageParts.Views.CartView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      ,local clr-namespace:VintageParts.Views  5      
�������$��	Helvetica)�5
   J   .��5      � ���#341e7c  � RowDefinitions  5   
   �� 5      $��9* �6   �� 5      $��2* �6   .+�5   
   � $��0��6   .+�5      #  MaterialDesignThemes.Wpf.Card  $��10,10,10,10q�6"   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�67      UniformCornerRadius$ 5Q�6h   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ partList��5      ��. $	��348��6�   $	��500��6�   ��6U   �� *Parts+ �� SelectedItem 6s   �� *SelectedPart+��5      �� 5      .R�5   "   ��  �� CornerRadius$ 6��6)   $��1q�6:   $	��Top=�6N   
������ ._�5   &   �� $ Vertical=�61   .+�5   *     $ 10Q�6>   $ Depth4=�6W   .��5   .   �� $ Vertical=�69   $
�� 250��6P   $	��120��6^   .+�5   2   �� $	��120��68   $	��120��6D   $��5q�6Q   ��6\   �� *
Media.Path+�� 5   2   $ 
Horizontal=�6=   .+�5   6   �� #��� ��6@   $��	Helvetica)�6^   �� 5   6   $��	Helvetica)�6V   ��6@   �� *Name+6V   5   3   �� 5   2   $ 
Horizontal=�6=   .+�5    6   ��    Quantity#���   6@   �� 5!   6   ��6@   �� *
Quantity+5"   3   �� 5#   2   $ 
Horizontal=�6=   .+�5$   6   ��  	 Price#���  6@   �� 5%   6   ��6@   �� *Price+5&   3   �� 5'   2   $ 
Horizontal=�6=   .+�5(   6   ��   Category#���  6@   �� 5)   6   ��6@   �� *Category.Name+5*   3   �� 5+   2   $ 
Horizontal=�6=   .+�5,   6   ��  	 Brand#���  6@   �� 5-   6   ��6@   �� *
Brand.Name+5.   3   �� 5/   2   $ 
Horizontal=�6=   .+�50   6   ��  
 Amount#���  6@   �� 51   6   ��6@   �� *Amount+ �� UpdateSourceTrigger$ PropertyChanged=�52   3   �� 53   2   
���   �6<   �� 54   2   $ 
Horizontal=�6=   .+�55   6   ��   RoundedButtonStyle#��[ 6=   ��6i   ��  �� RelativeSource �� 	 �� AncestorType$	 ListBoxL�
 �� Path$
 DataContext.DeleteItem�.��56   :   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Delete=�6R   57   7   �� 58   6   #��[ 6=   ��6i   ��  �� $	 ListBoxL�$
 DataContext.OpenFullInfo�.��59   :   �� $ 
Horizontal=�6E   .+�5:   :     $ ReadMore=�6R   �� 5;   :     More#���  6D   5<   ;   5=   7   5>   3   5?   /   5@   +   5A   '   5B   #   5C      ��5E      �� 5F       �� 
TargetType" ��6$    �� BasedOn# [B��5G       �� Triggers 5H   "   P� 5I   &    P� Property �@J�IsKeyboardFocusWithin P� Value . . �5J   *   ��  �� Property �@��
IsSelected �� Value . 6G   5K   '   5M      ��5O      �� 5P      .R�5Q   "   � 5R      5T      5U      5V      5W      � 5X   
   $��1��6   .+�5Y        $��10,10,10,10q�6"   $ Depth4=�67   $ 5Q�6h   .��5Z      �  � ColumnDefinitions 5[      �� 5\      $
��388* �6+   �� 5]      $
��203* �6+   �� 5^      $
��189* �6+   .+�5`      � $��0��6   .+�5a      �� $ Vertical=�6%   .+�5b      ��   Buy#���  6!   $	��200��6`   $��25��6l   $��Center=�6x   $��0,0,0,10q�6�   #��[ 6�   ��6A   �� *
BuyCommand+6�   �� 5c      $��20��6(   
���  ��$��12(�6c   ��64   �� *ErrorMessage+6c   5d      5e      � 5f      $��2��6   .+�5g      �� $ 
Horizontal=�6%   $	��Top=�6>   $��0,5,0,0 q�6V   .+�5h      ��   Delivery#���  6(   �� 5i      $	��105��6t    6'   �� *SelectedDelivery+��6Q   �� *
Deliveries+.  (System.Windows.Interactivity.Interaction   Triggers 5j      /  )System.Windows.Interactivity.EventTrigger  5k   "      	EventName$ SelectionChanged��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5l   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 6<   �� *DeliveryChanged+5m   #   ��5o      �� 5p   "   .R�5q   &   �� $��0q�6F   $��Bottom=�6Q   $��Center=�5r   (   ��5q   0   �� *Name+5r   (   5s   #   5u      5v      5w      � 5x      $��1��6   .+�5y      �� $ 
Horizontal=�6%   $	��Top=�6>   $��0,5,0,0q�6V   .+�5z      ��  	 Summary#��� 	 6(   �� 5{      ��6(   �� *	Summary+5|      5}      5~      5      5�      5�      5�      