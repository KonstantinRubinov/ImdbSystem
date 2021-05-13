using ImdbSystem.EntityDataBase.SqlEntityDataBase;
using System.Collections.Generic;
using System.Linq;

namespace ImdbSystem
{
	public class EntityUsersManager : EntityBaseManager, IUsersRepository
	{
		public List<UserModel> GetAllUsers()
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).ToList();
			}
			else
			{
				return DB.GetAllUsers().Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).ToList();
			}
		}

		public UserModel GetOneUserById(string id)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userID.Equals(id)).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneUserById(id).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
		}

		public UserModel GetOneUserByName(string name)
		{
			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(name)).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
			else
			{
				return DB.GetOneUserByName(name).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
		}

		public UserModel AddUser(UserModel userModel)
		{
			UserModel user;
			if (GlobalVariable.queryType == 0)
			{
				string userPassword2 = ComputeHash.ComputeNewHash(userModel.userPassword);
				USER user2 = new USER
				{
					userID = userModel.userID,
					userFirstName = userModel.userFirstName,
					userLastName = userModel.userLastName,
					userNickName = userModel.userNickName,
					userPassword = userPassword2,
					userEmail = userModel.userEmail,
					userGender = userModel.userGender,
					userBirthDate = userModel.userBirthDate,
					userPicture = userModel.userPicture,
					userImdbPass = userModel.userImdbPass
				};
				DB.USERS.Add(user2);
				DB.SaveChanges();
				user = GetOneUserById(user2.userID);
			}
			else
			{
				user = DB.AddUser(userModel.userID, userModel.userFirstName, userModel.userLastName, userModel.userNickName, userModel.userBirthDate, userModel.userGender, userModel.userEmail, ComputeHash.ComputeNewHash(userModel.userPassword), userModel.userPicture, userModel.userImdbPass).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
				

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}
			return user;
		}

		public UserModel UpdateUser(UserModel userModel)
		{
			UserModel user;
			if (GlobalVariable.queryType == 0)
			{
				string userPassword2 = ComputeHash.ComputeNewHash(userModel.userPassword);
				USER user2 = DB.USERS.Where(u => u.userID.Equals(userModel.userID)).SingleOrDefault();
				if (user2 == null)
					return null;
				user2.userID = userModel.userID;
				user2.userFirstName = userModel.userFirstName;
				user2.userLastName = userModel.userLastName;
				user2.userNickName = userModel.userNickName;
				user2.userPassword = userPassword2;
				user2.userEmail = userModel.userEmail;
				user2.userGender = userModel.userGender;
				user2.userBirthDate = userModel.userBirthDate;
				user2.userPicture = userModel.userPicture;
				user2.userImdbPass = userModel.userImdbPass;
				DB.SaveChanges();
				user = GetOneUserById(user2.userID);
			}
			else
			{
				user = DB.UpdateUser(userModel.userID, userModel.userFirstName, userModel.userLastName, userModel.userNickName, userModel.userBirthDate, userModel.userGender, userModel.userEmail, ComputeHash.ComputeNewHash(userModel.userPassword), userModel.userPicture, userModel.userImdbPass).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}
			return user;
		}

		public int DeleteUser(string id)
		{
			if (GlobalVariable.queryType == 0)
			{
				USER user = DB.USERS.Where(u => u.userID.Equals(id)).SingleOrDefault();
				DB.USERS.Attach(user);
				if (user == null)
					return 0;
				DB.USERS.Remove(user);
				DB.SaveChanges();
				return 1;
			}
			else
			{
				return DB.DeleteUser(id);
			}
		}

		public LoginModel ReturnUserByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);

			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(checkUser.userNickName)).Where(u => u.userPassword.Equals(checkUser.userPassword)).Select(u => new LoginModel
				{
					userNickName = u.userNickName,
					userImdbPass = u.userImdbPass,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null
				}).SingleOrDefault();
			}
			else
			{
				return DB.ReturnUserByNamePassword(checkUser.userNickName, checkUser.userPassword).Select(u => new LoginModel
				{
					userNickName = u.userNickName,
					userImdbPass = u.userImdbPass,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null
				}).SingleOrDefault();
			}
		}

		public UserModel ReturnUserByNamePassword(string userNickName, string userPassword)
		{
			if (!CheckStringFormat.IsBase64String(userPassword))
			{
				userPassword = ComputeHash.ComputeNewHash(userPassword);
			}

			if (GlobalVariable.queryType == 0)
			{
				return DB.USERS.Where(u => u.userNickName.Equals(userNickName)).Where(u => u.userPassword.Equals(userPassword)).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
			else
			{
				return DB.ReturnUserByNameLogin(userNickName, userPassword).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
		}

		public string ReturnImdbPassByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);

			UserModel userModel;
			if (GlobalVariable.queryType == 0)
			{
				userModel = DB.USERS.Where(u => u.userNickName.Equals(checkUser.userNickName)).Where(u => u.userPassword.Equals(checkUser.userPassword)).Select(u => new UserModel
				{
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
			else
			{
				userModel = DB.ReturnImdbPassByNamePassword(checkUser.userNickName, checkUser.userPassword).Select(u => new UserModel
				{
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}

			return userModel.userImdbPass;
		}

		public string ReturnUserIdByUserPass(string userPass)
		{
			userPass = ComputeHash.ComputeNewHash(userPass);

			UserModel userModel;
			if (GlobalVariable.queryType == 0)
			{
				userModel = DB.USERS.Where(u => u.userPassword.Equals(userPass)).Select(u => new UserModel
				{
					userID = u.userID
				}).SingleOrDefault();
			}
			else
			{
				userModel = DB.ReturnUserIdByUserPass(userPass).Select(userID => new UserModel
				{
					userID = userID
				}).SingleOrDefault();
			}

			return userModel.userID;
		}

		public bool IsNameTaken(string name)
		{
			if (GlobalVariable.queryType == 0)
				return DB.USERS.Any(u => u.userNickName.ToLower().Equals(name.ToLower()));
			else
			{
				if (DB.IsNameTaken(name).Equals(1))
					return true;
				else
					return false;
			}
		}

		public UserModel UploadUserImage(string id, string img)
		{
			if (GlobalVariable.queryType == 0)
			{
				USER user = DB.USERS.Where(u => u.userID.Equals(id)).SingleOrDefault();
				if (user == null)
					return null;
				user.userPicture = img;
				DB.SaveChanges();
				return GetOneUserById(user.userID);
			}
			else
			{
				return DB.UploadUserImage(id, img).Select(u => new UserModel
				{
					userID = u.userID,
					userFirstName = u.userFirstName,
					userLastName = u.userLastName,
					userNickName = u.userNickName,
					userBirthDate = u.userBirthDate,
					userGender = u.userGender,
					userEmail = u.userEmail,
					userPassword = u.userPassword,
					userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
					userImdbPass = u.userImdbPass
				}).SingleOrDefault();
			}
		}

		public string ReturnUserIdByImdbPass(string imdbPass)
		{
			UserModel userModel;
			if (GlobalVariable.queryType == 0)
			{
				userModel = DB.USERS.Where(u => u.userImdbPass.Equals(imdbPass)).Select(u => new UserModel
				{
					userID = u.userID
				}).SingleOrDefault();
			}
			else
			{
				userModel = DB.ReturnUserIdByImdbPass(imdbPass).Select(userID => new UserModel
				{
					userID = userID
				}).SingleOrDefault();
			}

			return userModel.userID;
		}
	}
}