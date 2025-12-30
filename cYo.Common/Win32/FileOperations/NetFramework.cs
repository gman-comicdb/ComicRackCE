using System.IO;

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
