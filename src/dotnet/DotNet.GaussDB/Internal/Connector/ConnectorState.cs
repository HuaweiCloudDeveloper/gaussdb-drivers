namespace Gauss.Internal;

/// <summary>
/// Expresses the exact state of a connector.
/// </summary>
enum ConnectorState
{
    /// <summary>
    /// The connector has either not yet been opened or has been closed.
    /// </summary>
    Closed,

    /// <summary>
    /// The connector is currently connecting to a PostgreSQL server.
    /// </summary>
    Connecting,

    /// <summary>
    /// The connector is connected and may be used to send a new query.
    /// </summary>
    Ready,

    /// <summary>
    /// The connector is waiting for a response to a query which has been sent to the server.
    /// </summary>
    Executing,

    /// <summary>
    /// The connector is currently fetching and processing query results.
    /// </summary>
    Fetching,

    /// <summary>
    /// The connector is currently waiting for asynchronous notifications to arrive.
    /// </summary>
    Waiting,

    /// <summary>
    /// The connection was broken because an unexpected error occurred which left it in an unknown state.
    /// This state isn't implemented yet.
    /// </summary>
    Broken,

    /// <summary>
    /// The connector is engaged in a COPY operation.
    /// </summary>
    Copy,

    /// <summary>
    /// The connector is engaged in streaming replication.
    /// </summary>
    Replication,
}