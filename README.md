# 总体计划

|语言| 驱动名称                                                                                 | 对应PostgreSQL驱动                                                 | ORM框架                                                  |备注|
|----|--------------------------------------------------------------------------------------|----------------------------------------------------------------|--------------------------------------------------------|----|
|JAVA| [gaussdb-r2dbc](https://github.com/HuaweiCloudDeveloper/gaussdb-r2dbc)                  | [r2dbc-postgresql](https://github.com/pgjdbc/r2dbc-postgresql) | [Spring Data R2DBC](https://github.com/spring-projects/spring-data-relational/)                                  ||
|GO| [gaussdb-go](https://github.com/HuaweiCloudDeveloper/gaussdb-go)                     | [pgx](https://github.com/jackc/pgx)                            | [GORM](https://github.com/go-gorm/gorm)                ||
|Python| [gaussdb-python](https://github.com/HuaweiCloudDeveloper/gaussdb-python)             | [psycopg](https://github.com/psycopg/psycopg)                  | [SqlAlchemy](https://github.com/sqlalchemy/sqlalchemy) ||
|Python| [gaussdb-python-async](https://github.com/HuaweiCloudDeveloper/gaussdb-python-async) | [asyncpg](https://github.com/MagicStack/asyncpg)               | [SqlAlchemy](https://github.com/sqlalchemy/sqlalchemy) ||
|NodeJS| [gaussdb-node](https://github.com/HuaweiCloudDeveloper/gaussdb-node)                 | [node-postgres](https://github.com/brianc/node-postgres)       | [TypeORM](https://github.com/typeorm/typeorm)          ||
|.NET| [gaussdb-donet](https://github.com/HuaweiCloudDeveloper/gaussdb-donet)               | [npgsql](https://github.com/npgsql/npgsql)                     | [Entity Framework](https://github.com/dotnet/ef6)      ||

## 第一阶段目标
* 驱动可以连接gaussdb，基本功能正常
* 能够至少支撑一个ORM框架使用该驱动适配

# 标准规范

## JAVA

* 驱动名称: gaussdb-r2dbc
* group-id: com.huaweicloud.gaussdb
* artifactid: gaussdb-r2dbc
* package: com.huaweicloud.gaussdb

## GO

* 驱动名称: gaussdb-go
* 模块路径: github.com/HuaweiCloudDeveloper/gaussdb-go
* 包名: gaussdb

## NODEJS

* 驱动名称: gaussdb-node
* 模块路径: github.com/HuaweiCloudDeveloper/gaussdb-node
* 包名: gaussdb

# 重构步骤

## JAVA

* 下载项目，针对PostgreSQL编译和测试通过。
* 重构group-id, artifactid, 重构package
* 针对PostgreSQL编译和测试通过
* 修改认证逻辑
* 针对GaussDB编译和测试通过

## GO

1. 下载项目，针对PostgreSQL编译和测试通过
  * 从 github.com/jackc/pgx fork项目到 github.com/HuaweiCloudDeveloper/gaussdb-go。
  * 下载 gaussdb-go 项目源码。
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

1. FORK并下载项目(https://github.com/brianc/node-postgres)，针对PostgreSQL编译和测试通过
  * 

2. 重构模块路径和包名
  * 将模块路径从 github.com/brianc/node-postgres 修改为 github.com/HuaweiCloudDeveloper/gaussdb-node
  * 将包名从 pg 修改为 gaussdb。
  * 重构所有pg关联模块链。

3. 针对PostgreSQL编译和测试通过
  * 确保在修改模块路径和包名后，项目仍然能够编译并通过所有PostgreSQL的测试用例。

4. 修改认证逻辑
  * 根据GaussDB的认证逻辑与加密模块，修改gaussdb-node中的认证与加密模块。
  * 针对GaussDB跑相关模块测试用例通过

5. 针对GaussDB编译和测试通过
  * 针对GaussDB进行编译和跑测试用例，确保兼容的功能测试通过
  * 对测试用例进行修改把针对pg特性的测试用例剔除/修改为gaussdb特性测试用例
  * 重新测试通过

6. 根据GaussDB新特性功能持续优化驱动迭代至兼容最新版Gaussdb
  * 根据GaussDB最新特性功能编译优化驱动
  * 针对GaussDB新特性驱动功能验证
  * 完成GaussDB新特性功能测试用例编写


# 详细开发任务

详细开发任务在issues里面提交： https://github.com/HuaweiCloudDeveloper/gassdb-drivers/issues 

