using System.Reflection;

namespace cYo.Common.Reflection;

public static class AssemblyInfo
{
    public static string GetCompanyName(Assembly ass)
    {
        if (ass == null)
        {
            return null;
        }
        object[] customAttributes = ass.GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: true);
        return customAttributes.Length == 0 ? null : ((AssemblyCompanyAttribute)customAttributes[0]).Company;
    }

    public static string GetProductName(Assembly ass)
    {
        if (ass == null)
        {
            return null;
        }
        object[] customAttributes = ass.GetCustomAttributes(typeof(AssemblyProductAttribute), inherit: true);
        return customAttributes.Length == 0 ? null : ((AssemblyProductAttribute)customAttributes[0]).Product;
    }
}
