CREATE PROCEDURE [dbo].[spWorkSchedule_Remove]
	@GroomerId nvarchar(128),
	@Workday nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.WorkSchedule
	WHERE GroomerId = @GroomerId AND WorkdayId = (SELECT Id
												  FROM dbo.Workdays
												  WHERE Workday = @Workday)
END
