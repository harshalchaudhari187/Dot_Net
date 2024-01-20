CREATE PROCEDURE UpdateEmployee
	@Id int,
	@Name varchar(50),
	@City varchar(50),
	@Address varchar(50)
AS
	UPDATE Employees set Name =@Name, City =@City, Address = @Address where Id = @Id
RETURN 0
