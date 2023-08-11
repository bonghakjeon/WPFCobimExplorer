using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CobimExplorer.Interface.Page;

namespace CobimExplorer.Services.Page
{
    // TODO : 구현시 참고 유튜브 영상 URL -  https://youtu.be/2FPFgW0xVB0 (2023.07.12 jbh)

    public abstract class PageBase : Screen// , ICommand
    {
        #region 프로퍼티 

        //public event EventHandler CanExecuteChanged;

        #endregion 프로퍼티 

        #region 생성자

        public PageBase(IContainer container)
        {

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
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
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

        //public bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        //public abstract void Execute(object parameter);

        //protected void OnCanExecutedChanged()
        //{
        //    CanExecuteChanged?.Invoke(this, new EventArgs());
        //}

        #endregion 기본 메소드        
    }
}
