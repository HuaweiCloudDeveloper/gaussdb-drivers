# GaussDB和OpenGauss功能和语法差异

1. GaussDB与OpenGauss存在差异
2. GaussDB不存在的功能
3. OpenGauss不存在的功能


## GaussDB与OpenGauss存在差异

### `escape ''`语法行为差异

默认情况下， 语句 `select * from BasicEntityTest where data like '%\%' escape '';` 结果不同。

* OpenGauss
  无论什么情况，查询结果都是空。
 
* GaussDB
  当data字段包含`\`字符的时候，查询结果不为空。 

* 参考信息：https://gitcode.com/opengauss/openGauss-server/issues/7263

### JSON类型支持差异

* GaussDB 都支持：

```sql
SELECT JSON_CONTAINS('{"a":1,"b":2}'::json, '{"a":1}'); 
SELECT '{"a":1,"b":2}'::jsonb @> '{"a":1}'::jsonb;
```

* OpenGauss 只支持：

```sql
SELECT '{"a":1,"b":2}'::jsonb @> '{"a":1}'::jsonb;
```

* 参考信息：
* https://support.huaweicloud.com/centralized-devg-v8-gaussdb/gaussdb-42-2032.html
* https://docs.opengauss.org/zh/docs/7.0.0-RC2/docs/SQLReference/JSON-JSONB%E7%B1%BB%E5%9E%8B.html

## GaussDB不存在的功能

## OpenGauss不存在的功能

