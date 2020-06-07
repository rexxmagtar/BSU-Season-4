BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Report";
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
DROP TABLE IF EXISTS "proffesor";
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
DROP TABLE IF EXISTS "Department";
CREATE TABLE IF NOT EXISTS "Department" (
	"Name"	TEXT,
	"Id"	INTEGER,
	PRIMARY KEY("Id")
);
DROP TABLE IF EXISTS "admin";
CREATE TABLE IF NOT EXISTS "admin" (
	"id"	text NOT NULL,
	"login"	text NOT NULL,
	"password"	text NOT NULL,
	CONSTRAINT "student_pk" PRIMARY KEY("id")
);
COMMIT;
