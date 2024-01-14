using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace TaskBuddy.Class {
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Identity;

    public class UserConnectionLogger
    {
        private static readonly Lazy<UserConnectionLogger> lazyInstance = new Lazy<UserConnectionLogger>(() => new UserConnectionLogger());

 
        private string logFilePath = "userConnectionLog.txt";

        public static UserConnectionLogger Instance => lazyInstance.Value;

        private UserConnectionLogger()
        {
    
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }
        }

        public bool LogUserConnection(UserManager<IdentityUser> userManager, string userId)
        {
            try
            {
                var user = userManager.FindByIdAsync(userId).Result;

                string logMessage = $"User {user.UserName} connected on {DateTime.Now}";
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging user connection: {ex.Message}");
                return false;
            }
        }
    }

}