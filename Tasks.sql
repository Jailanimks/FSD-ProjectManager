USE [ProjectManagerDB]
GO
SET IDENTITY_INSERT [dbo].[Task] ON 
GO

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(1,'Requirement Gathering',1,1,'2018-01-10','2018-02-25',10,0,6)

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(2,'Clarification',1,1,'2018-02-25','2018-02-28',5,0,6)

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(3,'Analysis',2,1,'2018-03-01','2018-03-30',5,0,7)

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(4,'Analysis Document',2,1,'2018-04-01','2018-04-10',10,0,7)

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(5,'Design Document',3,1,'2018-04-10','2018-04-30',10,0,6)

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(6,'Technical Document',3,1,'2018-05-01','2018-05-30',10,0,7)

INSERT INTO [dbo].[Task](TaskId,Task,ParentId,ProjectID,StartDate,EndDate,Priority,Status,UserID)
VALUES(7,'Project Management',NULL,1,'2018-01-10','2018-05-30',10,0,1)

GO
SET IDENTITY_INSERT [dbo].[Task] OFF