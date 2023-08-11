using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CobimExplorer.Views
{
    /// <summary>
    /// MainV.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainV : Window
    {
        public MainV()
        {
            InitializeComponent();

            // this.DataContext = new MainVM();   // View(MainV)와 ViewModel(MainVM) 연결, 참고 URL - https://github.com/canton7/Stylet/wiki/ViewModel-First
        }

        private void PluScroll_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
