using Gma.System.MouseKeyHook;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Windows.Media;

namespace learnWpf
{
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents GlobalHook;
        private BitmapImage Idle = new BitmapImage(new Uri("assets/idle.png", UriKind.Relative));
        private BitmapImage RightHit = new BitmapImage(new Uri("assets/righthit.png", UriKind.Relative));
        private BitmapImage LeftHit = new BitmapImage(new Uri("assets/lefthit.png", UriKind.Relative));
        private BitmapImage DoubleHit = new BitmapImage(new Uri("assets/doublehit.png", UriKind.Relative));
        private Random RandomNumber = new Random();
        private MediaPlayer ClickSound = new MediaPlayer();
        private List<Keys> PressedKeys = new List<Keys>();
        private List<Keys> KeyList = new List<Keys>()
        {
            // Letters A-Z
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G,
            Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N,
            Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U,
            Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z,

            // Numbers 0-9 (top row)
            Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4,
            Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,

            // Symbols & punctuation keys
            Keys.Oem1,        // ; :
            Keys.Oem7,        // ' "
            Keys.Oemcomma,    // , <
            Keys.OemPeriod,   // . >
            Keys.OemQuestion, // / ?
            Keys.OemMinus,    // - _
            Keys.Oemplus,     // = +
            Keys.OemOpenBrackets,  // [ {
            Keys.Oem6,        // ] }
            Keys.Oem5,        // \ |
            Keys.Oemtilde     // ` ~
        };

        public MainWindow()
        {
            InitializeComponent();
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            Loaded += MainWindow_Loaded;
            ClickSound.Open(new Uri("assets/click.wav", UriKind.Relative));

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalHook = Hook.GlobalEvents();
            GlobalHook.KeyDown += GlobalHook_KeyDown;
            GlobalHook.KeyUp += GlobalHook_KeyUp;
        }

        private void GlobalHook_KeyUp(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyExists(e))
            {
                CurrentImage.Source = Idle;
            }
            PressedKeys.Remove(e.KeyCode);
        }

        private void GlobalHook_KeyDown(object? sender, System.Windows.Forms.KeyEventArgs e)
        {
            int randomBongo = RandomNumber.Next(1, 3);
            if (KeyExists(e))
            {
                ClickSound.Position = TimeSpan.Zero;
                ClickSound.Play();
                if (randomBongo == 1)
                {
                    CurrentImage.Source = LeftHit;
                }
                else if (randomBongo == 2)
                {
                    CurrentImage.Source = RightHit;
                }
            }


            if (!PressedKeys.Contains(e.KeyCode))
            {
                PressedKeys.Add(e.KeyCode);
            }
            PressedKeys.Add(e.KeyCode);

            if (CheckForESC(e))
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Minimized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            }
        }
        private bool KeyExists(System.Windows.Forms.KeyEventArgs e)
        {
            foreach (Keys key in KeyList)
            {
                if (key == e.KeyCode)
                    return true;
            }
            return false;
        }
        private bool CheckForESC(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                return true;
            }
            return false;
        }
    }
}