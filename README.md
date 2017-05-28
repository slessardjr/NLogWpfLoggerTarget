[1]: https://github.com/erizet/NlogViewer

# NLogWpfLoggerTarget
==========
A Logger Target for WPF Applications to display contents of the logger.

NLogWpfLoggerTarget is combination WPF-control and API to show NLog output in a WPF application. It is inspired by [The NLogViewer Project][1].

##How to use?##

If you XAML Add a namespace to the window

        xmlns:nLogWpfLoggerTarget="clr-namespace:NLogWpfLoggerTarget.Controls;assembly=NLogWpfLoggerTarget"

Then add the control to the XAML where you would like it.  Ideally it should be nested inside of a grid (See The Demo Code)

        <nLogWpfLoggerTarget:NLogWpfLoggerControl x:Name="LogControl"/>

Then add NLogWpfLoggerTarget as a target in the NLog.Config and add the rule to send logs to it.  You do not need to add
it as an extension becuase NLog 4.0+ will add extensions automatically if named correctly.

```xml
  <targets>
    <target xsi:type="NLogWpfLoggerTarget" name="nLogWpfLoggerTarget" />
  </targets>
  <rules>
    <logger minlevel="Trace" name="*" writeTo="nLogWpfLoggerTarget"/>
  </rules>
```