USE [MarketPlace]
GO

/****** Object:  StoredProcedure [dbo].[spUsersUpdate]    Script Date: 04/06/2025 15:04:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spUsersUpdate]
(
	  @Id			int
	, @FullName		varchar(150)
	, @Email		varchar(150)
	, @Password		varchar(500)
	, @Status		varchar(1)
	, @IsBlocked	bit
)
as
begin
	update Users with(rowlock) set
		  FullName = @FullName
		, Email = @Email
		, Password = @Password
		, Status = @Status
		, IsBlocked = @IsBlocked
		, ModificationDate = GETDATE()
	where
		Id = @Id
end
GO


