using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace ImdbSystem
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IImdbService" in both code and config file together.
	[ServiceContract]
	public interface IImdbService
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Users/?getByID={getByID}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		Task<HttpResponseMessage> GetImdbById(string getByID);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Users/?getByWord={getByWord}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		Task<HttpResponseMessage> GetImdbByWord(string getByWord);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Users/?getByTitle={getByTitle}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
		Task<HttpResponseMessage> GetImdbByTitle(string getByTitle);
	}
}
