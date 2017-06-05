drop table if exists t_user;

create table t_user(
	id int(10) primary key auto_increment,
	username varchar(32) not null,
	password varchar(32) not null
);

insert into t_user(username, password) values ('admin', '123');

commit;
select * from t_user;