using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ImdbSystem
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsersService" in both code and config file together.
	[ServiceContract]
	public interface IUsersService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Users/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllUsers();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Users/?getByID={getByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetOneUser(string getByID);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Users/check", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage ReturnUserByNamePassword(LoginModel loginModel);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Users/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddUser(UserModel userModel);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Users/?updateById={updateById}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateUser(string updateById, UserModel userModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Users/?deleteById={deleteById}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteUser(string deleteById);
	}
}
