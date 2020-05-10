DECLARE @numberOfTestCases INT = 10000


DELETE
FROM [request-service].[RequestPersonal].[PersonalDetails]
DELETE
FROM [request-service].[Request].[SupportActivities]

DELETE
FROM [request-service].[Request].[Request]


DBCC CHECKIDENT (
		'[request-service].[Request].[Request]',
		RESEED,
		0
		);

DECLARE @numberOfPostcodes INT = (
		SELECT count(1)
		FROM [AddressService].[Address].[Postcode]
		WHERE [IsActive] = 1
		)
DECLARE @everyXthPostcode INT = @numberOfPostcodes / @numberOfTestCases


INSERT INTO [request-service].[Request].[Request] (
	[PostCode],
	[DateRequested],
	[IsFulfillable]
	)
SELECT TOP (@numberOfTestCases) 
	[Postcode],
	GetUtcDate(),
	FLOOR(RAND(CHECKSUM(NEWID()))*(2))
FROM [AddressService].[Address].[Postcode]
WHERE [Id] % @everyXthPostcode = 0
AND [IsActive] = 1

INSERT INTO [request-service].[RequestPersonal].[PersonalDetails] (
	[RequestID],
	[OnBehalfOfAnother],
	[FurtherDetails],
	[RequestorFirstName],
	[RequestorLastName],
	[RequestorEmailAddress],
	[RequestorPhoneNumber]
	)
SELECT r.[ID],
	FLOOR(RAND()*(2)),
	'further details',
	'firstName',
	'lastName',
	'email@email.com',
	'07895 548256'
FROM [request-service].[Request].[Request] r


INSERT INTO [request-service].[Request].[SupportActivities] (
	[RequestID],
	[ActivityID]
	)
SELECT r.[ID],
	floor(RAND(CHECKSUM(NEWID())) * (11) + 1)
FROM [request-service].[Request].[Request] r

