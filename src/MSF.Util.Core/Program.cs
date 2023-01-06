using MSF.Util.Core.FluentEmail;
using System;
using System.Threading.Tasks;

namespace MSF.Util.Core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var esender = new EmailSender();
            await esender.SendEmail();
        }
    }
}
