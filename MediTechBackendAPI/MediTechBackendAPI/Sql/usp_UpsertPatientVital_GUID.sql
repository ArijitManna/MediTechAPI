-- Stored procedure: usp_UpsertPatientVital_GUID
-- Upsert a PatientVital row and its related PatientBloodPressure row (if provided).
-- If @Id is NULL a new guid is generated and an INSERT is performed. Otherwise UPDATE is attempted.
-- Returns the Id of the inserted/updated PatientVital in a single-row resultset.

IF OBJECT_ID('dbo.usp_UpsertPatientVital_GUID', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_UpsertPatientVital_GUID;
GO

CREATE PROCEDURE dbo.usp_UpsertPatientVital_GUID
    @Id UNIQUEIDENTIFIER = NULL,
    @PatientId UNIQUEIDENTIFIER,
    @BMI DECIMAL(5,2) = NULL,
    @HeartRate INT = NULL,
    @Weight DECIMAL(6,2) = NULL,
    @FBC NVARCHAR(250) = NULL,
    @Glucose DECIMAL(6,2) = NULL,
    @Temperature DECIMAL(5,2) = NULL,
    @SpO2 INT = NULL,
    @Systolic INT = NULL,
    @Diastolic INT = NULL,
    @RecordedOn DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ResultId UNIQUEIDENTIFIER;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @Id IS NULL
            SET @ResultId = NEWID();
        ELSE
            SET @ResultId = @Id;

        -- Upsert PatientVital
        IF EXISTS(SELECT 1 FROM dbo.PatientVital WHERE Id = @ResultId)
        BEGIN
            UPDATE dbo.PatientVital
            SET
                PatientId = @PatientId,
                BMI = @BMI,
                HeartRate = @HeartRate,
                Weight = @Weight,
                FBC = @FBC,
                Glucose = @Glucose,
                Temperature = @Temperature,
                SpO2 = @SpO2,
                RecordedOn = ISNULL(@RecordedOn, RecordedOn),
                ModifiedOn = SYSUTCDATETIME()
            WHERE Id = @ResultId;
        END
        ELSE
        BEGIN
            INSERT INTO dbo.PatientVital
                (Id, PatientId, BMI, HeartRate, Weight, FBC, Glucose, Temperature, SpO2, RecordedOn, CreatedOn)
            VALUES
                (@ResultId, @PatientId, @BMI, @HeartRate, @Weight, @FBC, @Glucose, @Temperature, @SpO2, ISNULL(@RecordedOn, SYSUTCDATETIME()), SYSUTCDATETIME());
        END

        -- Upsert PatientBloodPressure linked to PatientVitalId
        IF @Systolic IS NOT NULL OR @Diastolic IS NOT NULL
        BEGIN
            IF EXISTS(SELECT 1 FROM dbo.PatientBloodPressure WHERE PatientVitalId = @ResultId)
            BEGIN
                UPDATE dbo.PatientBloodPressure
                SET
                    Systolic = @Systolic,
                    Diastolic = @Diastolic,
                    ModifiedOn = SYSUTCDATETIME()
                WHERE PatientVitalId = @ResultId;
            END
            ELSE
            BEGIN
                INSERT INTO dbo.PatientBloodPressure
                    (Id, PatientVitalId, PatientId, Systolic, Diastolic, RecordedOn, CreatedOn)
                VALUES
                    (NEWID(), @ResultId, @PatientId, @Systolic, @Diastolic, ISNULL(@RecordedOn, SYSUTCDATETIME()), SYSUTCDATETIME());
            END
        END

        COMMIT TRANSACTION;

        -- Return the resulting Id (single-row resultset) so callers can read it.
        SELECT @ResultId AS Id;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrNum INT = ERROR_NUMBER();
        RAISERROR('usp_UpsertPatientVital_GUID failed: %s', 16, 1, @ErrMsg);
    END CATCH
END
GO
