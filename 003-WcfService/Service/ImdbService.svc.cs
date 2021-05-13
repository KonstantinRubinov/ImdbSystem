using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ImdbSystem
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ImdbService" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select ImdbService.svc or ImdbService.svc.cs at the Solution Explorer and start debugging.
	public class ImdbService : IImdbService
	{
		private IImdbRepository imdbRepository;
		private IUsersRepository userRepository;

		

		public ImdbService()
		{
			imdbRepository = new ImdbManager();

			if (GlobalVariable.logicType == 0)
				userRepository = new EntityUsersManager();
			else if (GlobalVariable.logicType == 1)
				userRepository = new SqlUsersManager ();
			else if (GlobalVariable.logicType == 2)
				userRepository = new MySqlUsersManager();
			else
				userRepository = new MongoUsersManager();
		}

		public async Task<HttpResponseMessage> GetImdbById(string getByID)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = userRepository.ReturnUserIdByImdbPass(userPass);

				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(await imdbRepository.GetImdbById(userPass, getByID, userID)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public async Task<HttpResponseMessage> GetImdbByTitle(string getByTitle)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = userRepository.ReturnUserIdByImdbPass(userPass);

				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(await imdbRepository.GetImdbByTitle(userPass, getByTitle, userID)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}

		public async Task<HttpResponseMessage> GetImdbByWord(string getByWord)
		{
			try
			{
				IncomingWebRequestContext woc = WebOperationContext.Current.IncomingRequest;
				string userPass = woc.Headers["userPass"];
				userPass = Encoding.UTF8.GetString(Convert.FromBase64String(userPass));

				string userID = userRepository.ReturnUserIdByImdbPass(userPass);

				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = new StringContent(JsonConvert.SerializeObject(await imdbRepository.GetImdbByWord(userPass, getByWord, userID)))
				};
				return hrm;
			}
			catch (Exception ex)
			{
				Errors errors = ErrorsHelper.GetErrors(ex);
				HttpResponseMessage hrm = new HttpResponseMessage(HttpStatusCode.InternalServerError)
				{
					Content = new StringContent(errors.ToString())
				};
				return hrm;
			}
		}
	}
}
