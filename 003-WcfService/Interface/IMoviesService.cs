using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ImdbSystem
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMoviesService" in both code and config file together.
	[ServiceContract]
	public interface IMoviesService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Movies/all/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetAllMovies();

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Movies/?getByWord={getByWord}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetByWord(string getByWord);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Movies/?getByID={getByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetById(string getByID);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Movies/?getByTitle={getByTitle}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage GetByTitle(string getByTitle);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Movies/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage AddMovie(MovieExtendModel movieModel);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Movies/?updateById={updateById}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage UpdateMovie(string updateById, MovieExtendModel movieModel);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Movies/?deletById={deletById}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		HttpResponseMessage DeleteMovie(string deletById);
	}
}
