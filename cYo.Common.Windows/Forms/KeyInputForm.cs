using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms;

public partial class KeyInputForm : FormEx
{
    private string description;

    [Browsable(false)]
    public CommandKey Key { get; private set; }

    public string Description
    {
        get => description;
        set
        {
            if (!(description == value))
            {
                description = value;
                Invalidate();
            }
        }
    }

    public KeyInputForm()
    {
        InitializeComponent();
    }

    protected override bool IsInputKey(Keys keyData)
    {
        return true;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (e.KeyCode is not Keys.ControlKey and not Keys.ShiftKey and not Keys.Menu)
        {
            Key = (CommandKey)e.KeyData;
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (string.IsNullOrEmpty(Description))
        {
            return;
        }
        using (SolidBrush brush = new(ForeColor))
        {
            using (StringFormat format = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.Graphics.DrawString(Description, Font, brush, base.ClientRectangle, format);
            }
        }
    }

    public static CommandKey Show(IWin32Window parent, string caption, string description)
    {
        using (KeyInputForm keyInputForm = new())
        {
            keyInputForm.Text = caption;
            keyInputForm.Description = description;
            return keyInputForm.ShowDialog(parent) == DialogResult.Cancel ? CommandKey.None : keyInputForm.Key;
        }
    }
}
