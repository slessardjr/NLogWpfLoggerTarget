using System;
using System.Globalization;
using System.Windows.Media;
using NLog;

namespace NLogWpfLoggerTarget.ViewModel
{
    public class LoggerTargetViewModel
    {
        public SolidColorBrush BackgroundColorBrush { get; }
        public SolidColorBrush BackgroundColorBrushMouseOver { get; }
        public Exception Exception { get; }
        public SolidColorBrush ForegroundColorBrush { get; set; }
        public SolidColorBrush ForegroundColorBrushMouseOver { get; }
        public String FormattedMessage { get; }
        public String Level { get; }
        public String LoggerName { get; }
        public String Time { get; }
        public String ToolTip { get; }

        public LoggerTargetViewModel(LogEventInfo logEventInfo)
        {
            ToolTip = logEventInfo.FormattedMessage;
            Level = logEventInfo.Level.ToString();
            FormattedMessage = logEventInfo.FormattedMessage;
            Exception = logEventInfo.Exception;
            LoggerName = logEventInfo.LoggerName;
            Time = logEventInfo.TimeStamp.ToString(CultureInfo.InvariantCulture);

            //Setup Colors
            ForegroundColorBrush = Brushes.Black;
            ForegroundColorBrushMouseOver = Brushes.Black;

            BackgroundColorBrush = GetBackgroundColor(logEventInfo.Level);
            BackgroundColorBrushMouseOver = Brushes.LightSkyBlue;            
        }

        protected SolidColorBrush GetBackgroundColor(LogLevel logLevel)
        {
            if (LogLevel.Warn.Equals(logLevel)) return Brushes.Yellow;
            if (LogLevel.Error.Equals(logLevel) || LogLevel.Fatal.Equals(logLevel)) return Brushes.Red;
            if (LogLevel.Trace.Equals(logLevel)) return Brushes.Fuchsia;
            return LogLevel.Debug.Equals(logLevel) ? Brushes.LightGreen : Brushes.White;
        }
    }
}
