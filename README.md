# SocialMedia

## Overview

The SocialMedia application aims to replicate a real social media web platform. It features different user roles such as User, Administrator, and SuperAdministrator. Users have various capabilities within the application, including creating, editing, and deleting posts, reporting others' posts, previewing their own and others' posts, viewing liked posts, contacting support, and searching for other people's profiles and posts.
Administrators can communicate with each other via Admin Chat to discuss various topics. They also have the ability to review reported posts and decide whether they should be taken down. Both Admins and SuperAdmins can promote regular Users to Admins, but only SuperAdmins can demote Admins back to regular Users. This project is developed as part of the final course at SoftUni C# Web(ASP.NET Core Advanced).

# Technologies Used
<ul>
  <li>.NET Core 8.0</li>
  <li>ASP.NET Core</li>
  <li>Entity Framework Core</li>
  <li>Microsoft SQL Server</li>
  <li>HTML5</li>
  <li>CSS3</li>
  <li>Javascript(AJAX, toastr)</li>
  <li>Bootstrap</li>
  <li>xUnit</li>
  <li>Moq</li>
  <li>SignalR</li>
</ul>

# Getting Started

Before running the application, ensure you have the necessary configurations either in the secrets.json(will work only if you are running the application in Development Environment) or appsettings.json file. Everywhere you see "Your" replace it with the actual value. Below you are going to find out how to get each of these values if you don't know.
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YourLocalDB;Database=SocialMediaDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "SendGridKey": "YourSendGridKey",
  "Authentication": {
    "Google": {
      "ClientId": "YourGoogleClientId",
      "ClientSecret": "YourGoogleClientSecret"
    }
  },
  "Email": "YourEmailAddress"
}
```
<ol>
  <li>For the DefaultConnection, you only need to specify your database server name or use "." (a period) for the local server. Additionally, you have the option to change the database name from "SocialMediaDB" to a different name of your choice.</li>
  <li>
    To configure the SendGridKey and Email settings:
    <ol type="I">
      <li>
        SendGridKey:
        <ul>
          <li>
            Register at SendGrid.
          </li>
          <li>
            Create a Sender under "Marketing - Senders."
          </li>
          <li>
            Generate an API Key under "Settings -> API Keys."
          </li>
          <li>
            Inside your application, replace "YourSendGridKey" with the generated API Key.
          </li>
        </ul>
      </li>
      <li>
        Email Address:
        <ul>
          <li>
            Use the email address you provided when setting up the Sender (under "From Email Address").
          </li>
          <li>
            Replace "YourEmailAddress" in the application settings with this email address.
          </li>
        </ul>
      </li>
    </ol>
  </li>
  <li>
    To set up the external login provider with Google, follow these steps:
    <ol type="I">
      <li>
        Google OAuth Configuration:
        <ul>
          <li>
            Access Google Cloud Console.
          </li>
          <li>
            If you don't have a Google account, create one.
          </li>
          <li>
            Go to the "OAuth consent screen" tab.
          </li>
          <li>
            Create a new project and choose "External" for the User Type.
          </li>
          <li>
            Follow the instructions provided to configure the OAuth consent screen.
          </li>
          <li>
            After completing the setup, you will be redirected back to the OAuth consent screen tab. Click on "Publish App" to finalize.
          </li>
        </ul>
      </li>
      <li>
        Create OAuth Client ID:
        <ul>
          <li>
            Go to the "Credentials" tab in the Google Cloud Console.
          </li>
          <li>
            Create credentials and choose "OAuth Client ID".
          </li>
          <li>
            Follow the instructions to configure the OAuth Client ID.
          </li>
          <li>
            In the "Authorized redirect URIs" section, enter https://localhost:{PORT}/signin-google, replacing {PORT} with the port number where your application is hosted.
          </li>
          <li>
            After completing the setup, a window will display your Client ID and Client Secret. Copy and save both securely.
          </li>
        </ul>
      </li>
      <li>
        Update Application Settings:
        <ul>
          <li>
            Replace "YourGoogleClientId" in your application settings with the copied Client ID.
          </li>
          <li>
            Replace "YourGoogleClientSecret" in your application settings with the copied Client Secret.
          </li>
        </ul>
      </li>
    </ol>
  </li>
  <li>That's it! Now you should be able to start the application and use it.</li>
</ol>

## Database Diagram
![database](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/5ee4476b-0839-4ffe-9e60-696b2dda4a7f)
