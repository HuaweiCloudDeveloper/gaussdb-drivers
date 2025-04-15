# GaussDB和PostgresSQL功能和语法差异

## GaussDB与PostgreSQL存在差异

### 实现 Upsert(update + insert) 功能语法差异

* PosgreSQL写法

```
INSERT INTO distributors (did, dname)
VALUES (5, 'Gizmo Transglobal'), 
(6, 'Associated Computing, Inc')
ON CONFLICT (did) DO UPDATE SET
dname = EXCLUDED.dname
WHERE zipcode <> '21201';
```

* GaussDB写法

```
INSERT INTO distributors (did, dname)
VALUES (5, 'Gizmo Transglobal'), 
(6, 'Associated Computing, Inc')
ON DUPLICATE KEY UPDATE 
dname = VALUES(dname)
WHERE zipcode <> '21201';
```

* 补充说明

参考链接：
    * https://www.postgresql.org/docs/current/sql-insert.html
    * https://support.huaweicloud.com/centralized-devg-v8-gaussdb/gaussdb-42-0653.html#section4

### 实现数据实时复制功能

* PosgreSQL写法

```
CREATE PUBLICATION alltables FOR ALL TABLES; -- 发布所有表
CREATE PUBLICATION mypublication FOR TABLE table_name; -- 发布单张表
CREATE SUBSCRIPTION mysub CONNECTION 'host=XX.XX.XX.XXX port=8000 user= user_name dbname= dbname password= ***'
  PUBLICATION mypublication; -- 订阅名称为'mypublication'的发布
```

* GaussDB写法

参考 [逻辑复制](https://doc.hcs.huawei.com/db/zh-cn/gaussdbqlh/24.7.30.10/fg-dist/gaussdb-18-0030.html)

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-0211178860880205142-1-1.html

### gaussdbjdbc.jar的executeBatch返回值不符合JDBC规范

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-02104174303512776081-1-1.html

### SELECT pg_type.* FROM pg_catalog.pg_type 缺少oid字段

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-0274178187455582064-1-1.html

### GaussDB不支持JSON和JSONB类型的隐式转换

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-0211179461468355165-1-1.html

### 不支持操作与系统表名或者视图名一致的表
* GaussDB写法
```
insert into PLAN_TABLE (name,id) values (?,?)
```
* PosgreSQL写法
```
insert into PLAN_TABLE (name,id) values (?,?)
```

### array_position(e.theArray, 'xyz') = 0函数表现不一致
* GaussDB写法
```
select
        ewa1_0.id,
        ewa1_0.the_array,
        ewa1_0.the_label,
        ewa1_0.the_labels 
    from
        EntityWithArrays ewa1_0 
    where
        (
            array_positions(ewa1_0.the_array, 'xyz')
        )[1]=0
```
* PosgreSQL写法
```
select
        ewa1_0.id,
        ewa1_0.the_array,
        ewa1_0.the_label,
        ewa1_0.the_labels 
    from
        EntityWithArrays ewa1_0 
    where
        case 
            when ewa1_0.the_array is not null 
                then coalesce(array_position(ewa1_0.the_array, 'xyz'), 0) 
        end=0
```

### array_trim(e.theArray, 1)函数表现不一致
* GaussDB写法
```
select
        array_trim(ewa1_0.the_array, 1) 
    from
        EntityWithArrays ewa1_0 
    where
        ewa1_0.id=1
```
* PosgreSQL写法
```
select
        trim_array(ewa1_0.the_array, 1) 
    from
        EntityWithArrays ewa1_0 
    where
        ewa1_0.id=1
```

### json_query(e.json, '$.theNestedObjects[*].id' with wrapper)函数表现不一致
* GaussDB写法
```
select
        json_build_array(jh1_0.json::json #> '{theNestedObjects,*,id}') 
    from
        JsonHolder jh1_0 
    where
        jh1_0.id=1
```
* PosgreSQL写法
```
select
        json_query(jh1_0.json, '$.theNestedObjects[*].id' with wrapper) 
    from
        JsonHolder jh1_0 
    where
        jh1_0.id=1
```

### json_objectagg(e.theString value e.theUuid)函数表现不一致
* GaussDB写法
```
select
        json_object_agg(CASE 
            WHEN eob1_0.the_string IS NOT NULL   
                THEN eob1_0.the_string 
        END, eob1_0.theuuid) 
    from
        EntityOfBasics eob1_0
```
* PosgreSQL写法
```
select
        json_objectagg(eob1_0.the_string:eob1_0.theuuid absent 
            on null 
    returning jsonb) from
        EntityOfBasics eob1_0
```

### json_objectagg(e.theString value e.theUuid)函数表现不一致
* GaussDB写法
```
select
        json_object_agg(CASE 
            WHEN cast(eob1_0.the_integer as varchar) IS NOT NULL   
                THEN cast(eob1_0.the_integer as varchar) 
        END, eob1_0.the_string) 
    from
        EntityOfBasics eob1_0
```
* PosgreSQL写法
```
select
        json_objectagg(cast(eob1_0.the_integer as varchar):eob1_0.the_string absent 
            on null with unique keys 
    returning jsonb) from
        EntityOfBasics eob1_0
```

### json_objectagg(e.theString value e.theUuid)函数表现不一致
* GaussDB写法
```
select
        json_merge('{"a":456, "b":[1,2], "c":{"a":1}}', '{"a":null, "b":[4,5], "c":{"b":1}}')
```
* PosgreSQL写法
```
select
        (with recursive args(d0, d1) as(select
            cast('{"a":456, "b":[1,2], "c":{"a":1}}' as jsonb), cast('{"a":null, "b":[4,5], "c":{"b":1}}' as jsonb)),
        val0(p, k, v) as (select
            '{}'::text[], s.k, t.d0->s.k 
        from
            args t 
        join
            lateral jsonb_object_keys(t.d0) s(k) 
                on 1=1 
        union
        select
            v.p||v.k, s.k, v.v->s.k 
        from
            val0 v 
        join
            lateral jsonb_object_keys(v.v) s(k) 
                on jsonb_typeof(v.v)='object'), val1(p, k, v) as (select
            '{}'::text[], s.k, t.d1->s.k 
    from
        args t 
    join
        lateral jsonb_object_keys(t.d1) s(k) 
            on 1=1 
    union
    select
        v.p||v.k, s.k, v.v->s.k 
    from
        val1 v 
    join
        lateral jsonb_object_keys(v.v) s(k) 
            on jsonb_typeof(v.v)='object'), res(v, p, l) as(select
        jsonb_object_agg(coalesce(v1.k, v0.k), coalesce(v1.v, v0.v)), coalesce(v1.p, v0.p), cardinality(coalesce(v1.p, v0.p)) 
from
    val0 v0 
full join
    val1 v1 
        on v0.p=v1.p 
        and v0.k=v1.k 
where
    cardinality(coalesce(v1.p, v0.p))=(select
        cardinality(v.p) 
    from
        val0 v 
    union
    select
        cardinality(v.p) 
    from
        val1 v 
    order by
        1 desc 
    limit
        1) 
    and jsonb_typeof(coalesce(v1.v)) is distinct 
from
    'null' 
group by
    coalesce(v1.p, v0.p), cardinality(coalesce(v1.p, v0.p)) 
union
all select
    jsonb_object_agg(coalesce(v1.k, v0.k), coalesce(case 
        when coalesce(v1.k, v0.k)=r.p[cardinality(r.p)] 
            then r.v 
    end, v1.v, v0.v)) filter (
where
    coalesce(case 
        when coalesce(v1.k, v0.k)=r.p[cardinality(r.p)] 
            then r.v 
    end, v1.v, v0.v) is not null), coalesce(v1.p, v0.p), r.l-1 
from
    val0 v0 
full join
    val1 v1 
        on v0.p=v1.p 
        and v0.k=v1.k 
join
    (select
        * 
    from
        res r 
    order by
        r.l 
    fetch
        first 1 rows with ties) r 
        on cardinality(coalesce(v1.p, v0.p))=r.l-1 
        and jsonb_typeof(coalesce(v1.v)) is distinct 
from
    'null' 
    and r.l<>0 
group by
    coalesce(v1.p, v0.p), r.l-1) select
    r.v 
from
    res r 
where
    r.l=0
)
```

### GaussDB NUMERIC 类型精度没PostgreSQL高
* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0211180070242768009-1-1.html


## GaussDB不存在的功能

### 不支持refcursor关键字

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0211178353127283084-1-1.html

### 不支持 CREATE/ALTER/DROP DOMAIN

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0210178360425351069-1-1.html

### 不支持 MultiRange

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0211178940977329145-1-1.html

### 不支持 LargeObject

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0211178941356334146-1-1.html

### 不支持 Serializable

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0213178941810463121-1-1.html
* https://support.huaweicloud.com/intl/zh-cn/centralized-devg-v2-gaussdb/gaussdb_42_0501.html

### 不支持 临时表Serial

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0213178941810463121-1-1.html

### 不支持 LISTEN/NOFITY statement

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0210178943513179110-1-1.html
* https://bbs.huaweicloud.com/forum/thread-0234178943691111112-1-1.html

### 不支持 line 类型

line类型是PostgreSQL新增加的用于描述平面直线的数据类型，GaussDB不存在对应的类型。 

### 不支持ltree的extenstion
会报类似错误: `ERROR:  Failed to open the extension control file: No such file or directory.`

PosgreSQL:
```sql
postgres=# create extension ltree;
CREATE EXTENSION
postgres=#
```
### 不支持SUPERUSER关键字
需要使用`SYSADMIN`代替.

PosgreSQL:
```sql
postgres=# create user pgx_pw with SUPERUSER PASSWORD 'Gaussdb@123!';
CREATE ROLE
postgres=#
```

### 不支持password_encryption关键字
需要使用`password_encryption_type`代替.

PosgreSQL:
```sql
postgres=# set password_encryption = md5;
SET
postgres=#
```

### 不支持unix_socket_directories关键字
需要使用`unix_socket_directory`代替.

PosgreSQL:
```sql
postgres=# show unix_socket_directories;
 unix_socket_directories
-------------------------
 /var/run/postgresql
(1 row)
```

### 用户名不支持某些特殊字符
会报类似的错误: `ERROR:  Invalid name:  tricky, ' } " \\ test user .`

PosgreSQL:
```sql
postgres=# create user " tricky, ' } "" \\ test user " SUPERUSER password 'secret';
CREATE ROLE
postgres=#
```

### unnest语法不支持
* GaussDB写法
```
select
        b1_0.id,
        p1_0.name,
        l1_0.name,
        l1_0.val 
    from
        Book b1_0 
    join
        unnest(b1_0.publishers) p1_0 
            on true 
    join
        unnest(b1_0.labels) l1_0 
            on true 
    order by
        b1_0.id,
        p1_0.name asc nulls first,
        l1_0.name asc nulls first,
        l1_0.val asc nulls first
```
* PosgreSQL写法
```
select
        b1_0.id,
        p1_0.name,
        l1_0.name,
        l1_0.val 
    from
        Book b1_0 
    join
        lateral unnest(b1_0.publishers) p1_0 
            on true 
    join
        lateral unnest(b1_0.labels) l1_0 
            on true 
    order by
        b1_0.id,
        p1_0.name asc nulls first,
        l1_0.name asc nulls first,
        l1_0.val asc nulls first
```

### json_table不支持
* GaussDB写法
```
select
        t1_0.theInt,
        t1_0.theFloat,
        t1_0.theString,
        t1_0.theBoolean,
        t1_0.theNull,
        t1_0.theObject,
        t1_0.theNestedInt,
        t1_0.theNestedFloat,
        t1_0.theNestedString,
        t1_0.arrayIndex,
        t1_0.arrayValue,
        t1_0.nonExisting 
    from
        EntityWithJson ewj1_0 
    join
        json_table(ewj1_0.json, '$' columns(theInt integer path '$.theInt', theFloat float path '$.theFloat', theString text path '$.theString', theBoolean boolean path '$.theBoolean', theNull text path '$.theNull', theObject jsonb path '$.theObject', theNestedInt integer path '$.theObject.theInt', theNestedFloat float path '$.theObject.theFloat', theNestedString text path '$.theObject.theString', nested '$.theArray[*]' columns(arrayIndex for ordinality, arrayValue text path '$'), nonExisting boolean exists path '$.nonExisting')) t1_0 
            on true 
    order by
        ewj1_0.id,
        t1_0.arrayIndex
```
* PosgreSQL写法
```
select
        t1_0.theInt,
        t1_0.theFloat,
        t1_0.theString,
        t1_0.theBoolean,
        t1_0.theNull,
        t1_0.theObject,
        t1_0.theNestedInt,
        t1_0.theNestedFloat,
        t1_0.theNestedString,
        t1_0.arrayIndex,
        t1_0.arrayValue,
        t1_0.nonExisting 
    from
        EntityWithJson ewj1_0 
    join
        lateral json_table(ewj1_0.json, '$' columns(theInt integer path '$.theInt', theFloat float path '$.theFloat', theString text path '$.theString', theBoolean boolean path '$.theBoolean', theNull text path '$.theNull', theObject jsonb path '$.theObject', theNestedInt integer path '$.theObject.theInt', theNestedFloat float path '$.theObject.theFloat', theNestedString text path '$.theObject.theString', nested '$.theArray[*]' columns(arrayIndex for ordinality, arrayValue text path '$'), nonExisting boolean exists path '$.nonExisting')) t1_0 
            on true 
    order by
        ewj1_0.id,
        t1_0.arrayIndex
```

### index无法内聚为一个函数
* hibernate
```
select index(e), e from generate_series(2, 3, 1) e order by index(e)
* 
```
* PosgreSQL写法
```
select
        e1_0.ordinality,
        e1_0.e1_0 
    from
        generate_series(2, 3, 1) with ordinality e1_0 
    order by
        e1_0.ordinality
```

### 日期操作内存溢出
* GaussDB写法 
```
select (current_date-cast(? as date))*86400*1e9
```
* PosgreSQL写法
```
select (current_date-cast(? as date))*86400*1e9
```
* 补充说明 
```
产品特性，不容兼容模式有不同的行为。默认的兼容模式是内存溢出
 ```

### 对结构性字段如json的某字段的更新和查询不支持
* GaussDB写法 
```
update JsonHolder jh1_0 set aggregate=coalesce(jh1_0.aggregate,'{}')||jsonb_build_object('theString',to_jsonb(cast(null as varchar)))
```
* PosgreSQL写法
```
update
        JsonHolder jh1_0 
    set
        aggregate=coalesce(jh1_0.aggregate, '{}')||jsonb_build_object('theString',
        to_jsonb(cast(null as varchar)))
```

### 对sinh，log，log10等函数不支持
* GaussDB写法 
```
select log10(eob1_0.the_int),log(?,eob1_0.the_int) from EntityOfBasics eob1_0 where eob1_0.id=?, Error Msg = Unsupported function.
```
* PosgreSQL写法
```
select log10(eob1_0.the_int),log(?,eob1_0.the_int) from EntityOfBasics eob1_0 where eob1_0.id=?, Error Msg = Unsupported function.
```

### 不支持insert on conflinct语法
* GaussDB写法 
```
insert into BasicEntity as be1_0(id,data) values (1,'John') on conflict do nothing
```
* PosgreSQL写法
```
insert into BasicEntity as be1_0(id,data) values (1,'John') on conflict do nothing
```

### 不支持 macaddr8类型

* 补充说明

参考链接：
* https://bbs.huaweicloud.com/forum/thread-0211180066801704005-1-1.html



## GaussDB已知缺陷

### SET LOCK_TIMEOUT提示错误

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-02127178278029106066-1-1.html

### 启动statement cache并且表结构发生变化时，数据库返回错误码不正确

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-02127179062938863125-1-1.html

应用程序在这个场景会捕获到数据库异常，最佳的状态是能够自动恢复。客户端驱动如果要做到自动恢复，需要数据库针对这个异常返回不一样的错误码。

### 除法会返回小数
执行`select 4/3;`会返回: `1.33333333333333326`

PosgreSQL:
```sql
postgres=# select 4/3;
 ?column?
----------
        1
(1 row)
```

## JDBC(gaussjdbc.jar)已知缺陷

### FETCH FIRST n ROWS提示语法错误

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-02104174364146264084-1-1.html

### 创建Date类型字段的表，实际类型为timestamp(0)

* 补充说明

参考链接：
    * https://bbs.huaweicloud.com/forum/thread-0234179025026914116-1-1.html

### 获取byte二位数组失败
```
final byte[][] byteArray = session.find( EntityWithDoubleByteArray.class, id ).getByteArray();

java.sql.SQLFeatureNotSupportedException: Method com.huawei.gaussdb.jdbc.jdbc.PgArray.getArrayImpl(long,int,Map) is not yet implemented.
```
