using System;
using System.Windows;
using System.Windows.Controls;

namespace CobimExplorer.Views.Pages
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellV : UserControl
    {
        public ShellV() 
        {
            InitializeComponent();

            // this.DataContext = new ShellViewModel();   // View(ShellView)와 ViewModel(ShellViewModel) 연결, 참고 URL - https://github.com/canton7/Stylet/wiki/ViewModel-First
        }
    }
}
