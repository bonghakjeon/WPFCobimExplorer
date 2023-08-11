using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CobimExplorer.Common.BindingProxy
{
    // TODO : BindingProxy.cs 필요시 사용 예정 (2023.07.26 jbh)
    // 참고 URL - https://codekiller.tistory.com/575
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // TODO : 의존 프로퍼티 
        // 참고 URL - https://afsdzvcx123.tistory.com/entry/8%EC%9E%A5-WPF-%EC%9D%98%EC%A1%B4%ED%94%84%EB%A1%9C%ED%8D%BC%ED%8B%B0DependencyProperty-%EB%9E%80
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}
