using Metronome.Models;
using Metronome.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Metronome
{
    
    /// <summary>
    /// MainPage that contains the simplest version of the Metronome
    /// </summary>
    public sealed partial class MainPage : Page
    {        
        public ObservableCollection<Sound> Sounds;
        private List<TimeSignature> TimeSignatures;
        private MetronomeClass novo;

        public MainPage()
        {
            this.InitializeComponent();

            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);
            TimeSignatures = TimeSignatureManager.GetTimeSignatures();

            novo = new MetronomeClass(TimeSignatures.ElementAt(2).UppNumber, TimeSignatures.ElementAt(2).LowNumber, 120, this.BaseUri, this, MediaElemTick, MediaElemTock, TimeSignatures);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MyPane.MySplitView.IsPaneOpen = !MyPane.MySplitView.IsPaneOpen;            
        }           

        private void sliderBpm_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (novo != null)
            {
                novo.TimerCanceled = true;
                StartStopButton.Content = "Start";
                novo.Bpm = (int)sliderBpm.Value;
                novo.CalculateBpm(novo.Bpm);
                DisplayTxtBlock.Text = novo.Bpm.ToString();
            }
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (!novo.TimerCanceled)
            {
                StartStopButton.Content = "Start";
                novo.TimerCanceled = true;
            }
            else
            {
                novo.TimerCanceled = false;
                StartStopButton.Content = "Stop";
                novo.TimerControl(novo.UpperNum, novo.LowerNum);
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            novo.SelectedTimeSignature = (TimeSignature)e.ClickedItem;
            novo.UpperNum = novo.SelectedTimeSignature.UppNumber;
            novo.LowerNum = novo.SelectedTimeSignature.LowNumber;

            novo.TimerCanceled = true;
            StartStopButton.Content = "Start";
        }

        private void TimeSignatureListView_Loaded(object sender, RoutedEventArgs e)
        {
            var lv = sender as ListView;

            if (lv != null)
                lv.SelectedIndex = 2;
        }    

        private void DisplayTxtBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (novo != null)
            {
                int result;
                novo.TimerCanceled = true;
                StartStopButton.Content = "Start";
                if (int.TryParse(DisplayTxtBlock.Text, out result) && result >= 10 && result <= 250)
                {
                    novo.Bpm = result;
                    sliderBpm.Value = novo.Bpm;
                }
            }
        }

        private async void DisplayTxtBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            if (novo != null)
            {
                int keyPressed;
                if (int.TryParse(DisplayTxtBlock.Text, out keyPressed))
                {
                    if (keyPressed < 10 || keyPressed > 250)
                    {
                        StartStopButton.IsEnabled = false;
                        var dialog = new Windows.UI.Popups.MessageDialog("Por favor insira valores entre 10 e 250");
                        var dialogResult = await dialog.ShowAsync();
                    }
                    else StartStopButton.IsEnabled = true;
                }
                else
                {
                    StartStopButton.IsEnabled = false;
                    var dialog = new Windows.UI.Popups.MessageDialog("Por favor insira um valor numérico");
                    var dialogResult = await dialog.ShowAsync();
                }

            }
        }

        private void DisplayTxtBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            StartStopButton.IsEnabled = false;
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            
        }
    }
}
