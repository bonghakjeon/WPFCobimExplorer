using Stylet;
using StyletIoC;
using System;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using CobimExplorer.Commands;
using System.Threading;
using CobimExplorer.ViewModels.Box;
using CobimExplorer.Services.Page;
using CobimExplorer.Interface.Page;
using CobimExplorer.Message;
using System.Collections.Generic;
using CobimExplorer.Common.Extensions;

namespace CobimExplorer.ViewModels.Pages
{

    public class ShellVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        #region TEST

        public string TestName { get => _TestName; set { _TestName = value; NotifyOfPropertyChange(); } }
        private string _TestName = string.Empty;

        public bool IsTestOpenPopup { get => _IsTestOpenPopup; set => SetAndNotify(ref _IsTestOpenPopup, value); }
        private bool _IsTestOpenPopup;

        #endregion TEST

        #endregion 프로퍼티 

        // public ShellViewModel TestViewModel { get; set; }

        // 참고 URL - https://velog.io/@esology/CWPF-MVVM-Command-Binding%EC%9D%84-%ED%86%B5%ED%95%B4-%EB%B2%84%ED%8A%BC-%EC%83%81%ED%83%9C-%EC%A0%9C%EC%96%B4%ED%95%98%EA%B8%B0
        //public WpfCommand CmdClick => Cmds.GetValueOrInsert(new WpfCommand()
        //{
        //    ExecuteAction = _ => DoSomething()
        //});

        public ICommand CmdClick { get; set; }

        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }


        //public ICommand MsgCommand { get; set; }

        // TODO : RelayCommand 클래스 프로퍼티 구현 (2023.07.13 jbh)
        // 참고 URL - https://youtu.be/s7pt3EkDyq4
        // public RelayCommand MsgCommand => new RelayCommand(execute => DoSomething());

        // public RelayCommand MsgCommand { get; set; }

        #region 생성자

        //public ShellViewModel() 
        //{
        //    // TODO: Button 컨트롤 Command Binding RelayCommand 구현 (2023.07.03 jbh)
        //    // 참고 URL - https://itpro.tistory.com/90
        //    CmdClick = new RelayCommand(DoSomething, CanDoSomething);
        //}


        //public ShellViewModel(IContainer container) 
        //{
        //    TestViewModel = this;
        //}


        public ShellVM(IEventAggregator events, IContainer container) : base(container) 
        {
            Container = container;

            // TODO : this.EventAggregator = events; 코드 추가 (2023.07.24 jbh)
            // 참고 URL - https://github.com/canton7/Stylet/wiki/3.-The-Taskbar
            this.EventAggregator = events;
            // MsgCommand = new RelayCommand(execute => DoSomething()); 
            // MsgCommand = new ViewModelCommand(ExecuteMethod, CanExecuteMethod);
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

            var vm = Container.Get<ShellVM>();
            string VMName = vm.GetViewModelName();

            var msg = new ChangeViewModelMsg(this, VMName);
            EventAggregator.PublishOnUIThread(msg);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        #endregion 기본 메소드 

        #region TEST

        //private void Timer_Tick(object sender, EventArgs e)
        //{

        //}

        public void ShowMessageBox(object param)
        {
            // Debug.WriteLine("DoSomething called");
            //MessageBox.Show("ShellViewModel called");
            //new CommonMsgBoxVM(container).Set("다운로드 실패", e.Message).ShowDialog();
            // EventAggregator.PublishOnUIThread(msg);
        }


        public void DoSomething(object param)
        {
            // Debug.WriteLine("DoSomething called");
            MessageBox.Show("ShellViewModel called");
        }

        private bool CanDoSomething(object param)
        {
            return true;
        }

        public void Handle(ChangeViewModelMsg message)
        {
            // throw new NotImplementedException();
        }

        //public override void Execute(object parameter)
        //{
        //    // throw new NotImplementedException();
        //}

        private void ExecuteMethod(object obj)
        {
            MessageBox.Show("코드비하인드가 아닌 Command ExecuteMethod");
        }

        private bool CanExecuteMethod(object arg)
        {
            return true;
        }

        #endregion TEST
    }
}
