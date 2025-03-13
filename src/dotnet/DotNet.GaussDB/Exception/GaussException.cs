using System.Data.Common;
using System.Net.Sockets;

namespace Gauss;

[Serializable]
public class GaussException : DbException
{
    public GaussException() {}

    public GaussException(string? message, Exception? innerException)
        : base(message, innerException) {}

    public GaussException(string? message)
        : base(message) { }

    public override bool IsTransient
        => InnerException is IOException or SocketException or TimeoutException or GaussException { IsTransient: true };
}