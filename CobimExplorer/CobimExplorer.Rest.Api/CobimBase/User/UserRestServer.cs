using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CobimExplorer.Rest.Api.NetWork.Http;
using CobimUtil;

namespace CobimExplorer.Rest.Api.CobimBase.User
{
    // 주의사항 - Http 통신 로직 구현시 "System.Text.json"의 경우 .Net Core에서는 잘 호환(사용)되지만 .Net Framework에서는 호환되지 않고 에러가 발생하는 경우가 존재함.
    // JSON Object 클래스 생성시 필요한 라이브러리 "Newtonsoft.Json" VS  "System.Text.Json" 비교
    // 참고 URL - https://bigexecution.tistory.com/166
    // JSON Object 클래스 생성시 필요한 라이브러리 "System.Text.Json" 사용 방법 
    // 참고 URL - https://www.csharpstudy.com/Data/Json-SystemTextJson.aspx
    // 참고 2 URL - https://msjo.kr/2019/09/24/1/
    // 참고 3 URL - https://yeko90.tistory.com/entry/chow-to-parse-json
    // 주의사항 - "System.Text.Json"의 경우 NuGet package를 설치

    // C# HTTP POST 웹 요청 만들기
    // 참고 URL - https://www.delftstack.com/ko/howto/csharp/csharp-post-request/#google_vignette
    // C# HTTP POST "WebClient" VS "HttpWebRequest/HttpWebResponse" VS "HttpClient"비교 
    // 참고 URL - https://stackoverflow.com/questions/1694388/webclient-vs-httpwebrequest-httpwebresponse
    // 참고 2 URL - https://www.infoworld.com/article/3198673/when-to-use-webclient-vs-httpclient-vs-httpwebrequest.html
    // 참고 3 URL - https://im-first-rate.tistory.com/154
    // 참고 4 URL - https://forum.dotnetdev.kr/t/http/2382/2
    public class UserRestServer
    {
        #region PostUserLoginAsync

        /// <summary>
        /// 사용자 로그인 (POST)
        /// Rest API 메서드 파라미터 "HttpClient client, string PassWord, string inputTenantId"
        /// </summary>
        public static async Task<LoginHelper.Login_Access_Token> PostUserLoginAsync(HttpClient client, LoginHelper.LoginPack loginPack)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : Http 통신 Post 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.08.30 jbh)

                //LoginHelper.LoginPack LoginPack = new LoginHelper.LoginPack
                //{
                //    id = LoginID,
                //    password = Password,
                //    inputTenantId = LoginHelper.inputTenantId,
                //};

                // HttpClient 클래스 객체 "client"의 요청헤더(DefaultRequestHeaders)에 데이터 추가 하기 전 초기화
                client.DefaultRequestHeaders.Clear();

                // TODO : LoginPack 클래스 객체 "LoginPack" string 클래스 json 문자열(content) 구현 (2023.08.21 jbh)
                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/standard/serialization/system-text-json/how-to?pivots=dotnet-8-0
                // json 문자열(하드코딩) 생성 코드 예시 -> (예) string content = "{\"id\":\"dvl001\",\"password\":\"qwer1234!\",\"inputTenantId\":\"inc-001\"}";
                string content = JsonSerializer.Serialize(loginPack);

                var jsonContent = new StringContent(
                                        content,
                                        Encoding.UTF8,
                                        LoginHelper.contentType
                                      );

                HttpResponseMessage response = await client.PostAsync(LoginHelper.auth_server_url, jsonContent);

                string json = await response.Content.ReadAsStringAsync();

                LoginHelper.Login_Access_Token token = JsonSerializer.Deserialize<LoginHelper.Login_Access_Token>(json);

                // TODO : 테스트 코드 - 강제로 오류 발생하도록 Exception 생성 하도록 구현 (필요시 사용)  (2023.10.19 jbh)
                // 참고 URL - https://morm.tistory.com/187
                // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions
                // throw new Exception();

                Logger.Information(currentMethod, "로그인 토큰 성공");

                // [테스트 완료] TODO : 로그 레벨(Debug, Verbose, Warning, Error, Fatal) 기록 테스트 진행 (2023.10.11 jbh)
                //Logger.Debug(currentMethod, "테스트 Debug 로그인 토큰");
                //Logger.Verbose(currentMethod, "테스트 Verbose 로그인 토큰");
                //Logger.Warning(currentMethod, "테스트 Warning 로그인 토큰");
                //Logger.Error(currentMethod, "테스트 Error 로그인 토큰");
                //Logger.Fatal(currentMethod, "테스트 Fatal 로그인 토큰");

                return token;
            }
            catch (Exception e)
            {
                // TODO : Http 통신 Post 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.08.30 jbh)
                // Log.Information(LogHelper.GetMethodPath(currentMethod) + e.Message);
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }

        }

        #endregion PostUserLoginAsync

        //public static async Task<Login_Access_Token> PostUserLoginAsync(string LoginID, string PassWord)
        //{
        // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
        // 참고 URL - https://slaner.tistory.com/73
        // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
        // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
        // var currentMethod = MethodBase.GetCurrentMethod();
        //    try
        //    {
        //        // HttpClient 사용 (POST 방식)
        //        HttpClient client = new HttpClient();


        //        //var values = new Dictionary<string, string>
        //        //{
        //        //    {"id", "dvl001"},
        //        //    {"password", "qwer1234!"},
        //        //    {"inputTenantId", "inc-001"}
        //        //};

        //        LoginPack LoginPack = new LoginPack {
        //            id = LoginID,
        //            password = PassWord,
        //            inputTenantId = "inc-001",
        //            auth_server_url = "http://bim.211.43.204.141.nip.io:31380/api/account/user/signin"
        //        };


        //        Login_Access_Token token = await httpManager.PostLoginConnect(client, LoginPack);
        //        Console.WriteLine("Access Token = " + token.resultData);

        //        //if (token.resultMessage == null)
        //        //{
        //        //    Console.WriteLine("\nLogin is OK...");

        //        //    // MessageBox.Show("Login is OK...");
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("Login is NOT OK...");
        //        //    Console.WriteLine("Error = " + token.resultMessage);
        //        //}
        //        return token;

        //        // else return null;

        //        // string url = "http://bim.service.backend.me.10.1.59.141.nip.io:31380/api/account/user/signin";
        //        // C# HttpClient Header "ContentType" 설정 방법 
        //        // 참고 URL - https://heyoonow.tistory.com/86
        //        //var data = new FormUrlEncodedContent(values);
        //        //data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        //var response = await client.PostAsync(url, data);
        //        //return response;


        //        //HttpClient client = new HttpClient();   

        //        // return response;

        //        //var JsonString = ""; // http 통신 (HttpClient - Connect)

        //        //var options = new JsonSerializerOptions
        //        //{
        //        //    PropertyNameCaseInsensitive = true,                          // case-insensitive 설정
        //        //    ReadCommentHandling = JsonCommentHandling.Skip,              // comments 허용
        //        //    AllowTrailingCommas = true,                                  // trailing commas 허용
        //        //    IncludeFields = true,                                        // fields도 serialize, deserialize 포함
        //        //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // null values 무시 설정 
        //        //};

        //        //var LoginUser = JsonSerializer.Deserialize<LoginPack>(JsonString, options);
        //        //string JsonLoginUser = JsonSerializer.Serialize(LoginUser);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(currentMethod, Logger.errorMessage + e.Message);
        //        throw;
        //    }

        //}

        #region sample

        #endregion sample
    }
}
