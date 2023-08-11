using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobimExplorer.Common.LogManager
{
    // TODO : 로그 메시지 출력 관련 클래스 구현 예정 (2023.07.14 jbh) 
    // 참고 URL - https://github.com/canton7/Stylet/wiki/Logging

    public class ExplorerLogger : Stylet.Logging.ILogger
    {
        public ExplorerLogger(string loggerName)
        {
            // TODO
        }

        public void Info(string format, params object[] args)
        {
            // TODO
        }

        public void Warn(string format, params object[] args)
        {
            // TODO
        }

        public void Error(Exception exception, string message = null)
        {
            // TODO
        }
    }
}
