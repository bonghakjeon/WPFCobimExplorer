namespace Keycloak_Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            Keycloak.Keycloak_Config c = new Keycloak.Keycloak_Config
            {
                //realm = Environment.GetEnvironmentVariable("keycloak_myclient_realm"),
                //auth_server_url = Environment.GetEnvironmentVariable("keycloak_myclient_auth_server_url"),
                //resource = Environment.GetEnvironmentVariable("keycloak_myclient_resource"),
                //credentials_secret = Environment.GetEnvironmentVariable("keycloak_myclient_credentials_secret")
                realm = "COBIM",
                //auth_server_url = "http://mysso-keycloak-1056104840.ap-northeast-2.elb.amazonaws.com:8080/",
                auth_server_url = "http://mysso.cobim.kr:8080/",
                resource = "mycde",  //<--client_id
                credentials_secret = ""
            };

            if (c.realm != null)
            {
                Keycloak.Keycloak_Access_Token token = Keycloak.Keycloak.login(client, c, textBox1.Text, textBox2.Text);
                Console.WriteLine("Access Token = " + token.access_token);

                if (token.error == null)
                {
                    Console.WriteLine("\nLogin is OK...");

                    loginResult.Text = "Login Result = Ok!";
                }
                else
                {
                    loginResult.Text = "Login Result = Not Ok!";

                    Console.WriteLine("Login is NOT OK...");
                    Console.WriteLine("Error = " + token.error);
                }
            }
        }



    }
}