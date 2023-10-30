using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobimExplorer.Rest.Api.CobimBase.Explorer
{
    public class ContainerHelper
    {
        #region 프로퍼티

        /// <summary>
        /// 9. 컨테이너 상세 조회 API
        /// </summary>
        public const string requestContainerUrl = "http://bim.211.43.204.141.nip.io:31380/api/container/detail/";

        /// <summary>
        /// 요청 데이터 - 프로젝트 아이디
        /// </summary>
        public const string projectId = "projectId";

        /// <summary>
        /// 요청 데이터 - 팀 아이디
        /// </summary>
        public const string teamId = "teamId";

        /// <summary>
        /// 요청 헤더 - 테넌트 아이디
        /// </summary>
        public const string tenant = "Tenant";

        /// <summary>
        /// 컨테이너 아이디 - (요청 데이터 공통 사용)
        /// </summary>
        public const string containerId = "containerId";

        /// <summary>
        /// 연관된 부모 컨테이너 정보 
        /// </summary>
        public const string linkedNodeInfo = "linkedNodeInfo";

        /// <summary>
        /// 컨테이너 정보, 파일정보
        /// </summary>
        public const string detail = "detail";

        /// <summary>
        /// 메타데이터 정보
        /// </summary>
        public const string metaDataList = "metaDataList";

        /// <summary>
        /// 마일스톤 업무
        /// </summary>
        public const string taskDetail = "taskDetail";

        /// <summary>
        /// 특정 마일스톤 업무 정보
        /// </summary>
        public const string milestoneTaskInfo = "milestoneTaskInfo";

        /// <summary>
        /// 마일스톤 - 업무 인덱스
        /// </summary>
        public const string idx = "idx";

        /// <summary>
        /// 작성자
        /// </summary>
        public const string createUserId = "createUserId";

        /// <summary>
        /// 버전
        /// </summary>
        public const string revsnVal = "revsnVal";

        /// <summary>
        /// 제목
        /// </summary>
        public const string title = "title";

        /// <summary>
        /// 파일유형 Dictionary
        /// </summary>
        public const string fileTypeCodeInfo = "fileTypeCodeInfo";

        /// <summary>
        /// 파일유형 systemCode
        /// </summary>
        public const string systemCode = "systemCode";

        /// <summary>
        /// 파일유형 codeName
        /// </summary>
        public const string codeName = "codeName";

        /// <summary>
        /// 메타데이터 - 소프트웨어
        /// </summary>
        public const string soft_ifo = "soft-ifo";

        /// <summary>
        /// 메타데이터 - 소프트웨어 metaName
        /// </summary>
        public const string soft_ifo_metaName = "소프트웨어";


        /// <summary>
        /// 메타데이터 - 소프트웨어버전
        /// </summary>
        public const string soft_ver = "soft-ver";

        /// <summary>
        /// 메타데이터 - 키워드/태그
        /// </summary>
        public const string find_tag = "find-tag";

        /// <summary>
        /// 메타 데이터 - metaCode
        /// </summary>
        public const string metaCode = "metaCode";

        /// <summary>
        /// 메타 데이터 - metaComment
        /// </summary>
        public const string metaComment = "metaComment";

        /// <summary>
        /// 메타 데이터 - parentMetaCode
        /// </summary>
        public const string parentMetaCode = "parentMetaCode";

        /// <summary>
        /// 메타 데이터 - metaName
        /// </summary>
        public const string metaName = "metaName";

        /// <summary>
        /// 파일유형 - 모델 systemCode
        /// </summary>
        public const string systemCodeModel = "typmodel";

        /// <summary>
        /// 파일유형 - 모델 codeName
        /// </summary>
        public const string codeNameModel = "모델";

        /// <summary>
        /// 파일유형 - 보고서 systemCode
        /// </summary>
        public const string systemCodeReprt = "typreprt";

        /// <summary>
        /// 파일유형 - 보고서 codeName
        /// </summary>
        public const string codeNameReprt = "보고서";

        // TODO : Rest API 9. 컨테이너 상세 조회 실행시 -> 서버를 통해 오는 JSON 데이터 중 "taskDetail"의 값이 null인 경우에 사용하는 인덱스 None "taskIdxNone" 구현 (2023.10.24 jbh)
        /// <summary>
        /// 마일스톤 - 업무 인덱스 None
        /// </summary>
        public const string taskIdxNone = "0";

        // TODO : Rest API 9. 컨테이너 상세 조회 실행시 -> 서버를 통해 오는 JSON 데이터 중 "taskDetail"의 값이 null인 경우에 사용하는 업무 유형 None "taskDetailTypeNone" 구현 (2023.10.24 jbh)
        /// <summary>
        /// 마일스톤 - 업무 유형 None
        /// </summary>
        public const string taskTypeNone = "관련업무선택";

        /// <summary>
        /// 마일스톤 - 업무 인덱스 요구사항정의
        /// </summary>
        public const string taskIdxDefine = "20";

        /// <summary>
        /// 마일스톤 - 업무 유형 요구사항정의
        /// </summary>
        public const string taskTypeDefine = "요구사항정의";

        /// <summary>
        /// 마일스톤 - 업무 인덱스 엔터티도출
        /// </summary>
        public const string taskIdxEntity = "21";

        /// <summary>
        /// 마일스톤 - 업무 유형 엔터티도출
        /// </summary>
        public const string taskTypeEntity = "엔터티도출";

        /// <summary>
        /// 마일스톤 - 업무 인덱스 요구사항정의서 작성
        /// </summary>
        public const string taskIdxCreation = "22";

        /// <summary>
        /// 마일스톤 - 업무 유형 요구사항정의서 작성
        /// </summary>
        public const string taskTypeCreation = "요구사항정의서 작성";

        /// <summary>
        /// 마일스톤 - 업무 인덱스 UI설계서 작성
        /// </summary>
        public const string taskIdxUI = "23";


        /// <summary>
        /// 마일스톤 - 업무 유형 UI설계서 작성
        /// </summary>
        public const string taskTypeUI = "UI설계서 작성";

        /// <summary>
        /// 파일명(이름)
        /// </summary>
        public const string fileNm = "fileNm";

        /// <summary>
        /// 파일 아이디
        /// </summary>
        public const string fileId = "fileId";


        #region 기본 정보 탭 - Label

        /// <summary>
        /// 공통 (컨테이너 정보, 파일정보) - 구분 
        /// </summary>
        public const string divLabel = "구분";

        /// <summary>
        /// 공통 (컨테이너 정보, 파일정보) - 내용
        /// </summary>
        // contents 의미
        // 참고 URL - https://confusingtimes.tistory.com/1821
        public const string contentsLabel = "내용";

        /// <summary>
        /// 컨테이너 정보 - 작성자
        /// </summary>
        public const string createUserLabel = "작성자";

        /// <summary>
        /// 컨테이너 정보 - 버전
        /// </summary>
        public const string versionLabel = "버전";

        /// <summary>
        /// 컨테이너 정보 - 제목
        /// </summary>
        public const string titleLabel = "제목";

        // TODO : 컨테이너 정보 Label const string 문자열 객체 "soft_ifo_Label", "soft_ver_Label", "find_tag_Label" 필요시 사용 예정 (2023.10.19 jbh)
        /// <summary>
        /// 컨테이너 정보 - 소프트웨어
        /// </summary>
        public const string soft_ifo_Label = "소프트웨어";

        /// <summary>
        /// 컨테이너 정보 - 소프트웨어버전
        /// </summary>
        public const string soft_ver_Label = "소프트웨어버전";

        /// <summary>
        /// 컨테이너 정보 - 키워드/태그
        /// </summary>
        public const string find_tag_Label = "키워드/태그";

        /// <summary>
        /// 컨테이너 정보 - 파일유형
        /// </summary>
        public const string fileTypeCodeLabel = "파일유형";

        /// <summary>
        /// 컨테이너 정보 - 마일스톤
        /// </summary>
        public const string taskDetailLabel = "마일스톤";

        /// <summary>
        /// 파일정보 - (요청 데이터 공통 사용)
        /// </summary>
        public const string fileName = "파일명";

        #endregion 기본 정보 탭 - Label

        #region 연관 컨테이너 

        /// <summary>
        /// 부모 컨테이너 아이디
        /// </summary>
        public const string targetId = "targetId";

        /// <summary>
        /// 자식 컨테이너 아이디(또는 자기 자신 컨테이너 ID)
        /// </summary>
        public const string sourceId = "sourceId";

        #endregion 연관 컨테이너

        #endregion 프로퍼티 

        #region 클래스 - ContainerPack

        /// <summary>
        /// 컨테이너 정보
        /// </summary>
        public class ContainerPack
        {
            /// <summary>
            /// 요청 파라미터 - 프로젝트 아이디 
            /// </summary>
            public string projectId { get; set; }

            /// <summary>
            /// 요청 파라미터 - 팀 아이디
            /// </summary>
            public string teamId { get; set; }

            /// <summary>
            /// 요청 파라미터 - containerId
            /// </summary>
            public string containerId { get; set; }
        }

        #endregion 클래스 - ContainerPack

        #region sample

        #endregion sample
    }
}
