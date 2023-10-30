using Serilog;
using Stylet;
using StyletIoC;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Reflection;
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
using CobimExplorer.Rest.Api.CobimBase.Explorer;
using CobimExplorer.Common.LogManager;

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
        /// 로그인 정보 
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

        //private bool CanExecuteMethod(object arg)
        //{
        //    throw new NotImplementedException();
        //}

        // TODO : 메서드 "CanExecuteMethod" 필요시 수정 예정 (2023.10.4 jbh)
        private bool CanExecuteMethod(object obj)
        {
            return true;
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
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                if (!this.ServerPaths.Contains(this.ServerPath))
                    this.ServerPaths.Add(this.ServerPath);
                AppSetting.Default.Login.ServerPath = this.ServerPath;
                //AppSetting.Default.Login.SiteViewCode = this.SiteViewCode;

                // TODO : 추후 필요시 "this.LoginID", "this.Password" 추가 예정(2023.08.30 jbh)
                //AppSetting.Default.Login.LoginId = this.LoginID;
                //AppSetting.Default.Login.Password = this.Password;
                AppSetting.Default.Login.LoginId = LoginHelper.testId;
                AppSetting.Default.Login.Password = LoginHelper.testPassword;

                AppSetting.Default.Login.ServerPaths = this.ServerPaths.ToList<string>();
                AppSetting.Default.Login.Token = login_access_token;

                // TODO : 응용 프로그램 기본 설정 클래스 (AppSetting) 중 ProjectSetting 클래스 프로퍼티 "Project"에
                //        테스트 프로젝트 아이디(TestProjectId), 테스트 팀 아이디(TestTeamId) 추가 구현 (2023.09.04 jbh)
                //        추후 필요 없을시 삭제 예정
                AppSetting.Default.Project.TestProjectId = ProjectHelper.testProjectId;
                AppSetting.Default.Project.TestTeamId = ProjectHelper.testTeamId;
            }
            catch (Exception e)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + "SaveOption Error = " + e.Message);
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

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("로그인 테스트 중입니다.");

                //Log.Information("웹소켓 연결 시도");
                //Log.Information("웹소켓 연결 성공");

                Log.Information(Logger.GetMethodPath(currentMethod) + "Http 통신 연결 시도");

                //// TODO : Rest API POST 방식 사용자 로그인 static 메서드 "PostUserLogin" 호출문 구현(2023.08.18 jbh)
                //var token = await UserRestServer.PostUserLoginAsync(client, LoginPack);
                var token = await UserRestServer.PostUserLoginAsync(client, LoginPack);

                // UserInfo.token = await UserRestServer.PostUserLoginAsync(client, LoginPack);

                if (token.resultData != null && token.resultMessage == null)
                {
                    Log.Information(Logger.GetMethodPath(currentMethod) + "Http 통신 연결 성공");

                    Log.Information(Logger.GetMethodPath(currentMethod) + "로그인 성공");

                    // [테스트 완료] TODO : 로그 레벨(Debug, Verbose, Warning, Error, Fatal) 기록 테스트 진행 (2023.10.11 jbh)
                    //Log.Debug(Logger.GetMethodPath(currentMethod) + "테스트 Debug 로그인");
                    //Log.Verbose(Logger.GetMethodPath(currentMethod) + "테스트 Verbose 로그인");
                    //Log.Warning(Logger.GetMethodPath(currentMethod) + "테스트 Warning 로그인");
                    //Log.Error(Logger.GetMethodPath(currentMethod) + "테스트 Error 로그인");
                    //Log.Fatal(Logger.GetMethodPath(currentMethod) + "테스트 Fatal 로그인");

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
                    Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + "로그인 실패");

                    //Console.WriteLine("Login is NOT OK...");
                    MessageBox.Show("Login is NOT OK...");
                    // Console.WriteLine("Error = " + token.resultData);

                    loginVM.IsLogin = false;
                }

                return;
            }
            catch (Exception e)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + e.Message);
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
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
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
