Create database BookMyTruckDB

use BookMyTruckDB


create table Users(
Mobile varchar(10) not null unique ,
UserId varchar(35) not null Primary key,
DisplayName varchar(25) ,
UserRole varchar(10) not null,
UserStatus bit default 0,
Password varchar(25) not null
);


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