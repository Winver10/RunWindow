var app = Adw.Application.New("io.github.winver10.run", Gio.ApplicationFlags.FlagsNone);

app.OnActivate += (sender, arg) =>
{
    var window = RunWindow.MainWindow.New();
    window.Application = (Adw.Application)sender;
    window.Show();
};

return app.RunWithSynchronizationContext(null);