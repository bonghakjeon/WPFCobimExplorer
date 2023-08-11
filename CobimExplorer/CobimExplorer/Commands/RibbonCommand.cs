using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobimExplorer.Services.Page;
using CobimExplorer.ViewModels;

namespace CobimExplorer.Commands
{
    // TODO : 구현시 참고 유튜브 영상 URL -  https://youtu.be/2FPFgW0xVB0 (2023.07.12 jbh)
    public class RibbonCommand : Command
    {
        private readonly MainVM _mainViewModel;

        //public string vmName { get => _vmName; set { _vmName = value; NotifyOfPropertyChange(); } }
        //private string _vmName = string.Empty;

        public RibbonCommand(MainVM mainVM, IContainer container)
        {
            _mainViewModel = mainVM;
        }

        public override void Execute(object parameter)
        {
            //_mainViewModel.ChangeExplorerViewModel();
            var changeVM = _mainViewModel.Items.Where(it => string.Equals(it.GetType().Name, parameter.ToString())).FirstOrDefault();

            //var changeVM = this.Items.Where(it => it.GetType().Equals(TestViewModel.GetType())).FirstOrDefault();

            if (changeVM != null)
            {
                // changeVM에 할당된 변경할 뷰모델 활성화
                _mainViewModel.ActivateItem(changeVM);
            }
        }
    }
}
