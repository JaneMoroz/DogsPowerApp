/*
Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся инструкции SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF not exists (SELECT * FROM dbo.Workdays)
BEGIN
    INSERT INTO dbo.Workdays(Workday)
    VALUES ('Monday'),
    ('Tuesday'),
    ('Wednesday'),
    ('Thursday'),
    ('Friday'),
    ('Saturday'),
    ('Sunday')
END

IF not exists (SELECT * FROM dbo.Weights)
BEGIN
    INSERT INTO dbo.Weights(WeightName)
    VALUES ('14.9 lbs. & Under'),
    ('15.0 - 29.9 lbs.'),
    ('30.0 - 44.9 lbs.'),
    ('45.0 - 59.9 lbs.'),
    ('60.0 - 74.9 lbs.'),
    ('75.0 - 89.9 lbs.'),
    ('90.0 - 104.9 lbs.'),
    ('105.0 - 119.9 lbs.'),
    ('120.0 - 134.9 lbs.'),
    ('135.0 - 149.9 lbs.'),
    ('150.0 - 174.9 lbs.'),
    ('175.0 - 189.9 lbs.'),
    ('190.0 - 204.9 lbs.')
END

IF not exists (SELECT * FROM dbo.TimeOptions)
BEGIN
    INSERT INTO dbo.TimeOptions(StartTime)
    VALUES ('9:00:00'),
    ('9:30:00'),
    ('10:00:00'),
    ('10:30:00'),
    ('11:00:00'),
    ('14:00:00'),
    ('14:30:00'),
    ('15:00:00'),
    ('15:30:00'),
    ('16:00:00'),
    ('16:30:00'),
    ('17:00:00'),
    ('17:30:00'),
    ('18:00:00'),
    ('18:30:00'),
    ('19:00:00'),
    ('19:30:00')
END

IF not exists (SELECT * FROM dbo.[Services])
BEGIN

     DECLARE @weightId1 int;
     DECLARE @weightId2 int;
     DECLARE @weightId3 int;
     DECLARE @weightId4 int;
     DECLARE @weightId5 int;
     DECLARE @weightId6 int;
     DECLARE @weightId7 int;
     DECLARE @weightId8 int;
     DECLARE @weightId9 int;
     DECLARE @weightId10 int;
     DECLARE @weightId11 int;
     DECLARE @weightId12 int;
     DECLARE @weightId13 int;
     DECLARE @weightId14 int;

     SELECT @weightId1 = Id FROM dbo.Weights WHERE [WeightName] = '14.9 lbs. & Under';
     SELECT @weightId2 = Id FROM dbo.Weights WHERE [WeightName] = '15.0 - 29.9 lbs.';
     SELECT @weightId3 = Id FROM dbo.Weights WHERE [WeightName] = '30.0 - 44.9 lbs.';
     SELECT @weightId4 = Id FROM dbo.Weights WHERE [WeightName] = '45.0 - 59.9 lbs.';
     SELECT @weightId5 = Id FROM dbo.Weights WHERE [WeightName] = '60.0 - 74.9 lbs.';
     SELECT @weightId6 = Id FROM dbo.Weights WHERE [WeightName] = '75.0 - 89.9 lbs.';
     SELECT @weightId7 = Id FROM dbo.Weights WHERE [WeightName] = '90.0 - 104.9 lbs.';
     SELECT @weightId8 = Id FROM dbo.Weights WHERE [WeightName] = '105.0 - 119.9 lbs.';
     SELECT @weightId9 = Id FROM dbo.Weights WHERE [WeightName] = '120.0 - 134.9 lbs.';
     SELECT @weightId10 = Id FROM dbo.Weights WHERE [WeightName] = '135.0 - 149.9 lbs.';
     SELECT @weightId11 = Id FROM dbo.Weights WHERE [WeightName] = '150.0 - 174.9 lbs.';
     SELECT @weightId12 = Id FROM dbo.Weights WHERE [WeightName] = '175.0 - 189.9 lbs.';
     SELECT @weightId13 = Id FROM dbo.Weights WHERE [WeightName] = '190.0 - 204.9 lbs.';
     SELECT @weightId14 = Id FROM dbo.Weights WHERE [WeightName] = 'All weights';

    INSERT INTO dbo.[Services](ServiceName, WeightId, Duration, Price)
    VALUES ('Basic Full Groom', @weightId1, '1:15:00', 60),
    ('Basic Full Groom', @weightId2, '1:15:00', 65),
    ('Basic Full Groom', @weightId3, '1:30:00', 70),
    ('Basic Full Groom', @weightId4, '1:45:00', 80),
    ('Basic Full Groom', @weightId5, '2:00:00', 90),
    ('Basic Full Groom', @weightId6, '2:30:00', 105),
    ('Basic Full Groom', @weightId7, '2:30:00', 120),
    ('Basic Full Groom', @weightId8, '2:45:00', 135),
    ('Basic Full Groom', @weightId9, '2:45:00', 150),
    ('Basic Full Groom', @weightId10, '3:00:00', 170),
    ('Basic Full Groom', @weightId11, '3:00:00', 190),
    ('Basic Full Groom', @weightId12, '3:00:00', 210),
    ('Basic Full Groom', @weightId13, '3:00:00', 230),

    ('Just A Bath', @weightId1, '1:00:00', 40),
    ('Just A Bath', @weightId2, '1:00:00', 45),
    ('Just A Bath', @weightId3, '1:30:00', 55),
    ('Just A Bath', @weightId4, '1:30:00', 70),
    ('Just A Bath', @weightId5, '2:30:00', 80),
    ('Just A Bath', @weightId6, '2:30:00', 90),
    ('Just A Bath', @weightId7, '2:00:00', 105),
    ('Just A Bath', @weightId8, '2:00:00', 115),
    ('Just A Bath', @weightId9, '2:30:00', 125),
    ('Just A Bath', @weightId10, '2:30:00', 140),
    ('Just A Bath', @weightId11, '2:30:00', 155),
    ('Just A Bath', @weightId12, '3:00:00', 170),
    ('Just A Bath', @weightId13, '3:00:00', 185),

    ('A La Carte Nail Trim/File', @weightId14, '00:20:00', 25)
END
