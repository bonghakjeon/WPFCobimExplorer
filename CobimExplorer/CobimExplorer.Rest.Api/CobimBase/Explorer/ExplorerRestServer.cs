using Serilog;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Reflection;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using CobimExplorer.Rest.Api.CobimBase.User;
using CobimUtil;
using System.Diagnostics;

namespace CobimExplorer.Rest.Api.CobimBase.Explorer
{
    public class ExplorerRestServer
    {

        #region GetProjectTeamListAsync

        // TODO : 프로젝트 + 팀목록 조회 Get 방식 구현 (2023.08.29 jbh)
        // 참고 URL - https://thebook.io/006890/1017/
        /// <summary>
        /// 프로젝트 목록 조회 (Get)
        /// Rest API 메서드 파라미터 "HttpClient client, string authorization"
        /// </summary>
        // TODO : 메서드 "GetExplorerProjectTeamListAsync" 리턴 자료형 Task<Dictionary<string, object>>으로 구현 예정 (2023.08.30 jbh)
        // public static async Task<string> GetExplorerProjectTeamListAsync(HttpClient client, string authorization)
        public static async Task<List<Dictionary<string, object>>> GetProjectTeamListAsync(HttpClient client, string authorization)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.08.30 jbh)

                // HttpClient 클래스 객체 "client"의 요청헤더(DefaultRequestHeaders)에 데이터 추가 하기 전 초기화
                client.DefaultRequestHeaders.Clear();

                // 요청 헤더 (메서드 파라미터 "로그인 토큰 값 - authorization") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                client.DefaultRequestHeaders.TryAddWithoutValidation(LoginHelper.authorization, authorization);

                HttpResponseMessage response = await client.GetAsync(ProjectHelper.requestUrl);

                string json = await response.Content.ReadAsStringAsync();

                /// <summary>
                /// TODO : JSON 데이터(json) -> Dictionary<string, object> 변환(Convert) 구현 및 var jtmp에 데이터 할당 (2023.08.30 jbh)
                /// (할당 받은 데이터는 조사식 -> 키워드 "jtmp" 입력 및 엔터 -> ResultView 에서 확인 가능 - var 자료형 이여서 IEnumerable<JsonNode> 형태이기 때문) (2023.08.30 jbh)
                /// 참고 URL - https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
                /// using System.Text.Json
                /// 참고 URL - https://www.csharpstudy.com/Data/Json-SystemTextJson.aspx
                /// 참고 2 URL - https://dhddl.tistory.com/250
                /// JsonNode 클래스 - 변경 가능한 JSON 문서 내의 단일 노드를 나타내는 기본 클래스를 의미
                /// 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.text.json.nodes.jsonnode?view=net-7.0
                /// JSON 이란? - 데이터를 표현하는 문자열 
                /// 파싱이란? - 다른 형식으로 저장된 데이터를 원하는 형식의 데이터로 변환하는 것을 말한다.
                /// JSON 파싱 - JSON 형식의 문자열을 객체로 변환하는 것
                /// 참고 URL - https://velog.io/@lzhxxn/JSON-Parsing%ED%8C%8C%EC%8B%B1%EC%9D%B4%EB%9E%80
                /// </summary>


                // TODO : JSON 데이터(json) -> Dictionary<string, object> 변환(Convert) 구현 및 var jtmp에 데이터 할당 (2023.08.30 jbh)
                //        할당 받은 데이터 var jtmp는 조사식 -> 키워드 "jtmp" 입력 및 엔터(또는 해당 var jdic 클릭 및 조사식으로 드래그) -> Results View 에서 확인 가능 - var 자료형 이여서 IEnumerable<JsonNode> 형태이기 때문 (2023.08.30 jbh)
                // TODO : JSON 데이터(json) 를 JsonObject.Parse(json) 사용해서 "resultData" 항목만 파싱(Json Object(객체) 변환) -> 파싱한 Json Object(객체)안에 존재하는 데이터들을 JsonArray (JSON 배열) 변환
                //        -> 해당 JsonArray (JSON 배열)에서 항목(Object) "projectEntity" 추출 -> 추출한 JsonArray 데이터를 var jtmp로 할당 하도록 구현 (2023.08.30 jbh)
                // var jtmp = (JsonObject.Parse(json)[ProjectHelper.resultData] as JsonArray).Select(x => x[ProjectHelper.projectEntity]);

                // TODO : 테스트 JSON 데이터(json) 구조 변경으로 인하여 "projectWithTeamInfos" 영역 안 데이터 "projectEntity", "teamEntity" 추출 구현 (2023.09.12 jbh)
                var jtmp = (JsonObject.Parse(json)[ProjectHelper.projectWithTeamInfos] as JsonArray).Select(x => x);

                // JsonNode -> json 데이터 형변환 구현 (2023.08.31 jbh)
                // string Testjson = JsonSerializer.Serialize(jtmp);

                // TODO : jtmp -> Select 메서드 사용 -> Dictionary<string, object>>(x) 형태로 Deserialize -> Deserialize해서 나온 Dictionary 객체를 List로 변환 및 해당 List에 속한 데이터를 var jdic에 할당 (2023.08.31 jbh) 
                //        할당 받은 데이터 var jdic는 조사식 -> 키워드 "jdic" 입력 및 엔터(또는 해당 var jdic 클릭 및 조사식으로 드래그) 확인 가능
                //var jdic = jtmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();

                //return jdic;

                var projectTeamList = jtmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();

                return projectTeamList;

                // TODO : 테스트 코드 필요시 사용 예정 (List<Dictionary<string, object>> 객체 jdic -> json 데이터 형변환 구현) (2023.08.31 jbh)
                // 참고 URL - https://stackoverflow.com/questions/9110724/serializing-a-list-to-json
                // string jsonDic = JsonSerializer.Serialize(jdic);

                //var pNameList = jtmp.Select(x => x["projectName"].ToString()).ToList();

                // TODO : 테스트 코드 필요시 사용 예정 (2023.09.04 jbh)
                // var values = JsonSerializer.Deserialize<Dictionary<string, object>>(json)[ProjectHelper.resultData].ToString();

                // TODO : 테스트 코드 필요시 사용 예정 (2023.09.04 jbh)
                // var temp = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(values);

                //var a = temp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x["projectEntity"].ToString()))
                //    .Select(x => x["projectName"].ToString()).ToList();
                //    ;

                // TODO : 테스트 코드 필요시 사용 예정 (JSON 데이터 중 "projectEntity" 항목 List 데이터를 var a에 할당) (2023.08.30 jbh)
                // var a = temp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x["projectEntity"].ToString())).ToList();

                //var temp2 = JsonSerializer.Deserialize<Dictionary<string, object>>(temp[0].ToString())["projectName"];

                //return values;
                //return jdic;

                // TODO : using System.Text.Json 사용 -> json 데이터 -> Dictionary<string, object> 로 변환(Convert, 파싱 같은 의미) 구현 예정 (2023.08.30 jbh)
                // 테스트용 JSON 데이터 양식 
                // Dictionary 영역 
                //              {
                //                  // List "resultData" 영역 
                //                  "resultData": [
                //                    {
                //                      // Dictionary "projectEntity" 영역 
                //                      "projectEntity": {
                //                          "useYn": "Y",
                //      "createIp": "127.0.0.1",
                //      "updateIp": null,
                //      "createDateTime": "2023-07-25T10:44:30.945+00:00",
                //      "updateDateTime": null,
                //      "createUserId": "horimu",
                //      "updateUserId": null,
                //      "tenantId": "inc-001",
                //      "projectId": "jc-constrct",
                //      "projectName": "제천 나들목 건설",
                //      "content": "제천 나들목 건설",
                //      "beginYmd": "20230725",
                //      "endYmd": "20230725",
                //      "projectFieldCode": "제천 나들목 건설",
                //      "orderingAgency": "제천 나들목 건설",
                //      "mainBusinessOperator": "제천 나들목 건설",
                //      "projectAddress": "제천 나들목 건설",
                //      "projectState": "sttsuse",
                //      "uniqueKey": "16889490cf064a5eb08b711dca5bf9e3",
                //      "projectCode": "exprss"
                //                      },
                // // Dictionary "teamEntity" 영역 
                //    "teamEntity": {
                //                          "useYn": "Y",
                //      "createIp": "127.0.0.1",
                //      "updateIp": null,
                //      "createDateTime": "2023-07-25T10:44:45.973+00:00",
                //      "updateDateTime": null,
                //      "createUserId": "horimu",
                //      "updateUserId": null,
                //      "tenantId": "inc-001",
                //      "projectId": "jc-constrct",
                //      "teamId": "inc-001-jc-constrct-t001",
                //      "teamName": "현장건설팀",
                //      "printTeamId": "t001",
                //      "teamUserList": [...]
                //    }
                //                  }
                //],
                //"resultCount": 0,
                //"resultMessage": null,
                //"tenantId": "inc-001",
                //"jwt": "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJzaWduIiwidGVhbXMiOiJpbmMtMDAxLWpjLWNvbnN0cmN0LXQwMDEsaW5jLTAwMS1wcm9qZWN0LTA5LTAxLWFhLXQwMDIiLCJyb2xlcyI6InJvbGVfMDAxLHJvbGVfMDAwIiwidGVuYW50SWQiOiJpbmMtMDAxIiwicHJvamVjdCI6ImpjLWNvbnN0cmN0LGthZmFrLHByb2plY3QtMDktMDEtYWEscHN5IiwidXNlck5hbWUiOiLqsJzrsJzsnpAtMDAxIiwiZXhwIjoxNjkxNzI2MDM2LCJ1c2VySWQiOiJkdmwwMDEifQ.BSPtl6i2tEq5c44lbqxuegFH1SaId3NM3OrPGgc7DtI",
                //"refreshToken": null,
                //"param": null,
                //"payload": null,
                //"actionUserInfo": null,
                //"searchType": "",
                //"searchTxt": "",
                //"projectId": null,
                //"teamId": null,
                //"pageNo": 0,
                //"rowSize": 0,
                //"userId": null,
                //"projectIds": null
                //              }


            }
            catch (Exception e)
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.08.30 jbh)
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion GetProjectTeamListAsync

        #region GetExplorerTeamUserListAsync

        // TODO : 팀에 소속된 사용자 리스트 조회 Get 방식 구현 예정 (2023.09.12 jbh)
        // 참고 URL - https://thebook.io/006890/1017/
        /// <summary>
        /// 팀에 소속된 사용자 리스트 조회 (Get)
        /// Rest API 메서드 파라미터 "HttpClient client, string projectId, string teamId, string authorization"
        /// </summary>
        // TODO : 메서드 "GetExplorerTeamUserListAsync" 리턴 자료형 Task<Dictionary<string, object>>으로 구현 예정 (2023.09.12 jbh)
        //public static async Task<List<Dictionary<string, object>>> GetExplorerTeamUserListAsync(HttpClient client, ProjectHelper.ProjectPack projectPack, string authorization)
        //{
        // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
        // 참고 URL - https://slaner.tistory.com/73

        // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
        // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
        // var currentMethod = MethodBase.GetCurrentMethod();

        //    try
        //    {
        //        var teamUserList = ;
        //        return teamUserList;
        //    }
        //    catch (Exception e)
        //    {
        //        // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.12 jbh)
        //        Logger.Error(currentMethod, Logger.errorMessage + e.Message);
        //        throw;
        //    }

        //}

        #endregion GetExplorerTeamUserListAsync

        #region GetFolderListAsync

        // TODO : 폴더 목록 리스트 조회 Get 방식 구현 (2023.09.01 jbh)
        // 참고 URL - https://thebook.io/006890/1017/
        /// <summary>
        /// 폴더 목록 리스트 조회 (Get)
        /// Rest API 메서드 파라미터 "HttpClient client, string projectId, string teamId, string authorization"
        /// </summary>
        // TODO : 메서드 "GetExplorerFolderListAsync" 리턴 자료형 Task<Dictionary<string, object>>으로 구현 예정 (2023.08.30 jbh)
        // public static async Task<string> GetExplorerProjectListAsync(HttpClient client, string authorization)
        public static async Task<List<Dictionary<string, object>>> GetFolderListAsync(HttpClient client, ProjectHelper.ProjectPack projectPack, string authorization)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.04 jbh)

                // HttpClient 클래스 객체 "client"의 요청헤더(DefaultRequestHeaders)에 데이터 추가 하기 전 초기화
                client.DefaultRequestHeaders.Clear();

                // 요청 헤더 (메서드 파라미터 "로그인 토큰 값 - authorization") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                client.DefaultRequestHeaders.TryAddWithoutValidation(LoginHelper.authorization, authorization);

                // TODO : 서버에 Http 통신(Get) 테스트 진행시 아래 URL 주소에 테스트 프로젝트 아이디 "jc-constrct", 팀아이디를 "inc-001-jc-constrct-t001" 메서드 파라미터로 넘겨 받아서 
                // Http Get 방식으로 호출해서 각각의 프로젝트 및 팀 아이디 하위의 폴더 + 파일 목록 JSON 데이터 가져오기 (2023.09.06 jbh)
                // 테스트용 URL 주소 - requestFolderUrl = "http://bim.211.43.204.141.nip.io:31380/api/folder/list/jc-constrct/inc-001-jc-constrct-t001";
                string requestFolderUrl = FolderHelper.requestFolderUrl + projectPack.projectId + "/" + projectPack.teamId;

                // string requestTestFolderUrl = ProjectHelper.requestFolderUrl + "project-09-01-aa" + "/" + "inc-001-project-09-01-aa-t002";

                HttpResponseMessage response = await client.GetAsync(requestFolderUrl);

                // HttpResponseMessage response = await client.GetAsync(requestTestFolderUrl);

                // 테스트용 Http 통신 (Get) 방식 코드 필요시 사용 예정 (2023.09.06 jbh)
                // HttpResponseMessage response = await client.GetAsync(ProjectHelper.requestFolderUrl);

                string json = await response.Content.ReadAsStringAsync();

                // TODO : 테스트 코드 필요시 사용 예정 (2023.09.04 jbh)
                // var values = JsonSerializer.Deserialize<Dictionary<string, object>>(json)[ProjectHelper.resultData].ToString();

                // TODO : JSON 데이터(json) -> Dictionary<string, object> 변환(Convert) 구현 및 var jtmp에 데이터 할당 (2023.08.30 jbh)
                //        할당 받은 데이터 var jtmp는 조사식 -> 키워드 "jtmp" 입력 및 엔터(또는 해당 var jdic 클릭 및 조사식으로 드래그) -> Results View 에서 확인 가능 - var 자료형 이여서 IEnumerable<JsonNode> 형태이기 때문 (2023.09.04 jbh)
                // TODO : JSON 데이터(json) 를 JsonObject.Parse(json) 사용해서 "resultData" 항목만 파싱(Json Object(객체) 변환) -> 파싱한 Json Object(객체)안에 존재하는 데이터들을 JsonArray (JSON 배열) 변환
                var jtmp = (JsonObject.Parse(json)[ProjectHelper.resultData] as JsonArray).Select(x => x);

                // var jtmp = (JsonObject.Parse(json) as JsonArray).Select(x => x);

                // TODO : jtmp -> Select 메서드 사용 -> Dictionary<string, object>>(x) 형태로 Deserialize -> Deserialize해서 나온 Dictionary 객체를 List로 변환 및 해당 List에 속한 데이터를 var jsonList에 할당 (2023.08.31 jbh) 
                //        할당 받은 데이터 var jsonList 조사식 -> 키워드 "jsonList" 입력 및 엔터(또는 해당 var jsonList 클릭 및 조사식으로 드래그) 확인 가능
                //var jsonList = jtmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();

                //return jsonList;

                // TODO : 폴더 리스트에 속한 폴더 데이터 Dictionary 객체의 Key "levelNo"의 값(Value)을 기준으로 내림차순(OrderByDescending) 정렬하여 리스트 구현 (2023.09.12 jbh)
                // 참고 URL - https://codechacha.com/ko/csharp-sort-list/
                // 참고 2 URL - https://developer-talk.tistory.com/694
                // 참고 3 URL - https://developer-talk.tistory.com/210
                var folderList = jtmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).OrderBy(x => Int32.Parse(x[FolderHelper.levelNo].ToString())).ToList();

                return folderList;
            }
            catch (Exception e)
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.04 jbh)
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion GetFolderListAsync

        #region GetFileListAsync

        // TODO : 파일 목록 리스트 조회 Get 방식 구현 예정 (2023.09.12 jbh)
        // 참고 URL - https://thebook.io/006890/1017/
        /// <summary>
        /// 파일 목록 리스트 조회 (Get)
        /// Rest API 메서드 파라미터 "HttpClient client, string projectId, string teamId, string authorization"
        /// </summary>
        // TODO : 메서드 "GetExplorerFileListAsync" 리턴 자료형 Task<Dictionary<string, object>>으로 구현 예정 (2023.08.30 jbh)
        // public static async Task<string> GetExplorerProjectListAsync(HttpClient client, string authorization)
        public static async Task<List<Dictionary<string, object>>> GetFileListAsync(HttpClient client, ProjectHelper.ProjectPack projectPack, string folderId, string authorization)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)

                // HttpClient 클래스 객체 "client"의 요청헤더(DefaultRequestHeaders)에 데이터 추가 하기 전 초기화
                client.DefaultRequestHeaders.Clear();

                // 요청 헤더 (메서드 파라미터 "로그인 토큰 값 - authorization") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                client.DefaultRequestHeaders.TryAddWithoutValidation(LoginHelper.authorization, authorization);

                // TODO : 서버에 Http 통신(Get) 테스트 진행시 아래 URL 주소에 테스트 프로젝트 아이디 "jc-constrct", 팀아이디를 "inc-001-jc-constrct-t001" 메서드 파라미터로 넘겨 받아서 
                // Http Get 방식으로 호출해서 각각의 프로젝트 및 팀 아이디 하위의 폴더 + 파일 목록 JSON 데이터 가져오기 (2023.09.06 jbh)
                // 테스트용 URL 주소 - requestFolderUrl = "http://bim.211.43.204.141.nip.io:31380/api/folder/list/jc-constrct/inc-001-jc-constrct-t001";
                // string requestTestFolderUrl = ProjectHelper.requestFolderUrl + "project-09-01-aa" + "/" + "inc-001-project-09-01-aa-t002";
                string requestFileUrl = FileHelper.requestFileUrl + projectPack.projectId + "/" + projectPack.teamId + "/" + folderId;

                HttpResponseMessage response = await client.GetAsync(requestFileUrl);

                string json = await response.Content.ReadAsStringAsync();

                var jtmp = (JsonObject.Parse(json)[FileHelper.containerList] as JsonArray).Select(x => x);

                var fileList = jtmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();

                return fileList;
            }
            catch (Exception e)
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion GetFileListAsync

        #region GetContainerDetailListAsync

        /// <summary>
        /// 컨테이너 상세 조회 (Get) - 컨테이너 정보, 파일정보 + 메타데이터 정보 + 마일스톤 정보
        /// Rest API 메서드 파라미터 "HttpClient client, string projectId, string teamId, string containerId, string authorization, string inputTenantId"
        /// </summary>
        public static async Task<List<Dictionary<string, List<Dictionary<string, object>>>>> GetContainerDetailListAsync(HttpClient client, ContainerHelper.ContainerPack containerPack, string authorization, string inputTenantId)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

            // 컨테이너 정보, 파일정보 + 메타데이터 정보 + 마일스톤 정보를 같이 담을 2차원 리스트 객체 "total_Info_List" 생성
            List<Dictionary<string, List<Dictionary<string, object>>>> total_Info_List = new List<Dictionary<string, List<Dictionary<string, object>>>>();

            // 연관된 부모 컨테이너 정보 리스트
            List<Dictionary<string, object>> linkedNodeInfoList = new List<Dictionary<string, object>>();

            // 연관된 부모 컨테이너 정보 Dictionary 객체 "linkedInfoDic" 생성
            var linkedNodeInfoDic = new Dictionary<string, List<Dictionary<string, object>>>();

            // 컨테이너 정보, 파일정보 리스트 객체 "containerDetailList" 생성
            List<Dictionary<string, object>> containerDetailList = new List<Dictionary<string, object>>();

            // 컨테이너 정보, 파일정보 Dictionary 객체 "containerDetailDic" 생성
            var containerDetailDic = new Dictionary<string, List<Dictionary<string, object>>>();


            // 메타데이터 정보 리스트 객체 "metaDataList" 생성
            List<Dictionary<string, object>> metaDataList = new List<Dictionary<string, object>>();

            // 메타데이터 정보 Dictionary 객체 "metaDataDic" 생성
            var metaDataDic = new Dictionary<string, List<Dictionary<string, object>>>();


            // 마일스톤 업무 리스트 객체 "taskDetailList" 생성
            List<Dictionary<string, object>> taskDetailList = new List<Dictionary<string, object>>();

            // 마일스톤 업무 Dictionary 객체 "taskDetailDic" 생성
            var taskDetailDic = new Dictionary<string, List<Dictionary<string, object>>>();

            try
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)

                // HttpClient 클래스 객체 "client"의 요청헤더(DefaultRequestHeaders)에 데이터 추가 하기 전 초기화
                client.DefaultRequestHeaders.Clear();

                // 요청 헤더 (메서드 파라미터 "로그인 토큰 값 - authorization") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                // TODO : 로그인 JWT 토큰 값 들어있는 string 클래스 객체 "authorization"를 bearer 토큰으로 변환하기 위해 문자열 "bearer " 추가해서
                // HttpClient 요청 헤더 client.DefaultRequestHeaders에 추가(TryAddWithoutValidation) (2023.10.12 jbh)
                // 추후 인재 INC와 소통 진행 -> JWT 토큰 또는 bearer 토큰 둘 중에 하나로 통일 예정 
                // 최종 결과 - client.DefaultRequestHeaders.Clear(); 사용하면 bearer 토큰 쓸 필요 없이 JWT 토큰만으로 사용 가능
                client.DefaultRequestHeaders.TryAddWithoutValidation(LoginHelper.authorization, authorization);
                // client.DefaultRequestHeaders.TryAddWithoutValidation(LoginHelper.authorization, authorization);

                // TryAddWithoutValidation 메서드 사용 - 요청 헤더 (메서드 파라미터 "테넌트 아이디 - inputTenantId") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                // client.DefaultRequestHeaders.TryAddWithoutValidation(ContainerHelper.tenant, inputTenantId);
                client.DefaultRequestHeaders.TryAddWithoutValidation(ContainerHelper.tenant, inputTenantId);
                

                // Add 메서드 사용 - 요청 헤더 (메서드 파라미터 "테넌트 아이디 - inputTenantId") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가
                // 참고 URL - https://vmpo.tistory.com/91
                // client.DefaultRequestHeaders.Add(ContainerHelper.tenant, inputTenantId);


                // TODO : 서버에 Http 통신(Get) 진행시 아래 URL 주소(ContainerHelper.requestContainerUrl)에
                // 프로젝트 아이디, 팀 아이디, 컨테이너 아이디를 "containerPack" 메서드 파라미터로 넘겨 받아서 
                // Http Get 방식으로 호출해서 컨테이너 상세 조회 목록 JSON 데이터 가져오기 (2023.09.06 jbh)
                string requestContainerUrl = ContainerHelper.requestContainerUrl + containerPack.projectId + "/" + containerPack.teamId + "/" + containerPack.containerId;

                HttpResponseMessage response = await client.GetAsync(requestContainerUrl);

                // TODO : 인재 INC에서 개발한 서버측에서 전달 받은 오류 메시지 "{"status":"io.jsonwebtoken.MalformedJwtException: JWT strings must contain exactly 2 period characters. Found: 10","message":"JWT strings must contain exactly 2 period characters. Found: 10","error":"io.jsonwebtoken.MalformedJwtException"}"
                //        해당 오류 메시지의 원인은 JWT 토큰을 파싱하는 과정에서 발생한 오류로 확인. 해당 오류 원인 파악 후 로직 수정 예정 (2023.10.12 jbh)
                //        참고 URL - https://cano721.tistory.com/216
                //        참고 2 URL - https://stackoverflow.com/questions/53949137/jwt-strings-must-contain-exactly-2-period-characters-found-0
                //        참고 3 URL - https://velog.io/@jiu_lee/Project-%EB%B9%84%EB%B0%80%EB%B2%88%ED%98%B8-%EC%B0%BE%EA%B8%B0-with-JWT
                string json = await response.Content.ReadAsStringAsync();

                // 연관된 부모 컨테이너 정보 리스트
                var jlinkedNodeInfoTmp = (JsonObject.Parse(json)[ContainerHelper.linkedNodeInfo] as JsonArray).Select(x => x);

                // TODO : jdetailTmp null Exception이 발생하는 원인 파악하기 (2023.10.12 jbh)
                // 컨테이너 정보, 파일정보
                var jdetailNode = (JsonObject.Parse(json)[ContainerHelper.detail]);
                var jdetailTmp = JsonSerializer.Deserialize<Dictionary<string, object>>(jdetailNode);

                // 메타데이터 정보
                var jmetaDataTmp = (JsonObject.Parse(json)[ContainerHelper.metaDataList] as JsonArray).Select(x => x);
                
                // 마일스톤 업무
                var jtaskDetailNode = (JsonObject.Parse(json)[ContainerHelper.taskDetail]);
                var jtaskDetailTmp = JsonSerializer.Deserialize<Dictionary<string, object>>(jtaskDetailNode);

                // TODO : containerDetailList null Exception이 발생하는 원인 파악하기 (2023.10.12 jbh)
                containerDetailList.Add(jdetailTmp);
                linkedNodeInfoList = jlinkedNodeInfoTmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();
                metaDataList = jmetaDataTmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();
                taskDetailList.Add(jtaskDetailTmp);

                linkedNodeInfoDic.Add(ContainerHelper.linkedNodeInfo, linkedNodeInfoList);
                containerDetailDic.Add(ContainerHelper.detail, containerDetailList);
                metaDataDic.Add(ContainerHelper.metaDataList, metaDataList);
                taskDetailDic.Add(ContainerHelper.taskDetail, taskDetailList);

                total_Info_List.Add(linkedNodeInfoDic);       // 연관된 부모 컨테이너 정보 리스트에 추가
                total_Info_List.Add(containerDetailDic);      // 컨테이너 정보, 파일정보 total_Info_List에 추가
                total_Info_List.Add(metaDataDic);             // 메타데이터 정보 total_Info_List에 추가
                total_Info_List.Add(taskDetailDic);           // 마일스톤 정보 total_Info_List에 추가

                return total_Info_List;
            }
            catch (Exception e)
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion GetContainerDetailListAsync

        #region GetTaskDetailListAsync

        // TODO : ComboxBox - 마일스톤의 경우 추후 인재 INC 측으로 마일스톤 RestAPI 요청 후 개발 진행 (2023.10.20 jbh)
        /// <summary>
        /// 마일스톤 목록 리스트 조회 (Get)
        /// </summary>
        // public static async Task<List<Dictionary<string, object>>> GetTaskDetailListAsync(HttpClient client, ProjectHelper.ProjectPack projectPack, string folderId, string authorization)
        public static async Task<object> GetTaskDetailListAsync()
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                object obj = null;

                return obj;

            }
            catch (Exception e)
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion GetTaskDetailListAsync

        #region GetFileDownLoadAsync

        // TODO : Rest API 파일 다운로드 (Get) 메서드 "GetFileDownLoadAsync" 구현 (2023.10.25 jbh)
        // 참고 URL - https://asecurity.dev/entry/NET-Download-file-from-HttpClientWebClient
        /// <summary>
        /// 파일 다운로드 (Get)
        /// </summary>
        public static async Task GetFileDownLoadAsync(HttpClient client, ContainerHelper.ContainerPack containerPack, FileHelper.FilePack filePack, string authorization, string inputTenantId)
        {
            // string fileName = string.Empty;             // 다운로드 받으려고 하는 파일 이름
            string downloadFilePath = string.Empty;     // 다운로드 받으려고 하는 최종 파일 경로 (파일 전체 경로 + 이름 중복일 때 변경된 파일 이름)

            // TODO : C# 문자열 보간 기능 사용해서 로그파일 생성할 상위 폴더 경로 문자열로 변환 (2023.10.11 jbh)
            // 참고 URL - https://gaeunhan.tistory.com/61
            // string dirPath = $"D:\\bhjeon\\COBIM-Explorer\\CobimExplorer\\CobimExplorer\\bin\\x64\\Debug\\Logs\\{projectFileName}";
            // Assembly.GetEntryAssembly().GetName().Name은 프로젝트 파일 "CobimExplorer"를 의미 - C:\\Users\\bhjeon\\Documents
            // string dirPath = $"C:\\Users\\bhjeon\\Documents\\{Assembly.GetEntryAssembly().GetName().Name}\\{containerPack.projectId}\\{containerPack.teamId}\\{containerPack.containerId}";

            // TODO : 다른 PC에서도 파일 다운로드 기능이 실행될 수 있도록 다운로드 받을 파일 전체 경로 "dirPath"의 루트 디렉토리 값을 내문서 폴더 (Environment.SpecialFolder.MyDocuments)로 설정 (2023.10.26 jbh)
            // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.environment.specialfolder?view=net-7.0
            // 파일 다운로드 전체 경로 예시 - "C:\Users\bhjeon\Documents\CobimExplorer\bmcb\inc-001-bmcb-t005\bmcb-imbu-func_016-sptl_901-002" - (SpecialFolder 시스템 디렉토리(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)) 사용)
            string dirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{Assembly.GetEntryAssembly().GetName().Name}\\{containerPack.projectId}\\{containerPack.teamId}\\{containerPack.containerId}";
            string filePath = dirPath + $"\\{filePack.fileName}";

            DirectoryInfo di = new DirectoryInfo(dirPath);
            
            FileInfo fi = new FileInfo(filePath);

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // 디렉터리(di)가 존재하지 않는 경우
                if (!di.Exists) Directory.CreateDirectory(dirPath);   // 디렉터리 새로 생성

                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)

                // HttpClient 클래스 객체 "client"의 요청헤더(DefaultRequestHeaders)에 데이터 추가 하기 전 초기화
                client.DefaultRequestHeaders.Clear();

                // 요청 헤더 (메서드 파라미터 "로그인 토큰 값 - authorization", "테넌드 아이디 - inputTenantId") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                client.DefaultRequestHeaders.TryAddWithoutValidation(LoginHelper.authorization, authorization);
                client.DefaultRequestHeaders.TryAddWithoutValidation(ContainerHelper.tenant, inputTenantId);

                // TODO : 서버에 Http 통신(Get) 테스트 진행시 아래 URL 주소에 테스트 프로젝트 아이디 "jc-constrct", 팀아이디를 "inc-001-jc-constrct-t001" 메서드 파라미터로 넘겨 받아서 
                // Http Get 방식으로 호출해서 파일 다운로드 테스트 진행 (2023.10.25 jbh)
                // 테스트용 URL 주소 - requestFileDownLoadUrl = "http://bim.211.43.204.141.nip.io:31380/api/folder/file/container/download//jc-constrct/inc-001-jc-constrct-t001";
                string requestFileDownLoadUrl = FileHelper.requestFileDownLoadUrl + containerPack.projectId + "/" + containerPack.teamId + "/" + containerPack.containerId + "/" + filePack.fileId;

                Debug.WriteLine($"{requestFileDownLoadUrl}");

                HttpResponseMessage response = await client.GetAsync(requestFileDownLoadUrl);


                // 새로 다운로드 받으려는 파일 전체 경로 - dirPath + 파일이름 - fi.Name 구하기
                downloadFilePath = FileHelper.FileDownLoadPath(dirPath, fi.Name);

                var ms = await response.Content.ReadAsStreamAsync();

                // TODO : 해당 using 문장을 벗어나면 FileStream 클래스 객체 fs Dipose 처리 (리소스 해제 및 자원 반납) 구현 (2023.10.25 jbh)
                //        아래 소스코드를 using문으로 구현 안하고 실행시 파일을 생성되나 FileStream 클래스 객체 fs가 close 처리가 자동으로 되지 않아서 
                //        다운로드 받은 이미지 파일(.jpg, .png 등등...)을 열어도 손상된 상태라고 하며 이미지를 볼 수 없으니 주의해야 한다.
                // 참고 URL - https://hongjinhyeon.tistory.com/92
                // TODO : httpClient 객체 client 사용 URL을 이용한 파일 다운로드 구현 (2023.10.26 jbh)
                // 참고 URL - https://asecurity.dev/entry/NET-Download-file-from-HttpClientWebClient
                // 참고 2 URL - https://stackoverflow.com/questions/45711428/download-file-with-webclient-or-httpclient
                using (var fs = File.Create(downloadFilePath))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    await ms.CopyToAsync(fs);
                    await ms.FlushAsync();
                    await fs.FlushAsync();

                    // fs.Close();
                }


                // TODO : 아래 테스트 코드 필요시 참고 (2023.10.25 jbh)
                // 이미지 파일(fi)이 존재하지 않는 경우 
                //if (!fi.Exists)
                //{
                //    // TODO : 아래 테스트 코드 2줄은 필요시 참고 (2023.10.25 jbh)
                //    //        단, 아래 테스트 코드 2줄 사용은 가능하나 다운로드 받는 파일의 용량을 크게 다운 받으므로 메모리 부족이 발생할 수 있다.
                //    //var cotent = await response.Content.ReadAsByteArrayAsync();
                //    //File.WriteAllBytes(fi.FullName, cotent);


                //    var ms = await response.Content.ReadAsStreamAsync();

                //    // TODO : 해당 using 문장을 벗어나면 FileStream 클래스 객체 fs Dipose 처리 (리소스 해제 및 자원 반납) 구현 (2023.10.25 jbh)
                //    //        아래 소스코드를 using문으로 구현 안하고 실행시 파일을 생성되나 FileStream 클래스 객체 fs가 close 처리가 자동으로 되지 않아서 
                //    //        다운로드 받은 이미지 파일(.jpg, .png 등등...)을 열어도 손상된 상태라고 하며 이미지를 볼 수 없으니 주의해야 한다.
                //    // 참고 URL - https://hongjinhyeon.tistory.com/92
                //    using (var fs = File.Create(fi.FullName))
                //    {
                //        ms.Seek(0, SeekOrigin.Begin);
                //        await ms.CopyToAsync(fs);
                //        await ms.FlushAsync();
                //        await fs.FlushAsync();

                //        // fs.Close();
                //    }
                //}

                //// 이미지 파일(fi)이 존재하는 경우 
                //// 다운로드 받을 파일명이 중복일 때, 기존 파일명 + N 으로 생성하도록 구현 (2023.10.25 jbh)
                //// 참고 URL - https://pseudo-code.tistory.com/166
                //else if (fi.Exists 
                //         && (fi.FullName.Equals(filePack.fileName))
                //         && filePack.fileName.Length > 0)
                //{
                //    var ms = await response.Content.ReadAsStreamAsync();

                //    using ()
                //    {

                //    }
                //}

                return;
            }
            catch (Exception e)
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.09.20 jbh)
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
            // return;
        }

        #endregion GetFileDownLoadAsync

        #region sample

        #endregion sample

    }
}
