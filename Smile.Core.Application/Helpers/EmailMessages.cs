using Smile.Core.Application.Models.Email;

namespace Smile.Core.Application.Helpers
{
    public static class EmailMessages
    {
        private const string BorderBottom = "<div style=" + "\"border-bottom:2px solid black; margin-top: 5px; margin-bottom: 5px;\"" + "></div>";

        public static EmailMessage ActivationAccountEmail(string email, string username, string callbackUrl)
          => new EmailMessage(
              email: email,
              subject: "Smile - activate your account",
              message: $"<p>Hi <strong>{username}</strong>!</p>" +
                  BorderBottom +
                  $"<p>In order to activate your account on Smile, click link below.<br><br>" +
                  $"Activation account link: <a href='{callbackUrl}'>LINK</a></p>" +
                  "<p>Best regards,<br>" +
                  "Smile team</p>"
        );

        public static EmailMessage ResetPasswordEmail(string email, string username, string callbackUrl)
           => new EmailMessage(
               email: email,
               subject: "Smile - reset password",
               message: $"<p>Hi <strong>{username}</strong>!</p>" +
                   BorderBottom +
                   $"<p>In order to reset your password on Smile, click link below.<br><br>" +
                   $"Reset password link: <a href='{callbackUrl}'>LINK</a></p>" +
                   "<p>Best regards,<br>" +
                   "Smile team</p>"
       );

        public static EmailMessage EmailChangeEmail(string email, string callbackUrl)
          => new EmailMessage(
              email: email,
              subject: "Smile - change email",
              message: $"<p>Hi <strong>{email}</strong>!</p><br><br>" +
                  $"<p>In order to change your email on Smile, click link below.<br><br>" +
                  $"Change email link: <a href='{callbackUrl}'>LINK</a></p>" +
                  "<p>Best regards,<br>" +
                  "Smile team</p>"
       );

        public static EmailMessage ReplyEmail(string email, string content, string subject)
           => new EmailMessage(
               email: email,
               subject: "Smile - support panel",
               message: $"<p>Hi <strong>{email}</strong>!</p>" +
                   BorderBottom +
                   $"<h3>Report: <i>{subject}</i></h3>" +
                   $"<p>{content}</p>" +
                   "<p>Best regards,<br>" +
                   "Smile team</p>"
       );
    }
}