-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2020-05-19 13:17:09.729

-- tables
-- Table: answer
CREATE TABLE answer (
    id text NOT NULL CONSTRAINT answer_pk PRIMARY KEY,
    ans_value text NOT NULL,
    correct boolean NOT NULL,
    task_id text NOT NULL,
    task_question text NOT NULL,
    CONSTRAINT answer_task FOREIGN KEY (task_id,task_question)
    REFERENCES task (id,question)
);

-- Table: class
CREATE TABLE class (
    id text NOT NULL CONSTRAINT class_pk PRIMARY KEY,
    name text NOT NULL,
    description text NOT NULL
);

-- Table: class_access
CREATE TABLE class_access (
    id text NOT NULL CONSTRAINT class_access_pk PRIMARY KEY,
    user_id text NOT NULL,
    access_type int NOT NULL,
    class_id text NOT NULL,
    student_id text NOT NULL,
    CONSTRAINT class_access_teacher FOREIGN KEY (user_id)
    REFERENCES teacher (id),
    CONSTRAINT class_access_class FOREIGN KEY (class_id)
    REFERENCES class (id),
    CONSTRAINT class_access_student FOREIGN KEY (student_id)
    REFERENCES student (id)
);

-- Table: marks
CREATE TABLE marks (
    id text NOT NULL CONSTRAINT marks_pk PRIMARY KEY,
    value int NOT NULL,
    student_id text NOT NULL,
    test_id text NOT NULL,
    CONSTRAINT marks_student FOREIGN KEY (student_id)
    REFERENCES student (id),
    CONSTRAINT marks_test FOREIGN KEY (test_id)
    REFERENCES test (id)
);

-- Table: student
CREATE TABLE student (
    id text NOT NULL CONSTRAINT student_pk PRIMARY KEY,
    login text NOT NULL,
    password text NOT NULL,
    name text NOT NULL,
    surname text NOT NULL
);

-- Table: task
CREATE TABLE task (
    id text NOT NULL,
    question text NOT NULL,
    test_id text NOT NULL,
    CONSTRAINT task_pk PRIMARY KEY (id,question),
    CONSTRAINT task_test FOREIGN KEY (test_id)
    REFERENCES test (id)
);

-- Table: teacher
CREATE TABLE teacher (
    id text NOT NULL CONSTRAINT teacher_pk PRIMARY KEY,
    login text NOT NULL,
    password text NOT NULL,
    name text NOT NULL,
    surname text NOT NULL
);

-- Table: test
CREATE TABLE test (
    id text NOT NULL CONSTRAINT test_pk PRIMARY KEY,
    name text NOT NULL,
    description text NOT NULL,
    class_id text NOT NULL,
    CONSTRAINT test_class FOREIGN KEY (class_id)
    REFERENCES class (id)
);

-- End of file.

