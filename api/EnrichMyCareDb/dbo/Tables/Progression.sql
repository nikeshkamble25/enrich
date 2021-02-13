CREATE TABLE [dbo].[Progression] (
    [ProgressionId]         INT              IDENTITY (1, 1) NOT NULL,
    [ProgressionReps]       INT              NULL,
    [ProgressionPerWeek]    INT              NULL,
    [ProgressionTimePeriod] INT              NULL,
    [InitialProgressionId]  INT              NULL,
    [IsActive]              BIT              NULL,
    [CreatedById]           UNIQUEIDENTIFIER NULL,
    [CreatedOn]             DATETIME         NULL,
    [ModifiedById]          UNIQUEIDENTIFIER NULL,
    [ModifiedOn]            DATETIME         NULL,
    CONSTRAINT [PK_Progression_1] PRIMARY KEY CLUSTERED ([ProgressionId] ASC),
    CONSTRAINT [FK_Progression_InitialProgression] FOREIGN KEY ([InitialProgressionId]) REFERENCES [dbo].[InitialProgression] ([InitialProgressionId])
);

