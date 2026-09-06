using System.Diagnostics;

namespace RunWindow;

public static class Exec
{
    static public void ExecCommand(string command)
    {
        var startinfo = new ProcessStartInfo
        {
            FileName = "x-terminal-emulator",
            Arguments = $"-e bash -c \"{command}\"",
            UseShellExecute = true,
        };

        Process.Start(startinfo);
    }
    static public void ExecCommandWithRoot(string comamnd)
    {
        var startinfo = new ProcessStartInfo
        {
            FileName = "x-terminal-emulator",
            Arguments = $"-e pkexec bash -c \"{comamnd}\"",
            UseShellExecute = true,
        };

        Process.Start(startinfo);
    }
    static public void OpenUrl(string url)
    {
        var startinfo = new ProcessStartInfo
        {
            FileName = "xdg-open",
            Arguments = url,
        };

        Process.Start(startinfo);
    }
}
