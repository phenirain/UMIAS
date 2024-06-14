CREATE DATABASE UMIAS;
GO

USE UMIAS;
GO

CREATE TABLE Admins (
    IdAdmin int IDENTITY,
    Surname nvarchar(50) NOT NULL,
    Admin_Name nvarchar(50) NOT NULL,
    Patronymic nvarchar(50) NULL,
    EnterPassword nvarchar(50) NOT NULL,
    PRIMARY KEY (IdAdmin)
);
GO

CREATE TABLE Specialities (
    IdSpeciality int IDENTITY,
    Speciality_Name varchar(50) NOT NULL,
    PRIMARY KEY (IdSpeciality)
);
GO

CREATE TABLE Patient (
    OMS bigint NOT NULL,
    Surname nvarchar(50) NOT NULL,
    Patient_Name nvarchar(50) NOT NULL,
    Patronymic nvarchar(50) NULL,
    BirthDate date NOT NULL,
    Patient_Address nvarchar(255) NOT NULL,
    LivingAddress nvarchar(255) NULL,
    Phone nvarchar(18) NULL,
    Email nvarchar(50) NULL,
    Nickname nvarchar(50) NULL,
    PRIMARY KEY (OMS)
);
GO

CREATE TABLE Statuses (
    IdStatus int IDENTITY,
    Status_Name nvarchar(50) NOT NULL,
    PRIMARY KEY (IdStatus)
);
GO

CREATE TABLE Directions (
    IdDirection char(18) NOT NULL,
    IdSpeciality int NOT NULL,
    OMS bigint NOT NULL,
    PRIMARY KEY (IdDirection),
    FOREIGN KEY (IdSpeciality) REFERENCES Specialities(IdSpeciality),
    FOREIGN KEY (OMS) REFERENCES Patient(OMS)
);
GO

CREATE TABLE Doctor (
    IdDoctor int IDENTITY,
    Surname nvarchar(50) NOT NULL,
    Doctor_Name nvarchar(50) NOT NULL,
    Patronymic nvarchar(50) NULL,
    IdSpeciality int NOT NULL,
    EnterPassword nvarchar(50) NOT NULL,
    WorkAddress nvarchar(50) NOT NULL,
    PRIMARY KEY (IdDoctor),
    FOREIGN KEY (IdSpeciality) REFERENCES Specialities(IdSpeciality)
);
GO

CREATE TABLE Appointments (
    IdAppointment int IDENTITY,
    OMS bigint NULL,
    IdDoctor int NOT NULL,
    AppointmentDate date NOT NULL,
    AppointmentTime time NOT NULL,
    IdStatus int NULL,
    PRIMARY KEY (Idappointment),
    FOREIGN KEY (OMS) REFERENCES Patient(OMS),
    FOREIGN KEY (IdDoctor) REFERENCES Doctor(IdDoctor),
    FOREIGN KEY (IdStatus) REFERENCES Statuses(IdStatus)
);
GO

CREATE TABLE AppointmentDocument (
    IdAppointment int NOT NULL,
    Rtf nvarchar(max) NOT NULL,
    PRIMARY KEY (IdAppointment),
    FOREIGN KEY (IdAppointment) REFERENCES Appointments(Idappointment)
);
GO

CREATE TABLE AnalyseDocument (
    IdAppointment int NOT NULL,
    Rtf nvarchar(max) NOT NULL,
    PRIMARY KEY (IdAppointment),
    FOREIGN KEY (IdAppointment) REFERENCES Appointments(Idappointment)
);
GO

CREATE TABLE ResearchDocument (
    IdAppointment int NOT NULL,
    Rtf nvarchar(max) NOT NULL,
    PRIMARY KEY (IdAppointment),
    FOREIGN KEY (IdAppointment) REFERENCES Appointments(Idappointment)
);
GO