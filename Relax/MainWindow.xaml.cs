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

        public MainWindow()
        {
            InitializeComponent();
        }

        List<AmbientSoundControl> controls = new List<AmbientSoundControl>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (UIElement c in this.mainGrid.Children)
            { 
                if (c is AmbientSoundControl)
                {
                    controls.Add((AmbientSoundControl) c);
                }
            }

            this.SetEngine();
        }

        private void SetEngine()
        {
            foreach (AmbientSoundControl c in controls)
                c.Engine = this.engine;
        }

        public static void Shuffle(IList list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                object value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void Randomize(int count)
        {
            Shuffle(controls);
            StopAllSounds();
            int picked = 0;
            foreach (AmbientSoundControl c in controls)
            {
                if (c.MaybePlay()) picked++;
                if (picked == count) break;
            }
            if (picked == 0)
                Randomize(count);
        }

        private void StopAllSounds()
        {
            foreach (AmbientSoundControl c in controls)
            {
                c.EnableThisSound.IsChecked = false;
            }
        }

        private void sldMasterVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.engine.SoundVolume = (float)sldMasterVolume.Value / 100.0f;
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            if (iudRandom.Value.HasValue)
                Randomize((int)iudRandom.Value);
        }

    }
}
