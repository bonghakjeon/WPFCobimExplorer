﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobimExplorer.Rest.Api.CobimBase.User
{
    public class LoginHelper
    {
        /// <summary>
        /// 1. 로그인
        /// 요청 파라미터 - 테스트 아이디
        /// </summary>
        public const string testId = "dvl001";

        /// <summary>
        /// 1. 로그인
        /// 요청 파라미터 - 테스트 비밀번호 
        /// </summary>
        public const string testPassword = "qwer1234!";

        /// <summary>
        /// 1. 로그인
        /// 요청 파라미터 - inputTenantId
        /// </summary>
        public const string inputTenantId = "inc-001";

        /// <summary>
        /// 1. 로그인
        /// 요청 URL - auth_server_url
        /// </summary>
        public const string auth_server_url = "http://bim.211.43.204.141.nip.io:31380/api/account/user/signin";

        /// <summary>
        /// 1. 로그인
        /// 요청 Header - Content-Type	
        /// </summary>
        public const string contentType = "application/json";

        /// <summary>
        /// 로그인 정보 
        /// </summary>
        public class LoginPack
        {
            /// <summary>
            /// 요청 파라미터 - 아이디
            /// </summary>
            public string id { get; set; }
            // public string realm { get; set; }

            /// <summary>
            /// 요청 파라미터 - 비밀번호
            /// </summary>
            public string password { get; set; }

            /// <summary>
            /// 요청 파라미터 - inputTenantId
            /// </summary>
            public string inputTenantId { get; set; }

            /// <summary>
            /// 요청 URL - auth_server_url
            /// </summary>
            // public string auth_server_url { get; set; }
        }

        /// <summary>
        /// 로그인 응답 결과 토큰 키
        /// </summary>
        public class Login_Access_Token
        {
            /// <summary>
            /// 응답결과 - 로그인 토큰 키 (resultData)
            /// </summary>
            public string resultData { get; set; }

            public int resultCount { get; set; }

            /// <summary>
            /// Http 통신 결과 메시지
            /// </summary>
            public string resultMessage { get; set; }

            public string tenantId { get; set; }

            public string jwt { get; set; }

            public string refreshToken { get; set; }

            public string param { get; set; }

            public string payload { get; set; }

            public string actionUserInfo { get; set; }
        }
    }
}
