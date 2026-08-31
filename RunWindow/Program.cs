var app = Gtk.Application.New("io.github.winver10.run", Gio.ApplicationFlags.FlagsNone);

app.OnActivate += (sender, arg) =>
{
    var window = RunWindow.MainWindow.New();
    window.Application = (Gtk.Application)sender;
    window.Show();
};

return app.RunWithSynchronizationContext(null);