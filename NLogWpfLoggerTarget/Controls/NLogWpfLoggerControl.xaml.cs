using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NLog.Common;
using NLogWpfLoggerTarget.ViewModel;

namespace NLogWpfLoggerTarget.Controls
{
    /// <summary>
    /// Interaction logic for NLogWpfLoggerControl.xaml
    /// </summary>
    public partial class NLogWpfLoggerControl
    {
        public ObservableCollection<LoggerTargetViewModel> LogEntries { get; }

        public Boolean IsTargetConfigured { get; }

        [Category("Data")]
        [Description("Width of time column in pixels")]
        [TypeConverter(typeof(LengthConverter))]
        public Double TimeWidth { get; set; }

        [Category("Data")]
        [Description("Width of Logger column in pixels, or auto if not specified")]
        [TypeConverter(typeof(LengthConverter))]
        public Double LoggerNameWidth { set; get; }

        [Category("Data")]
        [Description("Width of Level column in pixels")]
        [TypeConverter(typeof(LengthConverter))]
        public Double LevelWidth { get; set; }

        [Category("Data")]
        [Description("Width of Message column in pixels")]
        [TypeConverter(typeof(LengthConverter))]
        public Double MessageWidth { get; set; }

        [Category("Data")]
        [Description("Width of Exception column in pixels")]
        [TypeConverter(typeof(LengthConverter))]
        public Double ExceptionWidth { get; set; }

        [Category("Data")]
        [Description("Maximum number of log entries to show")]
        public Int32 MaximumLogEntries { get; set; }

        public NLogWpfLoggerControl()
        {
            IsTargetConfigured = false;
            LogEntries = new ObservableCollection<LoggerTargetViewModel>();

            MaximumLogEntries = 100;

            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            foreach (var target in NLog.LogManager.Configuration.AllTargets.Where(t => t is Target.NLogWpfLoggerTarget).Cast<Target.NLogWpfLoggerTarget>())
            {
                IsTargetConfigured = true;
                target.LogReceived += LogReceived;
            }
        }

        protected virtual void LogReceived(AsyncLogEventInfo log)
        {
            var vm = new LoggerTargetViewModel(log.LogEvent);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (LogEntries.Count >= MaximumLogEntries)
                    LogEntries.RemoveAt(0);

                LogEntries.Add(vm);

                LogListView.SelectedIndex = LogListView.Items.Count - 1;
                LogListView.ScrollIntoView(LogListView.SelectedItem);
                LogListView.SelectedIndex = -1;
            }));
        }
    }
}
