using Serilog;
using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using CobimExplorer.Services.Page;
using CobimExplorer.Interface.Page;
using CobimExplorer.Message;
using CobimExplorer.Commands;
using CobimExplorer.Common.Extensions;
using CobimExplorer.Utils.Password;
using CobimExplorer.Rest.Api.CobimBase.User;
using CobimExplorer.ViewModels.Pages;
using CobimExplorer.Models.User;
using CobimExplorer.Settings;
// using CobimExplorer.Core.Rest.Api.CobimBase.User;
// using Windows.Web.Http;

namespace CobimExplorer.ViewModels.Windows.Login
{
    public class LoginVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        public string ServerPath
        {
            get => this._ServerPath;
            set
            {
                this._ServerPath = value;
                this.NotifyOfPropertyChange(nameof(ServerPath));
            }
        }
        private string _ServerPath = LoginHelper.auth_server_url;

        public BindableCollection<string> ServerPaths { get; } = new BindableCollection<string>();

        public bool IsDarkTheme { get => _IsDarkTheme; set { _IsDarkTheme = value; NotifyOfPropertyChange(); } }
        private bool _IsDarkTheme;

        /// <summary>
        /// 로그인 (Login)
        /// </summary>
        public bool IsLogin
        {
            get => _IsLogin;
            set
            {
                _IsLogin = value;
                NotifyOfPropertyChange(nameof(IsLogin));
            }
        }
        private bool _IsLogin;

        /// <summary>
        /// 종료 (Exit)
        /// </summary>
        public bool IsExit
        {
            get => _IsExit;
            set
            {
                _IsExit = value;
                NotifyOfPropertyChange(nameof(_IsExit));
            }
        }
        private bool _IsExit;

        /// <summary>
        /// 로그인 ID
        /// </summary>
        public string LoginID
        {
            get => this._LoginID;
            set
            {
                _LoginID = value;
                NotifyOfPropertyChange(nameof(_LoginID));
            }
        }
        private string _LoginID;

        /// <summary>
        /// 비밀번호
        /// </summary>
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                NotifyOfPropertyChange(nameof(_Password));
            }
        }
        private string _Password;

        /// <summary>
        /// 비밀번호 문자열 공백(Null)여부 확인
        /// </summary>
        //public bool PasswordNullCheck 
        //{
        //    get => _PasswordNullCheck;
        //    set
        //    {
        //        _PasswordNullCheck = Password == null ? true : (Password == "" ? true : false);
        //        NotifyOfPropertyChange(nameof(_PasswordNullCheck));
        //    }
        //} 
        //private bool _PasswordNullCheck;

        /// <summary>
        /// HttpClient client (계속 호출하지 말고 싱글톤으로 사용해야함.)
        /// </summary>
        [Inject]
        public HttpClient client { get; set; }

        /// <summary>
        /// 로그인 정보 (계속 호출하지 말고 싱글톤으로 사용해야함.)
        /// </summary>
        public LoginHelper.LoginPack LoginPack { get; set; }

        /// <summary>
        /// 로그인 토큰 키 (계속 호출하지 말고 싱글톤으로 사용해야함.)
        /// </summary>
        //[Inject]
        //public LoginHelper.Login_Access_Token Token { get; set; }
        //[Inject]
        //public LoginHelper.Login_Access_Token Token { get; set; }

        /// <sumary>
        /// 사용자 정보
        /// </sumary>
        //[Inject]
        //public UserInfoView UserInfo { get => _UserInfo; set { _UserInfo = value; NotifyOfPropertyChange(); } }
        //private UserInfoView _UserInfo;

        /// <summary>
        /// 탐색기 화면
        /// </summary>\
        public ExplorerVM explorerVM { get; set; }

        /// <summary>
        /// 로그인 페이지
        /// </summary>
        public LoginVM loginVM { get; set; }

        // private readonly PaletteHelper paletteHelper = new PaletteHelper();

        /// <summary>
        /// 종료 Command
        /// </summary>
        public ICommand ExitCommand { get; set; }

        /// <summary>
        /// 로그인 Command
        /// </summary>
        public ICommand LoginCommand { get; set; }

        /// <summary>
        /// 체크인 Command
        /// </summary>
        public ICommand TestCommand { get; set; }

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

            ExitCommand  = new ButtonCommand(ExitApp, CanExecuteMethod);
            LoginCommand = new ButtonCommand(LoginAsync, CanExecuteMethod);
            TestCommand  = new ButtonCommand(TestAsync, CanExecuteMethod);
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

        private bool CanExecuteMethod(object arg)
        {
            throw new NotImplementedException();
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

        #region SaveOption

        private void SaveOption(LoginHelper.Login_Access_Token login_access_token)
        {
            try
            {
                if (!this.ServerPaths.Contains(this.ServerPath))
                    this.ServerPaths.Add(this.ServerPath);
                AppSetting.Default.Login.ServerPath = this.ServerPath;
                //AppSetting.Default.Login.SiteViewCode = this.SiteViewCode;

                // TODO : 추후 필요시 "this.LoginID", "this.Password" 사용 예정(2023.08.30 jbh)
                //AppSetting.Default.Login.LoginId = this.LoginID;
                //AppSetting.Default.Login.Password = this.Password;
                AppSetting.Default.Login.LoginId = LoginHelper.testId;
                AppSetting.Default.Login.Password = LoginHelper.testId;

                AppSetting.Default.Login.ServerPaths = this.ServerPaths.ToList<string>();
                AppSetting.Default.Login.Token = login_access_token;
            }
            catch (Exception e)
            {
                Log.Logger.Information("SaveOption Error = " + e.Message);
                MessageBox.Show(e.Message);
                return;
            }
        }

        #endregion SaveOption

        #region LoginAsync

        // TODO : ASP .NET CORE 웹서비스 프로젝트 파일 "CobimExplorer.Core.Rest.Api" 서비스 참조 추가 (2023.08.29 jbh)
        // 참고 URL - https://ojava.tistory.com/63
        // 참고 2 URL - https://blog.naver.com/icysword/140180123082
        public async Task LoginAsync(object obj)
        {
            // TODO : 추후 필요시 문자열 앞뒤 공백 제거(Trim) 사용 로직으로 변경 (2023.08.22 jbh)
            // 참고 URL - https://developer-talk.tistory.com/660
            //string clientID = LoginID.Trim();
            //string clientPW = Password.Trim();
            // string clientPW = PasswordHelper.Password;
            // string testID = LoginID.Trim();
            // string testPW = Password.Trim();

            // TODO : 테스트 기간 동안 로그인시 사용할 아이디(LoginHelper.testid), 비밀번호(LoginHelper.testpassword) 구현 (2023.08.23 jbh)
            string clientID = LoginHelper.testId;
            string clientPW = LoginHelper.testPassword;
            // string PassWord = 

            LoginPack = new LoginHelper.LoginPack
            {
                id = clientID,
                password = clientPW,
                inputTenantId = LoginHelper.inputTenantId,
            };

            //LoginHelper.LoginPack LoginPack = new LoginHelper.LoginPack
            //{
            //    id = clientID,
            //    password = clientPW,
            //    inputTenantId = LoginHelper.inputTenantId,
            //};

            try
            {
                MessageBox.Show("로그인 테스트 중입니다.");

                //Log.Logger.Information("웹소켓 연결 시도");
                //Log.Logger.Information("웹소켓 연결 성공");

                //// TODO : Rest API POST 방식 사용자 로그인 static 메서드 "PostUserLogin" 호출문 구현(2023.08.18 jbh)
                //var token = await UserRestServer.PostUserLoginAsync(client, LoginPack);
                var token = await UserRestServer.PostUserLoginAsync(client, LoginPack);

                // UserInfo.token = await UserRestServer.PostUserLoginAsync(client, LoginPack);

                if (token.resultData != null && token.resultMessage == null)
                {
                    // Console.WriteLine("\nLogin is OK...");
                    // 로그인 성공 
                    Log.Logger.Information("로그인 성공");

                    MessageBox.Show("Login is OK...");

                    // 로그인 성공
                    loginVM.IsLogin = true;

                    // 로그인 정보 저장
                    loginVM.SaveOption(token);


                    // ExplorerVM explorerVM = Container.Get(ExplorerVM).Set();

                    // TODO : 로그인 성공시 화면(Window - LoginV.xaml) 종료 구현 (2023.08.17 jbh)
                    // OnRequestClose(LoginVM, new EventArgs());
                    this.RequestClose(loginVM.IsLogin);
                }
                else
                {
                    // 로그인 실패 
                    Log.Logger.Information("로그인 실패");

                    //Console.WriteLine("Login is NOT OK...");
                    MessageBox.Show("Login is NOT OK...");
                    // Console.WriteLine("Error = " + token.resultData);

                    loginVM.IsLogin = false;
                }

                return;
            }
            catch (Exception e)
            {
                Log.Logger.Information("로그인 실패 = " + e.Message);
                MessageBox.Show(e.Message);
                loginVM.IsLogin = false;
            }
            finally
            {
                // TODO : 추후 필요 시 finally문에 코드 추가 예정 (2023.08.21 jbh)
            }
            return;
        }

        #endregion LoginAsync

        #region TestAsync

        //public async Task TestAsync()
        //{
        //    try
        //    {
        //        MessageBox.Show("테스트");
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

        public async Task TestAsync(object obj)
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
        public async Task ExitApp(object obj)
        {
            // LoginVM LoginVM = this;

            loginVM.IsExit = true;

            this.RequestClose(loginVM.IsExit);
            // Application.Current.Shutdown();
        }

        #endregion exitApp

        #region Sample

        #endregion Sample
    }
}
