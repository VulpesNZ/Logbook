CREATE TABLE [dbo].[IndustryActivity]
(
	[IndustryId] UNIQUEIDENTIFIER NOT NULL , 
    [ActivityId] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([ActivityId], [IndustryId]),	
    CONSTRAINT [FK_IndustryActivity_IndustryId] FOREIGN KEY ([IndustryId]) REFERENCES [Industry]([IndustryId]),  
    CONSTRAINT [FK_IndustryActivity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([ActivityId])
)
