Create database BookMyTruckDB

use BookMyTruckDB

select*from truck
select*from users
select*from Request
select*from Service

delete from Users where mobile='9095525796'
delete from Service where customerid='Purushothaman9095525796'

insert into Users values('8124878350','Purushothaman8124878350','Purushothaman','manager',0,'1234',0,'No Issues')
create table Users(
Mobile varchar(10) not null unique ,
UserId varchar(35) not null Primary key,
DisplayName varchar(25) ,
UserRole varchar(10) not null,
UserStatus bit default 0,
Password varchar(25) not null
);

alter table  Request add  AcceptStatus bit not null default 0
alter table  Service drop column ServiceStartDate 


 add  Description varchar(max) not null,
  

create table Truck(
TruckNumber varchar(10) not null primary key,
TruckType varchar(20) not null,
ManagerId varchar(35) not null foreign key references Users(UserId),
DriverName varchar(25) not null,
DriverLicenceNumber varchar(15) not null,
PickCity varchar(300) not null,
DropCity varchar(300) not null,
MaxCapacity int not null,
CostPerKM float not null,
TruckStatus bit not null default 0,
);
insert into truck values ('1234567890','TataAce','Purushothaman8124878350','Pro','123456789012345','Salem, Tamil Nadu, India','Chennai, Tamil Nadu, India','1','10000','false','false')
alter table truck alter column MaxCapacity float not null
alter table  Service alter column  ServiceStatus bit not null

update  truck  set BookedStatus=0 where truckNumber='tn29ay1213'
Create Table Service(
ServiceId int not null identity(1,1) primary Key,
CustomerId varchar(35) not null foreign key references Users(UserId),
ManagerId varchar(35) not null foreign key references Users(UserId),
PickupCity varchar(100) not null,
DropCity varchar(100) not null,
ServiceStartDate date not null,
ServiceEndDate date null,
ServiceStatus varchar not null,
SericeCost float not null,
ratings int null
);

Create Table Request(
RequestId int not null identity(1,1) primary Key,
CustomerId varchar(35) not null foreign key references Users(UserId),
ManagerId varchar(35) not null foreign key references Users(UserId),
PickupCity varchar(100) not null,
DropCity varchar(100) not null,
EstimatedStartDate date not null,
Distance int not null,
EstimatedCost float not null,
RequestStatus bit not null default 0
);