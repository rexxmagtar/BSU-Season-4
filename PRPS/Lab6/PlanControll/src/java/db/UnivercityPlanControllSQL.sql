BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Report" (
	"ReportID"	INTEGER,
	"StartDate"	TEXT,
	"EndDate"	TEXT,
	"Hours"	INTEGER,
	"Description"	TEXT,
	"ProfID"	INTEGER,
	"Completed"	TEXT DEFAULT 'NO',
	PRIMARY KEY("ReportID")
);
CREATE TABLE IF NOT EXISTS "proffesor" (
	"id"	text NOT NULL,
	"login"	text NOT NULL,
	"password"	text NOT NULL,
	"name"	text NOT NULL,
	"surname"	text NOT NULL,
	"departmentId"	INTEGER,
	"grade"	TEXT,
	"addres"	TEXT,
	"phone"	TEXT,
	CONSTRAINT "teacher_pk" PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "Department" (
	"Name"	TEXT,
	"Id"	INTEGER,
	PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "admin" (
	"id"	text NOT NULL,
	"login"	text NOT NULL,
	"password"	text NOT NULL,
	CONSTRAINT "student_pk" PRIMARY KEY("id")
);
INSERT INTO "Report" VALUES (0,'20.12.2020','12.10.2020',40,'SomeNewAddeReport',0,'YES');
INSERT INTO "Report" VALUES (1,'10.07.2020','10.11.2020',50,'MacOS Cataliiiina',0,'YES');
INSERT INTO "Report" VALUES (2,'03.05.2020','13.08.2020',30,'C# basics',0,'NO');
INSERT INTO "Report" VALUES (3,'04.12.2019','28.01.2020',20,'c++ Inheritance',0,'NO');
INSERT INTO "Report" VALUES (4,'10.10.2020','10.08.2020',40,'SomeDescription',0,'NO');
INSERT INTO "Report" VALUES (5,'10','20',20,'Description2
',0,'NO');
INSERT INTO "Report" VALUES (6,'20.12.2020','12.10.2020',10,'Java Servlets',0,'YES');
INSERT INTO "Report" VALUES (7,'20.12.2020','12.10.2020',40,'SomeNewAddeReport',1,'NO');
INSERT INTO "Report" VALUES (8,'20.12.2020','12.10.2020',40,'SomeNewAddeReport',1,'YES');
INSERT INTO "Report" VALUES (9,'20.12.2020','12.10.2020',100,'SomeNewAddeReport',1,'YES');
INSERT INTO "Report" VALUES (10,'20.12.2020','12.10.2020',150,'SomeNewAddeReport',2,'YES');
INSERT INTO "Report" VALUES (11,'20.12.2020','12.10.2020',30,'SomeNewAddeReport',2,'NO');
INSERT INTO "Report" VALUES (12,'20.12.2020','12.10.2020',10,'SomeNewAddeReport',2,'NO');
INSERT INTO "Report" VALUES (13,'10.10.2020','10.08.2020',40,'SomeDescription',3,'NO');
INSERT INTO "Report" VALUES (14,'10','20',20,'Description2
',3,'YES');
INSERT INTO "Report" VALUES (15,'20.12.2020','12.10.2020',10,'Java Servlets',3,'YES');
INSERT INTO "Report" VALUES (16,'20.12.2020','12.10.2020',150,'SomeNewAddeReport',3,'YES');
INSERT INTO "Report" VALUES (17,'20.12.2020','12.10.2020',40,'SomeNewAddeReport',2,'YES');
INSERT INTO "Report" VALUES (18,'20.12.2020','05.10.2020',3,'Newthon method',2,'NO');
INSERT INTO "Report" VALUES (19,'20.12.2020','05.10.2020',3,'Newthon method',2,'NO');
INSERT INTO "Report" VALUES (20,'20.12.2020','05.10.2020',3,'Newthon method',2,'NO');
INSERT INTO "Report" VALUES (21,'20.12.2020','05.10.2020',3,'Newthon method',2,'NO');
INSERT INTO "Report" VALUES (22,'20.12.2020','12.10.2020',40,'Java Servlets',0,'YES');
INSERT INTO "Report" VALUES (23,'05.05.2020','13.07.2020',20,'SQLLite course',0,'YES');
INSERT INTO "proffesor" VALUES ('0
','kash','123456','Sergey','Kashkevich',1,'Docent','Buckingan palace','+375 (29) 390-23-56');
INSERT INTO "proffesor" VALUES ('1','gutos','654321','Sergey','Gutnikov',1,'Docent','JAVAHOME','+375 (29) 395-12-34');
INSERT INTO "proffesor" VALUES ('2','dodo','LUPQR','Alex','Vorobiov',2,'Graduate Student','Naibohsakya22','+375 (29) 234-31-32');
INSERT INTO "proffesor" VALUES ('3','Matrix','Matrix','Eugenie','Matveev',4,'Docent','Parabola avenue','+375 (29) 231-12-34');
INSERT INTO "Department" VALUES ('ASC',1);
INSERT INTO "Department" VALUES ('CM',2);
INSERT INTO "Department" VALUES ('TP',3);
INSERT INTO "Department" VALUES ('Algebra',4);
INSERT INTO "admin" VALUES ('K0F2K9F2390KFWEKK','kalin','111111');
INSERT INTO "admin" VALUES ('0RI490I3409K9KGRE','rogozenko','222222');
INSERT INTO "admin" VALUES ('030KOPAKDKA0W990','borodako','171717');
COMMIT;
