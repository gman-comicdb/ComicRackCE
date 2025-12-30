using System.Net;
using System.Net.Sockets;

namespace cYo.Common.Net;

public static class IPAddressExtension
{
    public static bool IsPrivate(this IPAddress address)
    {
        return address.AddressFamily == AddressFamily.InterNetwork
            ? ((IPAddressV4)address).IsPrivate()
            : address.AddressFamily == AddressFamily.InterNetworkV6
            ? !address.IsIPv6LinkLocal && !address.IsIPv6SiteLocal ? IPAddress.IsLoopback(address) : true
            : true;
    }
}
