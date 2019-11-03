using CasinoUI.Models;
using CasinoUI.Models.WindowModels;
using CasinoUI.Utils;
using CasinoUI.Views;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CasinoUI.Controllers
{
    public class OptionsMenuController
    {
        private static readonly BitmapImage _backImg = Properties.Resources.backArrow.ToBitmapImage();
        private static readonly BitmapImage _casinoChip1 = Properties.Resources.PokerEntrer.ToBitmapImage();
        private static readonly BitmapImage _casinoChip2 = Properties.Resources.redChip.ToBitmapImage();

        private static Thickness _smallThick = new Thickness(3);
        private static Thickness _bigThick = new Thickness(1);
        private OptionsMenuModel _model;
        private OptionsMenu _view;
        private Window _parent;

        public OptionsMenuController(Window parent)
        {
            _model = new OptionsMenuModel();
            _view = new OptionsMenu();
            _parent = parent;
            _parent.Hide();
            _view.Show();

            AddEvents();
            ModifySlidersValue();
            TabControl_SelectionChanged(null, null); // The event doesn't fire when it's the first time, has to be called manually
        }

        private void AddEvents()
        {
            _view.BtnBack.MouseEnter += BtnBack_MouseEnter;
            _view.BtnBack.MouseLeave += BtnBack_MouseLeave;
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
            _view.ImgBack.Margin = _bigThick;
        }

        private void BtnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            _view.ImgBack.Margin = _smallThick;
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
            sp.Children.OfType<TextBlock>().First().FontSize = 22;
            sp.Children.OfType<Image>().ToList().ForEach(i => i.Source = _casinoChip2);
        }

        private void Header_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
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
