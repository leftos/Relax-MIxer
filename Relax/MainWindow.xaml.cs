using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IrrKlang;
using System.Collections;

namespace Relax
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected ISoundEngine engine = new ISoundEngine();
        protected ISound sndAlarm;
        protected ISound sndAC;
        protected ISound sndAquarium;
        protected ISound sndAtmospheric;
        protected ISound sndBasstone;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void chkAlarm_Checked(object sender, RoutedEventArgs e)
        {            
            sndAlarm = engine.Play2D("res/Alarm.ogg", true, true);
            sndAlarm.Volume = (float)sldAlarm.Value / 100.0f;
            sndAlarm.Paused = false;
        }

        private void chkAlarm_Unchecked(object sender, RoutedEventArgs e)
        {
            sndAlarm.Stop();
        }

        private void sldAlarm_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sndAlarm != null)
            {
                sndAlarm.Volume = (float)sldAlarm.Value / 100.0f;
            }
        }

        private void chkAC_Checked(object sender, RoutedEventArgs e)
        {
            sndAC = engine.Play2D("res/Airconditioner.ogg", true, true);
            sndAC.Volume = (float)sldAC.Value / 100.0f;
            sndAC.Paused = false;
        }

        private void chkAC_Unchecked(object sender, RoutedEventArgs e)
        {
            sndAC.Stop();
        }

        private void sldAC_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sndAC != null)
            {
                sndAC.Volume = (float)sldAC.Value / 100.0f;
            }
        }

        private void chkAquarium_Checked(object sender, RoutedEventArgs e)
        {
            sndAquarium = engine.Play2D("res/Aquarium.ogg", true, true);
            sndAquarium.Volume = (float)sldAquarium.Value / 100.0f;
            sndAquarium.Paused = false;
        }

        private void chkAquarium_Unchecked(object sender, RoutedEventArgs e)
        {
            sndAquarium.Stop();
        }

        private void sldAquarium_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sndAquarium != null)
            {
                sndAquarium.Volume = (float)sldAquarium.Value / 100.0f;
            }
        }

        private void sldMasterVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            engine.SoundVolume = (float)sldMasterVolume.Value / 100.0f;
        }

        private void chkAtmospheric_Checked(object sender, RoutedEventArgs e)
        {
            sndAtmospheric = engine.Play2D("res/Atmospheric.ogg", true, true);
            sndAtmospheric.Volume = (float)sldAtmospheric.Value / 100.0f;
            sndAtmospheric.Paused = false;
        }

        private void chkAtmospheric_Unchecked(object sender, RoutedEventArgs e)
        {
            sndAtmospheric.Stop();
        }

        private void sldAtmospheric_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sndAtmospheric != null)
            {
                sndAtmospheric.Volume = (float)sldAtmospheric.Value / 100.0f;
            }
        }

        private void chkBasstone_Checked(object sender, RoutedEventArgs e)
        {
            sndBasstone = engine.Play2D("res/Basstone.ogg", true, true);
            sndBasstone.Volume = (float)sldBasstone.Value / 100.0f;
            sndBasstone.Paused = false;
        }

        private void chkBasstone_Unchecked(object sender, RoutedEventArgs e)
        {
            sndBasstone.Stop();
        }

        private void sldBasstone_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sndBasstone != null)
            {
                sndBasstone.Volume = (float)sldBasstone.Value / 100.0f;
            }
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            ArrayList keeps = new ArrayList();

            if (keepAC.IsChecked == true) { keeps.Add("AC"); }
            if (keepAlarm.IsChecked == true) { keeps.Add("Alarm"); }
            if (keepAquarium.IsChecked == true) { keeps.Add("Aquarium");  }
            if (keepAtmospheric.IsChecked == true) { keeps.Add("Atmospheric");  }
            if (keepBasstone.IsChecked == true) { keeps.Add("Basstone");  }

            // Choose a random number of items from the ones kept for random choice
            if (keeps.Count == 0) return;

            chkAC.IsChecked = false;
            chkAlarm.IsChecked = false;
            chkAquarium.IsChecked = false;
            chkAtmospheric.IsChecked = false;
            chkBasstone.IsChecked = false;

            Random randomGen = new Random();
            int count = randomGen.Next(keeps.Count) + 1;

            ArrayList choices = new ArrayList(count);
            int choice;

            for (int i = 0; i < count; i++)
            {
                choice = randomGen.Next(keeps.Count);
                switch ((string)keeps[choice])
                {
                    case "AC":
                        chkAC.IsChecked = true;
                        break;
                    case "Alarm":
                        chkAlarm.IsChecked = true;
                        break;
                    case "Aquarium":
                        chkAquarium.IsChecked = true;
                        break;
                    case "Atmospheric":
                        chkAtmospheric.IsChecked=true;
                        break;
                    case "Basstone":
                        chkBasstone.IsChecked = true;
                        break;
                }
            }
        }
    }
}
