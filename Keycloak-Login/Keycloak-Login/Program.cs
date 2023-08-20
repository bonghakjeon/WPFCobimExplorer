using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
using FS.Keycloak.RestApiClient.Model;
using Keycloak_Login;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static async Task Main(string[] args)
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
                Keycloak.Keycloak_Access_Token token = Keycloak.Keycloak.login(client, c, "cdeuser", "Imbu@!7410");
                //Console.WriteLine("Access Error = " + token.error);
                //Console.WriteLine("Access Error = " + token.error_description);
                Console.WriteLine("Access Token = " + token.access_token);

                if (token.error == null)
                {
                    Console.WriteLine("\nLogin is OK...");
                }
                else
                {
                    Console.WriteLine("Login is NOT OK...");
                    Console.WriteLine("Error = " + token.error);
                }
            }

            Console.WriteLine("\n\nHej!");

            Application.Run(new Form1());
        }
    }


}