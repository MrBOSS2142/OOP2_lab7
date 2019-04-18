use master
go

Create database OOP_Lab7
go

Use OOP_Lab7
go

create TABLE [PULPIT]
(PULPIT nvarchar(20) not null primary key,
					  PULPIT_NAME nvarchar(100) null,
					  FACULTY nvarchar(10) not null)
go

Create Table [SUBJECT]
([SUBJECT] nvarchar(20) not null primary key, 
							[SUBJECT_NAME] nvarchar(50) not null,
							[CONTROL] nvarchar(20) check ([CONTROL] in ('Экзамен','Зачет')),
							COUNT_STUDENT int null)
go

Create TABLE [TEACHER]
(TEACHER_ID int not null constraint TEACHER_PK primary key,
					   TEACHER_NAME nvarchar(100) null,
					   --PHOTO image null,
					   GENDER  nvarchar(1) check (GENDER in('м','ж')),
					   PULPIT nvarchar(20) not null foreign key references PULPIT(PULPIT),
					   [SUBJECT] nvarchar(20) not null foreign key references [SUBJECT]([SUBJECT]))
go

Insert into [SUBJECT]
values ('БД', 'Базы данных', 'Экзамен', '300'),
	   ('ООП', 'Обьектно риентированное программирование', 'Экзамен', '300'),
	   ('КГиГ', 'Компьютерная геометрия и графика', 'Экзамен', '300'),
	   ('ТЦ', 'Теория цвета', 'Зачет', '60')

Insert into [PULPIT]
  values  ('ИСиТ', 'Информационных систем и технологий ','ИТ'),
          ('ИИВ', 'Информатики и веб-дизайна ','ИТ'),
		  ('Ф', 'Физики','ИТ'),
		  ('ПИ', 'Програмной инженерии','ИТ'),
		  ('ВМ', 'Высшей математики','ИТ')



Insert into [TEACHER]
Values (111, 'Смелов В.В.', 'м', 'ИСиТ', 'БД' ),
       (112, 'Пацей Н.В.', 'ж', 'ИСиТ', 'ООП' ),
	   (113, 'Сухорукова И.Г.', 'ж', 'ПИ', 'ТЦ' )


Select * from PULPIT