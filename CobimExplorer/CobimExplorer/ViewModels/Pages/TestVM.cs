using System;
using System.Collections.Generic;
using StyletIoC;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobimExplorer.Interface.Page;
using CobimExplorer.Services.Page;
using System.Windows;
using CobimExplorer.Message;
using Stylet;
using CobimExplorer.Common.Extensions;

namespace CobimExplorer.ViewModels.Pages
{
    public class TestVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        #region TEST

        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        #endregion TEST

        #endregion 프로퍼티 


        #region 생성자

        public TestVM(IEventAggregator events, IContainer container) : base(container)
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
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            var vm = Container.Get<TestVM>();

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
            MessageBox.Show("TestViewModel called");
        }

        private bool CanDoSomething(object param)
        {
            return true;
        }

        public void Handle(ChangeViewModelMsg message)
        {
            //throw new NotImplementedException();
        }

        //public override void Execute(object parameter)
        //{
        //    // throw new NotImplementedException();
        //}

        #endregion TEST
    }
}
