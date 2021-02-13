CREATE TABLE [dbo].[Patient] (
    [PatientId]     INT              IDENTITY (1, 1) NOT NULL,
    [PatientNumber] NVARCHAR (20)    NULL,
    [PatientName]   NVARCHAR (50)    NULL,
    [IsActive]      BIT              NULL,
    [CreatedById]   UNIQUEIDENTIFIER NULL,
    [CreatedOn]     DATETIME         NULL,
    [ModifiedById]  UNIQUEIDENTIFIER NULL,
    [ModifiedOn]    DATETIME         NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED ([PatientId] ASC)
);

