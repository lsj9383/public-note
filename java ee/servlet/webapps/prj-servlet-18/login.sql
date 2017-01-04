drop table if exists t_user;

create table t_user(
	id int(10),
	username varchar(32),
	password varchar(32)
);

insert into t_user(id, username, password) values (1, "admin", "123");
insert into t_user(id, username, password) values (2, "zhangsan", "123");


commit;

select * from t_user;