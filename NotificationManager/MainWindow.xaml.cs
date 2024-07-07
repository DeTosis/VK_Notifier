using System.ComponentModel;
using System.Windows;

namespace NotificationManager
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string d_Sender = "";
        public string SenderData_p {
            get { return d_Sender; }
            set {
                d_Sender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SenderData_p"));
            }
        }

        private string d_MSG = "";
        public string MSGData_p {
            get { return d_MSG; }
            set {
                d_MSG = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MSGData_p"));
            }
        }

        public MainWindow() {
            InitializeComponent();
            Window_Loaded();
        }

        public MainWindow((dynamic, dynamic, string) output) {
            ShowInTaskbar = false;
            DataContext = this;
            SenderData_p = $"{output.Item1} {output.Item2}";
            MSGData_p = $"{output.Item3}";
            InitializeComponent();
            Window_Loaded();
        }

        private void Window_Loaded() {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width - 6;
            this.Top = desktopWorkingArea.Bottom - this.Height - 6;
        }

        private void CloseNotification_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}