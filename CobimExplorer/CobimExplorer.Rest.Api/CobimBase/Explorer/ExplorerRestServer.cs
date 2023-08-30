using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace CobimExplorer.Rest.Api.CobimBase.Explorer
{
    public class ExplorerRestServer
    {
        // TODO : 프로젝트 목록 조회 Get 방식 구현 (2023.08.29 jbh)
        // 참고 URL - https://thebook.io/006890/1017/
        /// <summary>
        /// 프로젝트 목록 조회 (Get)
        /// Rest API 메서드 파라미터 "HttpClient client, string authorization"
        /// </summary>
        // TODO : 메서드 "GetExplorerProjectListAsync" 리턴 자료형 Task<Dictionary<string, object>>으로 구현 예정 (2023.08.30 jbh)
        // public static async Task<string> GetExplorerProjectListAsync(HttpClient client, string authorization)
        public static async Task<List<Dictionary<string, object>>> GetExplorerProjectListAsync(HttpClient client, string authorization)
        {
            try
            {
                // TODO : Http 통신 Get 방식 작업 시 서버로 부터 결과값 리턴되지 않고 시간이 길어지면 Time Out 처리 되면서 오류 메시지 출력 기능 구현 예정 (2023.08.30 jbh)

                // 요청 헤더 (메서드 파라미터 "로그인 토큰 값 - authorization") 메서드 파라미터 HttpClient 클래스 객체 "client"에 추가 
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);

                HttpResponseMessage response = await client.GetAsync(ProjectHelper.requestUrl);

                string json = await response.Content.ReadAsStringAsync();

                //return json;

                // Dictionary<string, object> ProjectList = new Dictionary<string, object>();

                // TODO : JSON 데이터(json) -> Dictionary<string, object> 변환(Convert) 구현 및 var jtmp에 데이터 할당 (2023.08.30 jbh)
                // (할당 받은 데이터는 조사식 -> 키워드 "jtmp" 입력 및 엔터 -> ResultView 에서 확인 가능 - var 자료형 이여서 IEnumerable<JsonNode> 형태이기 때문) (2023.08.30 jbh)
                // 참고 URL - https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
                // using System.Text.Json
                // 참고 URL - https://www.csharpstudy.com/Data/Json-SystemTextJson.aspx
                // 참고 2 URL - https://dhddl.tistory.com/250
                // JsonNode 클래스 - 변경 가능한 JSON 문서 내의 단일 노드를 나타내는 기본 클래스를 의미
                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.text.json.nodes.jsonnode?view=net-7.0
                // TODO : JSON 데이터(json) "resultData" 항목만 파싱 -> JsonArray 변환 -> 해당 JsonArray에서 항목 "projectEntity" 추출 구현 (2023.08.30 jbh)
                var jtmp = (JsonObject.Parse(json)["resultData"] as JsonArray).Select(x => x["projectEntity"]);

                // TODO : jtmp -> Select 메서드 사용 -> Dictionary<string, object>>(x) 형태로 Deserialize -> 마지막으로 List로 변환 
                var jdic = jtmp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x)).ToList();

                //var pNameList = jtmp.Select(x => x["projectName"].ToString()).ToList();

                 

                var values = JsonSerializer.Deserialize<Dictionary<string, object>>(json)["resultData"].ToString();

                var temp = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(values);

                //var a = temp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x["projectEntity"].ToString()))
                //    .Select(x => x["projectName"].ToString()).ToList();
                //    ;

                // TODO : 테스트 코드 JSON 데이터 중 "projectEntity" 항목 List 데이터를 var a에 할당 (2023.08.30 jbh)
                var a = temp.Select(x => JsonSerializer.Deserialize<Dictionary<string, object>>(x["projectEntity"].ToString())).ToList();

                //var temp2 = JsonSerializer.Deserialize<Dictionary<string, object>>(temp[0].ToString())["projectName"];

                //return values;
                return jdic;

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
                Log.Logger.Information(e.Message);
                throw;
            }
        }
    }
}
