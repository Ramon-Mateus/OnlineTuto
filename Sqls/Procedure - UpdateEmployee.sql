-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpdateEmployee
	@emp_id int,
	@fullname NVARCHAR(50),
	@email NVARCHAR(50),
	@phone NVARCHAR(50),
	@dob date,
	@loe NVARCHAR(50),
	@salary float
AS
BEGIN
	UPDATE Employee SET fullname=@fullname, email=@email, phone=@phone, date_of_birth=@dob, level_of_education=@loe, salary=@salary WHERE emp_id=@emp_id;
END
GO

EXEC UpdateEmployee 1, 'Hg of z city', 'hg@me.com', '76543', '2003-02-02', 'university', 4000.50