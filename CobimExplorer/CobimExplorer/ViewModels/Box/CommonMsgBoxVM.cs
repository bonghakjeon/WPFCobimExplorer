using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobimExplorer.Interface.Page;
using CobimExplorer.Services.Page;

namespace CobimExplorer.ViewModels.Box
{
    // TODO : MessageBox 필요시 구현 예정(2023.07.26 jbh)
    public class CommonMsgBoxVM : PageBase, IPageBase
    {
        public CommonMsgBoxVM(IContainer conatainer) : base(conatainer)
        {

        }

        //public override void Execute(object parameter)
        //{
        //    // throw new NotImplementedException();
        //}
    }
}
