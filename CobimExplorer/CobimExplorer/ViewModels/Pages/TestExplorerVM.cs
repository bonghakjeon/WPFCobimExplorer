using Stylet;
using StyletIoC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using CobimExplorer.Common.Extensions;
using CobimExplorer.Interface.Page;
using CobimExplorer.Message;
using CobimExplorer.Services.Page;

namespace CobimExplorer.ViewModels.Pages
{
    public class TestExplorerVM : PageBase, IPageBase, IHandle<ChangeViewModelMsg>
    {
        #region 프로퍼티 

        // TODO : 테스트 코드 추후 삭제 예정 (2023.07.14 jbh)
        //public TreeView GroupView = new TreeView();

        public IContainer Container { get; set; }

        [Inject]
        public IEventAggregator EventAggregator { get; set; }

        //public BindableCollection<TreeViewItem> DepthDatas { get; } = new BindableCollection<TreeViewItem>();

        //TODO : 아래 BindableCollection 구현 예시 참고해서 추후 BindableCollection 구현 예정 (2023.07.26 jbh)
        //public BindableCollection<ProgMenuView> DepthDatas { get; } = new BindableCollection<ProgMenuView>();

        #endregion 프로퍼티

        #region 생성자

        public TestExplorerVM(IEventAggregator events, IContainer container) : base(container)
        {
            Container = container;

            // TODO : this.EventAggregator = events; 코드 추가 (2023.07.24 jbh)
            // 참고 URL - https://github.com/canton7/Stylet/wiki/3.-The-Taskbar
            this.EventAggregator = events;
            // TODO : 테스트 코드 추후 삭제 예정 (2023.07.14 jbh)
            //InitTreeViewText();
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

            var vm = Container.Get<TestExplorerVM>();

            string VMName = vm.GetViewModelName();

            var msg = new ChangeViewModelMsg(this, VMName);
            EventAggregator.PublishOnUIThread(msg);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        public void Handle(ChangeViewModelMsg message)
        {
            // throw new NotImplementedException();
        }

        #endregion 기본 메소드

        #region InitTreeViewText

        // TODO : TreeView 데이터 출력 테스트 코드 추후 삭제 예정 (2023.07.14 jbh)
        // TreeView 참고 URL - http://inasie.tistory.com/19 
        // TreeView 참고 2 URL - https://insurang.tistory.com/383 
        /// <summary>
        /// TreeView 데이터 출력 
        /// </summary>
        //private void InitTreeViewText()
        //{
        //    GroupView.Items.Clear();    // Collection 초기화

        //    // foreach (string str in Directory.GetDirectories(""))   // 특정폴더
        //    foreach (string str in Directory.GetLogicalDrives())   // 루트폴더
        //    {
        //        try
        //        {
        //            TreeViewItem item = new TreeViewItem();
        //            item.Header = str;
        //            item.Tag = str;
        //            item.Expanded += new RoutedEventHandler(item_Expanded);   // 노드 확장시 추가

        //            GroupView.Items.Add(item);

                    
        //            GetSubDirectories(item);


        //        }

        //        catch (Exception except)
        //        {
        //            // MessageBox.Show(except.Message);   // 접근 거부 폴더로 인해 주석처리
        //        }
        //    }
        //}

        #endregion InitTreeViewText

        #region GetSubDirectories (서브 디렉토리)

        // TODO : TreeView 데이터 출력 테스트 코드 추후 삭제 예정 (2023.07.14 jbh)
        /// <summary>
        /// 서브 디렉토리
        /// </summary>
        /// <param name="itemParent"></param>
        //private void GetSubDirectories(TreeViewItem itemParent)
        //{
        //    if (itemParent == null) return;
        //    if (itemParent.Items.Count != 0) return;

        //    try
        //    {
        //        string strPath = itemParent.Tag as string;
        //        DirectoryInfo dInfoParent = new DirectoryInfo(strPath);
        //        foreach (DirectoryInfo dInfo in dInfoParent.GetDirectories())
        //        {
        //            TreeViewItem item = new TreeViewItem();
        //            item.Header = dInfo.Name;
        //            item.Tag = dInfo.FullName;
        //            item.Expanded += new RoutedEventHandler(item_Expanded);
        //            itemParent.Items.Add(item);
        //        }
        //    }

        //    catch (Exception except)
        //    {
        //        // MessageBox.Show(except.Message);   // 접근 거부 폴더로 인해 주석처리
        //    }
        //}

        #endregion GetSubDirectories (서브 디렉토리)

        #region item_Expanded

        /// <summary>
        /// 트리확장시 내용 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void item_Expanded(object sender, RoutedEventArgs e)
        //{
        //    TreeViewItem itemParent = sender as TreeViewItem;
        //    if (itemParent == null) return;
        //    if (itemParent.Items.Count == 0) return;
        //    foreach (TreeViewItem item in itemParent.Items)
        //    {
        //        GetSubDirectories(item);
        //    }
        //}

        #endregion item_Expanded

        #region Sample

        #endregion Sample
    }
}
