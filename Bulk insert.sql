BULK INSERT Dystrybutorzy
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_dystrybutorzy.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Cz³onkostwo
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_czlonkostwo.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Cz³onkowieObsady
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_czlonkowieobsady.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Filmy
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_filmy.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT FunkcjeObsady
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_funkcjeobsady.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Gatunki
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_gatunki.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Sale
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_sale.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Seanse
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_seanse.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO

BULK INSERT Spolszczenia
	FROM 'C:\Users\Mat\Desktop\Data-Warehouses-master\DataGenerator\bin\Debug\db_spolszczenia.txt'
	WITH
	(
		FIELDTERMINATOR = '~',
		ROWTERMINATOR = '0x0a',
		CODEPAGE = '1250',
		KEEPIDENTITY
	)

GO