//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace CobimExplorer.Rest.Api.CobimBase.User
//{
//    public static class LoginHelper
//    {
//        public const string inputTenantId = "inc-001";
//        public const string auth_server_url = "http://bim.211.43.204.141.nip.io:31380/api/account/user/signin";
//        public const string contentType = "application/json";
//    }

//    public class LoginPack
//    {
//        public string id { get; set; }
//        // public string realm { get; set; }

//        public string password { get; set; }

//        public string inputTenantId { get; set; }

//        public string auth_server_url { get; set; }

//        //public string resource { get; set; }
//        //public string credentials_secret { get; set; }
//    }

//    public class Login_Access_Token
//    {
//        /// <summary>
//        /// 로그인 토큰 키 (resultData)
//        /// </summary>
//        public string resultData { get; set; }

//        public int resultCount { get; set; }

//        public string resultMessage { get; set; }

//        public string tenantId { get; set; }

//        public string jwt { get; set; }

//        public string refreshToken { get; set; }

//        public string param { get; set; }

//        public string payload { get; set; }

//        public string actionUserInfo { get; set; }

//        // public string access_token { get; set; }
//        //public string token_type { get; set; }
//        //public long expires_in { get; set; }
//        //public string error { get; set; }
//        //public string error_description { get; set; }
//        //public HttpStatusCode status { get; set; }
//    }


//}
