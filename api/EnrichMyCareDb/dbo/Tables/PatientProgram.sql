CREATE TABLE [dbo].[PatientProgram] (
    [PatientProgramId] INT              IDENTITY (1, 1) NOT NULL,
    [PatientId]        INT              NULL,
    [ProgramId]        INT              NULL,
    [IsActive]         BIT              NULL,
    [CreatedById]      UNIQUEIDENTIFIER NULL,
    [CreatedOn]        DATETIME         NULL,
    [ModifiedById]     UNIQUEIDENTIFIER NULL,
    [ModifiedOn]       DATETIME         NULL,
    CONSTRAINT [PK_PatientProgram] PRIMARY KEY CLUSTERED ([PatientProgramId] ASC),
    CONSTRAINT [FK_PatientProgram_Patient] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patient] ([PatientId]),
    CONSTRAINT [FK_PatientProgram_Program] FOREIGN KEY ([ProgramId]) REFERENCES [dbo].[Program] ([ProgramId])
);

