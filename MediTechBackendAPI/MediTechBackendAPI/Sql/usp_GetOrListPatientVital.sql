-- Stored procedure: usp_GetOrListPatientVital
-- Returns either a single patient vital (when @Id provided) or list of vitals (when @Id is NULL).
-- Adjust JOIN to PatientBloodPressure depending on your schema (PatientBloodPressure.PatientVitalId or PatientBloodPressure.PatientId + RecordedOn).

IF OBJECT_ID('dbo.usp_GetOrListPatientVital', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetOrListPatientVital;
GO

CREATE PROCEDURE dbo.usp_GetOrListPatientVital
    @Id UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        pv.Id,
        pv.PatientId,
        pv.BMI,
        pv.HeartRate,
        pv.Weight,
        pv.FBC,
        pv.Glucose,
        pv.Temperature,
        pv.SpO2,
        pv.RecordedOn,
        pb.Systolic,
        pb.Diastolic
    FROM dbo.PatientVital pv
    LEFT JOIN dbo.PatientBloodPressure pb
        ON pb.PatientVitalId = pv.Id -- change this join if your BP table links differently
    WHERE (@Id IS NULL) OR (pv.Id = @Id)
    ORDER BY pv.RecordedOn DESC;
END
GO
