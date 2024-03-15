Create database pinterestdb

use pinterestdb

Create table [user]
(
	[id] Char(10) NOT NULL,
	[first_name] Nvarchar(20) NULL,
	[last_name] Nvarchar(20) NULL,
	[image] Image NULL,
	[introduction] Ntext NULL,
	[email] Char(20) NULL,
	[birthday] Datetime NULL,
	[gender] Integer NULL,
	[country] Nvarchar(15) NULL,
	[language] Nvarchar(15) NULL,
	Primary key ([id])
)
go

Create table [follower]
(
	[following_user_id] Char(10) NOT NULL,
	[followed_user_id] Char(10) NOT NULL,
	[status] Nvarchar(20) NULL,
	Primary key ([followed_user_id], [following_user_id]),
	Foreign key ([following_user_id]) references [user]([id]),
	Foreign key ([followed_user_id]) references [user]([id])
) 
go

Create table [account]
(
	[user_id] Char(10) NOT NULL,
	[username] Char(30) NULL,
	[password] Char(30) NULL,
	[token] Nvarchar(255) NULL,
	Primary key ([user_id]),
	Foreign key ([user_id]) references [user]([id])
) 
go

Create table [favorite]
(
	[id] Char(10) NOT NULL,
	[name] Nvarchar(40) NULL,
	[image] Image NULL,
	Primary key ([id])
) 
go

Create table [user_of_favorite]
(
	[user_id] Char(10) NOT NULL,
	[favorite_id] Char(10) NOT NULL
	Primary key ([user_id], [favorite_id]),
	Foreign key ([user_id]) references [user]([id]),
	Foreign key ([favorite_id]) references [favorite]([id])
)
go

Create table [collection]
(
	[id] Char(10) NOT NULL,
	[creator_id] Char(10) NOT NULL,
	[name] Nvarchar(40) NULL,
	[background] Image NULL,
	[description] Ntext NULL,
	[status] Nvarchar(20) NULL,
	Primary key ([id]),
	Foreign key ([creator_id]) references [user]([id])
) 
go

Create table [post]
(
	[id] Char(10) NOT NULL,
	[creator_id] Char(10) NOT NULL,
	[collection_id] Char(10) NOT NULL,
	[reference] Char(20) NULL,
	[caption] Nvarchar(40) NULL,
	[detail] Ntext NULL,
	[media] Nvarchar(255) NULL,
	[theme] Nvarchar(40) NULL,
	[like_amount] Integer NULL,
	Primary key([id]),
	Foreign key ([creator_id]) references [user]([id]),
	Foreign key ([collection_id]) references [collection]([id])
) 
go

Create table [comment]
(
	[id] Char(10) NOT NULL,
	[creator_id] Char(10) NOT NULL,
	[post_id] Char(10) NOT NULL,
	[content] Ntext NULL,
	[created_datetime] Datetime NULL,
	[like_amount] Integer NULL,
	[comment_replied_to_id] Char(10) NOT NULL,
	Primary key([id]),
	Foreign key ([creator_id]) references [user]([id]),
	Foreign key ([post_id]) references [post]([id]),
	Foreign key ([comment_replied_to_id]) references [comment]([id])
)
go

Create table [conversation]
(
	[id] Char(10) NOT NULL,
	[creator_id] Char(10) NOT NULL,
	[created_datetime] Datetime NULL,
	Primary key([id]),
	Foreign key ([creator_id]) references [user]([id])
)
go

Create table [participants]
(
	[id] Char(10) NOT NULL,
	[conversation_id] Char(10) NOT NULL,
	[user_id] Char(10) NOT NULL,
	Primary key ([id]),
	Foreign key ([user_id]) references [user]([id]),
	Foreign key ([conversation_id]) references [conversation]([id])
)
go

Create table [message]
(
	[id] Char(10) NOT NULL,
	[conversation_id] Char(10) NOT NULL,
	[creator_id] Char(10) NOT NULL,
	[content] Ntext NULL,
	[created_datetime] Datetime NULL,
	[status] Nvarchar(20) NULL,
	Primary key ([id]),
	Foreign key ([creator_id]) references [user]([id]),
	Foreign key ([conversation_id]) references [conversation]([id])
)