using Stylet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobimExplorer.Interface.Page;

namespace CobimExplorer.Message
{
    public class ChangeViewModelMsg 
    {
        #region 프로퍼티 

        public string VMName { get; set; }

        public ObservableCollection<IPageBase> Items 
        {
            get 
            {
                // 싱글톤 패턴 
                if (_Items == null)
                {
                    _Items = new ObservableCollection<IPageBase>();
                }

                return _Items;
            }
            set => _Items = value;
        }

        public ObservableCollection<IPageBase> _Items;


        public object obj { get; set; }

        #endregion 프로퍼티 

        public ChangeViewModelMsg(object o, string ViewModelName)
        {
            this.Items.Add((IPageBase)o);
            obj = o;
            VMName = ViewModelName;
        }
    }
}
