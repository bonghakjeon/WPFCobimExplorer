using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobimExplorer.Rest.Api.CobimBase.Explorer
{
    public class ProjectHelper
    {
        /// <summary>
        /// 4. 폴더 목록 조회 
        /// 요청 파라미터 - 프로젝트 아이디
        /// </summary>
        //public const string testProjectId = "jc-constrct";

        /// <summary>
        /// 4. 폴더 목록 조회 
        /// 요청 파라미터 - 팀 아이디
        /// </summary>
        //public const string testTeamId = "inc-001-jc-constrct-t001";

        /// <summary>
        /// 4. 폴더 목록 조회 
        /// 요청 URL
        /// </summary>
        public const string requestUrl = "http://bim.211.43.204.141.nip.io:31380/api/project/my/list";
    }
}
