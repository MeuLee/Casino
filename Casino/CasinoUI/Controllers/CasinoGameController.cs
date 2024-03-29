﻿using CasinoUI.Models.Settings;
using CasinoUI.Models.PlayerModel;
using CasinoUI.Models.WindowModels;
using CasinoUI.Views;
using CasinoUI.Views.Map.Tiles;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CasinoUI.Controllers
{
    public class CasinoGameController
    {
        private Window _parent;
        public CasinoGame View { get; private set; }
        public CasinoGameModel Model { get; private set; }

        private static Thickness _smallThick = new Thickness(3);
        private static Thickness _bigThick = new Thickness(1);
        public CasinoGameController(Window parent)
        {
            _parent = parent;
            Model = new CasinoGameModel();
            View = new CasinoGame();
            View.Show();
            _parent.Hide();
            ApplicationSettings.SoundPlayer.Start();
            ModifyUIElementsWithSkin();
            AddEvents();
            RaiseEvents();
        }

        private void ModifyUIElementsWithSkin()
        {
            var human = ApplicationSettings.HumanPlayer;
            View.LblPlayerName.Text = human.Name;
            View.ImgPlayer.Source = human.CurrentImage;
        }

        private void AddEvents()
        {
            View.BtnBack.MouseEnter += BtnBack_MouseEnter;
            View.BtnBack.MouseLeave += BtnBack_MouseLeave;
            View.BtnBack.Click += BtnBack_Click;
            View.Closed += View_Closed;
            View.KeyDown += View_KeyDown;
            View.PgBarAlcohol.ValueChanged += ProgressBar_ValueChanged;
            View.PgBarStress.ValueChanged += ProgressBar_ValueChanged;
        }

        private void View_Closed(object sender, EventArgs e)
        {
            ApplicationSettings.SoundPlayer.Stop();
        }

        private void BtnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            View.ImgBack.Margin = _bigThick;
        }

        private void BtnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            View.ImgBack.Margin = _smallThick;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            View.Close();
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            HumanPlayer player = ApplicationSettings.HumanPlayer;
            int oldPlayerX = player.X,
                oldPlayerY = player.Y;
            switch (e.Key)
            {
                case Key.A:
                    player.MoveLeft();
                    break;
                case Key.W:
                    player.MoveUp();
                    break;
                case Key.S:
                    player.MoveDown();                    
                    break;
                case Key.D:
                    player.MoveRight();                    
                    break;
                case Key.H:
                    View.PgBarAlcohol.Value--;
                    View.PgBarStress.Value--;
                    return;
                case Key.J:
                    View.PgBarAlcohol.Value++;
                    View.PgBarStress.Value++;
                    return;
                default:
                    return;
            }

            OnPlayerMoved(oldPlayerX, oldPlayerY); // TODO move this to set
        }

        public void OnPlayerMoved(int oldPlayerX, int oldPlayerY)
        {
            HumanPlayer player = ApplicationSettings.HumanPlayer;
            View.GameCanvas.InvalidateVisual();
            View.MiniMapCanvas.InvalidateVisual();
            ApplicationSettings.Map[player.X, player.Y]
                               .OnMovedOver?
                               .Invoke(this, new OnMovedOverEventArgs(oldPlayerX, oldPlayerY));
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressBar pgBar = sender as ProgressBar;
            Color c = Utils.ColorConverter.ColorFromHSL((100 - pgBar.Value), 1, 0.5);
            pgBar.Foreground = new SolidColorBrush(c);
        }

        private void RaiseEvents()
        {
            ProgressBar_ValueChanged(View.PgBarAlcohol, null);
            ProgressBar_ValueChanged(View.PgBarStress, null);
        }
    }
}
