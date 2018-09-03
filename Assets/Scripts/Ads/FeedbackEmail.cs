using UnityEngine;

public class FeedbackEmail
{
    public static void Send()
    {
        var email = "support@lite.games";
        var subject = $"Feedback - {Application.productName}";
        var body = $"Game Title: {Application.productName}\n" +
                   $"Game Type: Lite\n" +
                   $"AATKit: {AATKitBinding.GetVersion()}\n" +
                   $"Device Name: {SystemInfo.graphicsDeviceName}\n" +
                   $"OS Version: {SystemInfo.operatingSystem}\n" +
                   $"Device Language: {Application.systemLanguage}\n" +
                   $"Game language: English\n" +
                   $"Screen Dimensions: {Screen.width}x{Screen.height}";
        SendEmail(email, subject, body);
    }

    protected static void SendEmail (string email,string subject,string body)
    {
        subject = MyEscapeURL(subject);
        body = MyEscapeURL(body);
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    protected static string MyEscapeURL (string url)
    {
        return WWW.EscapeURL(url).Replace("+","%20");
    }
}