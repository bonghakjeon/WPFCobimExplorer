using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CobimExplorer.Common.LogManager
{
    // TODO : 로그 메시지 출력 관련 클래스 필요시 구현 및 사용 예정 (2023.07.14 jbh) 
    // 참고 URL - https://github.com/canton7/Stylet/wiki/Logging
    // 참고 2 URL - https://insurang.tistory.com/entry/WPF-C-%EB%A1%9C%EA%B7%B8%ED%8C%8C%EC%9D%BC-%EB%A7%8C%EB%93%A4%EA%B8%B0-LOG
    public class Logger
    {
        // TODO : static 메서드 "GetMethodPath" 테스트 후 오류 발생시 수정 예정(2023.10.11 jbh)
        //public static string GetMethodPath(MethodBase currentMethod)
        //{
        //    var fullMethodPath = string.Empty;

        //    try
        //    {
        //        fullMethodPath = "[" + currentMethod.DeclaringType.FullName.Replace("+", " | ").Replace("d__77", "") + "] : ";
        //        return fullMethodPath;
        //    }
        //    catch (Exception e)
        //    {
        //        var loggerPath = MethodBase.GetCurrentMethod().DeclaringType.FullName;
        //        Log.Error(loggerPath + Logger.errorMessage + e.Message);
        //        throw;
        //        //Log.Error($"Error name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
        //    }
        //}

        #region 프로퍼티 

        // TODO : 로그 타입 유형 문자열 (const string) 객체 다른 유형 필요시 추가 예정 (2023.10.11 jbh)
        public const string debugMessage = "디버그 - ";

        public const string errorMessage = "오류 - ";

        public const string warningMessage = "경고 - ";

        #endregion 프로퍼티 

        #region GetMethodPath

        /// <summary>
        /// 로그 실행한 Namespace 및 메서드(비동기 메서드 포함) 경로 추출
        /// </summary>
        /// <param name="currentMethod"></param>
        /// <returns></returns>
        public static string GetMethodPath(MethodBase currentMethod)
        {
            var fullMethodPath = string.Empty;

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentLoggerMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : 로그 실행한 Namespace 및 메서드(비동기 메서드 포함) 경로 추출하여 문자열로 변환 (2023.10.11 jbh)
                // 참고 URL - https://stackoverflow.com/questions/2968352/using-system-reflection-to-get-a-methods-full-name
                var generatedType = currentMethod.DeclaringType;
                var originalType = generatedType.DeclaringType;
                var foundMethod = originalType.GetMethods(
                      BindingFlags.Instance | BindingFlags.Static
                    | BindingFlags.Public | BindingFlags.NonPublic
                    | BindingFlags.DeclaredOnly)
                    .Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);
                //string test =  foundMethod.DeclaringType.Name + "." + foundMethod.Name;
                // string test = foundMethod.DeclaringType.FullName + "." + foundMethod.Name;

                // fullMethodPath = "[" + currentMethod.ReflectedType.ReflectedType.FullName.Replace("+", " | ").Replace("d__77", "") + "] : ";
                fullMethodPath = "[" + foundMethod.DeclaringType.FullName + " | " + foundMethod.Name + "] : ";
                return fullMethodPath;
            }
            catch (Exception e)
            {
                // var loggerPath = MethodBase.GetCurrentMethod().DeclaringType.FullName;
                // Log.Error(Logger.GetMethodPath(currentLoggerMethod) + Logger.errorMessage + e.Message);
                Log.Error(Logger.errorMessage + e.Message);
                throw;

            }
        }

        #endregion GetMethodPath

        #region sample

        //public class Logger : Stylet.Logging.ILogger
        //{
        //    #region 프로퍼티 

        //    /// <summary>
        //    /// 로그 객체 이름
        //    /// </summary>
        //    private readonly string Name;

        //    /// <summary>
        //    /// 로그 객체 이름 문자열
        //    /// </summary>
        //    private const string LoggerName = "CobimExplorer";

        //    /// <summary>
        //    /// 로그 객체
        //    /// </summary>
        //    public static Logger Log
        //    {
        //        get
        //        {
        //            // 싱글톤 패턴 
        //            if (_Log == null)
        //            {
        //                _Log = new Logger(LoggerName);
        //            }
        //            return _Log;
        //        }
        //        set => _Log = value;
        //    }
        //    private static Logger _Log;

        //    /// <summary>
        //    /// 로그 타입 - info
        //    /// </summary>
        //    public const string info = "Info";


        //    /// <summary>
        //    /// 로그 타입 - warn
        //    /// </summary>
        //    public const string warn = "Warn";

        //    /// <summary>
        //    /// 로그 타입 - error
        //    /// </summary>
        //    public const string error = "Error";


        //    #endregion 프로퍼티 

        //    #region 생성자

        //    public Logger(string loggerName)
        //    {
        //        // TODO
        //        this.Name = loggerName;
        //    }

        //    #endregion 생성자

        //    #region 기본 메소드

        //    public void Info(string format, params object[] args)
        //    {
        //        // TODO
        //        LogWrite(info, format, args.ToString());
        //    }

        //    public void Warn(string format, params object[] args)
        //    {
        //        // TODO
        //        LogWrite(warn, format);
        //    }

        //    public void Error(Exception exception, string message = null)
        //    {
        //        // TODO
        //        LogWrite(error, message);
        //    }

        //    #endregion 기본 메소드 

        //    #region LogWrite

        //    /// <summary>
        //    /// 로그 파일 생성 + 기록
        //    /// </summary>
        //    /// <param name="message"></param>
        //    private void LogWrite(string logType, string message, string args = null)
        //    {
        //        // string DirPath = Environment.CurrentDirectory + @"\Log";
        //        // TODO : 로그 파일 상위 디렉토리(폴더) 생성 위치("C:\\CobimExplorer\\Log") 문자열 string 객체 DirPath에 할당 (2023.10.6 jbh)
        //        // 참고 URL - https://lightgg.tistory.com/29
        //        string DirPath = "C:\\CobimExplorer\\Log";
        //        string FilePath = DirPath + "\\CobimExplorer_" + DateTime.Today.ToString("yyyyMMdd") + ".log";
        //        string temp = string.Empty;

        //        DirectoryInfo di = new DirectoryInfo(DirPath);
        //        FileInfo fi = new FileInfo(FilePath);

        //        try
        //        {
        //            // 디렉터리(di)가 존재하지 않는 경우
        //            if (!di.Exists) Directory.CreateDirectory(DirPath);   // 디렉터리 새로 생성

        //            // 로그 파일(fi)이 존재하지 않는 경우 
        //            if (!fi.Exists)
        //            {
        //                // TODO : StreamWriter 클래스 객체 sw 인코딩 방식 Encoding.UTF8 설정 (한글 인코딩 때문) (2023.10.6 jbh)
        //                // 참고 URL - https://cloudedi.tistory.com/29
        //                // 참고 2 URL - https://aspdotnet.tistory.com/1043
        //                // using문 사용해서 로그파일 새로 생성
        //                using (StreamWriter sw = new StreamWriter(FilePath, true, Encoding.UTF8))
        //                {
        //                    temp = string.Format("[{0}][{1}] {2}", logType, DateTime.Now, message);
        //                    sw.WriteLine(temp);
        //                    sw.Close();
        //                }
        //            }
        //            // 로그 파일(fi)이 존재하는 경우 
        //            else
        //            {
        //                // using문 사용해서 기존 로그파일에 로그 기록(문자열) 추가
        //                using (StreamWriter sw = File.AppendText(FilePath))
        //                {
        //                    temp = string.Format("[{0}] {1}", DateTime.Now, message);
        //                    sw.WriteLine(temp);
        //                    sw.Close();
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }

        //    #endregion LogWrite

        #endregion sample
    }
}
