CREATE TABLE [dbo].[InitialProgression] (
    [InitialProgressionId]         INT              IDENTITY (1, 1) NOT NULL,
    [InitialProgressionReps]       INT              NULL,
    [InitialProgressionPerWeek]    INT              NULL,
    [InitialProgressionTimePeriod] INT              NULL,
    [ExcerciseId]                  INT              NULL,
    [IsActive]                     BIT              NULL,
    [CreatedById]                  UNIQUEIDENTIFIER NULL,
    [CreatedOn]                    DATETIME         NULL,
    [ModifiedById]                 UNIQUEIDENTIFIER NULL,
    [ModifiedOn]                   DATETIME         NULL,
    CONSTRAINT [PK_Progression] PRIMARY KEY CLUSTERED ([InitialProgressionId] ASC),
    CONSTRAINT [FK_InitialProgression_Excercise] FOREIGN KEY ([ExcerciseId]) REFERENCES [dbo].[Excercise] ([ExcerciseId])
);

