CREATE TABLE [dbo].[Program] (
    [ProgramId]       INT              IDENTITY (1, 1) NOT NULL,
    [ProgramName]     NVARCHAR (50)    NULL,
    [ProgramDate]     DATE             NULL,
    [GoalsObjectives] NVARCHAR (MAX)   NULL,
    [IsActive]        BIT              NULL,
    [CreatedById]     UNIQUEIDENTIFIER NULL,
    [CreatedOn]       DATETIME         NULL,
    [ModifiedById]    UNIQUEIDENTIFIER NULL,
    [ModifiedOn]      DATETIME         NOT NULL,
    CONSTRAINT [PK_ExcerciseProgram] PRIMARY KEY CLUSTERED ([ProgramId] ASC)
);

