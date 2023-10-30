using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CobimExplorer.Rest.Api.CobimBase.Explorer;

namespace CobimExplorer.Converters
{
    // TODO : 탐색기 화면 뷰 (ExplorerV.xaml) 버튼 멀티 바인딩 전용 Converter 클래스 "ExplorerConverter.cs" 구현 (2023.09.19 jbh)
    // 참고 URL - https://stackoverflow.com/questions/1350598/passing-two-command-parameters-using-a-wpf-binding
    // 참고 2 URL - https://stackoverflow.com/questions/23499671/wpf-multibinding-button-isenabled-with-textboxes
    public class ExplorerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
