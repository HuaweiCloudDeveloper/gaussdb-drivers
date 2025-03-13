using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Gauss.Internal;

namespace Gauss;

public class ThrowHelper
{
       [DoesNotReturn]
    internal static void ThrowArgumentOutOfRangeException()
        => throw new ArgumentOutOfRangeException();

    [DoesNotReturn]
    internal static void ThrowArgumentOutOfRangeException(string paramName, string message)
        => throw new ArgumentOutOfRangeException(paramName, message);

    [DoesNotReturn]
    internal static void ThrowArgumentOutOfRangeException(string paramName, string message, object argument)
        => throw new ArgumentOutOfRangeException(paramName, string.Format(message, argument));

    [DoesNotReturn]
    internal static void ThrowUnreachableException(string message, object argument)
        => throw new UnreachableException(string.Format(message, argument));

    [DoesNotReturn]
    internal static void ThrowInvalidOperationException()
        => throw new InvalidOperationException();

    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(string message)
        => throw new InvalidOperationException(message);

    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(string message, object argument)
        => throw new InvalidOperationException(string.Format(message, argument));

    [DoesNotReturn]
    internal static void ThrowObjectDisposedException(string? objectName)
        => throw new ObjectDisposedException(objectName);

    [DoesNotReturn]
    internal static void ThrowObjectDisposedException(string objectName, string message)
        => throw new ObjectDisposedException(objectName, message);

    [DoesNotReturn]
    internal static void ThrowObjectDisposedException(string objectName, Exception? innerException)
        => throw new ObjectDisposedException(objectName, innerException);

    [DoesNotReturn]
    internal static void ThrowInvalidCastException(string message, object argument)
        => throw new InvalidCastException(string.Format(message, argument));

    [DoesNotReturn]
    internal static void ThrowInvalidCastException(string message) =>
        throw new InvalidCastException(message);

    [DoesNotReturn]
    internal static void ThrowInvalidCastException_NoValue() =>
        throw new InvalidCastException("Field is null.");

    [DoesNotReturn]
    internal static void ThrowGaussException(string message)
        => throw new GaussException(message);

    [DoesNotReturn]
    internal static void ThrowGaussException(string message, Exception? innerException)
        => throw new GaussException(message, innerException);

    [DoesNotReturn]
    internal static void ThrowGaussOperationInProgressException(GaussCommand command)
        => throw new GaussOperationInProgressException(command);

    [DoesNotReturn]
    internal static void ThrowGaussOperationInProgressException(ConnectorState state)
        => throw new GaussOperationInProgressException(state);

    [DoesNotReturn]
    internal static void ThrowArgumentException(string message)
        => throw new ArgumentException(message);

    [DoesNotReturn]
    internal static void ThrowArgumentException(string message, string paramName)
        => throw new ArgumentException(message, paramName);

    [DoesNotReturn]
    internal static void ThrowArgumentNullException(string message, string paramName)
        => throw new ArgumentNullException(paramName, message);

    [DoesNotReturn]
    internal static void ThrowIndexOutOfRangeException(string message)
        => throw new IndexOutOfRangeException(message);

    [DoesNotReturn]
    internal static void ThrowIndexOutOfRangeException(string message, object argument)
        => throw new IndexOutOfRangeException(string.Format(message, argument));

    [DoesNotReturn]
    internal static void ThrowNotSupportedException(string? message = null)
        => throw new NotSupportedException(message);

    [DoesNotReturn]
    internal static void ThrowGaussExceptionWithInnerTimeoutException(string message)
        => throw new GaussException(message, new TimeoutException());
}