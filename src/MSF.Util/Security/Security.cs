using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MSF.Util.Security
{
    public static class Security
    {
        public static void SecureStringDemo()
        {
            /*
                Does not ensure that plain text is immediately cleared after garbage collection.
             */
            string str = string.Empty;

            /*
                The garbage collector does not move or compact the memory occupied by a secure string, reducing the risk of unintentional data leaks. 
                The data is encrypted in memory, and it is immediately cleared when the object is disposed
             */
            SecureString secStr = new SecureString();
            var key = "PassworD";

            key.ToCharArray().ToList().ForEach(x => secStr.AppendChar(x));
            var password = new NetworkCredential(string.Empty, secStr).Password;

            Debug.WriteLine(password);

            secStr.Dispose();

        }

        public static void CrossSiteScriptingXSS()
        {
            /* 
                XSS Alows attackers to inject malicious scripts into a web application, affecting other users who visit the affected web pages
                
                1. Validate user input: Use validation to ensure that the data reeived from users is in the expected format and contains no malicious content.
                2. Encode any user input that is displayed on the web appliation using 'string encodedUserInput = HttpUtility.HtmlEncode(userInput);'
                3. Use secure libraries and frameworks
                4. Keep the software and libraries up-to-date
                5. Implement Content Security Policy (CSP) using HTTP header to specify the allowed sources.
             */
        }

        public static void SecureSocketLayerSSL()
        {
            /* 
                The Secure Socket Layer (SSL), now called Transport Layer Security (TLS), 
                is a protocol used to establish encrypted connections between clients and servers over a network
             */

            var tcpClient = new TcpClient();
            var sslStream = new SslStream(tcpClient.GetStream());

            sslStream.AuthenticateAsClient("remoteHostName");
            sslStream.AuthenticateAsServer(X509Certificate.CreateFromCertFile(""), false, SslProtocols.Tls, false);
        }

        public static void SQLInjection()
        {
            /* 
                SQL injection attacks involve an attacker injecting malicious SQL commands into an application’s database queries, 
                which can lead to unauthorized data access or manipulation

                1. Use parameterized queries: "SELECT * FROM Users WHERE Username = @username"
                2. Use Stored Procedures (separate the SQL query logic from the application code)
                3. Limit database permissions
             */
        }

        public static void HashCryptography()
        {
            /* 
                Cryptography plays a vital role in C# security, as it helps safeguard sensitive data, ensure data integrity, 
                and authenticate communication between parties
             */

            // Hash functions are one-way functions that generate a fixed-size output, called a hash, from input data.
            // SHA - Secure Hash Augorithm

            byte[] data = Encoding.UTF8.GetBytes("password");

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(data);

                string hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                Debug.WriteLine(hashString);
            }
        }
    }
}
