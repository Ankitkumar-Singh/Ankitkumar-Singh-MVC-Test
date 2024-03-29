USE [master]
GO
/****** Object:  Database [PatientManagement]    Script Date: 8/29/2019 6:14:26 PM ******/
CREATE DATABASE [PatientManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PatientManagement', FILENAME = N'C:\Users\user\PatientManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PatientManagement_log', FILENAME = N'C:\Users\user\PatientManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PatientManagement] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PatientManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PatientManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PatientManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PatientManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PatientManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PatientManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [PatientManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PatientManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PatientManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PatientManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PatientManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PatientManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PatientManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PatientManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PatientManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PatientManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PatientManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PatientManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PatientManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PatientManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PatientManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PatientManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PatientManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PatientManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PatientManagement] SET  MULTI_USER 
GO
ALTER DATABASE [PatientManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PatientManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PatientManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PatientManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PatientManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PatientManagement] SET QUERY_STORE = OFF
GO
USE [PatientManagement]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [PatientManagement]
GO
/****** Object:  Table [dbo].[AdviceDetails]    Script Date: 8/29/2019 6:14:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdviceDetails](
	[PatientId] [int] NOT NULL,
	[PatientMessage] [nvarchar](500) NULL,
	[DoctorMessage] [nvarchar](500) NULL,
	[AdviceId] [int] IDENTITY(1,1) NOT NULL,
	[AdviceTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentDetails]    Script Date: 8/29/2019 6:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentDetails](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[DiseaseInfo] [varchar](500) NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentTime] [varchar](50) NOT NULL,
	[SheduleUpdated] [bit] NOT NULL,
	[SheduleDate] [date] NOT NULL,
 CONSTRAINT [PK_AppoinmentDetails] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientDetails]    Script Date: 8/29/2019 6:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDetails](
	[Patient_Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [nchar](10) NOT NULL,
	[PhoneNumber] [nvarchar](11) NOT NULL,
	[Email] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[UserType] [bit] NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[Address] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_PatientDetails] PRIMARY KEY CLUSTERED 
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TreatmentDetails]    Script Date: 8/29/2019 6:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentDetails](
	[PatientId] [int] NOT NULL,
	[TreatmentDate] [date] NOT NULL,
	[Treatment] [nvarchar](500) NOT NULL,
	[Bill] [int] NOT NULL,
	[TreatmentId] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TreatmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdviceDetails]  WITH CHECK ADD  CONSTRAINT [FK_AdviceDetails_PatientDetails] FOREIGN KEY([PatientId])
REFERENCES [dbo].[PatientDetails] ([Patient_Id])
GO
ALTER TABLE [dbo].[AdviceDetails] CHECK CONSTRAINT [FK_AdviceDetails_PatientDetails]
GO
ALTER TABLE [dbo].[AppointmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_AppoinmentDetails_PatientDetails] FOREIGN KEY([PatientId])
REFERENCES [dbo].[PatientDetails] ([Patient_Id])
GO
ALTER TABLE [dbo].[AppointmentDetails] CHECK CONSTRAINT [FK_AppoinmentDetails_PatientDetails]
GO
ALTER TABLE [dbo].[TreatmentDetails]  WITH CHECK ADD FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[AppointmentDetails] ([AppointmentId])
GO
ALTER TABLE [dbo].[TreatmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentDetails_PatientDetails] FOREIGN KEY([PatientId])
REFERENCES [dbo].[PatientDetails] ([Patient_Id])
GO
ALTER TABLE [dbo].[TreatmentDetails] CHECK CONSTRAINT [FK_TreatmentDetails_PatientDetails]
GO
USE [master]
GO
ALTER DATABASE [PatientManagement] SET  READ_WRITE 
GO
