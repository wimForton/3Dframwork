   M S B A M L   `   `   `  ����`  \RibbonControlsLibrary, Version=4.0.0.11019, Culture=neutral, PublicKeyToken=31bf3856ad364e35tNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary!Microsoft.Windows.Controls.Ribbon  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   RibbonControlStyle<    6Microsoft.Windows.Controls.Ribbon.RibbonControlService  �� Property �� Value �� Triggers �� Binding �� Value P� Property P� Value.   (Microsoft.Windows.Controls.KeyTipService3   -Microsoft.Windows.Controls.Ribbon.RibbonGroup �� 
TargetType �� BasedOn4   .Microsoft.Windows.Controls.Ribbon.RibbonButton:   4Microsoft.Windows.Controls.Ribbon.RibbonToggleButton9   3Microsoft.Windows.Controls.Ribbon.RibbonRadioButton6   0Microsoft.Windows.Controls.Ribbon.RibbonCheckBox5   /Microsoft.Windows.Controls.Ribbon.RibbonTextBox8   2Microsoft.Windows.Controls.Ribbon.RibbonMenuButton9	   3Microsoft.Windows.Controls.Ribbon.RibbonSplitButton6
   0Microsoft.Windows.Controls.Ribbon.RibbonComboBox6   0Microsoft.Windows.Controls.Ribbon.RibbonMenuItem  FilterMenuItemStyle5   /Microsoft.Windows.Controls.Ribbon.RibbonGallery	 �� 	Resources=   7Microsoft.Windows.Controls.Ribbon.RibbonGalleryCategory;   5Microsoft.Windows.Controls.Ribbon.RibbonSplitMenuItemA   ;Microsoft.Windows.Controls.Ribbon.RibbonApplicationMenuItem
 � RowDefinitions �� 
FontWeightF   @Microsoft.Windows.Controls.Ribbon.RibbonApplicationSplitMenuItem9   3Microsoft.Windows.Controls.Ribbon.RibbonContextMenu:   4Microsoft.Windows.Controls.Ribbon.RibbonControlGroup  BoolToVisibilityConverter@   :Microsoft.Windows.Controls.Ribbon.RibbonContextualTabGroup �� 	Converter1   +Microsoft.Windows.Controls.Ribbon.RibbonTab  ContextualTabStyle�� 5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml     5      YribbonNclr-namespace:Microsoft.Windows.Controls.Ribbon;assembly=RibbonControlsLibrary  5      %!  &	        '  )    7   '  �    7   '      7   '  �    7   '  F    7   '  �    7   '  i    7   '	  �	    7 '
  #    7 &	 �    '      7  '  g    '  �    '  �    7 '  �    7 '      7 '  1    '  �    &	 �    '  �    7  &	 �    �� 5      .�5   
   ��   �@  Label 6>   �� *Label+�� 5   
     �@  LargeImageSource 6I   �� *
LargeImage+�� 5	   
     �@  SmallImageSource 6I   �� *
SmallImage+�� 5
   
     �@  ToolTipTitle 6E   �� *ToolTipTitle+�� 5   
     �@  ToolTipDescription 6K   �� *ToolTipDescription+�� 5   
     �@  ToolTipImageSource 6K   �� *ToolTipImage+�� 5   
     �@  ToolTipFooterTitle 6K   �� *ToolTipFooterTitle+�� 5   
      �@  ToolTipFooterDescription 6Q   �� *ToolTipFooterDescription+�� 5   
      �@  ToolTipFooterImageSource 6Q   �� *ToolTipFooterImage+ 5   
   �� 5       6   �� *
LargeImage+ 69   Z� .a�5      ��   �@  LargeImageSource 6Q   Z� 5      �� 5       6   �� *
SmallImage+ 69   Z� .a�5      ��   �@  SmallImageSource 6Q   Z� 5      �� 5       6   �� *ToolTipImage+ 6;   Z� .a�5      ��   �@  ToolTipImageSource 6S   Z� 5      �� 5       6   �� *ToolTipFooterImage+ 6A   Z� .a�5      ��    �@  ToolTipFooterImageSource 6Y   Z� 5      P� 5       �@  IsInQuickAccessToolBar .  . �5      ��   �@ KeyTip 69   �� *KeyTip+5      5!      �� 5$      "  6   8   .�5%   
   ��   �@ QuickAccessToolBarId 61   �� *Label+�� 5&   
     � �� 6#   �� *Label+5'      �� 5*      "  6   8   .�5+   
   ��   � �� 6$   �� *	Command+5,      �� 5/      "  6   8   .�50   
   ��   � �� 6$   �� *	Command+�� 51   
     �@d�	IsChecked 6&   �� *	IsChecked+52      �� 55      "  6   8   .�56   
   ��   � �� 6$   �� *	Command+�� 57   
     �@d�	IsChecked 6&   �� *	IsChecked+58      �� 5;      "  6   8   .�5<   
   ��   � �� 6$   �� *	Command+�� 5=   
     �@d�	IsChecked 6&   �� *	IsChecked+5>      �� 5A      "  6   8   .�5B   
   ��   �@ Command 6$   �� *	Command+�� 5C   
     � �� 6!   �� *Text+5D      �� 5G      "  6   8   .�5H   
   ��   �@ QuickAccessToolBarId 61   �� *	Command+�� 5I   
     �@ CanUserResizeVertically 64   �� *IsVerticallyResizable+�� 5J   
   !  �@ CanUserResizeHorizontally 66   �� *IsHorizontallyResizable+5K      �� 5N      " 	 6   8   .�5O   
   ��   �@	 IsCheckable 6(   �� *IsCheckable+�� 5P   
     �@	 	IsChecked 6&   �� *	IsChecked+�� 5Q   
     �@	 Command 6$   �� *	Command+�� 5R   
   "  �@	 HeaderQuickAccessToolBarId 67   �� *Label+�� 5S   
     �@	 DropDownToolTipTitle 61   �� *!DropDownButtonData.ToolTipTitle+�� 5T   
   "  �@	 DropDownToolTipDescription 67   �� *'%DropDownButtonData.ToolTipDescription+�� 5U   
   "  �@	 DropDownToolTipImageSource 67   �� *!DropDownButtonData.ToolTipImage+�� 5V   
   "  �@	 DropDownToolTipFooterTitle 67   �� *'%DropDownButtonData.ToolTipFooterTitle+�� 5W   
   (  �@	  DropDownToolTipFooterDescription 6=   �� *-+DropDownButtonData.ToolTipFooterDescription+�� 5X   
   (  �@	  DropDownToolTipFooterImageSource 6=   �� *'%DropDownButtonData.ToolTipFooterImage+5Y      �� 5\      " 
 6   8   .�5]   
   ��   �@
 
IsEditable . 6'   �� 5^   
     �@ CanUserResizeVertically . 64   5_      �� 5b      "  6   .�5c   
   ��   � �� 6#   �� *Label+5d      �� 5g      "  6   	 5h   
   ���� 5l      '        "  6   5m      .�5n   
   ��   �@  SmallImageSource 6I   �� *
SmallImage+�� 5o   
     � �� 6(   �� *CategoryDataCollection+�� 5p   
     �@ Command 6$   �� *	Command+�� 5q   
     �@ SelectedItem 6)   �� *SelectedItem+�� 5r   
     �@ CanUserFilter 6*   �� *CanUserFilter+�� 5s   
      �@ FilterItemContainerStyle8   65    5t   
   �� 5u       6   �� *
SmallImage+ 69   Z� .a�5v      ��   �@  SmallImageSource 6Q   Z� 5w      5y      �� 5|      "  6   .�5}   
   ��   � �� 6#   �� *Label+�� 5~   
     � �� 6(   �� *GalleryItemDataCollection+5      �� 5�      "  6   .�5�   
   ��   � �� 6#   �� *Label+�� 5�   
     �@ ImageSource 6(   �� *
SmallImage+�� 5�   
     �@  ToolTipTitle 6E   �� *ToolTipTitle+�� 5�   
     �@  ToolTipDescription 6K   �� *ToolTipDescription+�� 5�   
     �@  ToolTipImageSource 6K   �� *ToolTipImage+�� 5�   
     �@  ToolTipFooterTitle 6K   �� *ToolTipFooterTitle+�� 5�   
      �@  ToolTipFooterDescription 6Q   �� *ToolTipFooterDescription+�� 5�   
      �@  ToolTipFooterImageSource 6Q   �� *ToolTipFooterImage+�� 5�   
     �@ KeyTip 61   �� *KeyTip+�� 5�   
     �@m�Command 6$   �� *	Command+�� 5�   
     �@m�IsCheckable 6(   �� *IsCheckable+�� 5�   
     �@m�	IsChecked 6&   �� *	IsChecked+�� 5�   
     �@ CanUserResizeVertically 64   �� *IsVerticallyResizable+�� 5�   
   !  �@ CanUserResizeHorizontally 66   �� *IsHorizontallyResizable+ 5�   
   �� 5�       6   �� *
SmallImage+ 69   Z� .a�5�      ��   �@ ImageSource 60   Z� 5�      �� 5�       6   �� *ToolTipImage+ 6;   Z� .a�5�      ��   �@  ToolTipImageSource 6S   Z� 5�      �� 5�       6   �� *ToolTipFooterImage+ 6A   Z� .a�5�      ��    �@  ToolTipFooterImageSource 6Y   Z� 5�      5�      �� 5�      "  6   8   .�5�   
   �� "  �@ HeaderQuickAccessToolBarId 67   �� *	Command+�� 5�   
     �@ QuickAccessToolBarId 61   �� *DropDownButtonData.Command+�� 5�   
     �@ HeaderKeyTip 6)   �� *KeyTip+�� 5�   
     �@ KeyTip 6#   �� *DropDownButtonData.KeyTip+�� 5�   
     �@ DropDownToolTipTitle 61   �� *!DropDownButtonData.ToolTipTitle+�� 5�   
   "  �@ DropDownToolTipDescription 67   �� *'%DropDownButtonData.ToolTipDescription+�� 5�   
   "  �@ DropDownToolTipImageSource 67   �� *!DropDownButtonData.ToolTipImage+�� 5�   
   "  �@ DropDownToolTipFooterTitle 67   �� *'%DropDownButtonData.ToolTipFooterTitle+�� 5�   
   (  �@  DropDownToolTipFooterDescription 6=   �� *-+DropDownButtonData.ToolTipFooterDescription+�� 5�   
   (  �@  DropDownToolTipFooterImageSource 6=   �� *'%DropDownButtonData.ToolTipFooterImage+ 5�   
   �� 5�       6   �� *!DropDownButtonData.ToolTipImage+ 6N   Z� .a�5�      �� "  �@ DropDownToolTipImageSource 6?   Z� 5�      �� 5�       6   �� *'%DropDownButtonData.ToolTipFooterImage+ 6T   Z� .a�5�      �� (  �@  DropDownToolTipFooterImageSource 6E   Z� 5�      5�      �� 5�      "  6   8    5�   
   P� 5�       �@ Level$ Middle=�6'   . �5�      ��   �@ ImageSource 60   �� *
SmallImage+�� 5�        �@ KeyTip 69   �� *KeyTip+�� 5�        �@m�Command 6,   �� *	Command+�� 5�        � �� 6+   �� �� 5�        � �� 5�      �� 5�      .R�5�      � 
 5�   "   �� 5�   &   $
��Auto �64   �� 5�   &   $
��Auto �64   .+�5�   "   �� $��0��6-   $
 Bold"�6:   ��6L   �� *Label+�� 5�   "   $��1��6,   $
��Wrap=�6W   ��69   �� *ToolTipTitle+6W   5�      5�      5�      5�      5�      �� 5�      "  6   8   6H   �� 5�      "  6   	 5�   
   ���� 5�      '        "  6   5�      5�      �� 5�      "  6   .�5�   
   ��   � ��$ 2,0,2,0q�6#   5�      �� 5�      6#   �� 5�      "  6   .�5�   
   ��   �@ Header 6#   �� *Header+�� 5�   
     � y� 6'   �� *	IsVisible+8   5�      �� 5�      "  6   .�5�   
   ��    �@ ContextualTabGroupHeader 65   �� *ContextualTabGroupHeader+�� 5�   
     � �� 6#   �� *Header+�� 5�   
     �@ 
IsSelected 6'   �� *
IsSelected+5�      5�      