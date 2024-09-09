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
CREATE PROCEDURE CreateEmployee
	@username NVARCHAR(50),
	@password NVARCHAR(60),
	@fullname NVARCHAR(50),
	@email NVARCHAR(50),
	@phone NVARCHAR(50),
	@dob date,
	@loe NVARCHAR(50)
AS
BEGIN
	INSERT INTO UserEmployee (username, [password]) VALUES (@username, @password);
	INSERT INTO Employee (fullname, email, phone, date_of_birth, level_of_education, salary) VALUES (@fullname, @email, @phone, @dob, @loe, 0);
END
GO

EXEC CreateEmployee 'r_ant', '4321', 'Hg of', 'hg@me.com', '76543', '2022-02-02', 'masters'