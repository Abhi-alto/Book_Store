Create database Book_Store
use Book_store

Create table Users(
		Id int IDENTITY(1,1) PRIMARY KEY,
		Name varchar(250) Not Null,
		Email varchar(250) Not null Unique,
		Password varchar(50)Not Null,
		Phone_no bigInt Not null Unique
		)
		Select* from Users
drop table Users

Create procedure Register
				@name varchar(250)=null,
				@email varchar(250)=null,
				@password varchar(50)=null,
				@phone bigInt=0
	As
	Begin	
				Insert into Users([Name],[Email],[Password],[Phone_no])
				values(@name,@email,@password,@phone)
	END
drop procedure Register
Exec Register 'Abhishek','abhisheksri9719@gmail.com','Abhishek25',6392174011

	

Create Procedure Login
		@email varchar(250)=null,
		@password varchar(50)=null
	As
	Begin
			Select Email,[Password] from Users
				where Email=@email AND Password=@password
	End

create procedure ForgotPassword
@email varchar(50)
as 
begin 
	select Email from Users
		where Email =@email 
end


create procedure ResetPassword
	@password varchar(50),
	@id int
	as 
	begin 
		update Users set Password = @password
		where Id = @id 
	end

Create table Admin(
		AdminId int IDENTITY(1,1) PRIMARY KEY,
		AdminName varchar(250) Not Null,
		AdminEmail varchar(250) Not null Unique,
		AdminPassword varchar(50)Not Null,
		AdminPhone_no bigInt Not null Unique
		)
drop table Admin
Insert into Admin([AdminName],[AdminEmail],[AdminPassword],[AdminPhone_no])
values('Rahul','rahulgoswami45@gmail.com','RahulGoswami78',8974561225);

Create Procedure AdminLogin(
		@email varchar(250)=null,
		@password varchar(50)=null)
	As
	Begin
			Select AdminEmail,[AdminPassword] from Admin
				where AdminEmail=@email AND AdminPassword=@password
	End
drop procedure AdminLogin
Execute AdminLogin 'rahulgoswami45@gmail.com','RahulGoswami78'






























































































































































































		Begin
		 Insert into Users(
					Name,
					Email,
					Password,
					Phone_no)
		  values(@name,@email,@password,@phone)
			  End
