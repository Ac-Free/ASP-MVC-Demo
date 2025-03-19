/*
 Navicat Premium Dump SQL

 Source Server         : sqlserver
 Source Server Type    : SQL Server
 Source Server Version : 16001000 (16.00.1000)
 Source Host           : localhost:1433
 Source Catalog        : Book
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16001000 (16.00.1000)
 File Encoding         : 65001

 Date: 19/03/2025 14:10:05
*/


-- ----------------------------
-- Table structure for Book
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type IN ('U'))
	DROP TABLE [dbo].[Book]
GO

CREATE TABLE [dbo].[Book] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [BookName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserId] int  NULL,
  [Status] int  NULL
)
GO

ALTER TABLE [dbo].[Book] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'书名',
'SCHEMA', N'dbo',
'TABLE', N'Book',
'COLUMN', N'BookName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'被借阅用户id',
'SCHEMA', N'dbo',
'TABLE', N'Book',
'COLUMN', N'UserId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'0代表未被借阅，1代表已经借阅',
'SCHEMA', N'dbo',
'TABLE', N'Book',
'COLUMN', N'Status'
GO


-- ----------------------------
-- Records of Book
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Book] ON
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1', N'第一本书', N'1', N'1')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'2', N'第二本书', N'3', N'1')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'3', N'第三本书', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1002', N'第四本书', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1004', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1005', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1006', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1007', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1008', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1009', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1010', N'test', NULL, N'0')
GO

INSERT INTO [dbo].[Book] ([Id], [BookName], [UserId], [Status]) VALUES (N'1011', N'test', NULL, N'0')
GO

SET IDENTITY_INSERT [dbo].[Book] OFF
GO


-- ----------------------------
-- Table structure for User
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type IN ('U'))
	DROP TABLE [dbo].[User]
GO

CREATE TABLE [dbo].[User] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UserName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pwd] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[User] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'User',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'dbo',
'TABLE', N'User',
'COLUMN', N'Pwd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'User',
'COLUMN', N'CreateTime'
GO


-- ----------------------------
-- Records of User
-- ----------------------------
SET IDENTITY_INSERT [dbo].[User] ON
GO

INSERT INTO [dbo].[User] ([Id], [UserName], [Pwd], [CreateTime]) VALUES (N'1', N'zyc', N'123456', N'2025-03-18 16:27:46.703')
GO

INSERT INTO [dbo].[User] ([Id], [UserName], [Pwd], [CreateTime]) VALUES (N'2', N'aer', N'123456', N'2025-03-18 16:28:14.017')
GO

INSERT INTO [dbo].[User] ([Id], [UserName], [Pwd], [CreateTime]) VALUES (N'3', N'ccc', N'123456', N'2025-03-18 16:34:03.803')
GO

INSERT INTO [dbo].[User] ([Id], [UserName], [Pwd], [CreateTime]) VALUES (N'1002', N'admin', N'123456', N'2025-03-19 08:49:16.000')
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO


-- ----------------------------
-- Auto increment value for Book
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Book]', RESEED, 1011)
GO


-- ----------------------------
-- Primary Key structure for table Book
-- ----------------------------
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [PK__Book__3214EC075986DF8C] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for User
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[User]', RESEED, 1002)
GO


-- ----------------------------
-- Primary Key structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK__User__3214EC0716C657FE] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

