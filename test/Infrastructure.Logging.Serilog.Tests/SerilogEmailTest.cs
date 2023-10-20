using MySvc.Framework.Infrastructure.Serilog;
using Serilog;
using Serilog.Debugging;

namespace Infrastructure.Logging.Serilog.Tests
{
    public class SerilogEmailTest
    {
        [Fact]
        public void Works()
        {
            var selfLogMessages = new List<string>();
            SelfLog.Enable(selfLogMessages.Add);

            var fromEmail = "test01@myemail.com";
            var toEmail = "";
            var networkCredentialuserName = "test01@myemail.com";
            var networkCredentialpassword = "";

            using (var emailLogger = new LoggerConfiguration()
                       .WriteTo.EmailCustom(fromEmail, toEmail, "true",
                           "MySvc Framework Serilog Mail UnitText Subject", "true", 
                           "smtp.exmail.qq.com", networkCredentialuserName, networkCredentialpassword,
                           "465", "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
                           "1", "1", "Error")
                       .CreateLogger())
            {
                emailLogger.Error(new Exception("Test Exception"), "Test Exception Message");
            }

            Assert.Equal(Enumerable.Empty<string>(), selfLogMessages);
        }
    }
}