CREATE PROCEDURE usp_ClearBeaconDataOlderThanYear
AS
BEGIN
DELETE FROM [BeaconData] 
WHERE DATEDIFF(DAY, GETDATE(), [Timestamp]) < -365
END