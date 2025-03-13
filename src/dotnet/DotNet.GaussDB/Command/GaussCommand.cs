using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Gauss;
public class GaussCommand: DbCommand, ICloneable, IComponent
{
    string _commandText;
    
    public override void Cancel()
    {
        throw new NotImplementedException();
    }

    public override int ExecuteNonQuery()
    {
        throw new NotImplementedException();
    }

    public override object? ExecuteScalar()
    {
        throw new NotImplementedException();
    }

    public override void Prepare()
    {
        throw new NotImplementedException();
    }

   
    [AllowNull, DefaultValue("")]
    [Category("Data")]
    public override string CommandText
    {
        get => _commandText;
        set
        {
            _commandText = value ?? string.Empty;
        }
    }
    
    public override int CommandTimeout { get; set; }
    public override CommandType CommandType { get; set; }
    public override UpdateRowSource UpdatedRowSource { get; set; }
    protected override DbConnection? DbConnection { get; set; }
    protected override DbParameterCollection DbParameterCollection { get; }
    protected override DbTransaction? DbTransaction { get; set; }
    public override bool DesignTimeVisible { get; set; }

    protected override DbParameter CreateDbParameter()
    {
        throw new NotImplementedException();
    }

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
        throw new NotImplementedException();
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }
}