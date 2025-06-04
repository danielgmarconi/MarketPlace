USE [MarketPlace]
GO

/****** Object:  StoredProcedure [dbo].[spUsersCreate]    Script Date: 04/06/2025 15:00:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spUsersCreate]
(
	  @FullName	varchar(150)
	, @Email	varchar(150)
	, @Password	varchar(500)
)
as
begin
	insert into Users with(rowlock)
	(
		  FullName
		, Email
		, Password
		, Status
		, IsBlocked
		, CreationDate
	)
	values
	(
		  @FullName
		, @Email
		, @Password
		, 'A'
		, 1
		, GETDATE()
	)
	select * from Users where Id = @@IDENTITY
end
GO


