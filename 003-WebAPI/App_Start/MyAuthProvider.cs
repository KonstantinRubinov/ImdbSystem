using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImdbSystem
{
	public class MyAuthProvider : OAuthAuthorizationServerProvider
	{
		UserModel userdata;

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var identity = new ClaimsIdentity(context.Options.AuthenticationType);

			if (GlobalVariable.logicType == 0)
				userdata = new EntityUsersManager().ReturnUserByNamePassword(context.UserName, context.Password);
			else if (GlobalVariable.logicType == 1)
				userdata = new SqlUsersManager ().ReturnUserByNamePassword(context.UserName, context.Password);
			else if (GlobalVariable.logicType == 2)
				userdata = new MySqlUsersManager().ReturnUserByNamePassword(context.UserName, context.Password);
			else
				userdata = new MongoUsersManager().ReturnUserByNamePassword(context.UserName, context.Password);


			if (userdata != null)
			{
				identity.AddClaim(new Claim(ClaimTypes.SerialNumber, userdata.userImdbPass));
				identity.AddClaim(new Claim(ClaimTypes.Name, userdata.userID));
				context.Validated(identity);
			}
			else
			{
				context.SetError("invalid_grant", "Provided username and password is incorrect");
				context.Rejected();
			}
		}

		public override Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
			{
				context.AdditionalResponseParameters.Add(property.Key, property.Value);
				if (property.Key.Equals(".issued"))
				{
					context.AdditionalResponseParameters.Add(".issuedTimeStamp", GetTimestamp(DateTime.Parse(property.Value)));
				}
				if (property.Key.Equals(".expires"))
				{
					context.AdditionalResponseParameters.Add(".expiresTimeStamp", GetTimestamp(DateTime.Parse(property.Value)));
				}
			}

			context.AdditionalResponseParameters.Add("userNickName", userdata.userNickName);
			context.AdditionalResponseParameters.Add("userPicture", userdata.userPicture);
			return Task.FromResult<object>(null);
		}

		private string GetTimestamp(DateTime value)
		{
			return value.ToString("yyyyMMddHHmmssffff");
		}

	}
}