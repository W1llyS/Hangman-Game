   M S B A M L   `   `   `  ����E  AHangManV01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:HangManV01
HangManV01  5   	       HangManV01.Rules   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:HangManV01     5   	     � Title$  Rules��5   	   $	��550��6   $	��830��6$    � Icon$ Images/icon.ico��60   .��5	      � ���#333333.+�5
   
   �� $
��Auto=�6   .��5      �� $��Center=�6   $
��Wrap=�65   $	��Top=�6I   $��30q�6a   
����������#333333$��16(�6�   �� 5      $
��Bold"�6   $��18(�6(   $�
Objective:��66   �� 6J   USThe player aims to guess a word or phrase before the hangman figure is fully drawn.�� 5      �� 6   �� 5      $
��Bold"�6   $��18(�6(   $�Setup:��66   �� 6F   ��A computer program randomly selects a word or phrase or gets one from another player, and displays blanks for each letter. For example, for the word "COMPUTER", it would display: _ _ _ _ _ _ _ _.�� 5      �� 6   �� 5      $
��Bold"�6   $��18(�6(   $�How to Play:��66   �� 6L   �� 5      $�1. Guessing Letters: ��6   {y The player guesses a letter of the word. If the letter is in the word, the program fills it in the appropriate blank(s).�� 6�   �� 5      $�2. Incorrect Guesses: ��6   MK For every incorrect letter guessed, a part of the hangman figure is drawn.�� 6�   �� 5      $
��Bold"�6   $�3. Winning or Losing:��6(   �� 6G   ge- Winning: The player wins by guessing all the letters in the word before the hangman is fully drawn.�� 5   w   jh- Losing: The player loses if the hangman is fully drawn before all the letters in the word are guessed.�� 5      �� 6   �� 5      $
��Bold"�6   $��18(�6(   $�Computer Features:��66   �� 6R   YW- A visual representation of the hangman structure gets updated with every wrong guess.�� 5!      �� 6   �� 5"      $
��Bold"�6   $��18(�6(   $�Tips:��66   �� 6E   (&- Use hint to make the game more easy.�� 5#   8   CA- The difficulty level can be adjusted by the length of the word.5%      5&      �� 5'   
   -   5        $
��Back��5'      $��Right=�6!   $	��Top=�6=   $��
0,469,60,0q�6U   $	��100��6i   $��30��6u   ���#FF0000���#FFFFFF$��16(�6�   6�   5(      5)      