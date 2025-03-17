# GaussDB开源驱动介绍

本项目的目的是帮助繁荣[GaussDB](https://www.huaweicloud.com/product/gaussdb.html)和[OpenGauss](https://opengauss.org/zh/)开源生态。项目包括常见语言的驱动（早期都通过PostgreSQL开源驱动进行构建），在这些开源驱动的基础上，贡献其他开源软件，包括ORM等。

# 总体计划

| 语言   | 驱动名称                                                                          | 对应PostgreSQL驱动                                                 | ORM框架                                                                             | 备注                |
| ------ | --------------------------------------------------------------------------------- | ------------------------------------------------------------------ | ----------------------------------------------------------------------------------- | ------------------- |
| JAVA   | [gaussdb-r2dbc](https://github.com/HuaweiCloudDeveloper/gaussdb-r2dbc)               | [r2dbc-postgresql](https://github.com/pgjdbc/r2dbc-postgresql)        | [Spring Data R2DBC](https://github.com/spring-projects/spring-data-relational/)        |                     |
| GO     | [gaussdb-go](https://github.com/HuaweiCloudDeveloper/gaussdb-go)                     | [pgx](https://github.com/jackc/pgx)                                   | [GORM](https://github.com/go-gorm/gorm)                                                |                     |
| Python | [gaussdb-python](https://github.com/HuaweiCloudDeveloper/gaussdb-python)             | [psycopg](https://github.com/psycopg/psycopg)                         | [SqlAlchemy](https://github.com/sqlalchemy/sqlalchemy)                                 |                     |
| Python | [gaussdb-python-async](https://github.com/HuaweiCloudDeveloper/gaussdb-python-async) | [asyncpg](https://github.com/MagicStack/asyncpg)                      | [SqlAlchemy](https://github.com/sqlalchemy/sqlalchemy)                                 |                     |
| NodeJS | [gaussdb-node](https://github.com/HuaweiCloudDeveloper/gaussdb-node)                 | [node-postgres](https://github.com/brianc/node-postgres)              | [TypeORM](https://github.com/typeorm/typeorm)                                          |                     |
| .NET   | [gaussdb-dotnet](https://github.com/HuaweiCloudDeveloper/gaussdb-dotnet)             | [npgsql](https://github.com/npgsql/npgsql)                            | [Entity Framework Core](https://github.com/HuaweiCloudDeveloper/gaussdb-dotnet-efcore) |                     |
| PHP    | [gaussdb-php](https://github.com/HuaweiCloudDeveloper/gaussdb-php)                   | [pdo_pgsql](https://github.com/php/php-src/tree/master/ext/pdo_pgsql) | [Laravel](https://github.com/laravel/laravel)                                          | 代码需要推送PHP官网 |
| Rust   | [gaussdb-rust](https://github.com/HuaweiCloudDeveloper/gaussdb-rust)                 | [rust-postgres](https://github.com/sfackler/rust-postgres/)           | [Diesel](https://github.com/diesel-rs/diesel)                                          |                     |

## 第一阶段目标

* 驱动可以连接gaussdb，基本功能正常
* 能够至少支撑一个ORM框架使用该驱动适配

## 第一阶段任务分解

1. 制定项目的规范，包括项目名称、命名空间、代码结构、目录规范、发布规范等。
2. 确保项目合规修改。审核代码Licence信息，确保代码修改过程符合License的要求，确保不涉及抄袭。
3. 修改代码的认证逻辑，能够连接GaussDB。第一阶段任务需要确保SHA256认证方式功能正常，其他认证方式可选。
4. 根据规范，完成代码重构，重构后的代码满足项目规范。
5. 项目代码所有单元测试用例通过，集成测试用例通过，并发布一个版本，书写版本发布说明，版本说明详细介绍本版本所做的修改内容。
6. 参考PostgreSQL的README给出GaussDB的README，内容包括项目功能介绍、快速使用入门、如何进行代码贡献等。

# 标准规范

## JAVA

* 项目信息：

  * 项目名称（仓库名称）：gaussdb-r2dbc
  * 驱动名称(文档、传播): gaussdb-r2dbc
* Maven发布：

  * group-id: com.huaweicloud.gaussdb
  * artifactid: gaussdb-r2dbc
* 应用开发：

  * package: com.huaweicloud.gaussdb
  * 连接串：`r2dbc:gaussdb://<host>:<port>/<database>`
  * 驱动名称: `gaussdb`（ConnectionFactoryOptions）

## GO

* 项目名称: gaussdb-go
* 模块路径: github.com/HuaweiCloudDeveloper/gaussdb-go
* 包名: gaussdb

## NODEJS

* 项目名称: gaussdb-node
* 模块路径: github.com/HuaweiCloudDeveloper/gaussdb-node
* 包名: gaussdb

## C#

- 驱动名称：GaussDB
- 模块路径：github.com/HuaweiCloudDeveloper/gaussdb-dotnet
- Nuget包名：GaussDB

# 重构步骤

## JAVA

* 下载项目，写一个简单测试程序，针对PostgreSQL编译和测试通过。
* 修改认证逻辑
* 测试程序针对GaussDB编译和测试通过
* 重构group-id, artifactid, 重构package
* 针对PostgreSQL编译和测试通过

## GO

1. 下载项目，针对PostgreSQL编译和测试通过

* 下载 pgx 项目源码。
* 确保项目能够在本地编译并通过所有PostgreSQL的测试用例。

2. 重构模块路径和包名

* 将模块路径从 github.com/jackc/pgx 修改为 github.com/HuaweiCloudDeveloper/gaussdb-go。
* 将包名从 pgx 修改为 gaussdb。
* 更新所有导入路径和包引用。

3. 针对PostgreSQL编译和测试通过

* 确保在修改模块路径和包名后，项目仍然能够编译并通过所有PostgreSQL的测试用例。

4. 修改认证逻辑

* 根据GaussDB的认证机制，修改驱动中的认证逻辑。
* 确保新的认证逻辑能够与GaussDB正常通信。

5. 针对GaussDB编译和测试通过

* 针对GaussDB进行编译和测试，确保所有功能正常。

## NODEJS

1. FORK并下载项目 [node-postgres](https://github.com/brianc/node-postgres)，针对PostgreSQL编译和测试通过
2. 修改认证逻辑

* 根据GaussDB的认证逻辑与加密模块，修改gaussdb-node中的认证与加密模块。
* 针对GaussDB跑相关模块测试用例通过

3. 重构模块路径和包名

* 将模块路径从 github.com/brianc/node-postgres 修改为 github.com/HuaweiCloudDeveloper/gaussdb-node
* 将包名从 pg 修改为 gaussdb。
* 重构所有pg关联模块链。

4. 针对GaussDB编译和测试通过

* 针对GaussDB进行编译和跑测试用例，确保兼容的功能测试通过
* 对测试用例进行修改把针对pg特性的测试用例剔除/修改为gaussdb特性测试用例
* 重新测试通过


# 详细开发任务

详细开发任务在issues里面提交： https://github.com/HuaweiCloudDeveloper/gassdb-drivers/issues
