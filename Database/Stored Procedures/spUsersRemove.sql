USE [MarketPlace]
GO

/****** Object:  StoredProcedure [dbo].[spUsersRemove]    Script Date: 04/06/2025 15:02:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[spUsersRemove]
(
	  @Id			int
)
as
begin
	update Users with(rowlock) set
		  Status = 'D'
		, ModificationDate = GETDATE()
	where
		Id = @Id
end
GO


