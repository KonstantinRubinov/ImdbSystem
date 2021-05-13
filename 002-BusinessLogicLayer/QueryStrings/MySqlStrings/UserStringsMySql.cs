using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ImdbSystem
{
	static public class UserStringsMySql
	{
		static private string queryUsersString = "SELECT * from Users;";
		static private string queryUsersByIdString = "SELECT * from Users where userID=@userID;";
		static private string queryUsersByNameString = "SELECT * from Users where userNickName=@userNickName;";
		static private string queryUsersPost = "INSERT INTO Users (userID, userFirstName, userLastName, userNickName, userBirthDate, userGender, userEmail, userPassword, userPicture, userImdbPass) VALUES (@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userImdbPass); SELECT * FROM Users where userID=@userID;";
		static private string queryUsersUpdate = "UPDATE Users SET userID = @userID, userFirstName = @userFirstName, userLastName = @userLastName, userNickName = @userNickName, userBirthDate = @userBirthDate, userGender = @userGender, userEmail = @userEmail, userPassword = @userPassword, userPicture = @userPicture, userImdbPass = @userImdbPass where userID=@userID; SELECT * FROM Users where userID=@userID;";
		static private string queryUsersDelete = "DELETE FROM Users WHERE userID=@userID;";
		static private string queryUsersNamePassword = "SELECT userNickName, userImdbPass, userPicture from Users WHERE userNickName=@userNickName and userPassword=@userPassword;";
		static private string queryUsersByLogin = "SELECT * from Users WHERE userNickName=@userNickName and userPassword=@userPassword;";
		static private string queryUsersIdByPassword = "SELECT userID from Users WHERE userPassword=@userPassword;";
		static private string queryUsersIfNameExists = "SELECT COUNT(1) FROM Users WHERE userNickName = @userNickName;";
		static private string queryUsersPictureUpdate = "UPDATE Users SET userPicture = @userPicture where userID=@userID; SELECT * FROM Users where userID=@userID;";
		static private string queryUsersIdByImdbPass = "SELECT userID from Users WHERE userImdbPass=@userImdbPass;";

		static private string procedureUsersString = "CALL `imdbfavorites`.`GetAllUsers`();";
		static private string procedureUsersByIdString = "CALL `imdbfavorites`.`GetOneUserById`(@userID);";
		static private string procedureUsersByNameString = "CALL `imdbfavorites`.`GetOneUserByName`(@userNickName);";
		static private string procedureUsersPost = "CALL `imdbfavorites`.`AddUser`(@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userImdbPass);";
		static private string procedureUsersUpdate = "CALL `imdbfavorites`.`UpdateUser`(@userID, @userFirstName, @userLastName, @userNickName, @userBirthDate, @userGender, @userEmail, @userPassword, @userPicture, @userImdbPass);";
		static private string procedureUsersDelete = "CALL `imdbfavorites`.`DeleteUser`(@userID);";
		static private string procedureUsersNamePassword = "CALL `imdbfavorites`.`ReturnUserByNamePassword`(@userNickName, @userPassword);";
		static private string procedureUsersByLogin = "CALL `imdbfavorites`.`ReturnUserByNameLogin`(@userNickName, @userPassword);";
		static private string procedureUsersIdByPassword = "CALL `imdbfavorites`.`ReturnUserIdByUserPass`(@userPassword);";
		static private string procedureUsersIfNameExists = "CALL `imdbfavorites`.`IsNameTaken`(@userNickName);";
		static private string procedureUsersPictureUpdate = "CALL `imdbfavorites`.`UploadUserImage`(@userID, @userPicture);";
		static private string procedureUsersIdByImdbPass = "CALL `imdbfavorites`.`ReturnUserIdByImdbPass`(@userImdbPass);";
		static private string procedureUsersImdbByNamePassword = "CALL `imdbfavorites`.`ReturnImdbPassByNamePassword`(@userNickName, @userPassword);";

		static public MySqlCommand GetAllUsers()
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(queryUsersString);
			else
				return CreateSqlCommand(procedureUsersString);
		}

		static public MySqlCommand GetOneUserById(string id)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(id, queryUsersByIdString);
			else
				return CreateSqlCommandId(id, procedureUsersByIdString);

		}

		static public MySqlCommand GetOneUserByName(string name)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandName(name, queryUsersByNameString);
			else
				return CreateSqlCommandName(name, procedureUsersByNameString);
		}

		static public MySqlCommand AddUser(UserModel userModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userModel, queryUsersPost);
			else
				return CreateSqlCommand(userModel, procedureUsersPost);
		}

		static public MySqlCommand UpdateUser(UserModel userModel)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userModel, queryUsersUpdate);
			else
				return CreateSqlCommand(userModel, procedureUsersUpdate);
		}

		static public MySqlCommand DeleteUser(string id)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandId(id, queryUsersDelete);
			else
				return CreateSqlCommandId(id, procedureUsersDelete);
		}

		static public MySqlCommand ReturnUserByNamePassword(LoginModel checkUser)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(checkUser, queryUsersNamePassword);
			else
				return CreateSqlCommand(checkUser, procedureUsersNamePassword);
		}

		static public MySqlCommand ReturnUserByNamePassword(string userNickName, string userPassword)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommand(userNickName, userPassword, queryUsersByLogin);
			else
				return CreateSqlCommand(userNickName, userPassword, procedureUsersByLogin);
		}

		static public MySqlCommand ReturnUserIdByUserPass(string userPass)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPass(userPass, queryUsersIdByPassword);
			else
				return CreateSqlCommandPass(userPass, procedureUsersIdByPassword);
		}

		static public MySqlCommand IsNameTaken(string name)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandName(name, queryUsersIfNameExists);
			else
				return CreateSqlCommandName(name, procedureUsersIfNameExists);
		}

		static public MySqlCommand UploadUserImage(string id, string img)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandPicture(id, img, queryUsersPictureUpdate);
			else
				return CreateSqlCommandPicture(id, img, procedureUsersPictureUpdate);
		}

		static public MySqlCommand ReturnUserIdByImdbPass(string userImdbPass)
		{
			if (GlobalVariable.queryType == 0)
				return CreateSqlCommandImdbPass(userImdbPass, queryUsersIdByImdbPass);
			else
				return CreateSqlCommandImdbPass(userImdbPass, procedureUsersIdByImdbPass);
		}



		static private MySqlCommand CreateSqlCommand(UserModel user, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", user.userID);
			command.Parameters.AddWithValue("@userFirstName", user.userFirstName);
			command.Parameters.AddWithValue("@userLastName", user.userLastName);
			command.Parameters.AddWithValue("@userNickName", user.userNickName);
			command.Parameters.AddWithValue("@userBirthDate", user.userBirthDate);
			command.Parameters.AddWithValue("@userGender", user.userGender);
			command.Parameters.AddWithValue("@userEmail", user.userEmail);
			command.Parameters.AddWithValue("@userPassword", user.userPassword);
			command.Parameters.AddWithValue("@userPicture", user.userPicture);
			command.Parameters.AddWithValue("@userImdbPass", user.userImdbPass);

			return command;
		}



		static private MySqlCommand CreateSqlCommandId(string userID, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userID", userID);

			return command;
		}

		static private MySqlCommand CreateSqlCommandName(string userNickName, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			command.Parameters.AddWithValue("@userNickName", userNickName);

			return command;
		}

		static private MySqlCommand CreateSqlCommand(LoginModel user, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userNickName", user.userNickName);
			command.Parameters.AddWithValue("@userPassword", user.userPassword);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(string userNickName, string userPassword, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userNickName", userNickName);
			command.Parameters.AddWithValue("@userPassword", userPassword);
			Debug.WriteLine(command.CommandText);
			return command;
		}

		static private MySqlCommand CreateSqlCommandPicture(string userID, string userPicture, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userID", userID);
			command.Parameters.AddWithValue("@userPicture", userPicture);
			return command;
		}

		static private MySqlCommand CreateSqlCommandPass(string userPassword, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userPassword", userPassword);
			return command;
		}

		static private MySqlCommand CreateSqlCommandImdbPass(string userImdbPass, string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);
			command.Parameters.AddWithValue("@userImdbPass", userImdbPass);
			return command;
		}

		static private MySqlCommand CreateSqlCommand(string commandText)
		{
			MySqlCommand command = new MySqlCommand(commandText);

			return command;
		}
	}
}
