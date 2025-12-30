using System;
using System.Collections.Generic;
using System.Linq;

using cYo.Common.Localize;
using cYo.Common.Reflection;
using cYo.Common.Threading;

namespace cYo.Projects.ComicRack.Engine.IO.Provider;

public class ProviderFactory<T> : ProviderFactoryBase<T> where T : class
{
    public void RegisterProvider(Type pt, IEnumerable<FileFormat> formats, bool withLocking = true)
    {
        using (withLocking ? rwLock.UpgradeableReadLock() : null)
        {
            if (!providerDict.Any((IProviderInfo pi) => pi.ProviderType == pt))
            {
                using (withLocking ? rwLock.WriteLock() : null)
                {
                    providerDict.Add(new ProviderInfo(pt, formats));
                }
            }
        }
    }

    public override void RegisterProvider(Type pt, bool withLocking = true)
    {
        if (Activator.CreateInstance(pt) is not IValidateProvider validateProvider || validateProvider.IsValid)
        {
            RegisterProvider(pt, from ffa in pt.GetAttributes<FileFormatAttribute>()
                                 select ffa.Format, withLocking);
        }
    }

    public IEnumerable<ProviderInfo> GetProviderInfos()
    {
        return base.GetProviderInfos<ProviderInfo>();
    }

    public IEnumerable<ProviderInfo> GetSourceProviderInfos(string source)
    {
        return from pi in GetProviderInfos()
               where pi.Formats.Any((FileFormat f) => f.Supports(source))
               select pi;
    }

    public IEnumerable<Type> GetSourceProviderTypes(string source)
    {
        return from f in GetSourceProviderInfos(source)
               select f.ProviderType;
    }

    public ProviderInfo GetSourceProviderInfo(string source)
    {
        return GetSourceProviderInfos(source).FirstOrDefault();
    }

    public Type GetSourceProviderType(string source)
    {
        return GetSourceProviderTypes(source).FirstOrDefault();
    }

    public IEnumerable<FileFormat> GetSourceFormats()
    {
        return GetProviderInfos().SelectMany((ProviderInfo pi) => pi.Formats);
    }

    public IEnumerable<FileFormat> GetSourceFormats(string source)
    {
        return GetSourceProviderInfos(source).SelectMany((ProviderInfo pi) => pi.Formats);
    }

    protected virtual FileFormat GetActualSourceFormat(string source)
    {
        //Just returns the source format based on the extension
        return GetSourceFormat(source);
    }

    public FileFormat GetSourceFormat(string source, bool actualFormat = false)
    {
        return actualFormat ? GetActualSourceFormat(source) : GetSourceFormats(source).FirstOrDefault((FileFormat ff) => ff.Supports(source));
    }

    public string GetSourceFormatName(string source, bool actualFormat = false)
    {
        FileFormat sourceFormat = GetSourceFormat(source, actualFormat);
        return sourceFormat != null && !string.IsNullOrEmpty(sourceFormat.Name) ? sourceFormat.Name : TR.Default["Unknown", "Unknown"];
    }

    public Type GetFormatProviderType(string formatName)
    {
        return (from pi in GetProviderInfos()
                where pi.Formats.Any((FileFormat f) => f.Name == formatName)
                select pi.ProviderType).FirstOrDefault();
    }

    public Type GetFormatProviderType(int formatId)
    {
        return (from pi in GetProviderInfos()
                where pi.Formats.Any((FileFormat f) => f.Id == formatId)
                select pi.ProviderType).FirstOrDefault();
    }

    public IEnumerable<string> GetFileExtensions()
    {
        return GetSourceFormats().SelectMany((FileFormat f) => f.Extensions).Distinct();
    }

    public T CreateFormatProvider(string formatName)
    {
        try
        {
            return Activator.CreateInstance(GetFormatProviderType(formatName)) as T;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public T CreateFormatProvider(int formatId)
    {
        Type formatProviderType = GetFormatProviderType(formatId);
        return !(formatProviderType != null) ? null : Activator.CreateInstance(formatProviderType) as T;
    }

    public IEnumerable<T> CreateProviders()
    {
        return base.CreateProviders<ProviderInfo>();
    }

    public virtual T CreateSourceProvider(string source)
    {
        try
        {
            return Activator.CreateInstance(GetSourceProviderType(source)) as T;
        }
        catch
        {
            return null;
        }
    }

    public string GetDialogFilter(bool withAllFilter, bool sort)
    {
        IEnumerable<FileFormat> enumerable = GetSourceFormats();
        if (sort)
        {
            enumerable = enumerable.OrderBy((FileFormat f) => f);
        }
        return enumerable.GetDialogFilter(withAllFilter);
    }
}
