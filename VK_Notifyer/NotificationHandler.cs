using NotificationManager;
using System.Timers;

internal class NotificationHandler
{
    (dynamic, dynamic, string) output;
    MainWindow window;

    public NotificationHandler((dynamic, dynamic, string) output) {
        this.output = output;
        Notify();
    }

    private void Notify() {
        Thread thread = new Thread(() => {
            window = new MainWindow(output);
            window.Show();

            window.Closed += (sender2, e2) => window.Dispatcher.InvokeShutdown();
            System.Windows.Threading.Dispatcher.Run();
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();

        System.Timers.Timer timer = new System.Timers.Timer();
        timer.Interval = 3500;
        timer.AutoReset = false;
        timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        timer.Start();
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e) {
        window.Dispatcher.InvokeShutdown();
    }
}
