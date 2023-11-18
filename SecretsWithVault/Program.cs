using System;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace SecretsWithVault
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize one of the several auth methods.
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo("testtoken");

            // Initialize settings. You can also set proxies, custom delegates etc. here.
            var vaultClientSettings = new VaultClientSettings("http://localhost:8200", authMethod);

            IVaultClient vaultClient = new VaultClient(vaultClientSettings);

            var myKeys = await vaultClient.V1.Secrets.Cubbyhole.ReadSecretAsync("my-path");
        }
    }
}
