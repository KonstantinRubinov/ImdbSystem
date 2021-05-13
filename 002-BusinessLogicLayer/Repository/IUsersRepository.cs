using System.Collections.Generic;

namespace ImdbSystem
{
	public interface IUsersRepository
	{
		List<UserModel> GetAllUsers();
		UserModel GetOneUserById(string id);
		UserModel GetOneUserByName(string name);
		UserModel AddUser(UserModel userModel);
		UserModel UpdateUser(UserModel userModel);
		int DeleteUser(string id);
		LoginModel ReturnUserByNamePassword(LoginModel checkUser);
		UserModel ReturnUserByNamePassword(string userNickName, string userPassword);
		string ReturnImdbPassByNamePassword(LoginModel checkUser);
		string ReturnUserIdByUserPass(string userPass);
		bool IsNameTaken(string name);
		UserModel UploadUserImage(string id, string img);
		string ReturnUserIdByImdbPass(string imdbPass);
	}
}
