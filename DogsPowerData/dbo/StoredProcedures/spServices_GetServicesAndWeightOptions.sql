CREATE PROCEDURE [dbo].[spServices_GetServicesAndWeightOptions]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [s].[Id], [s].[ServiceName], [s].[Duration], [s].[Price], [w].[WeightName]
	FROM dbo.[Services] s
	LEFT JOIN dbo.Weights w ON s.WeightId = w.Id
END
