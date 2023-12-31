﻿using Serilog;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using CobimExplorer.Rest.Api.CobimBase.User;
using CobimUtil;
// using CobimExplorer.Rest.Api.CobimBase.User.LoginHelper;
//using static CobimExplorer.Rest.Api.CobimBase.User.LoginHelper;
//using static CobimExplorer.Rest.Api.CobimBase.User.LoginHelper;

namespace CobimExplorer.Rest.Api.NetWork.Http
{
    // TODO : httpManager 클래스(http 통신 역할) 필요 시 사용 예정(2023.08.22 jbh) 
    // 주의사항 - Http 통신 로직 구현시 "System.Text.json"의 경우 .Net Core에서는 잘 호환(사용)되지만 .Net Framework에서는 호환되지 않고 에러가 발생하는 경우가 존재함.
    public class httpManager
    {
        /// <summary>
        /// Http POST 통신 메서드 
        /// Rest API 메서드 파라미터 "string UserID, string PassWord, string inputTenantId"
        /// </summary>
        /// <param name="client">HttpClient</param>
        /// <param name="LoginPack">로그인 정보</param>
        /// <returns>Result 값</returns>
        public static async Task<LoginHelper.Login_Access_Token> PostLoginConnect(HttpClient client, LoginHelper.LoginPack LoginPack)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                var grant = new List<KeyValuePair<string, string>>();
                grant.Add(new KeyValuePair<string, string>("id", LoginPack.id));
                grant.Add(new KeyValuePair<string, string>("password", LoginPack.password));
                grant.Add(new KeyValuePair<string, string>("inputTenantId", LoginPack.inputTenantId));

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, LoginHelper.auth_server_url);
                req.Content = new FormUrlEncodedContent(grant);
                //req.Content.("id", LoginPack.id);
                //req.Content.TryAddWithoutValidation("password", LoginPack.password);
                //req.Content.TryAddWithoutValidation("inputTenantId", LoginPack.inputTenantId);
                req.Headers.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                // C# HttpClient Header "ContentType" 설정 방법 
                // 참고 URL - https://heyoonow.tistory.com/86
                //var data = new FormUrlEncodedContent(values);
                //data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                // req.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await Connect(client, req);
                string json = response.Content.ReadAsStringAsync().Result;
                LoginHelper.Login_Access_Token token = JsonSerializer.Deserialize<LoginHelper.Login_Access_Token>(json);

                return token;
            }
            catch (Exception e)
            {
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        /// <summary>
        /// Http Connect
        /// </summary>
        public static async Task<HttpResponseMessage> Connect(HttpClient client, HttpRequestMessage request)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {

                var jsonContent = new StringContent(
                    "\"id\":\"dvl001\",\"password\":\"qwer1234!\",\"inputTenantId\":\"inc001\""
                    , Encoding.UTF8, "application/json");

                var res = await client.PostAsync("http://bim.211.43.204.141.nip.io:31380/api/account/user/signin", jsonContent);

                // TODO : HttpResponseMessage response이 null로 리턴받아 Null Expcetion 발생하는 원인 파악하기 (2023.08.21 jbh)
                //HttpRequestMessage req = request;
                HttpResponseMessage response = await client.SendAsync(request);//.Result;
                string msg = "HTTP Client Connect:\n";
                msg += "Request:\n" + request.ToString() + "\n";
                if (request.Content != null)
                {
                    msg += request.Content.ReadAsStringAsync().Result + "\n\n";
                }
                msg += "response:\n" + response.ToString() + "\n";
                if (response.Content != null)
                {
                    msg += response.Content.ReadAsStringAsync().Result + "\n\n";
                }
                //Replace password and client secret
                // Serilog.Log.Debug(msg);
                Logger.Debug(currentMethod, Logger.debugMessage + msg);
                return response;
            }
            catch(Exception e)
            {
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
            
        }
    }
}
