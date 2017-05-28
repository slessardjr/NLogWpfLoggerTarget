using System;
using NLog.Common;
using NLog.Targets;

namespace NLogWpfLoggerTarget.Target
{
    [Target("NLogWpfLoggerTarget")]
    public class NLogWpfLoggerTarget : NLog.Targets.Target
    {
        public event Action<AsyncLogEventInfo> LogReceived;

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);
            LogReceived?.Invoke(logEvent);
        }
    }
}
