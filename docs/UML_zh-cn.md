# 统一语言

DDD 战略设计的第一步就是统一语言，也叫通用语言（UBIQUITOUS LANGUAGE），用于定义上下文的含义。

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