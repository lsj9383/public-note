drop table if exists t_user;

create table t_user(
	id int(10) primary key auto_increment,
	usercode varchar(32) not null,
	username varchar(32) not null
);