CREATE TABLE [dbo].[Excercise] (
    [ExcerciseId]   INT              IDENTITY (1, 1) NOT NULL,
    [ExcerciseName] NVARCHAR (50)    NULL,
    [ReviewDate]    DATE             NULL,
    [Description]   NVARCHAR (MAX)   NULL,
    [ProgramId]     INT              NULL,
    [IsActive]      BIT              NULL,
    [CreatedById]   UNIQUEIDENTIFIER NULL,
    [CreatedOn]     DATETIME         NULL,
    [ModifiedById]  UNIQUEIDENTIFIER NULL,
    [ModifiedOn]    DATETIME         NULL,
    CONSTRAINT [PK_Excercise] PRIMARY KEY CLUSTERED ([ExcerciseId] ASC)
);



