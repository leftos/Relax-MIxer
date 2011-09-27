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

namespace Relax
{
    /// <summary>
    /// Interaction logic for AmbientSoundControl.xaml
    /// </summary>
    public partial class AmbientSoundControl : UserControl
    {
        public ISoundEngine Engine { get; set; }
        public ISound Sound { get; protected set; }

        private string _internalName;
        public string InternalName
        {
            get
            {
                return this._internalName;
            }
            set
            {
                this._internalName = value;
            }
        }
        public string FriendlyName
        {
            get
            {
                return this.EnableThisSound.Content.ToString();
            }
            set
            {
                this.EnableThisSound.Content = value;
            }
        }
        public string FileName
        {
            get
            {
                return "res/{f}.ogg".Replace("{f}", InternalName);
            }
        }
        public float Volume
        {
            get
            {
                if (this.Sound != null) return this.Sound.Volume;
                else return 0;
            }
            set
            {
                if (this.Sound != null) this.Sound.Volume = value;
            }
        }

        public AmbientSoundControl()
        {
            InitializeComponent();
        }
        public AmbientSoundControl(ISoundEngine engine, string internalName, string friendlyName) : this()
        {
            this.Engine = engine;
            this.InternalName = internalName;
            this.FriendlyName = friendlyName;
        }

        private void Play()
        {
            //try
            //{
                this.Sound = this.Engine.Play2D(this.FileName, true, true);
                CheckVolume();
                this.Sound.Paused = false;
            //}
            //catch
            //{
            //}
        }
        private void Stop()
        {
            this.Sound.Stop();
        }

        private void EnableThisSound_Checked(object sender, RoutedEventArgs e)
        {
            this.Play();
        }
        private void EnableThisSound_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Stop();
        }

        private void VolumeOfThisSound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CheckVolume();
        }

        private void CheckVolume()
        {
            this.Volume = (float)this.VolumeOfThisSound.Value / 100.0f;
        }

        public bool MaybePlay()
        {
            this.EnableThisSound.IsChecked = new Random().Next(2) == 0 && this.KeepThisSound.IsChecked.Value;
            return (bool)this.EnableThisSound.IsChecked;
        }
    }
}
