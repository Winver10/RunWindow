#nullable enable
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
    Adw.HeaderBar _headerbar;
    Gtk.Box _layout = Gtk.Box.New(Orientation.Horizontal, 5);
    Label _desc = Label.New("Type the name of a program, forder, document, or Internet resource, and Linux will open it for you.");
    Label _open = Label.New("Open...:");
    Entry _input = Entry.New();
    Button _run = Button.NewWithLabel("Run");
    Button _cancel = Button.NewWithLabel("Cancel");
    Gtk.Box _buttons = Gtk.Box.New(Orientation.Horizontal, 10);
    Gtk.Box _main = Gtk.Box.New(Orientation.Vertical, 0);
    partial void Initialize()
    {
        Resizable = false;

        Title = "Run...";
        SetDefaultSize(350, 120);

        _headerbar = Adw.HeaderBar.New();
        _headerbar.SetDecorationLayout(":close");

        _layout.SetMarginBottom(10);
        _layout.SetMarginTop(10);
        _layout.SetMarginEnd(20);
        _layout.SetMarginStart(20);
        _layout.SetHalign(Align.Center);

        _desc.SetWrap(true);
        _desc.SetWrapMode(Pango.WrapMode.WordChar);
        _desc.SetMarginStart(15);
        _desc.SetMarginEnd(20);
        _desc.SetMarginTop(10);


        _input.PlaceholderText = "Input Command...";
        _input.SetSizeRequest(300, -1);

        _run.SetSizeRequest(60, -1);

        _buttons.SetMarginStart(20);
        _buttons.SetMarginEnd(20);
        _buttons.Append(_run);
        _buttons.Append(_cancel);

        _layout.Append(_open);

        _run.OnClicked += Run;

        _cancel.OnClicked += (_, _) =>
        {
            Close();
        };

        var execwithroot = CallbackAction.New((widget, args) =>
        {
            if (!string.IsNullOrEmpty(_input.GetText()))
            {
                Exec.ExecCommandWithRoot(_input.GetText());
                Close();
            }
            else
            {
                ErrorBell();
            }
            return true;
        });
        var _rootctrl = ShortcutController.New();
        _rootctrl.Scope = ShortcutScope.Global;
        _rootctrl.AddShortcut(Shortcut.New(ShortcutTrigger.ParseString("<Control><Shift>Return"), execwithroot));

        var exit = CallbackAction.New((widget, args) =>
        { Close(); return true; });
        _rootctrl.AddShortcut(Shortcut.New(ShortcutTrigger.ParseString("Escape"), exit));
        AddController(_rootctrl);

        _input.OnActivate += (_, _) => Run(null, null);
        _layout.Append(_input);

        _main.Append(_headerbar);
        _main.Append(_desc);
        _main.Append(_layout);
        _main.Append(_buttons);

        Content = _main;



        //var t = EventControllerKey.New();
        // t.OnKeyPressed += (sender, args) =>
        // {
        //     if (args.Keyval == Gdk.Constants.KEY_Return)
        //     {
        //         Exec.ExecCommand(input.Text_);
        //     }
        // };
    }

    private void Run(Button? sender, EventArgs? args)
    {
        if (!string.IsNullOrEmpty(_input.GetText()))
        {
            if (_input.GetText().StartsWith("https://", StringComparison.CurrentCultureIgnoreCase) || _input.GetText().StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) || _input.GetText().StartsWith("fps://", StringComparison.CurrentCultureIgnoreCase))
            {
                Exec.OpenUrl(_input.GetText());
            }
            else
            {
                Exec.ExecCommand(_input.GetText());
            }
            Close();
        }
        else
        {
            ErrorBell();
        }
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
