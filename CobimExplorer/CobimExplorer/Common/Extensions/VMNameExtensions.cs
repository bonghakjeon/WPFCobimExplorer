using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobimExplorer.Common.Extensions
{
    public static class VMNameExtensions
    {
        /// <summary>
        /// static 확장메서드 GetViewModelName 정의
        /// 첫번째 파라미터 - 뷰모델 객체만 사용하도록 지정
        /// 뷰모델 이름이 들어간 마지막 인덱스 구하기 
        /// </summary>
        public static string GetViewModelName(this object ViewModel)
        {
            int lastIndex = -1;
            
            // TODO: 뷰모델 이름 문자열 구하기 (2023.07.06 jbh)
            // 참고 URL - https://sheepone.tistory.com/54
            string[] vmstring = ViewModel.GetType().Name.Split('.');
            lastIndex = vmstring.Length - 1;   // 뷰모델 이름이 들어간 마지막 인덱스 구하기 

            string VMName = vmstring[lastIndex];

            return VMName;
        }
    }
}
