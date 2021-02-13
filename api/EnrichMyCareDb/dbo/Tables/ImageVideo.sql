CREATE TABLE [dbo].[ImageVideo] (
    [ImageVideoId]   INT              IDENTITY (1, 1) NOT NULL,
    [ImageVideoPath] NVARCHAR (500)   NULL,
    [ExcerciseId]    INT              NULL,
    [IsActive]       BIT              NULL,
    [CreatedById]    UNIQUEIDENTIFIER NULL,
    [CreatedOn]      DATETIME         NULL,
    [ModifiedById]   UNIQUEIDENTIFIER NULL,
    [ModifiedOn]     DATETIME         NULL,
    CONSTRAINT [PK_ImageVideo] PRIMARY KEY CLUSTERED ([ImageVideoId] ASC)
);



