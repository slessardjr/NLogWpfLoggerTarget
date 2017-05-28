using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NLog;
using LogLevel = NLog.LogLevel;

namespace NLogWpfLoggerTargetDemo
{
    /// <summary>
    /// Interaction logic for NLogWpfLoggerTargetDemo.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Task _logTask;
        private CancellationTokenSource _cancelLogTask;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Send_Click(Object sender, RoutedEventArgs e)
        {
            var logLevel = LogLevel.Off;

            if (sender.Equals(ButtonSendInfo)) logLevel = LogLevel.Info;
            if (sender.Equals(ButtonSendDebug)) logLevel = LogLevel.Debug;
            if (sender.Equals(ButtonSendTrace)) logLevel = LogLevel.Trace;
            if (sender.Equals(ButtonSendWarning)) logLevel = LogLevel.Warn;
            if (sender.Equals(ButtonSendError)) logLevel = LogLevel.Error;
            if (sender.Equals(ButtonSendFatal)) logLevel = LogLevel.Fatal;

            LogManager.GetLogger("button").Log(logLevel, TextBoxLogTextToSend.Text);
        }

        private void BackgroundSending_Checked(Object sender, RoutedEventArgs e)
        {
            _cancelLogTask = new CancellationTokenSource();
            var token = _cancelLogTask.Token;
            _logTask = new Task(SendLogs, token, token);
            _logTask.Start();
        }

        private void BackgroundSending_Unchecked(Object sender, RoutedEventArgs e)
        {
            _cancelLogTask?.Cancel();
        }

        private static void SendLogs(Object obj)
        {
            var cancellationToken = (CancellationToken)obj;

            var counter = 0;
            var log = LogManager.GetLogger("task");

            log.Debug("Backgroundtask started.");

            while (!cancellationToken.WaitHandle.WaitOne(2000))
            {
                log.Trace("Message # {0} from background task.", counter++);
            }

            log.Debug("Backgroundtask stopped.");
        }
    }
}
