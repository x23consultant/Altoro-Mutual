
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'altoro')
	DROP DATABASE [altoro]
GO

CREATE DATABASE altoro
GO

use [altoro]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[users]
GO

CREATE TABLE [dbo].[users] (
	[userid] [int] NOT NULL ,
	[username] [varchar] (50) NULL ,
	[first_name] [varchar] (50) NULL ,
	[last_name] [varchar] (50) NULL ,
	[password] [varchar] (50) NULL
) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[accounts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[accounts]
GO

CREATE TABLE [dbo].[accounts] (
	[accountid] [int] NOT NULL ,
	[userid] [int] NULL ,
	[acct_type] [varchar] (50) NULL
) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[promo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[promo]
GO

CREATE TABLE [dbo].[promo] (
	[userid] [int] NOT NULL ,
	[approved] [bit] NULL ,
	[card_type] [varchar] (50) NULL ,
	[interest] [decimal](18, 0) NULL ,
	[limit] [decimal](18, 2) NULL
) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[transactions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[transactions]
GO

CREATE TABLE [dbo].[transactions] (
	[transid] [int] IDENTITY (1, 1) NOT NULL ,
	[accountid] [int] NULL ,
	[debit] [bit] NULL ,
	[description] [varchar] (50) NULL ,
	[trans_date] [smalldatetime] NULL ,
	[amount] [decimal](18, 2) NULL
) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[subscribe]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[subscribe]
GO

CREATE TABLE [dbo].[subscribe] (
	[subscribeid] [int] IDENTITY (1, 1) NOT NULL ,
	[email] [varchar] (255) NULL
) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[debit]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[debit]
GO

CREATE VIEW dbo.debit
AS
SELECT     accountid, SUM(amount) AS debits
FROM         dbo.transactions
WHERE     (debit = 1)
GROUP BY accountid
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[credit]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[credit]
GO

CREATE VIEW dbo.credit
AS
SELECT     accountid, SUM(amount) AS credits
FROM         dbo.transactions
WHERE     (debit = 0)
GROUP BY accountid
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[balance]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[balance]
GO

CREATE VIEW dbo.balance
AS
SELECT     dbo.credit.accountid, dbo.credit.credits - dbo.debit.debits AS Balance
FROM         dbo.credit INNER JOIN
                      dbo.debit ON dbo.credit.accountid = dbo.debit.accountid

GO

-- Insert Users
insert into users (userid,username,first_name,last_name,password) values (1,'admin','Admin','User','admin');
insert into users (userid,username,first_name,last_name,password) values (2,'tuser','Test','User','tuser');
insert into users (userid,username,first_name,last_name,password) values (100116013,'sjoe','Smoking','Joe','frazier');
insert into users (userid,username,first_name,last_name,password) values (100116014,'jsmith','John','Smith','Demo1234');
insert into users (userid,username,first_name,last_name,password) values (100116015,'cclay','Cassius','Clay','Ali');
insert into users (userid,username,first_name,last_name,password) values (100116018,'sspeed','Sam','Speed','Demo1234');

-- Create Accounts
insert into accounts (accountid,userid,acct_type) values (20,2,'Checking');
insert into accounts (accountid,userid,acct_type) values (21,2,'Savings');
insert into accounts (accountid,userid,acct_type) values (1001160130,100116013,'Checking');
insert into accounts (accountid,userid,acct_type) values (1001160131,100116013,'Savings');
insert into accounts (accountid,userid,acct_type) values (1001160140,100116014,'Checking');
insert into accounts (accountid,userid,acct_type) values (1001160141,100116014,'Savings');
insert into accounts (accountid,userid,acct_type) values (1001160150,100116015,'Checking');
insert into accounts (accountid,userid,acct_type) values (1001160151,100116015,'Savings');
insert into accounts (accountid,userid,acct_type) values (1001160180,100116018,'Checking');
insert into accounts (accountid,userid,acct_type) values (1001160181,100116018,'Savings');

-- Add Promotion Approval
insert into promo (userid,approved,card_type,interest,limit) values (1,0,'',0,0.00);
insert into promo (userid,approved,card_type,interest,limit) values (2,1,'Platinum',6,20000.00);
insert into promo (userid,approved,card_type,interest,limit) values (100116013,1,'Platinum',5,12000.00);
insert into promo (userid,approved,card_type,interest,limit) values (100116014,1,'Gold',7,10000.00);
insert into promo (userid,approved,card_type,interest,limit) values (100116015,1,'Gold',2,15000.00);
insert into promo (userid,approved,card_type,interest,limit) values (100116018,0,'',0,0.00);

-- Create some transactions
declare @Date datetime
declare @R1 int
declare @R2 int
declare @X int
declare @Day int
declare @Month int
declare @Year int

set @Date = DateAdd(mm,-30,GetDate())
set @Day = Day(@Date)
set @Month = Month(@Date)
set @Year = Year(@Date)

if (@Day <= 15)
	begin
		set @Day = 1
	end
else
	begin
		set @Day = 15
	end

set @Date = cast(cast(@Year as varchar) + '-' + cast(@Month as varchar) + '-' + cast(@Day as varchar) as datetime)

while (@Date < GetDate())
	begin

		-- Get Paid on the 1st and 15th of the month
		insert into transactions (accountid,debit,description,trans_date,amount) values (20,0,'Paycheck',@Date,650.00);
		insert into transactions (accountid,debit,description,trans_date,amount) values (1001160130,0,'Paycheck',@Date,2100.00);
		insert into transactions (accountid,debit,description,trans_date,amount) values (1001160140,0,'Paycheck',@Date,1200.00);
		insert into transactions (accountid,debit,description,trans_date,amount) values (1001160150,0,'Paycheck',@Date,1700.00);
		insert into transactions (accountid,debit,description,trans_date,amount) values (1001160180,0,'Paycheck',@Date,800.00);

		-- Good Savings Plan
		if (@Day = 1)
			begin
				set @X = (select case when Balance > 2400 then Balance - 2400 else 0 end from balance where accountid = 1001160140);
				if (@X > 0)
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160140,1,'Transfer to Savings',@Date,@X);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160141,0,'Transfer from Savings',@Date,@X);
					end
				set @X = (select case when Balance > 1600 then Balance - 1600 else 0 end from balance where accountid = 1001160180);
				if (@X > 0)
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160180,1,'Transfer to Savings',@Date,@X);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160181,0,'Transfer from Savings',@Date,@X);
					end
				set @X = (select case when Balance > 1500 then Balance - 1500 else 0 end from balance where accountid = 20);
				if (@X > 0)
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (20,1,'Transfer to Savings',@Date,@X);
						insert into transactions (accountid,debit,description,trans_date,amount) values (21,0,'Transfer from Savings',@Date,@X);
					end
				set @X = (select case when Balance > 4000 then Balance - 4000 else 0 end from balance where accountid = 1001160130);
				if (@X > 0)
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160130,1,'Transfer to Savings',@Date,@X);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160131,0,'Transfer from Savings',@Date,@X);
					end
				set @X = (select case when Balance > 3500 then Balance - 3500 else 0 end from balance where accountid = 1001160150);
				if (@X > 0)
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160150,1,'Transfer to Savings',@Date,@X);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160151,0,'Transfer from Savings',@Date,@X);
					end
			end

		-- Random debits between payday and bill day
		set @X = 1
		while (@X < 14)
			begin
				set @R1 = Rand() * 12
				set @R2 = Rand() * 5
				if (DateAdd(dd,@X,@Date) < GetDate())
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (case when @R2=0 then 1001160140 when @R2=1 then 1001160180 when @R2=2 then 20 when @R2=3 then 1001160130 else 1001160150 end,1,case when @R1=0 then 'Transportation' when @R1=1 then 'Car Repair' when @R1=2 then 'Cleaners' when @R1=3 then 'Withdrawal' when @R1=4 then 'Dinner' when @R1=5 then 'Withdrawal' when @R1=6 then 'Groceries' when @R1=7 then 'Liquer Lyles' when @R1=8 then 'Clothing' when @R1=9 then 'Quick Mart' when @R1=10 then 'Entertainment' else 'Withdrawal' end,DateAdd(dd,@X,@Date),Rand()*100);
					end
				set @X = @X + 1
			end

		-- Pay bills on the 28th
		if (@Day = 15)
			begin
				set @Date = cast(cast(@Year as varchar) + '-' + cast(@Month as varchar) + '-28' as datetime)
				if (@Date < GetDate())
					begin
						insert into transactions (accountid,debit,description,trans_date,amount) values (20,1,'Rent',@Date,500.00);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160130,1,'Mortgage',@Date,1700.00);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160140,1,'Rent',@Date,800.00);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160150,1,'Mortgage',@Date,1800.00);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160180,1,'Rent',@Date,350.00);

						insert into transactions (accountid,debit,description,trans_date,amount) values (20,1,'Electric Bill',@Date,51.34);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160130,1,'Electric Bill',@Date,113.76);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160140,1,'Electric Bill',@Date,45.25);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160150,1,'Electric Bill',@Date,86.21);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160180,1,'Electric Bill',@Date,42.10);

						insert into transactions (accountid,debit,description,trans_date,amount) values (20,1,'Heating',@Date,46.02);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160130,1,'Heating',@Date,83.71);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160140,1,'Heating',@Date,29.99);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160150,1,'Heating',@Date,46.23);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160180,1,'Heating',@Date,45.00);

						insert into transactions (accountid,debit,description,trans_date,amount) values (20,1,'Sprint Phone',@Date,19.99);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160130,1,'Cingular',@Date,59.99);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160140,1,'Verizon',@Date,32.10);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160150,1,'Rogers',@Date,39.99);
						insert into transactions (accountid,debit,description,trans_date,amount) values (1001160180,1,'Sprint Phone',@Date,24.98);
					end
			end

		-- Set next month
		if (@Day = 15)
			begin
				set @Day = 1
				set @Month = @Month + 1
				if (@Month = 13)
					begin
						set @Month = 1
						set @Year = @Year + 1
					end
			end
		else
			begin
				set @Day = 15
			end

		set @Date = cast(cast(@Year as varchar) + '-' + cast(@Month as varchar) + '-' + cast(@Day as varchar) as datetime)

	end