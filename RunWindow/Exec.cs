using System.Diagnostics;

namespace RunWindow;

public static class Exec
{
    static public async Task ExecCommandAsync(string command)
    {
        var startinfo = new ProcessStartInfo
        {
            FileName = "x-terminal-emulator",
            Arguments = $"-e bash -c \"{command}\"",
            UseShellExecute = false,
        };

        using (var p = Process.Start(startinfo))
        {
            await p.WaitForExitAsync();
        }
    }
}
