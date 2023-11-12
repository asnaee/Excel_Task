create database Exal
go

use Exal
go
create table Department
(
    DepartmentId int primary key identity,
    [Name] varchar(50) not null
)
go

create table Doctor
(
    DoctorId int primary key identity,
    [Name] varchar(50) not null,
    contactNumber varchar(15) not null,
    DepartmentId int foreign key references Department(DepartmentId)
)
go

create table ServicePoint
(
    ServicePointId int primary key identity,
    [Name] varchar(50) not null
)
go

create table DoctorServicePoint
(
    Id int primary key identity,
    ServicePointId int foreign key references ServicePoint(ServicePointId),
    DoctorId int foreign key references Doctor(DoctorId)
)
go
-- Insert data into Department table
INSERT INTO Department ([Name]) VALUES
('Gynecology'),
('Pediatrics'),
('Radiology and Imaging');
go

-- Insert data into ServicePoint table
INSERT INTO ServicePoint ([name]) VALUES
('Antenatal Care'),
('Family Planning'),
('Postnatal Care');
go

-- Insert data into Doctor table
INSERT INTO Doctor ([Name], contactNumber, DepartmentId) VALUES
('Dr. Lissa Mwenda', '+260766219936', 1),
('Dr. Lissa Mwenda', '+260766219936', 1),
('Dr. Lissa Mwenda', '+260766219936', 1),
('Dr. Yvonne Sishuwa', '+260766219937', 2),
('Dr. Yvonne Sishuwa', '+260766219937', 2);
go

-- Insert data into DoctorServicePoint table
INSERT INTO DoctorServicePoint (ServicePointId, DoctorId) VALUES
(1, 1),
(2, 2),
(3, 3),
(2, 4),
(3, 5);
go
