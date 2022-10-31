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
Select * from Admin

create table Books(
			BookId int Identity(1,1) Primary Key,
			BookName varchar(250) Not Null,
			BookDescription varchar(2000) Not Null,
			BookImg varchar(100) Not Null,
			AuthorName varchar(250) Not Null,
			Rating decimal(2,1) Not Null,
			Rating_Count int Not Null,
			ActualPrice int Not Null,
			DiscountedPrice int Not Null,
			Quantity int
			)
Create procedure AddBook
			@bookName varchar(100)=Null,
			@bookDescription varchar(2000)=Null,
			@bookImg varchar(100)=Null,
			@authorName varchar(250)=Null,
			@rating decimal(2,1)=0.0,
			@rating_Count int=0,
			@actualPrice int=0,
			@discountedPrice int=0,
			@quantity int=0
	As
	Begin	
				Insert into Books([BookName],[BookDescription],[BookImg],[AuthorName],[Rating],[Rating_Count],[ActualPrice],[DiscountedPrice],[Quantity]
				)
				values(@bookName,@bookDescription,@bookImg,@authorName,	@rating,@rating_Count,@actualPrice,	@discountedPrice,@quantity);
	END
drop procedure AddBook

Select * from Books

Create procedure UpdateBook
			@bookId int =0,
			@bookDescription varchar(2000)=Null,
			@rating decimal(2,1)=0.0,
			@rating_Count int=0,
			@actualPrice int=0,
			@discountedPrice int=0,
			@quantity int=0
	As
	Begin	
				Update Books
				SET BookDescription=@bookDescription,Rating=@rating,Rating_Count=@rating_Count,ActualPrice=@actualPrice,DiscountedPrice=@discountedPrice,Quantity=@quantity
				where BookId=@bookId	
				
	 End
drop procedure UpdateBook
--delete book --
Create procedure DeleteBook
			@bookId int =0
	As
	Begin	
				DELETE FROM Books WHERE BookId=@bookId;	
				
	 End
drop procedure DeleteBook

--Get All Books--
Create procedure GetAllBooks
		As
		Begin
				Select * from Books 
		End

Exec GetAllBooks

--Get Book By ID--
Create procedure GetBookById(
					@bookId int=0)
As
Begin
		Select * from Books where
						BookId=@bookId
End

-- Cart Table --
Create table Cart(
			CartId int Identity(1,1) Primary Key Not Null,
			BookId int Foreign Key References Books(BookId) Not Null,
			Id int Foreign Key References Users(Id) Not Null,
			CartQuantity int );
Select * from Cart
drop table cart

Create Procedure AddCart(
				@bookId int=0,
				@id int=0,
				@cartQuantity int=0)
	As
	Begin
	Insert into Cart([BookId],[Id],[CartQuantity])
				values(@bookId,@id,@cartQuantity);
	End

Create Procedure UpdateCart(
				@cartId int=0,
				@bookId int=0,
				@cartQuantity int=0)
	As
	Begin
	Update Cart
			Set CartQuantity=@cartQuantity
			Where CartId = @cartId and BookId=@bookId
	End
drop procedure UpdateCart

Create procedure DeleteCart
			@cartId int =0
	As
	Begin	
				DELETE FROM Cart WHERE CartId=@cartId;	
				
	 End

Create procedure GetCart
		As
		Begin
				Select * from Cart 
		End

--WishList Table--
Create table WishList
			(
			WishListId int Primary key Identity(1,1),		
			Id int Foreign Key References Users(Id) Not Null,
			BookId int Foreign Key References Books(BookId) Not Null,
			)

create procedure AddToWishList
		@id int,
		@bookId int
		as 
		begin 
			insert into WishList(Id,BookId) 
			values(@id,@bookId);
		end

Create procedure DeleteFromWishList
			@wishListId int =0
	As
	Begin	
				DELETE FROM WishList WHERE
					WishListId=@wishListId ;			
	 End

Create procedure GetFromWishList
		As
		Begin
				Select * from WishList
		End





create table AddressInfo(
AddressId int primary key identity,
[Address] varchar(500) not null,
City varchar(110) not null,
[State] varchar(110) not null,
typeId int not null Foreign key References AddressType(typeId),
Id int Foreign Key References Users(Id) Not Null,
)

create table AddressType
(typeId int primary key identity,
AddressType varchar(60) not null )

insert into AddressType(AddressType) values ('Home'),('Work'),('Others');
select * from AddressType

create proc AddAddress
@Address varchar(600),
@City varchar(50),
@State varchar(50),
@typeId int ,
@userId int
as 
begin 
insert into AddressInfo(Address,City,State,typeId,Id) values (@Address,@City,@State,@typeId,@userId)
end

create proc UpdateAddress
@AddressId int,
@Address varchar(max),
@City varchar(110),
@State varchar(110),
@typeId int
as 
begin 
update AddressInfo set Address=@Address,City=@City,State=@State,typeId=@typeId where AddressId = @AddressId
end


select * from AddressInfo
select * from Admin
select * from WishList

create table [Order]
	(OrderId int Primary key Identity,
	OrderDate dateTime2 not null,
	TotalPrice int not null,
	AddressId int not null Foreign key references AddressInfo(AddressId),
	CartId int not null Foreign key references Cart(CartId),
	BookId int not null Foreign key references Books(BookId),
	Id int Foreign Key References Users(Id) Not Null,
)

select * from dbo.[Order]

create procedure TotalPriceCart
@cartId int
as 
begin 
select totalPrice=c.CartQuantity * b.DiscountedPrice from dbo.Cart c inner join dbo.Books b on c.BookId = b.BookId  where c.CartId = @cartId
end

exec TotalPriceCart 1

create proc AddOrder
	@orderDate dateTime2,
	@totalPrice money,
	@AddressId int ,
	@cartId int,
	@bookId int,
	@id int
	as
	begin 
		insert into  [Order](OrderDate,TotalPrice,AddressId,CartId,BookId,Id) values (@orderDate,@totalPrice,@AddressId,@cartId,@bookId,@id)
end

create table feedback(
feedbackId int Primary key identity,
Rating int not null,
comment varchar(2000),
bookId int not null Foreign key references dbo.Book(bookId),
userId int not null Foreign key references dbo.userRegistration(userId)
)

create proc spAddFeedback
@Rating int,
@comment varchar(2000),
@bookId int,
@userId int
as 
begin 
insert into dbo.feedback(Rating,comment,bookId,userId) values(@Rating,@comment,@bookId,@userId)
end

create proc spGetFeedback
@bookId int
as 
begin 
select * from dbo.feedback where bookId = @bookId 
end

create procedure GetOrder
@userId int
as 
Begin
select o.userId, o.orderId,o.orderDate,o.totalPrice,o.bookId,b.bookImg,b.bookName,
b.bookDiscountedPrice,c.cartId,c.Quantity,a.AddressId,a.Address,a.City,a.State,a.typeId from dbo.[Order] o 
inner join dbo.Book b on o.bookId = b.bookId 
inner join dbo.cart c on o.cartId = c.cartId 
inner join dbo.AddressInfo a on o.AddressId = a.AddressId where o.Id = @userId
end
exec spGetOrder 1

drop proc spGetOrder

select * from feedback
