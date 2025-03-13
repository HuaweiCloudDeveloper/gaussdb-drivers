# 统一语言

统一语言，也叫通用语言（UBIQUITOUS LANGUAGE），用于定义上下文的含义。

- 定义：提炼领域知识的产出物，体现在两个方面：① 统一的领域术语；②领域行为描述。
- 如何获取：统一语言就是需求分析的过程，也是团队中各个角色就系统目标、范围与具体功能达成一致的过程。
- 强调统一：无论是与领域专家的讨论，还是最终的实现代码，都使用相同的术语。
- 强调约束：既要有内涵也要有外延。



| 概念           | 英文                            | 定义                                                         |
| -------------- | ------------------------------- | ------------------------------------------------------------ |
| 数据源构建器   | DataSourceBuilder               | 用于构建数据源的对象，如 `GaussDBDataSourceBuilder` 可根据连接字符串构建 `GaussDBDataSource` 对象 |
| 数据源         | DataSource                      | 代表数据库连接的数据源，如通过 `GaussDBDataSourceBuilder` 构建的 `dataSource` 对象，用于打开数据库连接 |
| 数据库连接     | Database Connection             | 与数据库建立的连接，如通过 `dataSource.OpenConnectionAsync()` 打开的 `conn` 对象，用于执行数据库操作 |
| 数据库命令     | Database Command                | 用于执行 SQL 语句的命令对象，如 `GaussDBCommand`，可执行插入、查询等操作 |
| 数据读取器     | Data Reader                     | 用于读取数据库查询结果的对象，如 `GaussDBDataReader`，可逐行读取查询结果 |
| 数据库参数集合 | Database Parameter Collection   | 表示与 `GaussDBCommand` 相关的参数集合以及它们与 `DataSet` 中列的映射，如 `GaussDBParameterCollection` |
| 预备语句       | Prepared Statement              | 内部表示已准备好、正在准备或候选准备（即等待进一步使用）的语句，如 `PreparedStatement` |
| 数据库模式信息 | Database Schema Information     | 提供有关数据库模式的信息，如 `GaussDBSchema` 类用于获取表、列等模式信息 |
| 数据库列信息   | Database Column Information     | 提供有关数据库列的模式信息，如 `GaussDBDbColumn` 类          |
| 数据库信息     | Database Information            | 提供有关 PostgreSQL 及类似数据库的信息，如 `GaussDBDatabaseInfo` 类 |
| 复制连接       | Physical Replication Connection | 用于物理复制的数据库连接，如 `PhysicalReplicationConnection` 类 |



## 数据源 DataSource

| 概念         | 英文                       | 定义                                                         |
| ------------ | -------------------------- | ------------------------------------------------------------ |
| 数据源       | DataSource                 | 表示数据库连接的配置来源，如 `GaussDBDataSource` 类，通过数据源构建器创建，包含连接字符串、连接池配置等信息，用于生成数据库连接对象 |
| 数据源构建器 | DataSource Builder         | 用于创建数据源的工厂类，如 `GaussDBDataSourceBuilder`，可通过链式调用配置连接字符串、超时时间、SSL 模式等参数，最终生成 `GaussDBDataSource` 对象 |
| 连接字符串   | Connection String          | 数据源的核心配置，包含数据库地址、端口、认证信息等，如 `host=localhost;port=5432;username=postgres;password=xxx;database=mydb`，用于建立物理连接 |
| 连接池管理   | Connection Pool Management | 数据源内置的连接池机制，通过 `Pooling` 配置项控制是否启用，自动管理连接的创建、复用和释放，提高连接效率 |
| 异步构建     | Asynchronous Construction  | 支持异步创建数据源的方式，如 `BuildAsync` 方法，允许在不阻塞主线程的情况下初始化连接池和验证配置 |
| 配置选项     | Configuration Options      | 数据源的可配置参数集合，包括 `CommandTimeout`（命令超时）、`SslMode`（SSL 加密模式）、`MaxAutoPrepare`（最大预编译语句数）等，通过 `UseNpgsql` 方法链式设置 |
| 连接验证     | Connection Validation      | 数据源启动时的连接测试机制，通过 `ValidateConnection` 方法检查连接字符串有效性，确保配置正确后再投入使用 |
| 读写模式     | Read-Write Mode            | 数据源默认模式，允许执行所有数据库操作，包括查询和写入，对应 `ConnectionState.Open` 状态下的连接行为 |
| 只读模式     | Read-Only Mode             | 限制数据源仅执行查询操作的模式，通过 `WithReadOnly` 方法配置，可提升高并发读场景下的性能和安全性 |
| 复制配置     | Replication Configuration  | 用于数据库复制的特殊配置，如 `ReplicationMode` 设置主从复制模式，`ApplicationName` 标识复制客户端名称 |
| 超时设置     | Timeout Settings           | 控制数据源操作的超时时间，包括 `ConnectionTimeout`（连接超时）、`CommandTimeout`（命令执行超时）等，防止长时间阻塞 |
| 数据源工厂   | DataSource Factory         | 创建数据源的工厂接口，如 `NpgsqlDataSourceFactory`，支持自定义数据源构建逻辑，扩展框架的灵活性 |
| 共享配置     | Shared Configuration       | 多个数据源实例共享的基础配置，通过 `DataSourceOptions` 类实现，减少重复代码并统一管理连接策略 |



## 数据库连接 Database Connection

| 概念       | 英文                     | 定义                                                         |
| ---------- | ------------------------ | ------------------------------------------------------------ |
| 数据库连接 | Database Connection      | 用于在应用程序和数据库之间建立通信链路的对象，在代码中如 `GaussDBConnection` 类，通过它可以执行诸如打开、关闭连接，以及创建命令对象等操作，从而实现与数据库的交互 |
| 连接字符串 | Connection String        | 包含建立数据库连接所需信息的字符串，例如数据库服务器地址、端口、用户名、密码等，`GaussDBConnection` 在创建连接时会使用连接字符串来确定连接的目标数据库 |
| 连接状态   | Connection State         | 表示数据库连接当前所处的状态，如 `ConnectionState` 枚举，包含 `Closed`（关闭）、`Open`（打开）、`Connecting`（正在连接）等状态，`GaussDBConnection` 类中的 `State` 属性可以获取当前连接的状态 |
| 连接超时   | Connection Timeout       | 指在尝试建立数据库连接时允许的最大等待时间，若在该时间内未能成功建立连接，则会抛出超时异常，例如在创建 `GaussDBConnection` 时可以设置连接超时时间 |
| 异步连接   | Asynchronous Connection  | 支持异步操作的数据库连接方式，如 `GaussDBConnection` 类提供了 `OpenAsync` 方法，允许应用程序在不阻塞主线程的情况下异步打开数据库连接，提高应用程序的响应性能 |
| 连接池     | Connection Pool          | 用于管理和复用数据库连接的机制，在代码中相关的数据源类（如 `GaussDBDataSource`）会使用连接池，当需要新的连接时，会优先从连接池中获取可用的连接，使用完毕后将连接返回给连接池，以减少连接的创建和销毁开销 |
| 事务连接   | Transactional Connection | 在数据库事务操作中使用的连接，`GaussDBConnection` 支持事务操作，通过 `BeginTransaction` 方法可以开始一个事务，在事务期间执行的所有操作要么全部成功提交，要么全部回滚，以保证数据的一致性 |
| 只读连接   | Read-Only Connection     | 一种限制为只读操作的数据库连接，例如在某些场景下，应用程序只需要从数据库中读取数据而不需要进行写入操作，此时可以将 `GaussDBConnection` 设置为只读模式，以提高安全性和性能 |
| 复制连接   | Replication Connection   | 用于数据库复制操作的连接，如 `ReplicationConnection` 类，它专门用于处理数据库的复制相关任务，如物理复制、逻辑复制等 |



## 命令 Database Command

| 概念         | 英文                     | 定义                                                         |
| ------------ | ------------------------ | ------------------------------------------------------------ |
| 数据库命令   | Database Command         | 用于对数据库执行操作的指令，在代码中如 `GaussDBCommand` 类，它可以包含要执行的 SQL 语句、相关参数等，通过它可以执行诸如查询、插入、更新、删除等数据库操作 |
| 批处理命令   | Batch Command            | 一种可以批量执行多个数据库操作的命令，例如 `GaussDBBatchCommand`，可用于一次性执行多个 SQL 语句，提高执行效率 |
| 存储过程命令 | Stored Procedure Command | 用于调用数据库中存储过程的命令，在代码中针对 `CommandType.StoredProcedure` 类型的命令，会进行相应的处理，如构建特定的 SQL 语句来调用存储过程 |
| 表直接命令   | Table Direct Command     | 针对直接操作数据库表的命令，如 `CommandType.TableDirect` 类型的命令，通常用于执行类似 `SELECT * FROM` 表名的操作 |
| 命令文本     | Command Text             | 数据库命令中包含的 SQL 语句文本，例如 `GaussDBCommand` 类中的 `CommandText` 属性，它存储了要执行的具体 SQL 命令 |
| 命令类型     | Command Type             | 表示数据库命令的类型，如 `CommandType` 枚举，包含 `StoredProcedure`（存储过程）、`TableDirect`（表直接操作）、`Text`（普通文本 SQL 命令）等类型 |
| 命令参数     | Command Parameters       | 与数据库命令相关的参数，例如 `GaussDBParameterCollection` 类表示参数集合，在执行命令时可以将参数传递给 SQL 语句，实现动态查询等功能 |
| 命令构建器   | Command Builder          | 用于创建数据库命令的对象，如 `GaussDBCommandBuilder` 类，它可以创建用于自动插入、更新和删除操作的数据库命令 |
| 数据源命令   | DataSource Command       | 与数据源相关的数据库命令，例如 `GaussDBDataSourceCommand` 类，用于针对特定数据源创建和执行命令 |



## 读取器 Data Reader

| 概念           | 英文                       | 定义                                                         |
| -------------- | -------------------------- | ------------------------------------------------------------ |
| 数据读取器     | Data Reader                | 用于从数据库查询结果中读取数据的对象，例如 `GaussDBDataReader` 类，它提供了逐行读取数据的功能，允许应用程序按顺序访问查询结果集中的每一行数据 |
| 嵌套数据读取器 | Nested Data Reader         | 用于处理嵌套数据的读取器，像 `GaussDBNestedDataReader` 类，当需要读取数据库中嵌套结构的数据（如复合类型或数组）时，可以使用嵌套数据读取器来访问这些嵌套的数据 |
| 读取状态       | Reading State              | 表示数据读取器当前的读取状态，如 `ReaderState` 枚举，包含 `BeforeResult`（结果集之前）、`InResult`（在结果集中）、`BetweenResults`（结果集之间）、`Consumed`（已消耗）、`Closed`（已关闭）、`Disposed`（已释放）等状态，`GaussDBDataReader` 的 `State` 属性可以获取当前的读取状态 |
| 顺序读取       | Sequential Reading         | 一种按顺序读取数据的方式，例如在 `GaussDBDataReader` 中，当 `_isSequential` 标志为 `true` 时，表示使用顺序读取模式，这种模式下读取数据有一定的顺序要求 |
| 非顺序读取     | Non-Sequential Reading     | 与顺序读取相对，不按顺序读取数据的方式，在 `GaussDBDataReader` 中也支持非顺序读取，可根据需要灵活访问结果集中的数据 |
| 列序数         | Column Ordinal             | 表示数据读取器中列的索引，从 0 开始，例如在 `GaussDBDataReader` 的 `GetData` 方法中，通过列序数来指定要访问的列 |
| 列信息读取     | Column Information Reading | 读取关于数据库列的相关信息，如列的数据类型、长度等，`GaussDBDataReader` 可以提供列的元数据信息，方便应用程序了解和处理数据 |
| 数据格式读取   | Data Format Reading        | 读取数据时涉及的数据格式处理，例如数据可能以文本格式（`DataFormat.Text`）或二进制格式存储，`GaussDBDataReader` 在读取数据时会根据数据格式进行相应的处理 |
| 读取消息       | Reading Message            | 从数据库连接中读取消息的操作，如 `GaussDBDataReader` 中的 `ReadMessage` 方法，用于读取数据库服务器发送的消息，根据消息类型进行不同的处理 |



## 数据库参数集合 Database Parameter Collection

| 概念           | 英文                             | 定义                                                         |
| -------------- | -------------------------------- | ------------------------------------------------------------ |
| 数据库参数集合 | Database Parameter Collection    | 用于存储和管理与数据库命令相关的参数的集合对象，例如在 `GaussDBCommand` 中使用的参数集合，它可以包含多个参数，这些参数可用于动态构建 SQL 语句，避免 SQL 注入风险，增强代码的安全性和灵活性。 |
| 参数添加       | Parameter Addition               | 向数据库参数集合中添加新参数的操作。例如使用 `GaussDBParameterCollection` 的 `Add` 方法，可将一个 `GaussDBParameter` 对象添加到集合中，方便后续在执行数据库命令时使用这些参数。 |
| 参数查找       | Parameter Lookup                 | 根据参数名称或索引从数据库参数集合中查找特定参数的操作。可以通过参数名称调用 `GaussDBParameterCollection` 的索引器来获取对应的参数对象，也可以通过索引值进行查找，以便对特定参数进行修改或使用。 |
| 参数移除       | Parameter Removal                | 从数据库参数集合中移除指定参数的操作。使用 `GaussDBParameterCollection` 的 `Remove` 或 `RemoveAt` 方法，可分别根据参数对象或参数索引将其从集合中移除，以调整命令所需的参数。 |
| 参数计数       | Parameter Count                  | 表示数据库参数集合中当前包含的参数数量。通过 `GaussDBParameterCollection` 的 `Count` 属性可以获取该值，有助于了解集合的大小，进行循环操作或判断是否满足执行条件。 |
| 参数验证       | Parameter Validation             | 在添加或使用参数时，对参数的有效性进行检查的操作。确保参数的数据类型、长度、取值范围等符合数据库表结构和 SQL 语句的要求，避免因参数错误导致数据库操作失败。 |
| 参数类型       | Parameter Type                   | 每个参数所具有的数据类型，如整数、字符串、日期等。在 `GaussDBParameter` 中可通过 `DbType` 或 `NpgsqlDbType` 属性来指定参数的类型，以确保参数能正确地与数据库中的数据类型匹配。 |
| 参数值         | Parameter Value                  | 参数所携带的实际数据值。在 `GaussDBParameter` 中通过 `Value` 属性设置和获取该值，这些值将被传递到 SQL 语句中，用于动态查询或更新数据库。 |
| 参数方向       | Parameter Direction              | 指示参数在数据库操作中的流向，如输入参数（用于向数据库传递数据）、输出参数（用于从数据库获取返回值）、输入输出参数（既传递数据又获取返回值）和返回值参数（用于存储存储过程的返回值）。在 `GaussDBParameter` 中通过 `Direction` 属性进行设置。 |
| 参数集合清理   | Parameter Collection Clearance   | 清空数据库参数集合中所有参数的操作。使用 `GaussDBParameterCollection` 的 `Clear` 方法，可将集合恢复到初始状态，方便重新配置参数。 |
| 参数集合枚举   | Parameter Collection Enumeration | 对数据库参数集合中的所有参数进行遍历的操作。可以使用 `foreach` 循环结合 `GaussDBParameterCollection` 的枚举功能，依次访问集合中的每个参数，便于进行批量处理。 |



## 预备语句 Prepared Statement

| 概念               | 英文                                  | 定义                                                         |
| ------------------ | ------------------------------------- | ------------------------------------------------------------ |
| 预准备语句         | Prepared Statement                    | 一种预先编译的 SQL 语句，在数据库中进行一次解析和优化后可多次执行。在 `DotNetCore.GaussDB` 里，如 `GaussDBCommand` 可将 SQL 语句以预准备语句形式处理，通过绑定不同参数值多次执行，提高性能和安全性。 |
| 预准备语句缓存     | Prepared Statement Cache              | 用于存储预准备语句的缓存机制。数据库连接维护一个预准备语句缓存，当相同的 SQL 语句需要再次执行时，可直接从缓存中获取已编译好的预准备语句，避免重复编译，提高执行效率。 |
| 预准备语句绑定     | Prepared Statement Binding            | 为预准备语句中的参数绑定具体值的操作。在 `GaussDBCommand` 中，通过 `GaussDBParameter` 对象为预准备语句的参数赋值，使预准备语句能根据不同的参数值执行不同的查询或操作。 |
| 预准备语句执行     | Prepared Statement Execution          | 执行预准备语句的操作。在参数绑定完成后，调用 `GaussDBCommand` 的执行方法（如 `ExecuteNonQuery`、`ExecuteReader` 等）来执行预准备语句，获取相应的执行结果。 |
| 预准备语句生命周期 | Prepared Statement Lifecycle          | 预准备语句从创建、编译、使用到最终销毁的整个过程。创建时对 SQL 语句进行编译，使用时绑定参数并执行，当不再需要时，数据库会根据一定规则（如缓存策略）决定是否销毁该预准备语句。 |
| 自动预准备         | Automatic Preparation                 | 数据库连接自动将符合条件的 SQL 语句转换为预准备语句的功能。通过配置相关参数，数据库连接可以自动判断哪些 SQL 语句适合预准备，提高应用程序的性能，减少手动配置的工作量。 |
| 预准备语句最大数量 | Maximum Number of Prepared Statements | 数据库连接允许同时存在的预准备语句的最大数量。通过配置参数可以限制预准备语句缓存的大小，避免占用过多的数据库资源。 |
| 预准备语句失效     | Prepared Statement Invalidating       | 使预准备语句失效的操作。当数据库表结构发生变化或 SQL 语句需要更新时，可能需要使相关的预准备语句失效，以确保后续执行的语句是最新且正确的。 |
| 预准备语句状态     | Prepared Statement State              | 预准备语句当前所处的状态，如已准备好、正在准备、已失效等。不同的状态反映了预准备语句在生命周期中的不同阶段，有助于开发人员进行调试和管理。 |
| 预准备语句元数据   | Prepared Statement Metadata           | 关于预准备语句的元数据信息，包括参数数量、参数类型、返回结果集的列信息等。通过获取预准备语句的元数据，开发人员可以更好地了解和处理预准备语句的执行结果。 |



## 数据库模式信息 Database Schema Information

| 概念           | 英文                          | 定义                                                         |
| -------------- | ----------------------------- | ------------------------------------------------------------ |
| 数据库模式信息 | Database Schema Information   | 描述数据库中对象（如表、视图、列、索引等）的结构和组织的元数据。它定义了数据库的逻辑结构，包括对象的名称、类型、关系以及约束等信息，在 `DotNetCore.GaussDB` 中可通过特定方法获取这些信息。 |
| 模式名称       | Schema Name                   | 数据库模式的标识符，用于区分不同的模式。在 PostgreSQL 中可以有多个模式，每个模式可以包含自己的表、视图等对象。例如，在查询特定模式下的表时，需要指定模式名称。 |
| 表模式信息     | Table Schema Information      | 关于数据库表的详细结构信息，包括表名、列名、列的数据类型、主键、外键、约束等。通过获取表模式信息，开发人员可以了解表的结构，以便进行数据操作和查询。 |
| 列模式信息     | Column Schema Information     | 每列的详细描述，如列名、数据类型、是否允许为空、默认值等。在 `GaussDBDataReader` 或相关的模式获取方法中，可以获取到列的模式信息，这对于动态处理数据非常有用。 |
| 视图模式信息   | View Schema Information       | 视图的定义和结构信息，视图是基于一个或多个表的查询结果的虚拟表。视图模式信息包括视图的名称、查询定义以及涉及的列等，有助于理解视图的逻辑和使用方式。 |
| 索引模式信息   | Index Schema Information      | 数据库索引的相关信息，如索引名称、索引类型（如 B - 树索引、哈希索引等）、索引所涉及的列。索引用于提高数据查询的效率，了解索引模式信息可以帮助优化数据库查询。 |
| 约束模式信息   | Constraint Schema Information | 数据库中施加在表或列上的约束条件的信息，包括主键约束、唯一约束、外键约束、检查约束等。约束模式信息定义了数据的完整性规则，确保数据的准确性和一致性。 |
| 序列模式信息   | Sequence Schema Information   | 序列是一种用于生成唯一整数的数据库对象。序列模式信息包括序列的名称、起始值、增量值等，在需要自动生成唯一标识符的场景中会用到序列。 |
| 模式信息查询   | Schema Information Query      | 从数据库中获取模式信息的操作。在 `DotNetCore.GaussDB` 中，可以使用特定的 SQL 查询语句或相关的 API 方法来查询数据库的模式信息，例如查询 `pg_catalog` 系统表。 |
| 模式信息缓存   | Schema Information Cache      | 为了提高性能，对获取到的模式信息进行缓存的机制。当多次需要相同的模式信息时，可以直接从缓存中获取，避免重复查询数据库。 |
| 模式信息更新   | Schema Information Update     | 当数据库的结构发生变化（如创建、修改或删除表、列等）时，更新模式信息的操作。确保应用程序获取到的模式信息是最新的，以正确处理数据。 |



## 数据库列信息 Database Column Information

| 概念           | 英文                           | 定义                                                         |
| -------------- | ------------------------------ | ------------------------------------------------------------ |
| 数据库列信息   | Database Column Information    | 描述数据库表中列的各种属性和特征的元数据，它是数据库模式信息的重要组成部分，有助于了解列的用途、数据存储要求等，在 `DotNetCore.GaussDB` 中可借助相关方法获取。 |
| 列名           | Column Name                    | 每列的标识符，用于在 SQL 查询和程序代码中引用该列。列名应具有一定的描述性，以便开发人员和用户理解该列存储的数据内容。 |
| 列数据类型     | Column Data Type               | 定义列中存储的数据的类型，如整数（`int`）、字符串（`varchar`）、日期（`date`）等。不同的数据类型决定了列可以存储的数据范围和操作方式。 |
| 列长度         | Column Length                  | 对于某些数据类型（如字符串类型），指定列可以存储的最大字符数或字节数。例如，`varchar(50)` 表示该列最多可存储 50 个字符。 |
| 列是否允许为空 | Column Nullability             | 指示该列是否可以存储空值（`NULL`）。如果设置为不允许为空，则在插入或更新数据时，该列必须有一个有效的值。 |
| 列默认值       | Column Default Value           | 当插入数据时，如果没有为该列提供值，则使用的默认值。默认值可以是一个固定的值，也可以是一个函数（如当前日期函数）。 |
| 列主键标识     | Column Primary Key Indicator   | 表明该列是否是表的主键的一部分。主键用于唯一标识表中的每一行，通常具有唯一性和非空性的约束。 |
| 列外键关联     | Column Foreign Key Association | 如果该列是外键，则描述它与其他表中列的关联关系。外键用于建立表之间的关系，确保数据的参照完整性。 |
| 列注释         | Column Comment                 | 对列的用途和含义进行的文本描述，有助于开发人员和数据库管理员理解列的作用。在 PostgreSQL 中，可以为列添加注释，方便后续维护。 |
| 列顺序         | Column Order                   | 列在表中的物理顺序，通常从左到右编号。虽然在 SQL 查询中列的顺序不影响数据的逻辑结构，但在某些场景下（如按特定顺序显示数据）可能会用到。 |
| 列统计信息     | Column Statistical Information | 关于列中数据的统计信息，如数据的分布、唯一值的数量等。数据库系统会利用这些统计信息来优化查询执行计划。 |
| 列信息查询     | Column Information Query       | 从数据库中获取列信息的操作。在 `DotNetCore.GaussDB` 中，可以通过执行特定的 SQL 查询（如查询系统表 `pg_attribute`）或使用相关的 API 方法来获取列的详细信息。 |
| 列信息更新     | Column Information Update      | 当列的属性（如数据类型、长度、默认值等）发生变化时，更新列信息的操作。更新列信息可能会影响到相关的数据库操作和程序代码。 |



## 数据库信息 Database Information

| 概念       | 英文                     | 定义                                                         |
| ---------- | ------------------------ | ------------------------------------------------------------ |
| 数据库信息 | Database Information     | 描述数据库本身的元数据，包括名称、版本、状态、配置等基础属性。在 `DotNetCore.GaussDB` 中可通过 `GaussDBDatabaseInfo` 类或 `GaussDBConnection` 的扩展方法获取这些信息。 |
| 数据库名称 | Database Name            | 数据库的唯一标识符，用于区分同一实例中的不同数据库。例如在连接字符串中通过 `database=mydb` 指定目标数据库名称。 |
| 数据库版本 | Database Version         | 数据库管理系统的版本号，如 PostgreSQL 14.5。可通过 `SELECT version();` 或 `GaussDBConnection.ServerVersion` 获取。 |
| 创建时间   | Creation Time            | 数据库被创建的时间戳，记录数据库初始化的时间。在 PostgreSQL 中可通过查询 `pg_catalog.pg_database` 系统表获取。 |
| 数据库状态 | Database State           | 数据库当前的运行状态，如在线（`Online`）、离线（`Offline`）、恢复中（`InRecovery`）等。 |
| 数据库编码 | Database Encoding        | 数据库使用的字符编码方式，如 UTF8。编码决定了数据库支持的字符集范围。 |
| 数据库用户 | Database User            | 当前连接使用的数据库用户名，通过 `GaussDBConnection.User` 获取。 |
| 数据库权限 | Database Privileges      | 当前用户对数据库的操作权限，如读写权限、模式创建权限等。     |
| 数据库大小 | Database Size            | 数据库占用的磁盘空间大小，可通过 `SELECT pg_database_size('mydb');` 查询。 |
| 当前连接数 | Current Connections      | 当前连接到数据库的客户端数量，用于监控性能。                 |
| 数据库时区 | Database Timezone        | 数据库服务器使用的时区设置，影响日期时间类型的存储和显示。   |
| 字符集     | Character Set            | 数据库使用的字符集，如 `SQL_ASCII`、`UTF8`，与编码密切相关。 |
| 配置参数   | Configuration Parameters | 数据库的运行配置，如 `max_connections`、`work_mem` 等，可通过 `SHOW` 命令或系统表查询。 |
| 数据库扩展 | Database Extensions      | 已安装的数据库扩展功能，如 `pgcrypto`、`hstore`，扩展数据库的原生能力。 |
| 备份信息   | Backup Information       | 数据库备份相关的元数据，如最后备份时间、备份类型（全量 / 增量）等。 |
| 日志信息   | Logging Information      | 数据库日志的配置和状态，包括日志级别、存储路径等。           |



## 复制连接 Physical Replication Connection

| 概念         | 英文                            | 定义                                                         |
| ------------ | ------------------------------- | ------------------------------------------------------------ |
| 物理复制连接 | Physical Replication Connection | 用于在数据库系统中建立物理复制过程的连接通道，在 PostgreSQL 等数据库中，物理复制是基于数据块级别的复制方式，可确保主库和备库的数据一致性。在 `DotNetCore.GaussDB` 中，借助特定类和方法可创建和管理这种连接，以实现数据的物理复制。 |
| 连接参数     | Connection Parameters           | 建立物理复制连接时所需的一系列参数，包括数据库服务器的主机地址、端口号、用户名、密码、复制槽名称等。这些参数在创建 `PhysicalReplicationConnection` 对象时进行配置，确保连接到正确的主库并使用合适的复制槽。 |
| 复制槽       | Replication Slot                | 物理复制中用于跟踪主库事务进度的重要机制。复制槽为备库保留主库的 WAL（Write - Ahead Logging）日志，确保即使备库暂时断开连接，主库也不会过早删除备库需要的日志。在创建物理复制连接时，需要指定使用的复制槽名称。 |
| 连接建立     | Connection Establishment        | 通过调用 `PhysicalReplicationConnection` 的相应方法（如 `Open` 方法）来建立与主库的物理复制连接的操作。在建立连接过程中，会验证连接参数的有效性，尝试与主库进行通信并进行身份验证。 |
| 连接状态     | Connection State                | 物理复制连接当前所处的状态，如 `Closed`（关闭）、`Connecting`（正在连接）、`Open`（已打开）等。通过 `PhysicalReplicationConnection` 的 `State` 属性可以获取连接状态，方便开发人员进行连接管理和错误处理。 |
| 连接断开     | Connection Disconnection        | 终止物理复制连接的操作，可通过调用 `PhysicalReplicationConnection` 的 `Close` 方法实现。连接断开后，备库将停止从主库接收 WAL 日志。 |
| 数据同步     | Data Synchronization            | 物理复制连接建立后，主库将 WAL 日志发送给备库，备库根据这些日志对自身的数据进行更新，从而实现主备库之间的数据同步。这是物理复制连接的核心功能，确保备库的数据与主库保持一致。 |
| 日志接收     | Log Receiving                   | 备库通过物理复制连接从主库接收 WAL 日志的过程。`PhysicalReplicationConnection` 提供了相应的方法来处理日志的接收和解析，将接收到的日志应用到备库的数据文件中。 |
| 流复制       | Streaming Replication           | 物理复制的一种常见方式，主库以流的形式持续将 WAL 日志发送给备库。在 `DotNetCore.GaussDB` 中，物理复制连接支持流复制，可实现实时的数据同步。 |
| 复制错误处理 | Replication Error Handling      | 在物理复制过程中，可能会出现各种错误，如网络中断、日志解析错误等。需要对这些错误进行捕获和处理，通过 `PhysicalReplicationConnection` 的异常处理机制，开发人员可以采取相应的措施，如重试连接、记录错误日志等。 |
| 连接监控     | Connection Monitoring           | 对物理复制连接的状态、性能等进行实时监控的操作。通过监控连接的相关指标（如连接时间、日志接收速率等），可以及时发现潜在的问题并进行优化。 |