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
							[CONTROL] nvarchar(20) check ([CONTROL] in ('�������','�����')),
							COUNT_STUDENT int null)
go

Create TABLE [TEACHER]
(TEACHER_ID int not null constraint TEACHER_PK primary key,
					   TEACHER_NAME nvarchar(100) null,
					   --PHOTO image null,
					   GENDER  nvarchar(1) check (GENDER in('�','�')),
					   PULPIT nvarchar(20) not null foreign key references PULPIT(PULPIT),
					   [SUBJECT] nvarchar(20) not null foreign key references [SUBJECT]([SUBJECT]))
go

Insert into [SUBJECT]
values ('��', '���� ������', '�������', '300'),
	   ('���', '�������� �������������� ����������������', '�������', '300'),
	   ('����', '������������ ��������� � �������', '�������', '300'),
	   ('��', '������ �����', '�����', '60')

Insert into [PULPIT]
  values  ('����', '�������������� ������ � ���������� ','��'),
          ('���', '����������� � ���-������� ','��'),
		  ('�', '������','��'),
		  ('��', '���������� ���������','��'),
		  ('��', '������ ����������','��')



Insert into [TEACHER]
Values (111, '������ �.�.', '�', '����', '��' ),
       (112, '����� �.�.', '�', '����', '���' ),
	   (113, '���������� �.�.', '�', '��', '��' )


Select * from PULPIT