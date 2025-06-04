USE [MarketPlace]
GO

/****** Object:  StoredProcedure [dbo].[spUsersSelect]    Script Date: 04/06/2025 15:03:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[spUsersSelect]
(
	  @Id			int				= null
	, @FullName		varchar(150)	= null
	, @Email		varchar(150)	= null
	, @Password		varchar(500)	= null
	, @Status		varchar(1)		= null
	, @IsBlocked	bit				= null
)
as
begin
	select 
		  Id
		, FullName
		, Email
		, Password
		, Status
		, IsBlocked
		, CreationDate
		, ModificationDate
	from
		Users with(nolock)
	where
		(@Id is null or Id = @Id)
	and
		(@Email is null or Email like @Email + '%')
	and
		(@FullName is null or @FullName like @FullName + '%')
	and
		(@Status is null or Status = @Status) 
	and 
		(@IsBlocked is null or IsBlocked = @IsBlocked)
	and
		 Status <> 'D'
	order by
		Email asc
end
GO


