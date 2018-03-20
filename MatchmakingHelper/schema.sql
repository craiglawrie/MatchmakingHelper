-- Includes the create database and create table script
CREATE DATABASE MatchmakingHelper;

GO

USE MatchmakingHelper;

GO

DROP TABLE student_company_preferences;
DROP TABLE student;
DROP TABLE company;

-- Create Tables
CREATE TABLE student (
	id varchar(30) NOT NULL,
	name varchar(50) NOT NULL,
	email varchar(50) NOT NULL,

	CONSTRAINT pk_student PRIMARY KEY (id)
);

CREATE TABLE company (
	id int identity(1,1) NOT NULL,
	name varchar(50) NOT NULL,
	numberoftables int NOT NULL DEFAULT 1,

	CONSTRAINT pk_company PRIMARY KEY (id)
);

CREATE TABLE student_company_preferences (
	student_id varchar(30) NOT NULL,
	company_id int NOT NULL,
	preference_rank int NOT NULL,

	CONSTRAINT pk_scp PRIMARY KEY (student_id, company_id, preference_rank),
	CONSTRAINT fk_scp_student FOREIGN KEY (student_id) REFERENCES student(id),
	CONSTRAINT fk_scp_company FOREIGN KEY (company_id) REFERENCES company(id)
);