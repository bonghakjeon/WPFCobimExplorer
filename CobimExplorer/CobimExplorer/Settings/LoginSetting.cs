using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobimExplorerNet;
using CobimExplorer.Rest.Api.CobimBase.User;

namespace CobimExplorer.Settings
{
    public class LoginSetting : BindableBase
    {
        /// <summary>
        /// 서버 주소 
        /// </summary>
        public string ServerPath
        {
            get => this._ServerPath;
            set
            {
                this._ServerPath = value;
                this.Changed(nameof(ServerPath));
            }
        }
        private string _ServerPath = LoginHelper.auth_server_url;

        // TODO : _SiteViewCode 필요시 구현 예정(2023.08.30 jbh)
        //public string SiteViewCode
        //{
        //    get => this._SiteViewCode;
        //    set
        //    {
        //        this._SiteViewCode = value;
        //        this.Changed(nameof(SiteViewCode));
        //    }
        //}
        //private string _SiteViewCode = "1";

        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string LoginId
        {
            get => this._LoginId;
            set
            {
                this._LoginId = value;
                this.Changed(nameof(LoginId));
            }
        }
        // TODO : "LoginHelper.testId" 필요시 사용 예정 (2023.08.30 jbh)
        //private string _LoginId = LoginHelper.testId;
        private string _LoginId;

        /// <summary>
        /// 로그인 비밀번호
        /// </summary>
        public string Password
        {
            get => this._Password;
            set
            {
                this._Password = value;
                this.Changed(nameof(Password));
            }
        }
        // TODO : "LoginHelper.testPassword" 필요시 사용 예정 (2023.08.30 jbh)
        //private string _Password = LoginHelper.testPassword;
        private string _Password;

        /// <summary>
        /// 서버 주소 리스트 
        /// </summary>
        public List<string> ServerPaths
        {
            get => this._ServerPaths;
            set
            {
                this._ServerPaths = value;
                this.Changed(nameof(ServerPaths));
            }
        }
        private List<string> _ServerPaths = new List<string>()
        {
            LoginHelper.auth_server_url
        };

        /// <summary>
        /// 로그인 토큰 값
        /// </summary>
        public LoginHelper.Login_Access_Token Token
        {
            get => this._Token;
            set
            {
                this._Token = value;
                this.Changed(nameof(Token));
            }
        }
        private LoginHelper.Login_Access_Token _Token = new LoginHelper.Login_Access_Token();


        // TODO : 마지막 로그인 시점 "_DtLastLogIn" 필요시 추후 구현 예정 (2023.08.30 jbh)
        //public DateTime? DtLastLogIn
        //{
        //    get => this._DtLastLogIn;
        //    set
        //    {
        //        this._DtLastLogIn = value;
        //        this.Changed(nameof(DtLastLogIn));
        //    }
        //}
        //private DateTime? _DtLastLogIn = new DateTime?(DateTime.Now);

    }
}
