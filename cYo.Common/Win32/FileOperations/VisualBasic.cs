using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

using Microsoft.VisualBasic.FileIO;

namespace cYo.Common.Win32.FileOperations;

internal class VisualBasic : FileOperation
{
    public VisualBasic(ShellFileDeleteOptions options) : base(options)
    {
    }

    public override void DeleteFile(string file)
    {
        VerifyFile(file);

        bool sendToRecycle = !_options.HasFlag(ShellFileDeleteOptions.NoRecycleBin);
        RecycleOption recycleOption = sendToRecycle ? RecycleOption.SendToRecycleBin : RecycleOption.DeletePermanently;
        FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, recycleOption);
    }
}
