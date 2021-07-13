CREATE PROCEDURE [dbo].[spWorkSchedule_Add]
	@GroomerId nvarchar(128),
	@Workday nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	
	INSERT INTO dbo.WorkSchedule(GroomerId, WorkdayId)
	VALUES(@GroomerId, (SELECT Id
						FROM dbo.Workdays
						WHERE Workday = @Workday))
END
