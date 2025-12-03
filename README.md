<p align="center">
  <p align="center">
    <a href="README_EN.md"><strong>English</strong></a> | <strong>简体中文</strong>
</p>

本项目的目的是帮助繁荣 [GaussDB](https://www.huaweicloud.com/product/gaussdb.html) 和 [OpenGauss](https://opengauss.org/zh/) 开源生态。

项目包括常见语言的驱动，这些驱动目前都通过PostgreSQL开源驱动进行构建。GaussDB开源生态信息参考 [gaussdb-ecosystem](https://github.com/HuaweiCloudDeveloper/gaussdb-ecosystem) 。

> ***如果发现项目能帮助到您，别忘了点击右上角`star`表示鼓励***

# 项目信息

| 语言   | 驱动名称                                                                          | 对应Postgres驱动 | 示例代码                                                                                      | 发布地址                                                                                    |
| ------ | --------------------------------------------------------------------------------- | ------------------------------------------------------------------ |-------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------|
| JAVA   | JDBC       | [PgJDBC](https://github.com/pgjdbc/pgjdbc)        | [示例代码](https://support.huaweicloud.com/distributed-devg-v8-gaussdb/gaussdb-12-0059.html)  | [已发布](https://repo.maven.apache.org/maven2/com/huaweicloud/gaussdb/gaussdbjdbc/)        |
| JAVA   | [gaussdb-r2dbc](https://github.com/HuaweiCloudDeveloper/gaussdb-r2dbc)               | [r2dbc-postgresql](https://github.com/pgjdbc/r2dbc-postgresql)        | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-r2dbc-examples)                    | [已发布](https://repo.maven.apache.org/maven2/com/huaweicloud/gaussdb/gaussdb-r2dbc/)      |
| GO     | [gaussdb-go](https://github.com/HuaweiCloudDeveloper/gaussdb-go)                     | [pgx](https://github.com/jackc/pgx)                                   | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-go/tree/master/examples)           | [已发布](https://pkg.go.dev/github.com/HuaweiCloudDeveloper/gaussdb-go)                    |
| .NET   | [gaussdb-dotnet](https://github.com/HuaweiCloudDeveloper/gaussdb-dotnet)             | [npgsql](https://github.com/npgsql/npgsql)                            | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-dotnet/tree/main/example)          | [已发布](https://www.nuget.org/packages/HuaweiCloud.Driver.GaussDB/)                       |
| Python | [gaussdb-python](https://github.com/HuaweiCloudDeveloper/gaussdb-python)             | [psycopg](https://github.com/psycopg/psycopg)                         | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-python/tree/master/example)        | [已发布](https://pypi.org/project/gaussdb/)                                                |
| Python | [gaussdb-python-async](https://github.com/HuaweiCloudDeveloper/gaussdb-python-async) | [asyncpg](https://github.com/MagicStack/asyncpg)                      | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-python-async/tree/master/examples) | [已发布](https://pypi.org/project/async-gaussdb/)                                          |
| NodeJS | [gaussdb-node](https://github.com/HuaweiCloudDeveloper/gaussdb-node)                 | [node-postgres](https://github.com/brianc/node-postgres)              | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-node/tree/master/examples)         | [已发布](https://www.npmjs.com/package/gaussdb-node)                                       |
| PHP    | [gaussdb-php](https://github.com/HuaweiCloudDeveloper/gaussdb-php)                   | [pdo_pgsql](https://github.com/php/php-src/tree/master/ext/pdo_pgsql) |                                                                                           | 开发中                                                                                     |
| Rust   | [gaussdb-rust](https://github.com/HuaweiCloudDeveloper/gaussdb-rust)                 | [rust-postgres](https://github.com/sfackler/rust-postgres/)           | [示例代码](https://github.com/HuaweiCloudDeveloper/gaussdb-rust/tree/master/examples)         | [已发布](https://crates.io/crates/gaussdb)                                                 |
| C   | libpq   | [libpq](https://github.com/postgres/postgres/blob/master/src/include/libpq/libpq.h)           | [示例代码](https://support.huaweicloud.com/distributed-devg-v8-gaussdb/gaussdb-12-1845.html)  | [已发布](https://support.huaweicloud.com/distributed-devg-v8-gaussdb/gaussdb-12-1845.html) |
| C   | ODBC   | [psqlodbc](https://github.com/postgresql-interfaces/psqlodbc)           | [示例代码](https://support.huaweicloud.com/distributed-devg-v8-gaussdb/gaussdb-12-1832.html)  | [已发布](https://support.huaweicloud.com/distributed-devg-v8-gaussdb/gaussdb-12-1830.html) |

# 其他参考信息

* [GaussDB和PostgresSQL功能和语法差异](docs/diff-gaussdb-postgres.md)
* [GaussDB和OpenGauss功能和语法差异](docs/diff-gaussdb-opengauss.md)
* [GaussDB、OpenGauss、JDBC驱动已知缺陷](docs/known-bugs.md)
