using Metronome.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Metronome.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Composer : Page
    {
        public ObservableCollection<Sound> Sounds;
        private MetronomeClass novo;

        public Composer()
        {
            this.InitializeComponent();
            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);
            
            novo = new MetronomeClass(6, 8, 120, this.BaseUri, this, MediaElemTick, MediaElemTock);
            
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MyPane.MySplitView.IsPaneOpen = !MyPane.MySplitView.IsPaneOpen;
        }

        private void sliderBpm_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (novo != null)
            {
                novo.Bpm = (int)sliderBpm.Value;
                novo.CalculateBpm(novo.Bpm);
                DisplayTxtBlock.Text = novo.Bpm.ToString();             
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompassNumCombo == null || novo==null) return;
                        
            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;
            novo.CompassPickerComboBox = int.Parse(item.Content.ToString());
                        
        }
    }
}
