Group Messaging Sample
====================

A simple group messaging application written using Twilio and ASP.NET MVC 5.

To learn more about this application, check out my blog post showing how to create a simple Group Messaging application using Twilio and ASP.NET MVC.

=== About this Application 

This application was created using Visual Studio 2013, ASP.NET MVC 5 and Entity Framework 6 and Twilio.  This application you will need to create a Twilio account, which you can do for free.

To run this project you will need to replace the temporary Twilio AccountSid and AuthToken values in `Credentials.cs`:

    public static string AccountSid {get { return "[YOUR_ACCOUNT_SID]";}}
    public static string AuthToken { get { return "[YOUR_AUTH_TOKEN]";}}

