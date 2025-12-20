using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

using Microsoft.VisualBasic.FileIO;

namespace cYo.Common.Win32.FileOperations;

internal class NetFramework : FileOperation
{
    public NetFramework() : base(ShellFileDeleteOptions.None)
    {
    }

    public override void DeleteFile(string file)
    {
        VerifyFile(file);
        File.Delete(file);
    }
}
