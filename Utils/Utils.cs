using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using jhray.com.Services;

namespace jhray.com.Utils
{
    public static class Utils
    {
        public static Dictionary<string, string> GetLinesOfMetadata(string path)
        {
            var lines = File.ReadAllLines(path);
            return lines.ToDictionary(
                lin => lin.Substring(0, lin.IndexOf(":")),
                lin => lin.Substring(lin.IndexOf(":") + 1).Trim());
        }

        public static string WriteMetadataFile(Dictionary<string, string> metadata, string path)
        {
            return path;
        }

        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
