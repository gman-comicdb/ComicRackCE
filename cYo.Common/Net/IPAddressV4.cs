using System;
using System.Net;
using System.Net.Sockets;

namespace cYo.Common.Net;

public struct IPAddressV4 : IComparable<IPAddressV4>
{
    public static readonly IPAddressV4 Empty = new(0, 0, 0, 0);

    public static readonly IPAddressV4 Loopback = new(127, 0, 0, 1);

    public static readonly IPAddressV4 PrivateStartA = new(10, 0, 0, 0);

    public static readonly IPAddressV4 PrivateEndA = new(10, 255, 255, 255);

    public static readonly IPAddressV4 PrivateStartB = new(172, 16, 0, 0);

    public static readonly IPAddressV4 PrivateEndB = new(172, 31, 255, 255);

    public static readonly IPAddressV4 PrivateStartC = new(192, 168, 0, 0);

    public static readonly IPAddressV4 PrivateEndC = new(192, 168, 255, 255);

    public static readonly IPAddressV4 PrivateStartD = new(169, 254, 0, 0);

    public static readonly IPAddressV4 PrivateEndD = new(169, 254, 255, 255);

    public byte A { get; private set; }

    public byte B { get; private set; }

    public byte C { get; private set; }

    public byte D { get; private set; }

    public IPAddressV4(int a, int b, int c, int d)
    {
        this = default;
        A = (byte)a;
        B = (byte)b;
        C = (byte)c;
        D = (byte)d;
    }

    public IPAddressV4(byte[] bytes)
        : this(bytes[0], bytes[1], bytes[2], bytes[3])
    {
    }

    public IPAddressV4(string address)
    {
        this = default;
        if (!TryParse(address, out var address2))
        {
            throw new ArgumentException("no valid ip v4 address");
        }
        A = address2.A;
        B = address2.B;
        C = address2.C;
        D = address2.D;
    }

    public byte[] GetAddressBytes()
    {
        return
        [
            A,
            B,
            C,
            D
        ];
    }

    public long GetLongAddress()
    {
        return (long)(((ulong)A << 24) + ((ulong)B << 16) + ((ulong)C << 8) + D);
    }

    public int GetAddress()
    {
        return (int)GetLongAddress();
    }

    public bool InRange(IPAddressV4 a, IPAddressV4 b)
    {
        return this >= a ? this <= b : false;
    }

    public bool IsPrivate()
    {
        return !InRange(PrivateStartA, PrivateEndA) && !InRange(PrivateStartB, PrivateEndB) && !InRange(PrivateStartC, PrivateEndC) && !InRange(PrivateStartD, PrivateEndD)
            ? this == Loopback
            : true;
    }

    public int CompareTo(IPAddressV4 other)
    {
        return Math.Sign(GetLongAddress() - other.GetLongAddress());
    }

    public override bool Equals(object obj)
    {
        try
        {
            return GetAddress() == ((IPAddressV4)obj).GetAddress();
        }
        catch (Exception)
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"{A}.{B}.{C}.{D}";
    }

    public override int GetHashCode()
    {
        return GetAddress();
    }

    public static bool TryParse(string text, out IPAddressV4 address)
    {
        address = Empty;
        if (!IPAddress.TryParse(text, out var address2))
        {
            return false;
        }
        try
        {
            address = (IPAddressV4)address2;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static explicit operator IPAddressV4(IPAddress address)
    {
        return address.AddressFamily != AddressFamily.InterNetwork
            ? throw new InvalidCastException("Can only cast ip v4 addresses")
            : new IPAddressV4(address.GetAddressBytes());
    }

    public static implicit operator IPAddress(IPAddressV4 address)
    {
        return new IPAddress(address.GetAddressBytes());
    }

    public static bool operator ==(IPAddressV4 a, IPAddressV4 b)
    {
        return a.CompareTo(b) == 0;
    }

    public static bool operator !=(IPAddressV4 a, IPAddressV4 b)
    {
        return a.CompareTo(b) != 0;
    }

    public static bool operator >(IPAddressV4 a, IPAddressV4 b)
    {
        return a.CompareTo(b) > 0;
    }

    public static bool operator <(IPAddressV4 a, IPAddressV4 b)
    {
        return a.CompareTo(b) < 0;
    }

    public static bool operator >=(IPAddressV4 a, IPAddressV4 b)
    {
        return a.CompareTo(b) >= 0;
    }

    public static bool operator <=(IPAddressV4 a, IPAddressV4 b)
    {
        return a.CompareTo(b) <= 0;
    }
}
