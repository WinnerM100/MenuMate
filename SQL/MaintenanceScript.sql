--drop table Client;
--drop table Comanda;
--drop table Meniu;
--drop table Ospatar;
--drop table Rezervare;
--drop table dbo.Role;
--drop table dbo.RoleUser;
--drop table dbo.User;

insert into Role(Id, Name, Value) VALUES
(NewId(),'CLIENT', 'client'),
(NewId(),'USER', 'User'),
(NewId(),'ADMIN', 'Admin')