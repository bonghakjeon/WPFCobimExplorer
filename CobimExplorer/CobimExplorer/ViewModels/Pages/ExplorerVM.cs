using Serilog;
using Stylet;
using StyletIoC;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using CobimExplorer.Commands;
using CobimExplorer.Message;
using CobimExplorer.Models.Explorer;
using CobimExplorer.Services.Page;
using CobimExplorer.Interface.Page;
using CobimExplorer.Common.Extensions;
using System.Collections.ObjectModel;
using CobimExplorer.Rest.Api.CobimBase.User;
using CobimExplorer.Rest.Api.CobimBase.Explorer;
using CobimExplorer.ViewModels.Windows.Login;
using CobimExplorer.Models.User;
using System.Net.Http;
using CobimExplorer.Settings;

namespace CobimExplorer.ViewModels.Pages
{
    public class ExplorerVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        /// <summary>
        /// 다운로드 Command
        /// </summary>
        public ICommand DownlLoadCommand { get; set; }

        /// <summary>
        /// 체크아웃 Command
        /// </summary>
        public ICommand CheckOutCommand { get; set; }

        /// <summary>
        /// 체크인 Command
        /// </summary>
        public ICommand CheckInCommand { get; set; }

        /// <summary>
        /// HttpClient client (계속 호출하지 말고 싱글톤으로 사용해야함.)
        /// </summary>
        [Inject]
        public HttpClient client { get; set; }

        // TODO : 사용자 정보 프로퍼티 "UserInfo" 필요시 사용 예정 (2023.08.30 jbh)
        /// <sumary>
        /// 사용자 정보
        /// </sumary>
        //[Inject]
        //public UserInfoView UserInfo { get => _UserInfo; set { _UserInfo = value; NotifyOfPropertyChange(); } }
        //private UserInfoView _UserInfo;

        /// <summary>
        /// 로그인 토큰 키 (계속 호출하지 말고 싱글톤으로 사용해야함.)
        /// </summary>
        public LoginHelper.Login_Access_Token Token { get; set; }

        /// <summary>
        /// 로그인 페이지
        /// </summary>
        // public LoginVM loginVM { get; set; }

        // TODO : 프로젝트 목록 ObservableCollection "ProjectTypeCollection" 추후 자료형 타입 ProjectName -> ProjectTypeView(사용자 정의 클래스) 필요시 사용 예정 (2023.08.30 jbh)
        // TODO : 프로젝트 목록 사용자 정의 클래스 "ProjectTypeView" 구현 예정 (2023.08.24 jbh)
        /// <summary>
        /// 프로젝트 목록 ObservableCollection 
        /// </summary>
        //public ObservableCollection<ProjectTypeView> ProjectTypeCollection { get => _ProjectTypeCollection; set { _ProjectTypeCollection = value; NotifyOfPropertyChange(); } }
        //public List<ProjectTypeView> ProjectTypeList 
        //{
        //    get 
        //    { 
        //        // 싱글톤 패턴 
        //        if (_projectTypeList == null)
        //        {
        //            _projectTypeList = new List<ProjectTypeView>();
        //        }

        //        return _projectTypeList;
        //    } 
        //    set => _projectTypeList = value;  
        //}
        //private ObservableCollection<ProjectTypeView> _ProjectTypeCollection = new ObservableCollection<ProjectTypeView>();

        public List<Dictionary<string, object>> ProjectList { get => _ProjectList; set { _ProjectList = value; NotifyOfPropertyChange(); } }
        private List<Dictionary<string, object>> _ProjectList = new List<Dictionary<string, object>>();

        #endregion 프로퍼티 

        #region 생성자

        public ExplorerVM(IEventAggregator events, IContainer container) : base(container)
        {
            Container = container;

            // TODO : this.EventAggregator = events; 코드 추가 (2023.07.24 jbh)
            // 참고 URL - https://github.com/canton7/Stylet/wiki/3.-The-Taskbar
            this.EventAggregator = events;

            DownlLoadCommand = new ButtonCommand(DownLoadAsync, CanExecuteMethod);
            CheckOutCommand  = new ButtonCommand(CheckOutAsync, CanExecuteMethod);
            CheckInCommand   = new ButtonCommand(CheckInAsync, CanExecuteMethod);
        }

        #endregion 생성자 

        #region 기본 메소드


        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            // TODO : TreeView 데이터 출력 테스트 코드 추후 삭제 예정 (2023.07.14 jbh)
            // InitTreeViewText();

            var vm = Container.Get<ExplorerVM>();

            string VMName = vm.GetViewModelName();

            var msg = new ChangeViewModelMsg(this, VMName);
            EventAggregator.PublishOnUIThread(msg);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }



        private bool CanExecuteMethod(object obj)
        {
            return true;
        }


        #endregion 기본 메소드

        #region Set 

        // TODO : 추후 필요시 사용 예정 (2023.08.30 jbh)
        //public static void Set(UserInfoView userInfoView)
        //{
        //    try
        //    {
        //        UserInfo = userInfoView;
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        #endregion Set

        #region DownlLoadAsync

        public async Task DownLoadAsync(object obj)
        {
            string loginId = AppSetting.Default.Login.LoginId;             // 로그인 아이디 
            string password = AppSetting.Default.Login.Password;           // 비밀번호 
            string tokenKey = AppSetting.Default.Login.Token.resultData;   // 로그인 토큰 키 

            try
            {
                MessageBox.Show("다운로드 테스트 중입니다.");

                ProjectList.Clear();   // 프로젝트 목록 리스트 초기화(Clear)

                ProjectList = await ExplorerRestServer.GetExplorerProjectListAsync(client, tokenKey);


                // TODO : 테스트 코드 프로젝트 목록 리스트 "ProjectTypeList"에 데이터 추가 로직 필요시 사용 예정(2023.08.30 jbh)
                //ProjectTypeCollection.Add(new ProjectTypeView() { ProjectName = "Project001" });
                //ProjectTypeCollection.Add(new ProjectTypeView() { ProjectName = "Project002" });
                //ProjectTypeCollection.Add(new ProjectTypeView() { ProjectName = "Project003" });
                //ProjectTypeCollection.Add(new ProjectTypeView() { ProjectName = "..." });

                return;
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message);
            }
            finally
            {
                // TODO : 추후 필요 시 finally문에 코드 추가 예정 (2023.08.24 jbh)
            }
            return;
        }

        #endregion DownlLoadAsync

        #region CheckOutAsync

        public async Task CheckOutAsync(object obj)
        {
            try
            {
                MessageBox.Show("체크아웃 테스트 중입니다.");

                return;
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message);
            }
            finally
            {
                // TODO : 추후 필요 시 finally문에 코드 추가 예정 (2023.08.24 jbh)
            }
            return;
        }

        #endregion CheckOutAsync

        #region CheckInAsync

        public async Task CheckInAsync(object obj)
        {
            try
            {
                MessageBox.Show("체크인 테스트 중입니다.");

                return;
            }
            catch (Exception e)
            {
                Log.Logger.Information(e.Message);
            }
            finally
            {
                // TODO : 추후 필요 시 finally문에 코드 추가 예정 (2023.08.24 jbh)
            }
            return;
        }

        #endregion CheckInAsync

        #region Handle
        public void Handle(ChangeViewModelMsg message)
        {
            // throw new NotImplementedException();
        }

        #endregion Handle

        #region Sample

        #endregion Sample
    }
}
