using Stylet;
using StyletIoC;
using Serilog;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Controls.Ribbon;
using CobimExplorer.Services.Page;
using CobimExplorer.Interface.Page;
using CobimExplorer.Message;
using CobimExplorer.Commands;
using CobimExplorer.ViewModels.Pages;
using CobimExplorer.ViewModels.Windows.Login;

namespace CobimExplorer.ViewModels
{
    #region TEST 클래스 

    #endregion TEST 클래스 
    public class MainVM : Conductor<IPageBase>.Collection.OneActive, IHandle<ChangeViewModelMsg>   /* TODO: IChildSalesPage, IHandle<> 인터페이스 추후 상속 예정 (2023.07.05 jbh) */
    {
        #region 프로퍼티 

        /// <summary>
        /// ShellVM
        /// </summary>
        public const string ShellVMName = "ShellVM";

        /// <summary>
        /// TestVM
        /// </summary>
        public const string TestVMName = "TestVM";

        /// <summary>
        /// TestExplorerVM
        /// </summary>
        public const string TestExpVMName = "TestExplorerVM";

        /// <summary>
        /// TestLazyTreeVM
        /// </summary>
        public const string TestLazyTrVMName = "TestLazyTreeVM";


        /// <summary>
        /// ExplorerVM
        /// </summary>
        public const string ExpVMName = "ExplorerVM";

        //public ChangeViewModelMsg ReceivedMessage;
        /// <summary>
        /// Button Command (TestViewModel)
        /// </summary>
        public ICommand TestVMCommand { get; set; }

        /// <summary>
        /// Button Command (CobimExplorerViewModel)
        /// </summary>
        public ICommand TestExpVMCommand { get; set; }

        /// <summary>
        /// Button Command (TestLazyTreeVM)
        /// </summary>
        public ICommand TestLazyTreeVMCommand { get; set; }

        /// <summary>
        /// Button Command (ExplorerVM)
        /// </summary>
        public ICommand ExpVMCommand { get; set; }


        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        //public IPageBase PageBase { get; set; }

        [Inject]
        public IWindowManager WindowManager { get; set; }

        // TODO : 프로퍼티 "lastIndex" 추후 필요시 추가 예정 (2023.07.26 jbh)
        /// <summary>
        /// 뷰모델 이름 들어간 문자열 배열 (string[]) 마지막 인덱스
        /// </summary>
        //public int lastIndex { get => _lastIndex; set { _lastIndex = value; NotifyOfPropertyChange(); } }
        //private int _lastIndex = -1;

        public Visibility visibleShellVM { get => _visibleShellVM; set { _visibleShellVM = value; NotifyOfPropertyChange(); } }
        private Visibility _visibleShellVM = Visibility.Visible;

        public Visibility visibleTestVM { get => _visibleTestVM; set { _visibleTestVM = value; NotifyOfPropertyChange(); } }
        private Visibility _visibleTestVM = Visibility.Collapsed;

        public Visibility visibleTestExpVM { get => _visibleTestExpVM; set { _visibleTestExpVM = value; NotifyOfPropertyChange(); } }
        private Visibility _visibleTestExpVM = Visibility.Collapsed;

        public Visibility visibleTestLazyTreeVM { get => _visibleTestLazyTreeVM; set { _visibleTestLazyTreeVM = value; NotifyOfPropertyChange(); } }
        private Visibility _visibleTestLazyTreeVM = Visibility.Collapsed;

        public Visibility visibleExpVM { get => _visibleExpVM; set { _visibleExpVM = value; NotifyOfPropertyChange(); } }
        private Visibility _visibleExpVM = Visibility.Collapsed;

        // TODO : 프로퍼티 private set; 기능이 무엇인지 꼭 확인하기 (2023.07.07 jbh)
        // private set; 의 역할 - 해당 필드 값을 읽기전용으로 쓰겠다는 것을 의미
        // 참고 URL - https://yeko90.tistory.com/entry/c-private-set-%EC%82%AC%EC%9A%A9-%EC%9D%B4%EC%9C%A0%ED%94%84%EB%9F%AC%ED%8D%BC%ED%8B%B0
        // TODO : 뷰모델 프로퍼티 MainVM.cs 에서 추가시 아래처럼 메서드 "NotifyOfPropertyChange" 추가하기 (2023.07.11 jbh)
        //[Inject]
        public ShellVM ShellVM { get => _ShellVM; set { _ShellVM = value; NotifyOfPropertyChange(); } }
        private ShellVM _ShellVM;

        //[Inject]
        public TestVM TestVM { get => _TestVM; set { _TestVM = value; NotifyOfPropertyChange(); } }
        private TestVM _TestVM;

        //[Inject]
        public TestExplorerVM TestExplorerVM { get => _TestExplorerVM; set { _TestExplorerVM = value; NotifyOfPropertyChange(); } }
        private TestExplorerVM _TestExplorerVM;

        //[Inject]
        public TestLazyTreeVM TestLazyTreeVM { get => _TestLazyTreeVM; set { _TestLazyTreeVM = value; NotifyOfPropertyChange(); } }
        private TestLazyTreeVM _TestLazyTreeVM;

        //[Inject]
        public ExplorerVM ExplorerVM { get => _ExplorerVM; set { _ExplorerVM = value; NotifyOfPropertyChange(); } }
        private ExplorerVM _ExplorerVM;

        #endregion 프로퍼티 

        #region 생성자

        // TODO: 생성자 MainVM 로직 보완 (2023.07.04 jbh)
        public MainVM(IEventAggregator events, IContainer container)
        {
            //this.InnerViewModel = new InnerViewModel();
            this.Container = container;
            this.TestVMCommand = new RibbonCommand(this, Container);
            this.TestExpVMCommand = new RibbonCommand(this, Container);
            this.TestLazyTreeVMCommand = new RibbonCommand(this, Container);
            this.ExpVMCommand = new RibbonCommand(this, Container);
            // TODO: this.ShellViewModel, this.TestViewModel 객체 생성 안하고도 ViewModel에서 이벤트 메서드(ShowMessageBox, DoSomething)가 실행 되도록 로직 수정하기(2023.07.05 jbh)
            //this.ShellViewModel = new ShellViewModel(container);
            //this.TestViewModel = new TestViewModel(container);

            //this.TestVM         = testVM;
            //this.ShellVM        = shellVM;
            //this.CobimExplorerVM = CobimExplorerVM;
            //this.ExplorerVM            = explorerVM;
            //this.TestLazyTreeVM        = testLazyTreeVM;

            // TODO : events.Subscribe(this); 코드 추가 (2023.07.24 jbh)
            // 참고 URL - https://github.com/canton7/Stylet/wiki/3.-The-Taskbar
            events.Subscribe(this);
        }

        #endregion 생성자 

        #region 기본 메소드

        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();

            OpenLogin();   // 로그인 화면 출력 

            EventAggregator.Subscribe(this);   // EventAggregator 구독 (Subscribe)

            InitSetting(); // 화면 초기화 
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();

            //DoDeactivate();

            EventAggregator.Unsubscribe(this);
        }

        // TODO : OnClose 메서드 로직 추후 수정 예정 (2023.07.04 jbh)
        protected override void OnClose()
        {
            base.OnClose();
            // EventAggregator.Unsubscribe();
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
        }

        #endregion 기본 메소드      

        #region OpenLogin

        public void OpenLogin()
        {
            // TODO : 오류 발생시 로그인 생성 로직 수정 예정 (2023.08.17 jbh)
            // this.Container.Get<LoginVM>().ToShowData();
            //bool result = this.Container.Get<LoginVM>().ToShowData().IsLogin;

            // TODO : 오류 발생 또는 로그인 실패시 종료 로직 필요시 수정 예정 (2023.08.17 jbh)
            // if (!result) return;  // 오류 발생, 로그인 실패, 프로그램 종료(닫기 "X" 버튼) 시 종료 

            //bool result = this.Container.Get<LoginVM>().ToShowData().IsExit;
            //if (result) Application.Current.Shutdown();
            var vm = this.Container.Get<LoginVM>().ToShowData();  // 로그인 화면 생성 및 결과 리턴

            // TODO : 닫기("X") 버튼 클릭 시 프로그램 종료 로직 구현 (2023.08.18 jbh)
            if (!vm.IsLogin && vm.IsExit) Application.Current.Shutdown();
        }

        #endregion OpenLogin

        #region InitSetting

        private void InitSetting()
        {
            try
            {
                var vms = Container.GetAll<IPageBase>().ToArray();   // <IPageBase> 인터페이스를 상속 받는 모든 클래스 요소들을 컨테이너에서 가져와서(Container.GetAll)

                this.Items.AddRange(vms);
                var first = this.Items.Where(it => string.Equals(it.GetType().Name, nameof(ShellVM))).FirstOrDefault();

                if (first != null)
                {
                    // "ShellVM" 뷰모델과 연동된 초기화면 뷰(ShellV)를 열어준다.
                    this.ActivateItem(first);
                }
            }
            catch (Exception e)
            {
                Log.Logger.Information("오류 :" + e.Message);
                // Application.Current.Shutdown();
            }
        }

        #endregion InitSetting

        #region Display

        private void Display(ChangeViewModelMsg message)
        {
            switch (message.VMName)
            {
                case ShellVMName:
                    // TODO: this.ShellVM 객체 생성 안 하고도 ShellVM.cs 에서 이벤트 메서드(ShowMessageBox, DoSomething)가 실행 되도록 로직 수정하기(2023.07.07 jbh)
                    this.ShellVM = (ShellVM)message.obj;
                    visibleShellVM = Visibility.Visible;
                    visibleTestVM = Visibility.Collapsed;
                    visibleTestExpVM = Visibility.Collapsed;
                    visibleTestLazyTreeVM = Visibility.Collapsed;
                    visibleExpVM = Visibility.Collapsed;
                    break;

                case TestVMName:
                    // TODO: this.TestVM 객체 생성 안 하고도 TestVMl.cs 에서 이벤트 메서드(ShowMessageBox, DoSomething)가 실행 되도록 로직 수정하기(2023.07.07 jbh)
                    this.TestVM = (TestVM)message.obj;
                    visibleShellVM = Visibility.Collapsed;
                    visibleTestVM = Visibility.Visible;
                    visibleTestExpVM = Visibility.Collapsed;
                    visibleTestLazyTreeVM = Visibility.Collapsed;
                    visibleExpVM = Visibility.Collapsed;
                    break;

                case TestExpVMName:
                    // TODO: this.CobimExplorerVM 객체 생성 안 하고도 CobimExplorerVM.cs 에서 이벤트 메서드(ShowMessageBox, DoSomething)가 실행 되도록 로직 수정하기(2023.07.07 jbh)
                    this.TestExplorerVM = (TestExplorerVM)message.obj;
                    visibleShellVM = Visibility.Collapsed;
                    visibleTestVM = Visibility.Collapsed;
                    visibleTestExpVM = Visibility.Visible;
                    visibleTestLazyTreeVM = Visibility.Collapsed;
                    visibleExpVM = Visibility.Collapsed;
                    break;

                case TestLazyTrVMName:
                    // TODO: this.TestLazyTreeVM 객체 생성 안 하고도 TestLazyTreeVM.cs 에서 이벤트 메서드(ShowMessageBox, DoSomething)가 실행 되도록 로직 수정하기(2023.07.07 jbh)
                    this.TestLazyTreeVM = (TestLazyTreeVM)message.obj;
                    visibleShellVM = Visibility.Collapsed;
                    visibleTestVM = Visibility.Collapsed;
                    visibleTestExpVM = Visibility.Collapsed;
                    visibleTestLazyTreeVM = Visibility.Visible;
                    visibleExpVM = Visibility.Collapsed;
                    break;

                case ExpVMName:
                    // TODO: this.ExplorerVM 객체 생성 안 하고도 ExplorerVM.cs 에서 이벤트 메서드(ShowMessageBox, DoSomething)가 실행 되도록 로직 수정하기(2023.07.07 jbh)
                    this.ExplorerVM = (ExplorerVM)message.obj;
                    visibleShellVM = Visibility.Collapsed;
                    visibleTestVM = Visibility.Collapsed;
                    visibleTestExpVM = Visibility.Collapsed;
                    visibleTestLazyTreeVM = Visibility.Collapsed;
                    visibleExpVM = Visibility.Visible;
                    break;
            }
        }

        #endregion Display

        #region Handle

        /// <summary>
        /// 받은 메세지로 동작한다.
        /// </summary>
        /// <param name="message"></param>
        public void Handle(ChangeViewModelMsg message)
        {
            foreach (var item in message.Items)
            {
                if (item.GetType().Name.Equals(message.VMName) && message?.VMName != null)
                {
                    // TODO : 아래 테스트 코드 추후 삭제 예정 (2023.07.26 jbh)
                    // var changeVM = message.Items.Where(it => string.Equals(it.GetType().Name, message.VMName)).FirstOrDefault();
                    // this.ActivateItem(changeVM);

                    Display(message);
                }
            }

            //foreach(var item in this.Items)
            //{
            //    if(item.GetType().Name.Equals(message.VMName) && message?.VMName != null)
            //    {
            //        this.ActivateItem(item);

            //        bool change = true;

            //        Display(change);
            //    }

            //}
        }

        #endregion Handle

        // TODO : 추후 필요 없을시 삭제 예정 (2023.07.13 jbh)
        //public void ChangeExplorerViewModel(string vmName)
        //{
        //    //var vms = Container.GetAll<IPageBase>().ToArray();   // <IPageBase> 인터페이스를 상속 받는 모든 클래스 요소들을 컨테이너에서 가져와서(Container.GetAll)

        //    //this.Items.Clear();
        //    //this.Items.AddRange(vms);
        //    // var changeVM = this.Items.Where(it => string.Equals(it.GetType().Name, nameof(ExplorerViewModel))).FirstOrDefault();
        //    var changeVM = this.Items.Where(it => string.Equals(it.GetType().Name, vmName)).FirstOrDefault();

        //    //var changeVM = this.Items.Where(it => it.GetType().Equals(TestViewModel.GetType())).FirstOrDefault();

        //    if (changeVM != null)
        //    {
        //        // "ShellViewModel" 뷰모델과 연동된 초기화면 뷰(ShellView)를 열어준다.
        //        this.ActivateItem(changeVM);
        //    }
        //}



        #region TEST

        /// <summary>
        /// 버튼 테스트 
        /// </summary>
        // 메시지 박스 출력 참고 URL - https://gigong.tistory.com/52
        //public void DoSomething()
        //{
        //    // Debug.WriteLine("DoSomething called");
        //    MessageBox.Show("Button Test");
        //}

        #endregion TEST

        #region Sample

        #endregion Sample
    }
}
