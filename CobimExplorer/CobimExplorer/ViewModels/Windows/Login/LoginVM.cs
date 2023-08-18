using Serilog;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using CobimExplorer.Services.Page;
using CobimExplorer.Interface.Page;
using CobimExplorer.Message;
using CobimExplorer.Common.Extensions;

namespace CobimExplorer.ViewModels.Windows.Login
{
    public class LoginVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        public bool IsDarkTheme { get => _IsDarkTheme; set { _IsDarkTheme = value; NotifyOfPropertyChange(); } }
        private bool _IsDarkTheme;

        /// <summary>
        /// 로그인 (Login)
        /// </summary>
        public bool IsLogin
        {
            get => this._IsLogin;
            set
            {
                this._IsLogin = value;
                this.NotifyOfPropertyChange(nameof(IsLogin));
            }
        }
        private bool _IsLogin;

        /// <summary>
        /// 종료 (Exit)
        /// </summary>
        public bool IsExit
        {
            get => this._IsExit;
            set
            {
                this._IsExit = value;
                this.NotifyOfPropertyChange(nameof(_IsExit));
            }
        }
        private bool _IsExit;

        /// <summary>
        /// 로그인 페이지
        /// </summary>
        public LoginVM loginVM { get; set; }

        // private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        [Inject]
        public IWindowManager WindowManager { get; set; }
        public Action CloseAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion 프로퍼티 

        #region 생성자 

        public LoginVM(IEventAggregator events, IContainer container) : base(container)
        {
            Container = container;

            // TODO : this.EventAggregator = events; 코드 추가 (2023.07.24 jbh)
            // 참고 URL - https://github.com/canton7/Stylet/wiki/3.-The-Taskbar
            this.EventAggregator = events;
        }
        #endregion 생성자

        #region 기본 메소드 

        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();
            loginVM = this;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        #endregion 기본 메소드

        #region ToShowData 

        public LoginVM ToShowData()
        {
            this.WindowManager.ShowDialog((object)this);
            return this;
        }

        #endregion ToShowData

        #region toggleTheme

        // TODO : 추후 필요시 테마 색상 변경 메서드 "toggleTheme" 구현 예정 (2023.08.17 jbh)
        /// <summary>
        /// 테마 색상 변경 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void toggleTheme(object sender, RoutedEventArgs e)
        //{
        //    ITheme theme = paletteHelper.GetTheme();

        //    if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
        //    {
        //        IsDarkTheme = false;
        //        theme.SetBaseTheme(Theme.Light);
        //    }
        //    else
        //    {
        //        IsDarkTheme = true;
        //        theme.SetBaseTheme(Theme.Dark);
        //    }
        //    paletteHelper.SetTheme(theme);
        //}

        #endregion toggleTheme

        #region LoginAsync

        public async Task LoginAsync()
        {
            // LoginVM loginVM = this;

            try
             {
                MessageBox.Show("로그인 테스트 중입니다.");

                // TODO: 웹서비스 "로그인" Rest API 호출시 await 추가 예정 (2023.08.17 jbh)

                // 로그인 성공 
                Log.Logger.Information("로그인 성공");

                //Log.Logger.Information("웹소켓 연결 시도");
                //Log.Logger.Information("웹소켓 연결 성공");
                loginVM.IsLogin = true;

                // TODO : 로그인 성공시 화면(Window - LoginV.xaml) 종료 구현 (2023.08.17 jbh)
                // OnRequestClose(loginVM, new EventArgs());
                this.RequestClose(loginVM.IsLogin);
            }
            catch (Exception e)
            {
                Log.Logger.Information("로그인 실패 = " + e.Message);
                MessageBox.Show(e.Message);
                throw;
            }
        }

        #endregion LoginAsync

        #region TestAsync

        public async Task TestAsync()
        {
            try
            {
                MessageBox.Show("테스트");
            }
            catch (Exception e)
            {
                throw;
            }
        }


        #endregion TestAsync

        #region Handle

        public void Handle(ChangeViewModelMsg message)
        {
            // throw new NotImplementedException();
        }

        #endregion Handle

        #region exitApp

        // TODO : 종료 버튼 이벤트 메서드 "ExitApp" 필요시 추후 구현 예정(2023.08.17 jbh)
        /// <summary>
        /// 종료 버튼 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExitApp()
        {
            // LoginVM loginVM = this;

            loginVM.IsExit = true;

            this.RequestClose(loginVM.IsExit);
            // Application.Current.Shutdown();
        }

        #endregion exitApp

        #region Sample

        #endregion Sample
    }
}
