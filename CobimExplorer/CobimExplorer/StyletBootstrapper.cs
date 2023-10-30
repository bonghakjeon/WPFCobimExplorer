using Serilog;
using Stylet;
using StyletIoC;
using Stylet.Logging;
using System;
using System.Text;
using System.Reflection;
using System.Net.Http;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using CobimExplorer.ViewModels;
using CobimExplorer.ViewModels.Pages;
using CobimExplorer.Interface.Page;
using CobimExplorer.Common.LogManager;
using CobimExplorer.Rest.Api.CobimBase.User;
using CobimExplorer.ViewModels.Windows.Login;
using CobimExplorer.Models.User;

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
            // Stylet.Logging.LogManager.LoggerFactory = name => new Logger(name); // LogManager 구성   참고 URL - https://github.com/canton7/Stylet/wiki/Logging
            // Stylet.Logging.LogManager.Enabled = true;                                   // LogManager 활성화 참고 URL - https://github.com/canton7/Stylet/wiki/Logging

            // TODO : StyletBootstrapper.cs 이벤트 메서드 OnStart 추후 로직 보완 예정 (2023.07.03 jbh)
            base.OnStart();
            bool isDebug = true;
#if DEBUG   // 디버그 모드인 경우 
#else       // Release 모드인 경우
            isDebug = false;
#endif
            if (!isDebug)
            {
                // <Guid("1FDFD875-A485-425E-89BE-7C480B57AA36")>
                AppCenter.Start("1FDFD875-A485-425E-89BE-7C480B57AA36", typeof(Analytics), typeof(Crashes));
            }

            // TODO : Stylet LogManager 클래스 사용 안하고 Serilog 이용해서 로그 기록 및 로그 파일 생성 (2023.10.10 jbh)
            // 참고 URL - https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-C-Serilog-%EC%82%AC%EC%9A%A9%ED%95%98%EC%97%AC-%EB%A1%9C%EA%B7%B8-%EB%82%A8%EA%B8%B0%EA%B8%B0
            // 참고 2 URL - https://blog.naver.com/PostView.naver?blogId=wolfre&logNo=221713399852&parentCategoryNo=&categoryNo=26&viewDate=&isShowPopularPosts=true&from=search
            // 참고 3 URL - https://www.reddit.com/r/dotnet/comments/qhwa4i/serilog_remove_files_older_than_30_days/
            // 참고 4 URL - https://github.com/serilog/serilog-sinks-file#rolling-policies

            // TODO : 에러 로그를 메일로 보내는 로직(.WriteTo.Email) 필요시 추후 구현 예정 (2023.10.10 jbh)
            // 참고 URL - https://github.com/serilog/serilog-sinks-email
            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()   // 로그 남기는 기준 ex) Verbose 이상 다 남겨야 함.
                                          // Verbose(0) => Debug => Information => Warning => Error => Fatal(5)
                .WriteTo.Console()        // 콘솔 파일에도 로그 출력
                .WriteTo.Debug()
                // 로그 파일 이름을 날짜 형식(예)"CobimExplorer_20231016.log" 으로 로그 파일 생성
                .WriteTo.File(
                // Assembly.GetEntryAssembly().GetName().Name은 프로젝트 파일 "CobimExplorer"를 의미
                // $"Logs\\{Assembly.GetEntryAssembly().GetName().Name}\\{Assembly.GetEntryAssembly().GetName().Name}_{DateTime.Now.ToString("yyyyMMdd")}.log",
                path: $"Logs\\{Assembly.GetEntryAssembly().GetName().Name}\\{Assembly.GetEntryAssembly().GetName().Name}_.log",
                Serilog.Events.LogEventLevel.Verbose,
                // TODO : 로그 파일에 날짜를 수동으로 지정할 필요 없이 rollingInterval: RollingInterval.Day, 사용해서 로그파일 이름 언더바(_)옆에 로그 생성된 날짜가 추가되도록 구현
                //        (이렇게 구현해야 아래 프로퍼티 "retainedFileCountLimit" 사용해서 날짜가 지난 로그파일 삭제 처리 할 수 있음.) (2023.10.16 jbh) 
                // 참고 URL - https://stackoverflow.com/questions/32108148/serilog-rollingfile
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,   // 로그 파일 최대 사이즈 1GB  null 옵션 가능
                // TODO : 테스트 용 로그 파일 갯수 2개(2일치 로그) 설정(2일 이상 지난 오래된 로그는 삭제 처리)
                //        2일 지난 로그 파일 정상적으로 삭제 처리시 아래 주석 처리된 소스코드 "retainedFileCountLimit: 62,"로 다시 구현해야 함. (2023.10.11 jbh)
                // 참고 URL - https://stackoverflow.com/questions/44577336/how-do-i-automatically-tail-delete-older-logs-using-serilog-in-a-net-wpf-appl
                retainedFileCountLimit: 2,  
                // retainedFileCountLimit: 62,  // 로그 파일 갯수 62개(2달치 로그) 설정(2달 지난 오래된 로그는 삭제 처리) - 기본 31개 설정 가능, null 옵션 가능
                encoding: Encoding.UTF8
                )
                .CreateLogger();
            //LogManager.LoggerFactory = name => new SerilogStyletAdapter(log, name);
            LogManager.Enabled = false;   // Stylet LogManager 비활성화 (사용 안함.)
            Log.Logger = log;             // Serilog를 CobimExplorer에서 사용할 수 있도록 설정 (전역 로그)

            // 로그 레벨 기록 예시
            //Log.Information($"Info name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
            //Log.Debug($"Debug name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
            //Log.Verbose($"Verbose name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
            //Log.Warning($"Warning name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
            //Log.Error($"Error name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
            //Log.Fatal($"Fatal name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");

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
            builder.Bind<HttpClient>().ToSelf().InSingletonScope();
            // builder.Bind<LoginVM>().ToSelf().InSingletonScope();
            //builder.Bind<LoginHelper.LoginPack>().ToSelf().InSingletonScope();
            //builder.Bind<LoginHelper.Login_Access_Token>().ToSelf().InSingletonScope();
            //builder.Bind<UserInfoView>().ToSelf().InSingletonScope();
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

            bool isRelease = false;
#if DEBUG   // 디버그 모드일 경우 
#else       // Release 모드일 경우
            isRelease = true;
#endif
            if (isRelease)
            {
                e.Handled = true;   //  프로그램이 죽지 않고 진행 가능 하도록 
            }
        }
    }
}
