using System.Diagnostics;

namespace RunWindow;

public static class Exec
{
    static public void ExecCommandAsync(string command)
    {
        var startinfo = new ProcessStartInfo
        {
            FileName = "x-terminal-emulator",
            Arguments = $"-e bash -c \"{command}\"",
            UseShellExecute = true,
        };

        Process.Start(startinfo);
    }
}
