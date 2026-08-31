using GObject;
using Graphene;

namespace RunWindow;

[GObject.Subclass<Gtk.Window>]
public partial class MainWindow
{
    static public new MainWindow New()
    {
        return NewWithProperties([]);
    }

    partial void Initialize()
    {
        Title = "Run...";
        SetDefaultSize(600, 60);

        var layout = Gtk.Box.New(Gtk.Orientation.Horizontal, 5);
        layout.SetMarginBottom(10);
        layout.SetMarginTop(10);
        layout.SetMarginEnd(20);
        layout.SetMarginStart(20);


        var input = Gtk.Entry.New();
        input.PlaceholderText = "Input Command...";
        input.SetSizeRequest(320, 30);

        var button = Gtk.Button.New();
        button.Label = "Run..";
        button.SetSizeRequest(30, 30);

        layout.Append(input);

        button.OnClicked += async (_, _) =>
        {
            await Exec.ExecCommandAsync(input.GetText());
        };

        layout.Append(button);

        Child = layout;
    }
}
