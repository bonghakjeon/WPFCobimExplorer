using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CobimUtil;
using System.Windows;

namespace CobimExplorer.Rest.Api.CobimBase.Explorer
{
    public class FileHelper
    {
        #region 프로퍼티

        /// <summary>
        /// 파일이 속한 상위 프로젝트 아이디
        /// </summary>
        public const string projectId = "projectId";

        /// <summary>
        /// 파일이 속한 상위 팀 아이디
        /// </summary>
        public const string teamId = "teamId";

        /// <summary>
        /// 파일이 속한 상위 폴더 아이디
        /// </summary>
        public const string folderId = "folderId";

        /// <summary>
        /// 파일 이름
        /// </summary>
        public const string fileNm = "fileNm";

        /// <summary>
        /// 파일 아이디
        /// </summary>
        public const string fileId = "fileId";

        /// <summary>
        /// 파일 컨테이너 그룹
        /// </summary>
        public const string containerGroup = "containerGroup";

        /// <summary>
        /// 파일 컨테이너 ID
        /// </summary>
        public const string containerId = "containerId";

        /// <summary>
        /// 제목
        /// </summary>
        public const string title = "title";

        /// <summary>
        /// 작성자
        /// </summary>
        public const string createUserId = "createUserId";

        /// <summary>
        /// 등록일시
        /// </summary>
        public const string createDateTime = "createDateTime";

        /// <summary>
        /// 파일 버전
        /// </summary>
        public const string revsnVal = "revsnVal";

        /// <summary>
        /// 파일 유형 정보
        /// </summary>
        public const string fileTypeCodeInfo = "fileTypeCodeInfo";

        /// <summary>
        /// 파일 유형 이름 
        /// </summary>
        public const string fileTypeCodeName = "codeName";

        /// <summary>
        /// 파일 유형
        /// </summary>
        public const string fileTypeCode = "fileTypeCode";


        /// <summary>
        /// 파일 단계 정보
        /// </summary>
        public const string stepCodeInfo = "stepCodeInfo";

        /// <summary>
        /// 파일 단계 이름 
        /// </summary>
        public const string stepCodeName = "codeName";

        /// <summary>
        /// 파일 상태 정보
        /// </summary>
        public const string statusCodeInfo = "statusCodeInfo";

        /// <summary>
        /// 파일 체크 정보
        /// </summary>
        public const string checkCodeInfo = "checkCodeInfo";

        /// <summary>
        /// 파일 상태 이름
        /// </summary>
        public const string statusCodeName = "codeName";

        /// <summary>
        /// 파일 리스트
        /// </summary>
        public const string fileList = "fileList";

        /// <summary>
        /// 파일 컨테이너 리스트 (파일 속성 정보 + 메타 데이터 포함) 
        /// </summary>
        public const string containerList = "containerList";


        // TODO : 6. 컨테이너 목록 조회 (파일 리스트 조회 API) const string 변수 "requestFolderUrl" 필요시 수정 예정 (2023.09.20 jbh)
        /// <summary>
        /// 6. 컨테이너 목록 조회 (파일 리스트 조회 API)
        /// 요청 URL
        /// </summary>
        // public const string requestFolderUrl = "http://bim.211.43.204.141.nip.io:31380/api/folder/list/jc-constrct/inc-001-jc-constrct-t001";
        // public const string requestFolderUrl = "http://bim.211.43.204.141.nip.io:31380/api/folder/list/";
        // public const string requestFileUrl = "http://bim.211.43.204.141.nip.io:31380/api/container/list/:projectId/:teamId/:folderId";
        public const string requestFileUrl = "http://bim.211.43.204.141.nip.io:31380/api/container/list/";


        /// <summary>
        /// 7. 파일 다운로드 (파일 다운로드 API)
        /// </summary>
        public const string requestFileDownLoadUrl = "http://bim.211.43.204.141.nip.io:31380/api/folder/file/container/download/";

        #endregion 프로퍼티

        #region 클래스 - FilePack

        /// <summary>
        /// 파일 정보
        /// </summary>
        public class FilePack
        {
            /// <summary>
            /// 요청 파라미터 - 파일 아이디 
            /// </summary>
            public string fileId { get; set; }

            /// <summary>
            /// 응답 결과 - 다운로드한 파일 이름
            /// </summary>
            public string fileName { get; set; }
        }

        #endregion 클래스 - FilePack

        #region FileDownLoadName

        /// <summary>
        /// 새로 다운로드 받으려는 파일 이름(파일 전체 경로 - pDirPath + 파일이름 - fi.Name)이 기존에 다운로드 받은 파일(파일 전체 경로 - pDirPath + 파일이름 - fi.Name)과 이름 중복일 때 (N)으로 파일명 생성
        /// </summary>
        /// <param name="pDirPath"></param>
        /// <param name="pFileName"></param>
        /// <returns></returns>
        public static string FileDownLoadPath (string pDirPath, string pFileName)
        {
            string fileName         = string.Empty;    // 파일 이름 (파일 전체 경로 + 파일이름 - fi.Name)
            string dirMapPath       = string.Empty;    // 다운로드 받으려고 하는 파일 중복 여부 체크할 때 필요한 파일 경로 
            string downloadFilePath = string.Empty;    // 다운로드 받으려고 하는 최종 파일 경로 (파일 전체 경로 + 이름 중복일 때 변경된 파일 이름)

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : 다운로드 받을 파일명이 중복일 때, 기존 파일명 +N 으로 생성하도록 구현(2023.10.25 jbh)
                // 참고 URL - https://pseudo-code.tistory.com/166
                fileName = pFileName;
                

                if (fileName.Length > 0)
                {
                    int startIndex    = 0;                  // 파일이름 문자열 시작
                    int indexOfDot    = fileName.LastIndexOf(".");
                    string strName    = fileName.Substring(startIndex, indexOfDot);
                    string strExt     = fileName.Substring(indexOfDot);

                    bool bExist       = true;               // 중복된 파일 존재여부 확인
                    int fileCnt       = 0;                  // 중복된 파일 다운로드시 파일 이름에 붙일 숫자(N) 

                    // string dirMapPath = string.Empty;    

                    while (bExist)
                    {
                        dirMapPath    = pDirPath;           // 파일 전체 경로 - pDirPath 문자열 값을 string 클래스 객체 dirMapPath에 할당
                        string pathCombine = Path.Combine(dirMapPath, fileName);

                        // 해당 파일 경로에 똑같은 이름의 파일이 존재하는 경우 
                        if (File.Exists(pathCombine))
                        {
                            if (fileCnt == 0) MessageBox.Show("대상 폴더에 이미 동일한 이름의 파일이 존재합니다.\r\n다운로드 받으시겠습니까?");
                            fileCnt++;
                            // 기존 파일 + (N)으로 파일명 변경 
                            fileName = strName + "(" + fileCnt + ")" + strExt;
                        }
                        // 똑같은 이름의 파일이 존재하지 않는 경우 
                        else bExist = false;
                    }
                }

                downloadFilePath = Path.Combine(dirMapPath, fileName);  
                return downloadFilePath;   // 다운로드 받으려고 하는 최종 파일 경로 (파일 전체 경로 + 이름 중복일 때 변경된 파일 이름) 리턴
                // return fileName;
            }
            catch (Exception e)
            {
                Logger.Error(currentMethod, Logger.errorMessage + e.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion FileDownLoadName

        #region sample

        #endregion sample
    }
}
