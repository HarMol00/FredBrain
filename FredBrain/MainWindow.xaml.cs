using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FredBrain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Process trainController;

        public int Lok1 { get { return Properties.Settings.Default.Lok1; } set { Properties.Settings.Default.Lok1 = value; } }
        public int Lok2 { get { return Properties.Settings.Default.Lok2; } set { Properties.Settings.Default.Lok2 = value; } }
        public int Lok3 { get { return Properties.Settings.Default.Lok3; } set { Properties.Settings.Default.Lok3 = value; } }
        public int Lok4 { get { return Properties.Settings.Default.Lok4; } set { Properties.Settings.Default.Lok4 = value; } }

        public double Zoom
        {
            get
            {
                return Properties.Settings.Default.Zoom;
            }
            set
            {
                if (Properties.Settings.Default.Zoom.CompareTo(value) != 0)
                {
                    Properties.Settings.Default.Zoom = value;
                    RaisePropertyChanged("Zoom");
                }
            }
        }

        public bool Zoom05 { get { return Zoom.CompareTo(0.9) == 0; } set { Zoom = 0.9; ZoomChanged(); } }
        public bool Zoom10 { get { return Zoom.CompareTo(1.0) == 0; } set { Zoom = 1.0; ZoomChanged(); } }
        public bool Zoom20 { get { return Zoom.CompareTo(1.2) == 0; } set { Zoom = 1.2; ZoomChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Closing += OnClosing;

            Top = Properties.Settings.Default.WindowTop;
            Left = Properties.Settings.Default.WindowLeft;

            MoveIntoView();

            var processes = Process.GetProcessesByName("RailTCS32");
            if (processes.Any())
            {
                trainController = processes[0];
                trainController.Exited += TrainControllerExited;
                trainController.EnableRaisingEvents = true;
            }
        }

        private void TrainControllerExited(object sender, EventArgs e)
        {
            Dispatcher.Invoke((Action)(Close));
        }

        private void MoveIntoView()
        {
            if (Top + Height / 2 > SystemParameters.VirtualScreenHeight)
            {
                Top = SystemParameters.VirtualScreenHeight - Height;
            }

            if (Left + Width / 2 > SystemParameters.VirtualScreenWidth)
            {
                Left = SystemParameters.VirtualScreenWidth - Width;
            }

            if (Top < 0)
            {
                Top = 0;
            }

            if (Left < 0)
            {
                Left = 0;
            }
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.WindowTop = Top;
            Properties.Settings.Default.WindowLeft = Left;

            Properties.Settings.Default.Save();
        }

        private void MoveHandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ZoomChanged()
        {
            RaisePropertyChanged("Zoom05");
            RaisePropertyChanged("Zoom10");
            RaisePropertyChanged("Zoom20");
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
 