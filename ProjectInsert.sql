USE [ProjectManagerDB]
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 

GO
INSERT [dbo].[Projects] ([ProjectID], [Project], [StartDate], [EndDate],Priority,ManagerID,Suspended) VALUES (1, N'Tracker', '2018-01-10', '2018-12-30',5,1,0)
INSERT [dbo].[Projects] ([ProjectID], [Project], [StartDate], [EndDate],Priority,ManagerID,Suspended) VALUES (2, N'LMS', NULL, NULL,10,2,0)
INSERT [dbo].[Projects] ([ProjectID], [Project], [StartDate], [EndDate],Priority,ManagerID,Suspended) VALUES (3, N'Pluto', '2018-02-10', '2018-12-15',5,1,0)
INSERT [dbo].[Projects] ([ProjectID], [Project], [StartDate], [EndDate],Priority,ManagerID,Suspended) VALUES (4, N'DMS', '2018-08-10', '2018-12-25',10,3,0)
GO
SET IDENTITY_INSERT [dbo].[Projects] OFF




SELECT * FROM [Projects]
