using StyletIoC;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobimExplorer.Services.Page;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace CobimExplorer.ViewModels.Windows.Login
{
    // 로그인 화면(TestLoginV.xaml) 뷰 모델 구현 
    // 유튜브 참고 URL - https://youtu.be/hu7QG8OEOuk 
    public class TestLoginVM : PageBase
    {
        #region 프로퍼티 

        public bool IsDarkTheme { get => _IsDarkTheme; set { _IsDarkTheme = value; NotifyOfPropertyChange(); } }
        private bool _IsDarkTheme; 

        private readonly PaletteHelper paletteHelper = new PaletteHelper();


        #endregion 프로퍼티 

        #region 생성자 

        public TestLoginVM(IContainer container) : base(container)
        {

        }
        #endregion 생성자

        #region toggleTheme

        /// <summary>
        /// 테마 색상 변경 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        #endregion toggleTheme

        #region exitApp

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion exitApp

        #region OnMouseLeftButtonDown

        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonDown(e);
        //    DragMove();

        //}

        #endregion OnMouseLeftButtonDown
    }

    //public class TestLoginVM
    //{
    //    public TestLoginVM(IContainer container)
    //    {

    //    }
    //}
}
