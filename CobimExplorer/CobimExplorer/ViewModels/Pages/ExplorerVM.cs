using Stylet;
using StyletIoC;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using CobimExplorer.Message;
using CobimExplorer.Services.Page;
using CobimExplorer.Interface.Page;
using CobimExplorer.Common.Extensions;

namespace CobimExplorer.ViewModels.Pages
{
    public class ExplorerVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        #endregion 프로퍼티 

        #region 생성자

        public ExplorerVM(IEventAggregator events, IContainer container) : base(container)
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

        //protected override void OnActivate()
        //{
        //    base.OnActivate();

        //    // TODO : TreeView 데이터 출력 테스트 코드 추후 삭제 예정 (2023.07.14 jbh)
        //    // InitTreeViewText();

        //    var vm = Container.Get<ExplorerVM>();

        //    string VMName = vm.GetViewModelName();

        //    var msg = new ChangeViewModelMsg(this, VMName);
        //    EventAggregator.PublishOnUIThread(msg);
        //}

        //protected override void OnDeactivate()
        //{
        //    base.OnDeactivate();
        //}

        public void Handle(ChangeViewModelMsg message)
        {
            // throw new NotImplementedException();
        }

        #endregion 기본 메소드

        #region Sample

        #endregion Sample
    }
}
