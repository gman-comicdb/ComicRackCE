using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

using cYo.Common.Collections;
using cYo.Common.Threading;

namespace cYo.Projects.ComicRack.Engine.IO.Provider;

public abstract class ProviderFactoryBase<T> where T : class
{
    protected readonly ReaderWriterLockSlim rwLock = new();

    protected readonly List<IProviderInfo> providerDict = new();

    public abstract void RegisterProvider(Type t, bool withLocking);

    public void RegisterProviders()
    {
        RegisterProviders(Assembly.GetExecutingAssembly());
    }

    public void RegisterProviders(Assembly assembly)
    {
        RegisterProviders(assembly, typeof(T));
    }

    public void RegisterProviders(Assembly assembly, Type baseType)
    {
        using (rwLock.WriteLock())
        {
            (from t in assembly.GetTypes()
             where !t.IsAbstract && t.IsSubclassOf(baseType) && t.GetConstructor([]) != null
             select t).ForEach((Type t) => RegisterProvider(t, withLocking: false));
        }
    }

    protected IEnumerable<TInfo> GetProviderInfos<TInfo>() where TInfo : class, IProviderInfo
    {
        return providerDict.ReadLock(rwLock).Cast<TInfo>();
    }

    protected IEnumerable<Type> GetProviderTypes<TInfo>() where TInfo : class, IProviderInfo
    {
        return from pi in GetProviderInfos<TInfo>()
               select pi.ProviderType;
    }

    public IEnumerable<T> CreateProviders<TInfo>() where TInfo : class, IProviderInfo
    {
        return from t in GetProviderTypes<TInfo>()
               select Activator.CreateInstance(t) as T;
    }
}