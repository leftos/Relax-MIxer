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
using System.IO;

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
            foreach (UIElement c in this.soundGrid.Children)
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
            // Increase number of sounds picked, still keeps it under count
            if (picked < count)
                Randomize(count-picked);
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

        private void mnuProfilesLoad_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".rmp";
            ofd.AddExtension = true;
            ofd.Filter = "Relax Mixer profiles (*.rmp)|*.rmp";

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                string filename = ofd.FileName;

                TextReader tr = new StreamReader(filename);

                string temp;
                string cname;
                float cvolume;

                try
                {
                    temp = tr.ReadLine();

                    if (temp == "Relax Mixer Profile")
                    {
                        StopAllSounds();
                        
                        while ((temp = tr.ReadLine()) != null)
                        {
                            string[] attrs = temp.Split(' ');
                            cname = attrs[0];
                            cvolume = (float)Convert.ToDouble(attrs[1]);

                            foreach (AmbientSoundControl c in controls)
                            {
                                if (c.Name == cname)
                                {
                                    c.VolumeOfThisSound.Value = cvolume * 100;
                                    c.EnableThisSound.IsChecked = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    
                }
            }
        }

        private void mnuProfilesSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.DefaultExt = ".rmp";
            sfd.Filter = "Relax Mixer profiles (*.rmp)|*.rmp";
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;
            sfd.ValidateNames = true;

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                string filename = sfd.FileName;

                StreamWriter tw = new StreamWriter(filename);

                tw.WriteLine("Relax Mixer Profile");
                
                foreach (AmbientSoundControl c in controls)
                {
                    if (c.EnableThisSound.IsChecked == true)
                    {
                        string temp = c.Name + " " + c.Volume;
                        tw.WriteLine(temp);
                    }
                }

                tw.Close();
            }
        }

    }
}
