MySql

CREATE DATABASE ImdbFavorites;

CREATE TABLE ImdbFavorites.users (
	userID nvarchar(9),
	userFirstName NVARCHAR(50) NOT NULL,
	userLastName NVARCHAR(50) NOT NULL,
	userNickName NVARCHAR(50) NOT NULL,
	userBirthDate DATETIME NULL,
	userGender NVARCHAR(50) NOT NULL,
	userEmail NVARCHAR(50) NOT NULL,
	userPassword NVARCHAR(50) NOT NULL,
	userPicture NVARCHAR(50) NULL,
	userImdbPass NVARCHAR(50) NULL,
    PRIMARY KEY ( userID )
);

CREATE TABLE ImdbFavorites.MOVIES (
	movieImdbID nvarchar(15) NOT NULL,
	movieTitle nvarchar(50) NOT NULL,
	moviePoster nvarchar(10000) NOT NULL,
	movieYear int NOT NULL,
	userID nvarchar(9) ,
    PRIMARY KEY (movieImdbID , userID),
    FOREIGN KEY (userID)
        REFERENCES users (userID)
);

CREATE TABLE ImdbFavorites.MOVIEEXTENDS (
	movieImdbID nvarchar(15) NOT NULL,
	moviePlot nvarchar(1000) NOT NULL,
	movieUrl nvarchar(1000) NOT NULL,
	movieRated nvarchar(10) NOT NULL,
	movieImdbRating real NOT NULL,
	movieSeen bit NOT NULL,
	userID nvarchar(9) NOT NULL,
    
    PRIMARY KEY (movieImdbID , userID),
    FOREIGN KEY (movieImdbID,userID)
        REFERENCES MOVIES (movieImdbID,userID)
);



Delimiter $$
CREATE PROCEDURE ImdbFavorites.AddExtendedMovie (IN movieImdbID varchar(15), IN movieTitle varchar(50), IN moviePoster varchar(1000), IN movieYear int, IN userID varchar(9), IN moviePlot varchar(1000), IN movieUrl varchar(1000), IN movieRated varchar(10), IN movieImdbRating real, IN movieSeen bit)
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
    START TRANSACTION;
		INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (movieImdbID, movieTitle, moviePoster, movieYear, userID);
		INSERT INTO MOVIEEXTENDS (movieImdbID, moviePlot, movieUrl, movieRated, movieImdbRating, movieSeen, userID) VALUES (movieImdbID, moviePlot, movieUrl, movieRated, movieImdbRating, movieSeen, userID);
		SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen, MOVIEEXTENDS.userID From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=movieImdbID and Movies.userID=userID;
	COMMIT;
END $$
Delimiter ;


DELIMITER $$
CREATE PROCEDURE ImdbFavorites.AddMovie(IN movieImdbID varchar(15), IN movieTitle varchar(50), IN moviePoster varchar(1000), IN movieYear int, IN userID varchar(9))
BEGIN
	INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (movieImdbID, movieTitle, moviePoster, movieYear, userID);
	SELECT * FROM Movies WHERE Movies.movieImdbID = movieImdbID and Movies.userID=userID;
END$$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE ImdbFavorites.AddUser(IN userID varchar(9), IN userFirstName varchar(50), IN userLastName varchar(50), IN userNickName varchar(50), IN userBirthDate datetime, IN userGender varchar(50), IN userEmail varchar(50), IN userPassword varchar(50), IN userPicture varchar(50), IN userImdbPass varchar(50))
BEGIN
	INSERT INTO Users (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userImdbPass) VALUES (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userImdbPass);
	SELECT * From Users where Users.userID=userID;
END$$
DELIMITER ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.DeleteExtendedMovie (IN movieImdbID varchar(15), IN userID varchar(9))
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
    START TRANSACTION;
			DELETE FROM MOVIEEXTENDS WHERE MOVIEEXTENDS.movieImdbID = movieImdbID and MOVIEEXTENDS.userID=userID;
			DELETE FROM Movies WHERE Movies.movieImdbID = movieImdbID and Movies.userID=userID;
	COMMIT;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.DeleteMovie (IN movieImdbID varchar(15), IN userID varchar(9))
BEGIN
	DELETE FROM Movies WHERE Movies.movieImdbID=movieImdbID and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.DeleteUser (IN userID varchar(9))
BEGIN
	DELETE FROM Users WHERE Users.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetAllExtendedMovies (IN userID varchar(9))
BEGIN
	SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetAllMovies (IN userID varchar(9))
BEGIN
	SELECT * from Movies where Movies.userID=@userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetAllUsers ()
BEGIN
	SELECT * From Users;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetById (In movieImdbID varchar(15), In userID varchar(9))
BEGIN
	SELECT * from Movies where Movies.movieImdbID=movieImdbID and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetByIdExtendedMovies (In movieImdbID varchar(15), In userID varchar(9))
BEGIN
	SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieImdbID=movieImdbID and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetByTitle (In movieTitle varchar(50), In userID varchar(9))
BEGIN
	SELECT * from Movies where Movies.movieTitle=movieTitle and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetByTitleExtendedMovies (In movieTitle varchar(50), In userID varchar(9))
BEGIN
	SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieTitle=movieTitle and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetByWord (In word varchar(50), In userID varchar(9))
BEGIN
	SELECT * from Movies where Movies.movieTitle LIKE CONCAT('%', word, '%') and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetByWordExtendedMovies (In word varchar(50), In userID varchar(9))
BEGIN
	SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieTitle LIKE CONCAT('%', word, '%') and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetOneUserById (In userID varchar(9))
BEGIN
	SELECT * From Users where Users.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.GetOneUserByName (In userNickName varchar(50))
BEGIN
	SELECT * From Users where Users.userNickName=userNickName;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.IsNameTaken (In userNickName varchar(50))
BEGIN
	SELECT COUNT(1) FROM Users WHERE Users.userNickName = userNickName;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.ReturnImdbPassByNamePassword (In userNickName varchar(50), IN userPassword varchar(50))
BEGIN
	SELECT * from Users WHERE Users.userNickName=userNickName and Users.userPassword=userPassword;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.ReturnUserByNameLogin (In userNickName varchar(50), IN userPassword varchar(50))
BEGIN
	SELECT * from Users WHERE Users.userNickName=userNickName and Users.userPassword=userPassword;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.ReturnUserByNamePassword (In userNickName varchar(50), IN userPassword varchar(50))
BEGIN
	SELECT userNickName, userImdbPass, userPicture from Users WHERE Users.userNickName=userNickName and Users.userPassword=userPassword;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.ReturnUserIdByImdbPass (In movieImdbID varchar(15))
BEGIN
	SELECT userID from Users WHERE Users.userImdbPass=movieImdbID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.ReturnUserIdByUserPass (In userPassword varchar(50))
BEGIN
	SELECT userID from Users WHERE Users.userPassword=userPassword;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.UpdateExtendedMovie (IN movieImdbID varchar(15), IN movieTitle varchar(50), IN moviePoster varchar(1000), IN movieYear int, IN userID varchar(9), IN moviePlot varchar(1000), IN movieUrl varchar(1000), IN movieRated varchar(10), IN movieImdbRating real, IN movieSeen bit)
BEGIN
	DECLARE exit handler for sqlexception
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
 
	DECLARE exit handler for sqlwarning
	  BEGIN
		GET DIAGNOSTICS CONDITION 1
		@p1 = RETURNED_SQLSTATE, @p2 = MESSAGE_TEXT;
		SELECT @p1 as RETURNED_SQLSTATE  , @p2 as MESSAGE_TEXT;
		ROLLBACK;
	END;
	
    START TRANSACTION;
			UPDATE Movies SET Movies.movieImdbID = movieImdbID, Movies.movieTitle = movieTitle, Movies.moviePoster = moviePoster, Movies.movieYear = movieYear, Movies.userID = userID WHERE Movies.movieImdbID = movieImdbID;
			UPDATE MOVIEEXTENDS SET MOVIEEXTENDS.movieImdbID = movieImdbID, MOVIEEXTENDS.moviePlot = moviePlot, MOVIEEXTENDS.movieUrl = movieUrl, MOVIEEXTENDS.movieRated = movieRated, MOVIEEXTENDS.movieImdbRating = movieImdbRating, MOVIEEXTENDS.movieSeen = movieSeen;
			SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=movieImdbID and Movies.userID=userID;
	COMMIT;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.UpdateMovie (In movieImdbID varchar(15), In movieTitle varchar(50), In moviePoster varchar(1000), In movieYear int, In userID varchar(9))
BEGIN
	UPDATE Movies SET Movies.movieImdbID = movieImdbID, Movies.movieTitle = movieTitle, Movies.moviePoster = moviePoster, Movies.movieYear = movieYear, Movies.userID = userID WHERE Movies.movieImdbID = movieImdbID;
	SELECT * FROM Movies WHERE Movies.movieImdbID = movieImdbID and Movies.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.UpdateUser (In userID varchar(9), In userFirstName varchar(50), In userLastName varchar(50), In userNickName varchar(50), In userBirthDate datetime, In userGender varchar(50), In userEmail varchar(50), In userPassword varchar(50), In userPicture varchar(50), In userImdbPass varchar(50))
BEGIN
	UPDATE Users SET Users.userID = userID, Users.userFirstName = userFirstName, Users.userLastName = userLastName, Users.userNickName = userNickName, Users.userBirthDate = userBirthDate, Users.userGender = userGender, Users.userEmail = userEmail, Users.userPassword = userPassword, Users.userPicture = userPicture, Users.userImdbPass = userImdbPass where Users.userID=userID;
	SELECT * From Users where Users.userID=userID;
END $$
Delimiter ;


Delimiter $$
CREATE PROCEDURE ImdbFavorites.UploadUserImage (IN userID varchar(9), IN userPicture varchar(50))
BEGIN
	UPDATE Users SET Users.userPicture = userPicture where Users.userID=userID;
	SELECT * From Users where Users.userID=userID;
END $$
Delimiter ;

//-------------------------------------------------------------------------------------------------------------------------//
Sql

CREATE DATABASE ImdbFavorites;


CREATE TABLE USERS (
	userID nvarchar(9) PRIMARY KEY ,
	userFirstName nvarchar(50) NOT NULL,
	userLastName nvarchar(50) NOT NULL,
	userNickName nvarchar(50) NOT NULL,
	userBirthDate datetime NULL,
	userGender nvarchar(50) NOT NULL,
	userEmail nvarchar(50) NOT NULL,
	userPassword nvarchar(50) NOT NULL,
	userPicture nvarchar(50) NULL,
	userImdbPass nvarchar(50) NULL,
);

CREATE TABLE MOVIES (
	movieImdbID nvarchar(15) NOT NULL,
	movieTitle nvarchar(50) NOT NULL,
	moviePoster nvarchar(MAX) NOT NULL,
	movieYear int NOT NULL,
	userID nvarchar(9) FOREIGN KEY REFERENCES USERS(userID),
	CONSTRAINT PK_Movies PRIMARY KEY (movieImdbID,userID)
);

CREATE TABLE MOVIEEXTENDS (
	movieImdbID nvarchar(15) NOT NULL,
	moviePlot nvarchar(MAX) NOT NULL,
	movieUrl nvarchar(MAX) NOT NULL,
	movieRated nvarchar(10) NOT NULL,
	movieImdbRating real NOT NULL,
	movieSeen bit NOT NULL,
	userID nvarchar(9) NOT NULL,
	CONSTRAINT PK_MoviesExtends PRIMARY KEY (movieImdbID,userID),
	CONSTRAINT PK_MoviesExtends2 FOREIGN KEY (movieImdbID,userID) REFERENCES MOVIES(movieImdbID,userID)
);


CREATE PROCEDURE [dbo].[AddExtendedMovie] (@movieImdbID nvarchar(15), @movieTitle nvarchar(50), @moviePoster nvarchar(MAX), @movieYear int, @userID nvarchar(9), @moviePlot nvarchar(MAX), @movieUrl nvarchar(MAX), @movieRated nvarchar(10), @movieImdbRating real, @movieSeen bit)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID);
			INSERT INTO MOVIEEXTENDS (movieImdbID, moviePlot, movieUrl, movieRated, movieImdbRating, movieSeen, userID) VALUES (@movieImdbID, @moviePlot, @movieUrl, @movieRated, @movieImdbRating, @movieSeen, @userID);
			SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=@movieImdbID and Movies.userID=@userID
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	
	
CREATE PROCEDURE [dbo].[AddMovie] (@movieImdbID nvarchar(15), @movieTitle nvarchar(50), @moviePoster nvarchar(MAX), @movieYear int, @userID nvarchar(9))
AS
INSERT INTO Movies (movieImdbID, movieTitle, moviePoster, movieYear, userID) VALUES (@movieImdbID, @movieTitle, @moviePoster, @movieYear, @userID);
SELECT * FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;


CREATE PROCEDURE [dbo].[AddUser] (@userID nvarchar(9), @userFirstName nvarchar(50), @userLastName nvarchar(50), @userNickName nvarchar(50), @userBirthDate datetime, @userGender nvarchar(50), @userEmail nvarchar(50), @userPassword nvarchar(50), @userPicture nvarchar(50), @userImdbPass nvarchar(50))
AS
INSERT INTO Users (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userImdbPass) VALUES (@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userImdbPass);
SELECT * From Users where userID=@userID;


CREATE PROCEDURE [dbo].[DeleteExtendedMovie] (@movieImdbID nvarchar(15), @userID nvarchar(9))
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			DELETE FROM MOVIEEXTENDS WHERE movieImdbID = @movieImdbID and userID=@userID;
			DELETE FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID;
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch
	
	
CREATE PROCEDURE [dbo].[DeleteMovie] (@movieImdbID nvarchar(15), @userID nvarchar(9))
AS
DELETE FROM Movies WHERE movieImdbID=@movieImdbID and userID=@userID


CREATE PROCEDURE [dbo].[DeleteUser] @userID nvarchar(9)
AS
DELETE FROM Users WHERE userID=@userID


CREATE PROCEDURE [dbo].[GetAllExtendedMovies] @userID nvarchar(9)
AS
SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.userID=@userID


CREATE PROCEDURE [dbo].[GetAllMovies] @userID nvarchar(9)
AS
SELECT * from Movies where userID=@userID


CREATE PROCEDURE [dbo].[GetAllUsers]
AS
SELECT * From Users;


CREATE PROCEDURE [dbo].[GetById] (@movieImdbID nvarchar(15), @userID nvarchar(9))
AS
SELECT * from Movies where movieImdbID=@movieImdbID and userID=@userID


CREATE PROCEDURE [dbo].[GetByIdExtendedMovies] (@movieImdbID nvarchar(15), @userID nvarchar(9))
AS
SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieImdbID=@movieImdbID and Movies.userID=@userID


CREATE PROCEDURE [dbo].[GetByTitle] (@movieTitle nvarchar(50), @userID nvarchar(9))
AS
SELECT * from Movies where movieTitle=@movieTitle and userID=@userID


CREATE PROCEDURE [dbo].[GetByTitleExtendedMovies] (@movieTitle nvarchar(50), @userID nvarchar(9))
AS
SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieTitle=@movieTitle and Movies.userID=@userID


CREATE PROCEDURE [dbo].[GetByWord] (@word nvarchar(50), @userID nvarchar(9))
AS
SELECT * from Movies where movieTitle LIKE '%' + @word + '%' and userID=@userID


CREATE PROCEDURE [dbo].[GetByWordExtendedMovies] (@word nvarchar(50), @userID nvarchar(9))
AS
SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID and Movies.userID=MOVIEEXTENDS.userID where Movies.movieTitle LIKE '%' + @word + '%' and Movies.userID=@userID


CREATE PROCEDURE [dbo].[GetOneUserById] @userID nvarchar(9)
AS
SELECT * From Users where userID=@userID


CREATE PROCEDURE [dbo].[GetOneUserByName] @userNickName nvarchar(50)
AS
SELECT * From Users where userNickName=@userNickName


CREATE PROCEDURE [dbo].[IsNameTaken] @userNickName nvarchar(50)
AS
SELECT COUNT(1) FROM Users WHERE userNickName = @userNickName


CREATE PROCEDURE [dbo].[ReturnImdbPassByNamePassword] (@userNickName nvarchar(50), @userPassword nvarchar(50))
AS
SELECT * from Users WHERE userNickName=@userNickName and userPassword=@userPassword


CREATE PROCEDURE [dbo].[ReturnUserByNameLogin] (@userNickName nvarchar(50), @userPassword nvarchar(50))
AS
SELECT * from Users WHERE userNickName=@userNickName and userPassword=@userPassword


CREATE PROCEDURE [dbo].[ReturnUserByNamePassword] (@userNickName nvarchar(50), @userPassword nvarchar(50))
AS
SELECT userNickName, userImdbPass, userPicture from Users WHERE userNickName=@userNickName and userPassword=@userPassword


CREATE PROCEDURE [dbo].[ReturnUserIdByImdbPass] @movieImdbID nvarchar(15)
AS
SELECT userID from Users WHERE userImdbPass=@movieImdbID


CREATE PROCEDURE [dbo].[ReturnUserIdByUserPass] @userPassword nvarchar(50)
AS
SELECT userID from Users WHERE userPassword=@userPassword


CREATE PROCEDURE [dbo].[UpdateExtendedMovie] (@movieImdbID nvarchar(15), @movieTitle nvarchar(50), @moviePoster nvarchar(MAX), @movieYear int, @userID nvarchar(9), @moviePlot nvarchar(MAX), @movieUrl nvarchar(MAX), @movieRated nvarchar(10), @movieImdbRating real, @movieSeen bit)
AS
set transaction isolation level Read Uncommitted
	begin try
		begin transaction
			UPDATE Movies SET movieImdbID = @movieImdbID, movieTitle = @movieTitle, moviePoster = @moviePoster, movieYear = @movieYear, userID = @userID WHERE movieImdbID = @movieImdbID;
			UPDATE MOVIEEXTENDS SET MOVIEEXTENDS.movieImdbID = @movieImdbID, MOVIEEXTENDS.moviePlot = @moviePlot, MOVIEEXTENDS.movieUrl = @movieUrl, MOVIEEXTENDS.movieRated = @movieRated, MOVIEEXTENDS.movieImdbRating = @movieImdbRating, MOVIEEXTENDS.movieSeen = @movieSeen;
			SELECT Movies.movieImdbID, Movies.movieTitle, Movies.moviePoster, Movies.userID, Movies.movieYear, MOVIEEXTENDS.moviePlot, MOVIEEXTENDS.movieUrl, MOVIEEXTENDS.movieRated, MOVIEEXTENDS.movieImdbRating, MOVIEEXTENDS.movieSeen From Movies LEFT JOIN MOVIEEXTENDS ON Movies.movieImdbID=MOVIEEXTENDS.movieImdbID where Movies.movieImdbID=@movieImdbID and Movies.userID=@userID
		commit transaction
	end try
	begin catch
		rollback transaction
	end catch

	
CREATE PROCEDURE [dbo].[UpdateMovie] (@movieImdbID nvarchar(15), @movieTitle nvarchar(50), @moviePoster nvarchar(MAX), @movieYear int, @userID nvarchar(9))
AS
UPDATE Movies SET movieImdbID = @movieImdbID, movieTitle = @movieTitle, moviePoster = @moviePoster, movieYear = @movieYear, userID = @userID WHERE movieImdbID = @movieImdbID;
SELECT * FROM Movies WHERE movieImdbID = @movieImdbID and userID=@userID


CREATE PROCEDURE [dbo].[UpdateUser] (@userID nvarchar(9), @userFirstName nvarchar(50), @userLastName nvarchar(50), @userNickName nvarchar(50), @userBirthDate datetime, @userGender nvarchar(50), @userEmail nvarchar(50), @userPassword nvarchar(50), @userPicture nvarchar(50), @userImdbPass nvarchar(50))
AS
UPDATE Users SET userID = @userID, userFirstName = @userFirstName, userLastName = @userLastName, userNickName = @userNickName, userBirthDate = @userBirthDate, userGender = @userGender, userEmail = @userEmail, userPassword = @userPassword, userPicture = @userPicture, userImdbPass = @userImdbPass where userID=@userID;
SELECT * From Users where userID=@userID;


CREATE PROCEDURE [dbo].[UploadUserImage] (@userID nvarchar(9), @userPicture nvarchar(50))
AS
UPDATE Users SET userPicture = @userPicture where userID=@userID;
SELECT * From Users where userID=@userID;