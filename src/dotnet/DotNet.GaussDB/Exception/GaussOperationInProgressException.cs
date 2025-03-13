using Gauss.Internal;

namespace Gauss;

public class GaussOperationInProgressException : GaussException
{
    public GaussOperationInProgressException(GaussCommand command)
        : base("A command is already in progress: " + command.CommandText)
        => CommandInProgress = command;

    internal GaussOperationInProgressException(ConnectorState state)
        : base($"The connection is already in state '{state}'")
    {
    }

    public GaussCommand? CommandInProgress { get; }
}