USE [master]
GO
/****** Object:  Database [pinterestdb]    Script Date: 18/03/2024 9:14:32 PM ******/
CREATE DATABASE [pinterestdb]
GO
ALTER DATABASE [pinterestdb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pinterestdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pinterestdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pinterestdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pinterestdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pinterestdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pinterestdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [pinterestdb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [pinterestdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pinterestdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pinterestdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pinterestdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pinterestdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pinterestdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pinterestdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pinterestdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pinterestdb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [pinterestdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pinterestdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pinterestdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pinterestdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pinterestdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pinterestdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pinterestdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pinterestdb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [pinterestdb] SET  MULTI_USER 
GO
ALTER DATABASE [pinterestdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pinterestdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pinterestdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pinterestdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pinterestdb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [pinterestdb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [pinterestdb] SET QUERY_STORE = ON
GO
ALTER DATABASE [pinterestdb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [pinterestdb]
GO
/****** Object:  Table [dbo].[account]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[token] [nvarchar](255) NOT NULL,
	[token_created] [datetime] NULL,
	[token_updated] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[collection]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[collection](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[background] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[comment_reply_to_id] [nvarchar](255) NOT NULL,
	[user_id] [int] NOT NULL,
	[post_id] [bigint] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[created_at] [datetime] NULL,
	[like_amount] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[conversation]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[conversation](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_by_user_id] [int] NOT NULL,
	[created_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[favourite]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[favourite](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[image] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[follower]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[follower](
	[following_user_id] [int] NOT NULL,
	[follower_user_id] [int] NOT NULL,
	[status] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[message]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[message](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[conversation_id] [bigint] NOT NULL,
	[created_by_user_id] [int] NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[created_at] [datetime] NOT NULL,
	[status] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[migrations]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[migrations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[migration] [nvarchar](255) NOT NULL,
	[batch] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personal_access_tokens]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personal_access_tokens](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[tokenable_type] [nvarchar](255) NOT NULL,
	[tokenable_id] [bigint] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[token] [nvarchar](64) NOT NULL,
	[abilities] [nvarchar](max) NULL,
	[last_used_at] [datetime] NULL,
	[expires_at] [datetime] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[collection_id] [bigint] NOT NULL,
	[reference] [nvarchar](255) NOT NULL,
	[caption] [nvarchar](255) NULL,
	[detail] [nvarchar](max) NOT NULL,
	[theme] [nvarchar](255) NULL,
	[media] [nvarchar](255) NULL,
	[like_amount] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 18/03/2024 9:14:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] NOT NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NOT NULL,
	[image_url] [nvarchar](255) NULL,
	[introduction] [nvarchar](max) NULL,
	[email] [nvarchar](255) NOT NULL,
	[birthday] [date] NOT NULL,
	[gender] [bit] NOT NULL,
	[country] [nvarchar](255) NOT NULL,
	[language] [nvarchar](255) NOT NULL,
 CONSTRAINT [user_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[account] ON 

INSERT [dbo].[account] ([user_id], [username], [password], [token], [token_created], [token_updated]) VALUES (1, N'trungquanbg3', N'$2y$12$0CdYM8ghTc8Xs8coLe9ixu84MAQPdbbyke.tnI7QJfYP6l.R8IWUO', N'Ne2v884eM3q6mr7pUt3QuAfQ8xXu6HwQ', CAST(N'2024-03-17T17:07:29.723' AS DateTime), CAST(N'2024-03-17T17:07:29.723' AS DateTime))
INSERT [dbo].[account] ([user_id], [username], [password], [token], [token_created], [token_updated]) VALUES (2, N'trungquanbg4', N'$2y$12$E425vubSkbYPocFVcFMdJeViq1oO.65uR7ENOEwsAT7oAARaVPGUm', N'igc6rB9t9sGfHwpB3KgRO9ZIa1AbXga7', CAST(N'2024-03-17T17:09:28.113' AS DateTime), CAST(N'2024-03-17T17:09:28.113' AS DateTime))
INSERT [dbo].[account] ([user_id], [username], [password], [token], [token_created], [token_updated]) VALUES (3, N'testuser01', N'$2y$12$iWRSMSQeVI9B2QDZv.MCaOirb.vu9dg4TBni7wLfkghXP.CvM7eme', N'LPTX8Jr1gKLf3AMXviZZRJdXBeLDweZz', CAST(N'2024-03-17T17:10:10.733' AS DateTime), CAST(N'2024-03-17T17:10:10.733' AS DateTime))
SET IDENTITY_INSERT [dbo].[account] OFF
GO
SET IDENTITY_INSERT [dbo].[migrations] ON 

INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (1, N'2019_12_14_000001_create_personal_access_tokens_table', 1)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (2, N'2024_03_17_132321_create_account_table', 2)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (3, N'2014_10_12_000000_create_user_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (4, N'2024_03_17_133408_create_follower_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (5, N'2024_03_17_134143_create_favourite_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (6, N'2024_03_17_134402_create_collection_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (7, N'2024_03_17_134656_create_post_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (8, N'2024_03_17_134951_create_comment_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (9, N'2024_03_17_135419_create_conversation_table', 3)
INSERT [dbo].[migrations] ([id], [migration], [batch]) VALUES (10, N'2024_03_17_135914_create_message_table', 3)
SET IDENTITY_INSERT [dbo].[migrations] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [account_token_unique]    Script Date: 18/03/2024 9:14:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [account_token_unique] ON [dbo].[account]
(
	[token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [account_username_unique]    Script Date: 18/03/2024 9:14:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [account_username_unique] ON [dbo].[account]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [personal_access_tokens_token_unique]    Script Date: 18/03/2024 9:14:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [personal_access_tokens_token_unique] ON [dbo].[personal_access_tokens]
(
	[token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [personal_access_tokens_tokenable_type_tokenable_id_index]    Script Date: 18/03/2024 9:14:33 PM ******/
CREATE NONCLUSTERED INDEX [personal_access_tokens_tokenable_type_tokenable_id_index] ON [dbo].[personal_access_tokens]
(
	[tokenable_type] ASC,
	[tokenable_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [user_email_unique]    Script Date: 18/03/2024 9:14:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [user_email_unique] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[comment] ADD  DEFAULT ('0') FOR [like_amount]
GO
ALTER TABLE [dbo].[message] ADD  DEFAULT ('2024-03-17 17:04:31') FOR [created_at]
GO
ALTER TABLE [dbo].[post] ADD  DEFAULT ('0') FOR [like_amount]
GO
ALTER TABLE [dbo].[collection]  WITH CHECK ADD  CONSTRAINT [collection_user_id_foreign] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[collection] CHECK CONSTRAINT [collection_user_id_foreign]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [comment_post_id_foreign] FOREIGN KEY([post_id])
REFERENCES [dbo].[post] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [comment_post_id_foreign]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [comment_user_id_foreign] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [comment_user_id_foreign]
GO
ALTER TABLE [dbo].[conversation]  WITH CHECK ADD  CONSTRAINT [conversation_created_by_user_id_foreign] FOREIGN KEY([created_by_user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[conversation] CHECK CONSTRAINT [conversation_created_by_user_id_foreign]
GO
ALTER TABLE [dbo].[favourite]  WITH CHECK ADD  CONSTRAINT [favourite_user_id_foreign] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[favourite] CHECK CONSTRAINT [favourite_user_id_foreign]
GO
ALTER TABLE [dbo].[follower]  WITH CHECK ADD  CONSTRAINT [follower_follower_user_id_foreign] FOREIGN KEY([follower_user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[follower] CHECK CONSTRAINT [follower_follower_user_id_foreign]
GO
ALTER TABLE [dbo].[follower]  WITH CHECK ADD  CONSTRAINT [follower_following_user_id_foreign] FOREIGN KEY([following_user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[follower] CHECK CONSTRAINT [follower_following_user_id_foreign]
GO
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [message_conversation_id_foreign] FOREIGN KEY([conversation_id])
REFERENCES [dbo].[conversation] ([id])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [message_conversation_id_foreign]
GO
ALTER TABLE [dbo].[message]  WITH CHECK ADD  CONSTRAINT [message_created_by_user_id_foreign] FOREIGN KEY([created_by_user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[message] CHECK CONSTRAINT [message_created_by_user_id_foreign]
GO
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [post_collection_id_foreign] FOREIGN KEY([collection_id])
REFERENCES [dbo].[collection] ([id])
GO
ALTER TABLE [dbo].[post] CHECK CONSTRAINT [post_collection_id_foreign]
GO
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [post_user_id_foreign] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[post] CHECK CONSTRAINT [post_user_id_foreign]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [user_id_foreign] FOREIGN KEY([id])
REFERENCES [dbo].[account] ([user_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [user_id_foreign]
GO
USE [master]
GO
ALTER DATABASE [pinterestdb] SET  READ_WRITE 
GO
