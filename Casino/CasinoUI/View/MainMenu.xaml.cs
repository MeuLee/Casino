using CasinoUI.Model.Cards;
using System;
using System.Drawing;
﻿using CasinoUI.View;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CasinoUI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameCardStack _cards = new GameCardStack();
        public MainWindow()
        {
            //Button btn = new Button();
            //btn.Name = "Button";
            //btn.Click += button_Click;
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CasinoGame game = new CasinoGame();
            game.Show();
            this.Close();
        }
    }
}
