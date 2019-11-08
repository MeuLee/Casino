using CasinoUI.Models.PlayerModel.PlayerSkin;
using CasinoUI.Models.Settings;
using CasinoUI.Models.WindowModels;
using CasinoUI.Utils;
using CasinoUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CasinoUI.Controllers
{
    public class OptionsMenuController
    {
        private static readonly BitmapImage _backImg = Properties.Resources.backArrow.ToBitmapImage();
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();

        private static Thickness _bigThick = new Thickness(3);
        private static Thickness _smallThick = new Thickness(1);
        private static Brush _mouseOverBrush = Brushes.White;
        private static Brush _mouseLeaveBrush = Brushes.RosyBrown;
        private OptionsMenuModel _model;
        private OptionsMenu _view;
        private Window _parent;
        private List<Skin> _skins = SkinManager.Instance.SkinsList;
        private int _skinIndex = 0;
        private static int _skinCount = SkinsGenerator.SKIN_COUNT;

        public OptionsMenuController(Window parent)
        {
            _model = new OptionsMenuModel();
            _view = new OptionsMenu();
            _parent = parent;
            _parent.Hide();
            _view.Show();
            _skinIndex = 0;

            AddEvents();
            ModifySlidersValue();
            TabControl_SelectionChanged(null, null); // The event doesn't fire when it's the first time, has to be called manually
            ModifySkinsTab();
        }

        private void ModifySkinsTab()
        {
            SetPictureIndexes(out int leftIndex, out int centerIndex, out int rightIndex);
            _view.TbCharacterName.Text = _skins[centerIndex].Name;
            _view.ImgLeftCharacter.Source = _skins[leftIndex].DownImages[0];
            _view.ImgCenterCharacter.Source = _skins[centerIndex].DownImages[0];
            _view.ImgRightCharacter.Source = _skins[rightIndex].DownImages[0];
            ApplicationSettings.HumanPlayer.CurrentSkin = _skins[centerIndex];
        }

        private void SetPictureIndexes(out int leftIndex, out int centerIndex, out int rightIndex)
        {
            leftIndex = 0 > _skinIndex - 1 ? _skinCount - 1 :_skinIndex - 1;
            centerIndex = _skinIndex;
            rightIndex = _skinIndex + 1 >= _skinCount ? 0 : _skinIndex + 1;
        }

        private void AddEvents()
        {
            _view.BtnBack.MouseEnter += BtnBack_MouseEnter;
            _view.BtnBack.MouseLeave += BtnBack_MouseLeave;
            _view.BtnBack.MouseEnter += Btn_MouseOver;
            _view.BtnBack.MouseLeave += Btn_MouseLeave;
            _view.BtnBack.Click += BtnBack_Click;

            foreach (var tabItem in _view.TabControl.Items.OfType<TabItem>())
            {
                var header = tabItem.Header as StackPanel;
                header.MouseEnter += Header_MouseEnter;
                header.MouseLeave += Header_MouseLeave;
            }
            _view.TabControl.SelectionChanged += TabControl_SelectionChanged;
            _view.SldSong.ValueChanged += SldSong_ValueChanged;
            _view.SldSFX.ValueChanged += SldSFX_ValueChanged;
            _view.BtnLeft.MouseEnter += Btn_MouseOver;
            _view.BtnRight.MouseEnter += Btn_MouseOver;
            _view.BtnLeft.MouseLeave += Btn_MouseLeave;
            _view.BtnRight.MouseLeave += Btn_MouseLeave;
            _view.SldSFX.MouseEnter += Slider_MouseEnter;
            _view.SldSong.MouseEnter += Slider_MouseEnter;
            _view.SldSFX.MouseLeave += Slider_MouseLeave;
            _view.SldSong.MouseLeave += Slider_MouseLeave;
            _view.BtnLeft.Click += BtnLeft_Click;
            _view.BtnRight.Click += BtnRight_Click;
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            _skinIndex = (_skinIndex + 1) % _skinCount;
            ModifySkinsTab();

        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            _skinIndex = (_skinIndex - 1) % _skinCount;
            if (0 > _skinIndex)
            {
                _skinIndex += _skinCount;
            }
            ModifySkinsTab();
        }

        private void Slider_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Slider).Cursor = Cursors.Hand;
        }

        private void Slider_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Slider).Cursor = Cursors.SizeWE;
        }

        private void Btn_MouseOver(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.BorderBrush = _mouseOverBrush;
            btn.Cursor = Cursors.Hand;
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            btn.BorderBrush = _mouseLeaveBrush;
            btn.Cursor = Cursors.Arrow;
        }

        private void SldSong_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider sld = sender as Slider;
            _model.ModifySongVolume(sld.Value);
        }

        private void SldSFX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider sld = sender as Slider;
            _model.ModifySFXVolume(sld.Value);
        }

        private void BtnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Cursor = Cursors.Hand;
            _view.ImgBack.Margin = _smallThick;
        }

        private void BtnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Cursor = Cursors.Arrow;
            _view.ImgBack.Margin = _bigThick;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            _view.Close();
        }

        private void Header_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            if (_view.TabControl.SelectedItem == sp.Parent) return;
            sp.Cursor = Cursors.Hand;
            sp.Children.OfType<TextBlock>().First().FontSize = 22;
            sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip2);
        }

        private void Header_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            sp.Cursor = Cursors.Arrow;
            if (_view.TabControl.SelectedItem == sp.Parent) return;
            sp.Children.OfType<TextBlock>().First().FontSize = 18;
            sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var tab in _view.TabControl.Items.OfType<TabItem>())
            {
                StackPanel sp = tab.Header as StackPanel;
                if (tab == _view.TabControl.SelectedItem)
                {
                    sp.Children.OfType<TextBlock>().First().FontSize = 22;
                    sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip2);
                }
                else
                {
                    sp.Children.OfType<TextBlock>().First().FontSize = 18;
                    sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip1);
                }
            }
        }

        private void ModifySlidersValue()
        {
            _view.SldSong.Value = ApplicationSettings.SoundPlayer.SongVolume;
            _view.SldSFX.Value = ApplicationSettings.SoundPlayer.SFXVolume;
        }
    }
}
