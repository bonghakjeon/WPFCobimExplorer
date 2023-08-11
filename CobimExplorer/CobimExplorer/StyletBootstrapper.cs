using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Serilog;
using Stylet;
using StyletIoC;
using Stylet.Logging;
using System;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using CobimExplorer.ViewModels;
using CobimExplorer.ViewModels.Pages;
using CobimExplorer.Interface.Page;
using CobimExplorer.Common.LogManager;

namespace CobimExplorer
{
    public class StyletBootstrapper : Bootstrapper<MainVM>
    {
        //TODO : 추후 필요시 사용 예정 (2023.07.03 jbh)
        //private string DeviceConfigPath = PathInfo.DeviceConfigPath;

        /// <summary>
        /// Gets or sets the Bootstrapper's IoC container. This is created after ConfigureIoC has been run.
        /// </summary>
        // protected IContainer Container { get; set; }

        /// <summary>
        /// 1. 시작 시
        /// </summary>
        protected override void OnStart()
        {
            // This is called just after the application is started, but before the IoC container is set up.
            // Set up things like logging, etc
            Stylet.Logging.LogManager.LoggerFactory = name => new ExplorerLogger(name); // LogManager 구성   참고 URL - https://github.com/canton7/Stylet/wiki/Logging
            Stylet.Logging.LogManager.Enabled = true;                                   // LogManager 활성화 참고 URL - https://github.com/canton7/Stylet/wiki/Logging

            // TODO : StyletBootstrapper.cs 이벤트 메서드 OnStart 추후 로직 보완 예정 (2023.07.03 jbh)
            base.OnStart();
#if DEBUG
            bool isDebug = true;
#else
            isDebug = false;
#endif
            if (!isDebug)
            {
                // <Guid("1FDFD875-A485-425E-89BE-7C480B57AA36")>
                AppCenter.Start("1FDFD875-A485-425E-89BE-7C480B57AA36", typeof(Analytics), typeof(Crashes));
            }
        }

        /// <summary>
        /// 2. 컨테이너 초기화
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
            // Bind your own types. Concrete types are automatically self-bound.
            //builder.Bind<IMyInterface>().To<MyType>();

            base.ConfigureIoC(builder);
            // builder.Assemblies.Add(typeof(MainVM).Assembly);    // Autobind 및 ToAllImplementatinos에서 검색한 어셈블리 목록을 가져오거나 설정

            // TODO: 새로 추가할 싱글톤 객체가 존재할시 아래처럼 싱글톤 객체로 설정하기 (2023.07.10 jbh)
            // 싱글톤 참고 URL - https://morit.tistory.com/5
            // 싱글톤 참고 2 URL - https://github.com/canton7/Stylet/wiki/StyletIoC-Configuration
            // 싱글톤 객체 설정 또 다른 예시 - builder.Bind<IConfigurationManager>().ToFactory(container => new ConfigurationManager()).InSingletonScope();
            builder.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            builder.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
            // builder.Bind<MainVM>().ToSelf().InSingletonScope();

            // TODO : ShellViewModel, TestViewModel 말고도 "셀프 바인딩"할 뷰모델 찾아서 아래처럼 구현하기 (2023.07.10 jbh)
            // 셀프 바인딩 - StyletIoC에게 CobimExplorer.sln 솔루션이 MainVM, ShellViewModel, TestViewModel을 요청할 때마다 모든 종속 항목이 이미 채워진 ShellViewModel, TestViewModel을 제공 해주는 기능.
            // 참고 URL - https://github.com/canton7/Stylet/wiki/StyletIoC-Configuration
            // 셀프 바인딩 예시 1 - builder.Bind<ShellViewModel>().ToSelf();
            // 셀프 바인딩 예시 2 - builder.Bind<ShellViewModel>().To<ShellViewModel>();
            builder.Bind<MainVM>().ToSelf();
            builder.Bind<ShellVM>().ToSelf();
            builder.Bind<TestVM>().ToSelf();
            builder.Bind<TestExplorerVM>().ToSelf();
            builder.Bind<TestLazyTreeVM>().ToSelf();
            builder.Bind<ExplorerVM>().ToSelf();

            // TODO: 추후 필요시 입력 받은 키 값을 전달 (Clear, Back space, 숫자 키 등등.... ) 받는 인터페이스 IPageBase 멤버  메서드 "OnReceivePosKeyUp" 구현 예정 (2023.07.03 jbh)
            builder.Bind<IPageBase>().ToAllImplementations();

            //string strDeviceConfig = string.Empty;

            //TODO : 객체 "config" 바인딩 하는 로직 추후 필요시 사용 예정(Rest API로 부터 return 받는 json 객체 deserialize 할 때 사용할 클래스 "A" 구현시 참고) (2023.07.03 jbh)
            //if (File.Exists(DeviceConfigPath)) strDeviceConfig = File.ReadAllText(DeviceConfigPath);
            //else
            //{
            //    strDeviceConfig = JsonSerializer.Serialize(new DeviceConfig(), new JsonSerializerOptions() { });
            //    using (File.Create(DeviceConfigPath))
            //    {
            //        //Log   
            //    }
            //    File.WriteAllText(DeviceConfigPath, strDeviceConfig);
            //}


            //var config = JsonSerializer.Deserialize<DeviceConfig>(strDeviceConfig, new JsonSerializerOptions() { });
            //builder.Bind<DeviceConfig>().ToInstance(config);
        }

        /// <summary>
        /// 3. 컨테이너 초기화 후 설정
        /// <see cref="ConfigureIoC(IStyletIoCBuilder)"/> 이후
        /// </summary>
        protected override void Configure()
        {
            // Perform any other configuration before the application starts
            // This is called after Stylet has created the IoC container, so this.Container exists, but before the
            // Root ViewModel is launched.
            // Configure your services, etc, in here
            // 참고 URL - https://github.com/canton7/Stylet/wiki/The-ViewManager
            base.Configure();
            var vm = this.Container.Get<ViewManager>();
            vm.ViewNameSuffix = "V";
            vm.ViewModelNameSuffix = "VM";
            vm.NamespaceTransformations = new Dictionary<string, string>()
            {
                ["CobimExplorer.ViewModels"] = "CobimExplorer.Views"
            };
        }

        /// <summary>
        /// 4. 실행 후
        /// </summary>
        protected override void OnLaunch()
        {
            // This is called just after the root ViewModel has been launched
            // Something like a version check that displays a dialog might be launched from here
            base.OnLaunch();
        }

        /// <summary>
        /// 5. 종료 시
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Called on Application.Exit
            //SystemCurrentInfo.Instance.Dispose();
            base.OnExit(e);
        }

        /// <summary>
        /// 6. 예외
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            // Called on Application.DispatcherUnhandledException
            base.OnUnhandledException(e);

            // 인터넷 연결 되어있을 때에 Exception 수집 
            Crashes.TrackError(e.Exception);
#if DEBUG
            var isRelease = false;
#else
            isRelease = true;
#endif
            if (isRelease)
            {
                e.Handled = true;   //  프로그램이 죽지 않고 진행 가능 하도록 
            }
        }
    }
}
