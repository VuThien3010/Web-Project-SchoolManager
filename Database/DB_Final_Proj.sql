USE [master]
GO
/****** Object:  Database [SchoolManagement]    Script Date: 12/20/2019 6:44:30 PM ******/
CREATE DATABASE [SchoolManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SchoolManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SchoolManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SchoolManagement] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SchoolManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [SchoolManagement] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolManagement] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SchoolManagement', N'ON'
GO
ALTER DATABASE [SchoolManagement] SET QUERY_STORE = OFF
GO
USE [SchoolManagement]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SchoolManagement]
GO
/****** Object:  User [IIS APPPOOL\SchoolManagement]    Script Date: 12/20/2019 6:44:31 PM ******/
CREATE USER [IIS APPPOOL\SchoolManagement] FOR LOGIN [IIS APPPOOL\SchoolManagement] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\SchoolManagement]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[adminId] [varchar](40) NOT NULL,
	[adminName] [varchar](30) NULL,
	[DateofBirth] [datetime] NULL,
	[adminPhone] [varchar](10) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[adminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Course]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[courseId] [int] IDENTITY(1,1) NOT NULL,
	[subjectId] [int] NOT NULL,
	[teacherId] [varchar](40) NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[teacherId] ASC,
	[subjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[docId] [int] IDENTITY(1,1) NOT NULL,
	[courseId] [int] NULL,
	[teacherId] [varchar](40) NULL,
	[docUrl] [varchar](255) NULL,
	[description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[docId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Enrollment]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[courseId] [int] NOT NULL,
	[studentId] [varchar](40) NOT NULL,
	[Grade] [int] NULL,
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[courseId] ASC,
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[studentId] [varchar](40) NOT NULL,
	[studentName] [varchar](40) NULL,
	[DateofBirth] [datetime] NULL,
	[studentPhone] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subject]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[subjectId] [int] IDENTITY(1,1) NOT NULL,
	[subjectName] [nvarchar](40) NULL,
	[subjectDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[subjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[teacherId] [varchar](40) NOT NULL,
	[teacherName] [varchar](40) NULL,
	[teacherPhone] [varchar](10) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[teacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/20/2019 6:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userName] [varchar](40) NOT NULL,
	[userPassword] [varchar](40) NULL,
	[userType] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Admin] ([adminId], [adminName], [DateofBirth], [adminPhone]) VALUES (N'admin1username', N'Anderson Silva', CAST(N'1985-05-17T00:00:00.000' AS DateTime), N'0903222769')
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([courseId], [subjectId], [teacherId]) VALUES (1, 3, N'teacher1username')
INSERT [dbo].[Course] ([courseId], [subjectId], [teacherId]) VALUES (2, 4, N'teacher1username')
INSERT [dbo].[Course] ([courseId], [subjectId], [teacherId]) VALUES (3, 5, N'teacher2username')
INSERT [dbo].[Course] ([courseId], [subjectId], [teacherId]) VALUES (4, 6, N'teacher2username')
INSERT [dbo].[Course] ([courseId], [subjectId], [teacherId]) VALUES (5, 7, N'teacher2username')
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[Document] ON 

INSERT [dbo].[Document] ([docId], [courseId], [teacherId], [docUrl], [description]) VALUES (3, 4, N'teacher2username', N'/UploadedDocuments/637116037017408966_donDeNghi.docx', N'image modified 2')
INSERT [dbo].[Document] ([docId], [courseId], [teacherId], [docUrl], [description]) VALUES (4, 5, N'teacher2username', N'/UploadedDocuments/637124426427831461_ch2_Intro_RM.ppt', N'Chapter 2 introduction')
INSERT [dbo].[Document] ([docId], [courseId], [teacherId], [docUrl], [description]) VALUES (6, 4, N'teacher2username', N'/UploadedDocuments/637124521179068942_Chapter00.pdf', N'asm Chapter 00')
INSERT [dbo].[Document] ([docId], [courseId], [teacherId], [docUrl], [description]) VALUES (7, 5, N'teacher2username', N'/UploadedDocuments/637124526438006662_ch5_DatabaseDesign_ER Approach.ppt', N'Chapter 5 _ 2')
INSERT [dbo].[Document] ([docId], [courseId], [teacherId], [docUrl], [description]) VALUES (9, 4, N'teacher2username', N'/UploadedDocuments/637124586120725200_Chapter02.pdf', N'Chapter 02.01')
SET IDENTITY_INSERT [dbo].[Document] OFF
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (1, N'stu1username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (1, N'stu2username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (1, N'stu5username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (2, N'stu1username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (2, N'stu2username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (2, N'stu5username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (2, N'stu6username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (3, N'stu1username', 8)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (3, N'stu2username', 8)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (3, N'stu3username', 7)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (3, N'stu5username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (4, N'stu1username', 10)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (4, N'stu2username', 7)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (4, N'stu4username', 8)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (5, N'stu1username', NULL)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (5, N'stu2username', 4)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (5, N'stu5username', 8)
INSERT [dbo].[Enrollment] ([courseId], [studentId], [Grade]) VALUES (5, N'stu6username', NULL)
INSERT [dbo].[Student] ([studentId], [studentName], [DateofBirth], [studentPhone]) VALUES (N'stu1username', N'Jimmy', CAST(N'1994-04-10T00:00:00.000' AS DateTime), 914322351)
INSERT [dbo].[Student] ([studentId], [studentName], [DateofBirth], [studentPhone]) VALUES (N'stu2username', N'Ben', CAST(N'1998-07-08T00:00:00.000' AS DateTime), 945821139)
INSERT [dbo].[Student] ([studentId], [studentName], [DateofBirth], [studentPhone]) VALUES (N'stu3username', N'Tyron', CAST(N'1992-09-12T00:00:00.000' AS DateTime), 923168208)
INSERT [dbo].[Student] ([studentId], [studentName], [DateofBirth], [studentPhone]) VALUES (N'stu4username', N'Jack', CAST(N'1992-08-11T00:00:00.000' AS DateTime), 953502457)
INSERT [dbo].[Student] ([studentId], [studentName], [DateofBirth], [studentPhone]) VALUES (N'stu5username', N'Billy', CAST(N'1998-10-05T00:00:00.000' AS DateTime), 958540347)
INSERT [dbo].[Student] ([studentId], [studentName], [DateofBirth], [studentPhone]) VALUES (N'stu6username', N'Mike', CAST(N'1997-06-10T00:00:00.000' AS DateTime), 980346130)
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([subjectId], [subjectName], [subjectDescription]) VALUES (3, N'Introduction to Programming', N'This course provides basic programming concepts using C/C++ programming language, knowledge of data presentation in computing, numeric systems, and methods to solve a programming problem. Moreover, this course also presents computational thinking, programming styles, approaches to problem-solving and instructions to create console applications using the standard I/O routines in C/C++ with MS Visual Studio.')
INSERT [dbo].[Subject] ([subjectId], [subjectName], [subjectDescription]) VALUES (4, N'Programming Techniques', N'This is an intermediate course with an 
emphasis on specialized knowledge in the design and analysis of efficient algorithms. Students are exposed to various algorithm design paradigms. The module serves two purposes: to improve students’ ability to design algorithms in different areas and to prepare students for the study of more advanced algorithms. The module covers lower and upper bounds, recurrences, basic algorithm paradigms such as prune-and-search, dynamic programming, recursion, big-numbers, divide and conquer, greedy algorithms and some selected advanced topics.')
INSERT [dbo].[Subject] ([subjectId], [subjectName], [subjectDescription]) VALUES (5, N'Data Structures and Algorithms', N'This course provides students with 
specialized knowledge in data structures and algorithms used for developing 
computer programs. Students are able to analyze and describe algorithms using pseudocodes as well as develop the algorithms on a computer using C/C++ programming language. Furthermore, this course also provides students with the ability to apply data structures and algorithms to solve real-world problems. Besides, students can work in groups and develop their presentation skills through seminars.')
INSERT [dbo].[Subject] ([subjectId], [subjectName], [subjectDescription]) VALUES (6, N'Object-Oriented Programming', N'Object-oriented programming (OOP) 
is a programming paradigm based on the concept of "objects", 
which may contain data, in the form of fields, often known as attributes;
 and code, in the form of procedures, often known as methods. A feature of objects is that an object''s procedures can access and often modify the data fields of the object with which they are associated (objects have a notion of "this" or "self"). In OOP, computer programs are designed by making them out of objects that interact with one another.
This course provides students with specialized knowledge in OOP used for developing application programs. Students are able to write and run programs using C++/C#/Java/Python programming language or JavaScript. Furthermore, this course also provides students with the ability to apply OOP to solve real-world problems. Besides, students can develop their teamwork and presentation skills through seminars.
')
INSERT [dbo].[Subject] ([subjectId], [subjectName], [subjectDescription]) VALUES (7, N'Database Systems', N'This course covers the fundamentals of database architectures and database systems, focusing on basics such as the data model, relational algebra, SQL and query optimization. The course also features database design and relational design principles based on dependencies and normal forms. It is designed for undergraduate students; no prior database experience is assumed.')
SET IDENTITY_INSERT [dbo].[Subject] OFF
INSERT [dbo].[Teacher] ([teacherId], [teacherName], [teacherPhone]) VALUES (N'teacher1username', N'Jame Gosling', N'0957126750')
INSERT [dbo].[Teacher] ([teacherId], [teacherName], [teacherPhone]) VALUES (N'teacher2username', N'Bjarne Stroustrup', N'0917659721')
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'admin1username', N'admin1password', 0)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'stu1username', N'stu1password', 2)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'stu2username', N'stu2password', 2)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'stu3username', N'stu3password', 2)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'stu4username', N'stu4password', 2)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'stu5username', N'123456', 2)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'stu6username', N'stu6password', 2)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher1username', N'teacher1password', 1)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher2username', N'teacher2password', 1)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher3username', N'123456', 1)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher4username', N'teacher4password', 1)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher5username', N'teacher5password', 1)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher6username', N'teacher6password', 1)
INSERT [dbo].[Users] ([userName], [userPassword], [userType]) VALUES (N'teacher7username', N'teacher7password', 1)
/****** Object:  Index [unique_courseId]    Script Date: 12/20/2019 6:44:31 PM ******/
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [unique_courseId] UNIQUE NONCLUSTERED 
(
	[courseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Users] FOREIGN KEY([adminId])
REFERENCES [dbo].[Users] ([userName])
GO
ALTER TABLE [dbo].[Admin] CHECK CONSTRAINT [FK_Admin_Users]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Subject] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subject] ([subjectId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Subject]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Teacher] FOREIGN KEY([teacherId])
REFERENCES [dbo].[Teacher] ([teacherId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Teacher]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Course] FOREIGN KEY([courseId])
REFERENCES [dbo].[Course] ([courseId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Course]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Teacher] FOREIGN KEY([teacherId])
REFERENCES [dbo].[Teacher] ([teacherId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Teacher]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course] FOREIGN KEY([courseId])
REFERENCES [dbo].[Course] ([courseId])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Student] FOREIGN KEY([studentId])
REFERENCES [dbo].[Student] ([studentId])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Users] FOREIGN KEY([studentId])
REFERENCES [dbo].[Users] ([userName])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Users]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_Users] FOREIGN KEY([teacherId])
REFERENCES [dbo].[Users] ([userName])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK_Teacher_Users]
GO
USE [master]
GO
ALTER DATABASE [SchoolManagement] SET  READ_WRITE 
GO
