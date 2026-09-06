using GObject;
using Graphene;
using Adw;
using Gtk;
using Gdk;

namespace RunWindow;

[GObject.Subclass<Adw.ApplicationWindow>]
public partial class MainWindow
{
    static public new MainWindow New()
    {
        return new MainWindow();
    }
    public MainWindow() : base()
    {
        Initialize();
    }

    partial void Initialize()
    {
        Title = "Run...";
        SetDefaultSize(350, 120);

        var head = Adw.HeaderBar.New();

        var layout = Gtk.Box.New(Gtk.Orientation.Horizontal, 5);
        layout.SetMarginBottom(10);
        layout.SetMarginTop(10);
        layout.SetMarginEnd(20);
        layout.SetMarginStart(20);
        layout.SetHalign(Align.Center);

        var describ = Gtk.Label.New("Type the name of a program, forder, document, or Internet resource, and Linux will open it for you.");
        describ.SetWrap(true);
        describ.SetWrapMode(Pango.WrapMode.WordChar);
        describ.SetMarginStart(15);
        describ.SetMarginEnd(20);
        describ.SetMarginTop(10);
        var open = Gtk.Label.New("Open...:");


        var input = Gtk.Entry.New();
        input.PlaceholderText = "Input Command...";
        input.SetSizeRequest(300, -1);

        var button = Gtk.Button.New();
        button.Label = "Run..";
        button.SetSizeRequest(60, -1);

        var cancel_button = Button.NewWithLabel("Cancel");

        var options = Gtk.Box.New(Orientation.Horizontal, 10);
        options.SetMarginStart(20);
        options.SetMarginEnd(20);
        options.Append(button);
        options.Append(cancel_button);

        layout.Append(open);

        button.OnClicked += async (_, _) =>
        {
            Exec.ExecCommand(input.GetText());
            Close();
        };

        cancel_button.OnClicked += (_, _) =>
        {
            Close();
        };

        var execwithroot = CallbackAction.New((widget, args_) =>
        {
            Exec.ExecCommandWithRoot(input.GetText());
            Close();
            return true;
        });
        var controller = ShortcutController.New();
        controller.Scope = ShortcutScope.Global;
        controller.AddShortcut(Shortcut.New(ShortcutTrigger.ParseString("<Control><Shift>Return"), execwithroot));
        input.AddController(controller);
        layout.Append(input);

        var mainbox = Gtk.Box.New(Orientation.Vertical, 0);
        mainbox.Append(head);
        mainbox.Append(describ);
        mainbox.Append(layout);
        mainbox.Append(options);

        Content = mainbox;



        //var t = EventControllerKey.New();
        // t.OnKeyPressed += (sender, args) =>
        // {
        //     if (args.Keyval == Gdk.Constants.KEY_Return)
        //     {
        //         Exec.ExecCommand(input.Text_);
        //     }
        // };
    }

    // private bool ChooseFileToRun(object? sender, EventArgs e)
    // {
    //     var chooser = new FileChooserNative
    //     {
    //         Title = "Choose a file to run",
    //         Action = FileChooserAction.Open
    //     };

    //     bool isOpened = false;

    //     chooser.OnResponse += (sender, args) =>
    //     {
    //         if (args.ResponseId == (int)ResponseType.Accept)
    //         {
    //             if (chooser.GetFile() != null)
    //             {
    //                 Exec.ExecCommand(chooser.GetFile().GetPath());
    //                 isOpened = !isOpened;
    //                 chooser.Destroy();
    //             }
    //         }
    //     };

    //     chooser.Show();
    //     return isOpened;
    // }
}
