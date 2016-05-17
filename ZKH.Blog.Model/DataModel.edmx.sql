
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/01/2016 11:45:51
-- Generated from EDMX file: D:\ZKH\ZKH\C Sharp\Visual Studio\TFS\ZKH.Blog\ZKH.Blog\ZKH.Blog.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ZKHBLOGDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ArticleTypeArticleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ArticleInfo] DROP CONSTRAINT [FK_ArticleTypeArticleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentInfoArticleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentInfo] DROP CONSTRAINT [FK_CommentInfoArticleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageInfoReplyInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReplyInfo] DROP CONSTRAINT [FK_MessageInfoReplyInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PhotoInfoPhotoType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhotoInfo] DROP CONSTRAINT [FK_PhotoInfoPhotoType];
GO
IF OBJECT_ID(N'[dbo].[FK_ReplyInfoCommentInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReplyInfo] DROP CONSTRAINT [FK_ReplyInfoCommentInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AdminInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdminInfo];
GO
IF OBJECT_ID(N'[dbo].[AdminInfoExt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdminInfoExt];
GO
IF OBJECT_ID(N'[dbo].[AdvertisingInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdvertisingInfo];
GO
IF OBJECT_ID(N'[dbo].[ArticleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleInfo];
GO
IF OBJECT_ID(N'[dbo].[ArticleType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ArticleType];
GO
IF OBJECT_ID(N'[dbo].[CommentInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentInfo];
GO
IF OBJECT_ID(N'[dbo].[MessageInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageInfo];
GO
IF OBJECT_ID(N'[dbo].[PhotoInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotoInfo];
GO
IF OBJECT_ID(N'[dbo].[PhotoType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhotoType];
GO
IF OBJECT_ID(N'[dbo].[ReplyInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReplyInfo];
GO
IF OBJECT_ID(N'[dbo].[WebInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WebInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AdminInfo'
CREATE TABLE [dbo].[AdminInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(max)  NOT NULL,
    [Pwd] nvarchar(max)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifiedOn] datetime  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [IsSuperUser] bit  NOT NULL
);
GO

-- Creating table 'AdminInfoExt'
CREATE TABLE [dbo].[AdminInfoExt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Age] nvarchar(max)  NOT NULL,
    [Folk] nvarchar(max)  NOT NULL,
    [Kultur] nvarchar(max)  NOT NULL,
    [Business] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [QQ] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [School] nvarchar(max)  NOT NULL,
    [Speciality] nvarchar(max)  NOT NULL,
    [BeAware] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [Country] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifiedOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [AdminInfoId] int  NOT NULL
);
GO

-- Creating table 'ArticleInfo'
CREATE TABLE [dbo].[ArticleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifiedOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ArticleTypeId] int  NOT NULL,
    [Summary] nvarchar(max)  NULL,
    [Author] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ArticleType'
CREATE TABLE [dbo].[ArticleType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(max)  NULL
);
GO

-- Creating table 'CommentInfo'
CREATE TABLE [dbo].[CommentInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ArticleInfoId] int  NOT NULL
);
GO

-- Creating table 'PhotoInfo'
CREATE TABLE [dbo].[PhotoInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [PhotoTypeId] int  NOT NULL
);
GO

-- Creating table 'PhotoType'
CREATE TABLE [dbo].[PhotoType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [SubTime] datetime  NOT NULL,
    [ModifiedOn] datetime  NOT NULL
);
GO

-- Creating table 'MessageInfo'
CREATE TABLE [dbo].[MessageInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Sex] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'ReplyInfo'
CREATE TABLE [dbo].[ReplyInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [MessageInfoId] int  NOT NULL
);
GO

-- Creating table 'WebInfo'
CREATE TABLE [dbo].[WebInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Creator] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
    [Photo] nvarchar(max)  NOT NULL,
    [Vedio] nvarchar(max)  NOT NULL,
    [Logo] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AdvertisingInfo'
CREATE TABLE [dbo].[AdvertisingInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Link] nvarchar(max)  NOT NULL,
    [Signer] nvarchar(max)  NOT NULL,
    [Company] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifiedOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [DueDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AdminInfo'
ALTER TABLE [dbo].[AdminInfo]
ADD CONSTRAINT [PK_AdminInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdminInfoExt'
ALTER TABLE [dbo].[AdminInfoExt]
ADD CONSTRAINT [PK_AdminInfoExt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArticleInfo'
ALTER TABLE [dbo].[ArticleInfo]
ADD CONSTRAINT [PK_ArticleInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ArticleType'
ALTER TABLE [dbo].[ArticleType]
ADD CONSTRAINT [PK_ArticleType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentInfo'
ALTER TABLE [dbo].[CommentInfo]
ADD CONSTRAINT [PK_CommentInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhotoInfo'
ALTER TABLE [dbo].[PhotoInfo]
ADD CONSTRAINT [PK_PhotoInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhotoType'
ALTER TABLE [dbo].[PhotoType]
ADD CONSTRAINT [PK_PhotoType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageInfo'
ALTER TABLE [dbo].[MessageInfo]
ADD CONSTRAINT [PK_MessageInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReplyInfo'
ALTER TABLE [dbo].[ReplyInfo]
ADD CONSTRAINT [PK_ReplyInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WebInfo'
ALTER TABLE [dbo].[WebInfo]
ADD CONSTRAINT [PK_WebInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdvertisingInfo'
ALTER TABLE [dbo].[AdvertisingInfo]
ADD CONSTRAINT [PK_AdvertisingInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ArticleTypeId] in table 'ArticleInfo'
ALTER TABLE [dbo].[ArticleInfo]
ADD CONSTRAINT [FK_ArticleTypeArticleInfo]
    FOREIGN KEY ([ArticleTypeId])
    REFERENCES [dbo].[ArticleType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleTypeArticleInfo'
CREATE INDEX [IX_FK_ArticleTypeArticleInfo]
ON [dbo].[ArticleInfo]
    ([ArticleTypeId]);
GO

-- Creating foreign key on [PhotoTypeId] in table 'PhotoInfo'
ALTER TABLE [dbo].[PhotoInfo]
ADD CONSTRAINT [FK_PhotoInfoPhotoType]
    FOREIGN KEY ([PhotoTypeId])
    REFERENCES [dbo].[PhotoType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PhotoInfoPhotoType'
CREATE INDEX [IX_FK_PhotoInfoPhotoType]
ON [dbo].[PhotoInfo]
    ([PhotoTypeId]);
GO

-- Creating foreign key on [ArticleInfoId] in table 'CommentInfo'
ALTER TABLE [dbo].[CommentInfo]
ADD CONSTRAINT [FK_CommentInfoArticleInfo]
    FOREIGN KEY ([ArticleInfoId])
    REFERENCES [dbo].[ArticleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentInfoArticleInfo'
CREATE INDEX [IX_FK_CommentInfoArticleInfo]
ON [dbo].[CommentInfo]
    ([ArticleInfoId]);
GO

-- Creating foreign key on [MessageInfoId] in table 'ReplyInfo'
ALTER TABLE [dbo].[ReplyInfo]
ADD CONSTRAINT [FK_MessageInfoReplyInfo]
    FOREIGN KEY ([MessageInfoId])
    REFERENCES [dbo].[MessageInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageInfoReplyInfo'
CREATE INDEX [IX_FK_MessageInfoReplyInfo]
ON [dbo].[ReplyInfo]
    ([MessageInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------