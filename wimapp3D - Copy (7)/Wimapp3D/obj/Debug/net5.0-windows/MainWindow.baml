   M S B A M L   `   `   `  ����C  ?Wimapp3D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Wimapp3DWimapp3D  5   	       Wimapp3D.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Wimapp3D     5   	     � Title$  
MainWindow��5   	   $	��720��6   $
��1024��6)   ���	#FF787878.��5
      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��1* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��350 �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� -   5        $�� Obj loader ��5      $
��Left=�6]   $��5,5,0,0q�5      $	��Top=�6&   $��20��6>   ���	#FF303030
�������5   ,   �� 5      -   5        $�� Multiprim ��5      $
��Left=�6U   $��5,5,0,0q�5      $	��Top=�6&   $��20��6>   ���	#FF303030
�������5   ,   �� 5      -   5        $�� Twist ��5      $
��Left=�6M   $��5,5,0,0q�5      $	��Top=�6&   $��20��6>   ���	#FF303030
�������5   ,   �� 5      -   5         ��Name$ TimeText��5      $��	FrameNr: ��6*   
�������$��10,5,0,0q�6O   $��15(�6a   5       �� 5!   
   $��0��6   $��1��6"   $ 
Horizontal=�62   .+�5"      �� -   5        $�� Layout Nodes ��5"      $��5,5,5,0q�5#      $	��Top=�6&   $��20��6>   ���	#FF303030
�������$��1��5$   ?   �� 5%      -   5        $�� Close proporties ��5%      $��5,5,5,0q�5&      $	��Top=�6&   $��20��6>   ���	#FF303030
�������$��1��5'   ?   5(      �� 5)   
   $
��Auto=�6   $��1��6:   $��1��6G   .��5*      �� -   5         ��Name$ ProportieWindowStack��5*      ���	#FF616060 �� CanVerticallyScroll$
 True��6N   5,      5-      �� 5.   
   -   5         ��Name$! MainWindowCanvasConnections��5.      $��1��6   ���	#FF2B2B2B$
}�True��6Z   52      �� 54   
   -	   5        $ MainWindowCanvas��54      $��1��6   $
}�True��68   58      ]� 59   
   -
   5         ]�Name$ TimeSliderStack��59   ,   ���	#FF5B5B5B$��2��6E   $��2��6R   5;      5<      5>      