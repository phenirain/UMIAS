CREATE DATABASE Pract10;
GO

USE Pract10;
GO

CREATE TABLE Admin (
    IdAdmin int IDENTITY,
    Surname varchar(50) NOT NULL,
    Name varchar(50) NOT NULL,
    Patronymic nvarchar(50) NULL,
    EnterPassword varchar(50) NOT NULL,
    PRIMARY KEY (IdAdmin)
);
GO

INSERT INTO Admin (Surname, Name, Patronymic, EnterPassword) 
VALUES 
('����������', '������', '����������', 'Ruslan123'),
('��������', '����', '�������', 'Lisa123'),
('�������', '������', '����������', 'Nikita123'),
('������', '����', '���������', 'Gleb123'),
('�����������', '������', '����������', 'Bogdan123');
GO

CREATE TABLE Specialities (
    IdSpeciality int IDENTITY,
    Name varchar(50) NOT NULL,
    PRIMARY KEY (IdSpeciality)
);
GO

INSERT INTO Specialities (Name) 
VALUES 
('��������'), 
('������'), 
('�������');
GO

CREATE TABLE Patient (
    OMS bigint NOT NULL,
    Surname varchar(50) NOT NULL,
    Name varchar(50) NOT NULL,
    Patronymic nvarchar(50) NULL,
    BirthDate date NOT NULL,
    Address varchar(255) NOT NULL,
    LivingAddress varchar(255) NULL,
    Phone nvarchar(18) NULL,
    Email nvarchar(50) NULL,
    Nickname varchar(50) NULL,
    PRIMARY KEY (OMS)
);
GO

INSERT INTO Patient (OMS, Surname, Name, Patronymic, BirthDate, Address, LivingAddress, Phone, Email, Nickname) 
VALUES 
(1234567890417542, '�������', '������', '��������', '1990-01-15', '��. ������, �. 1', '��. ���������, �.20', '+7 (999) 123-45-67', 'example@mail.com', 'ivan89'),
(2345678901334785, '�������', '�����', '��������', '1985-07-20', '��. �������, �. 5, ��. 10', '��. ����������� �.10', '+7 (999) 456-78-90', 'masha85@mail.com', 'Maria52');
GO

CREATE TABLE Status (
    IdStatus int IDENTITY,
    Name varchar(50) NOT NULL,
    PRIMARY KEY (IdStatus)
);
GO

INSERT INTO Status (Name) 
VALUES 
('��������'), 
('�� �������'), 
('�������');
GO

CREATE TABLE Directions (
    Iddirection char(18) NOT NULL,
    IdSpeciality int NOT NULL,
    OMS bigint NOT NULL,
    PRIMARY KEY (Iddirection),
    FOREIGN KEY (IdSpeciality) REFERENCES Specialities(IdSpeciality),
    FOREIGN KEY (OMS) REFERENCES Patient(OMS)
);
GO

INSERT INTO Directions (Iddirection, IdSpeciality, OMS) 
VALUES 
('A1', 1, 1234567890417542),
('B2', 2, 2345678901334785);
GO

CREATE TABLE Doctor (
    IdDoctor int IDENTITY,
    Surname varchar(50) NOT NULL,
    Name varchar(50) NOT NULL,
    Patronymic nvarchar(50) NULL,
    IdSpeciality int NOT NULL,
    EnterPassword varchar(50) NOT NULL,
    WorkAddress varchar(50) NOT NULL,
    PRIMARY KEY (IdDoctor),
    FOREIGN KEY (IdSpeciality) REFERENCES Specialities(IdSpeciality)
);
GO

INSERT INTO Doctor (Surname, Name, Patronymic, IdSpeciality, EnterPassword, WorkAddress) 
VALUES 
('������', '�������', '��������', 1, 'doctor123', '����������� �1'),
('�������', '�����', '����������', 3, 'doc456', '������� �����������');
GO

CREATE TABLE Appointments (
    Idappointment int IDENTITY,
    OMS bigint NULL,
    IdDoctor int NOT NULL,
    AppointmentDate date NOT NULL,
    AppointmentTime time NOT NULL,
    IdStatus int NULL,
    PRIMARY KEY (Idappointment),
    FOREIGN KEY (OMS) REFERENCES Patient(OMS),
    FOREIGN KEY (IdDoctor) REFERENCES Doctor(IdDoctor),
    FOREIGN KEY (IdStatus) REFERENCES Status(IdStatus)
);
GO

INSERT INTO Appointments (OMS, IdDoctor, AppointmentDate, AppointmentTime, IdStatus) 
VALUES 
(1234567890417542, 1, '2022-04-15', '10:00:00', 1),
(2345678901334785, 2, '2022-04-20', '13:30:00', 1);
GO

CREATE TABLE AppointmentDocument (
    Midappointment int NOT NULL,
    Rtf nvarchar(max) NOT NULL,
    PRIMARY KEY (Midappointment),
    FOREIGN KEY (Midappointment) REFERENCES Appointments(Idappointment)
);
GO

INSERT INTO AppointmentDocument (Midappointment, Rtf)
VALUES 
(1, '�����1'),
(2, '�����2');
GO

CREATE TABLE AnalyseDocument (
    Midappointment int NOT NULL,
    Rtf nvarchar(max) NOT NULL,
    PRIMARY KEY (Midappointment),
    FOREIGN KEY (Midappointment) REFERENCES Appointments(Idappointment)
);
GO

INSERT INTO AnalyseDocument (Midappointment, Rtf)
VALUES 
(1, '���������1'),
(2, '���������2');
GO

CREATE TABLE ResearchDocument (
    Midappointment int NOT NULL,
    Rtf nvarchar(max) NOT NULL,
    PRIMARY KEY (Midappointment),
    FOREIGN KEY (Midappointment) REFERENCES Appointments(Idappointment)
);
GO

INSERT INTO ResearchDocument (Midappointment, Rtf)
VALUES 
(1, '��������1'),
(2, '��������2');
GO