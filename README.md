# SocialMedia

## Overview

The SocialMedia application aims to replicate a real social media web platform. It features different user roles such as User, Administrator, and SuperAdministrator. Users have various capabilities within the application, including creating, editing, and deleting posts, reporting others' posts, previewing their own and others' posts, viewing liked posts, contacting support, previewing announcements created by admins, and searching for other people's profiles and posts.
Administrators can communicate with each other via Admin Chat to discuss various topics. They also have the ability to review reported posts and decide whether they should be taken down. Both Admins and SuperAdmins can promote regular Users to Admins, but only SuperAdmins can demote Admins back to regular Users. They can also create new announcements which users can read and be informed. This project is developed as part of the final course at SoftUni C# Web(ASP.NET Core Advanced).

## Technologies Used
<ul>
  <li>.NET Core 8.0</li>
  <li>ASP.NET Core</li>
  <li>Entity Framework Core</li>
  <li>Microsoft SQL Server</li>
  <li>HTML5</li>
  <li>CSS3</li>
  <li>JavaScript(AJAX, toastr)</li>
  <li>Bootstrap</li>
  <li>xUnit</li>
  <li>Moq</li>
  <li>SignalR</li>
</ul>

## Getting Started

Before running the application, ensure you have the necessary configurations either in the secrets.json (which works only if you are running the application in the Development Environment) or appsettings.json file. Replace every occurrence of "Your" with the actual value. Follow the steps below to obtain these values or follow the official documentation by Microsoft: [Google Configuration](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-8.0#create-the-google-oauth-20-client-id-and-secret) [SendGrid Configuration](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-8.0&tabs=visual-studio)

```json
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
    <ol type="1">
      <li>
        SendGridKey:
        <ul>
          <li>
            Register at SendGrid(https://shorturl.at/gvyBR).
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
    <ol type="1">
      <li>
        Google OAuth Configuration:
        <ul>
          <li>
            Access Google Cloud Console(https://shorturl.at/bdmJU).
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

## Seed
When you update the database and apply the migrations, countries, roles and one super admin will be seeded and ready for use.

## Database Diagram
![databaseNew](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/42b06537-128e-4d94-8e46-b60faa741d98)


## Functionality

User Registration
![register](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/3a29466b-8f1b-4471-af44-5c18ce5c4518)


User Login
![login](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/c29595eb-3252-4877-92e9-67d513794d9c)


User Login With External Login Provider(Google)
![googlelogin](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/8535c4a2-d1d5-484c-b86a-66626a559c44)


User Home Page
![home](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/f17a76a3-2994-4540-8d98-3c7a8dc474f8)


User Add Page
![add](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/008137f2-9b99-476b-93ff-ad076e3e0577)


User My Posts Page
![myposts](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/fc455f53-f456-482a-b685-7f87eb3fe652)


User Liked Posts Page
![likedposts](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/badbed2e-ed75-4f3f-b004-b6801c1bac00)


User Contact Us Page
![contactus](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/1f6d5f77-61f1-4b2e-a079-616e2ae1ce74)


User Terms and Conditions(T&C) Page
![termsandconditions](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/d58ada61-8441-44fa-8f28-dbdcc8e8b7de)


User Search For Profiles Page
![profiles](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/b21e3fc1-48b3-4be6-a0aa-d7de321143d1)


User Profile Settings
![userprofilesettings](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/47b33e0c-bc35-4719-adc9-bf775bc6aa16)


User Status Code 404 Not Found
![notfound](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/d840216c-2a35-4b1f-a351-5451c5eeb79d)


User Status Code 401 Unauthorized
![unauthorized](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/9bd13f2f-441b-4b1a-85ab-7fbc2dc83af0)


User Status Code 400 Bad Request
![badrequest](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/2cbaf901-62c4-4471-8dfe-93eac6b7bdb8)


User Report Post
![reportpost](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/046b0285-cb7e-4a2d-b9ba-6d935c911f28)


User Delete Post
![deletepost](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/633940dd-986a-4f19-890b-755a6e417bcc)


Admin Dashboard Page
![dashboard](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/b1c22c80-47d3-4cc9-8f47-cbc5717b94c7)


Admin Dashboard(Admin Chat Showcase) Page
![dashboardchat](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/b52f0db7-36ad-438a-87db-552e3b39a588)


Admin Reports Page
![reports](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/dd09ad23-dcc0-4e55-b46c-5c020e52bb29)

Admin Preview Reported Post
![previewreportedPost](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/640ddce3-5a1f-4e92-a035-5fe68053f777)


Admin Dismiss Reported Post
![dismisspost](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/ebe51520-3abe-4e02-bf47-37ee531932de)


Admin Successfully Deleted Reported Post
![successfullydeletedreportedpost](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/58336992-c2b5-4e58-aa50-56103cc2b78d)


Admin Users Page
![users](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/b87df00a-d12b-4b14-a1e5-69a43a2d8a9e)


Admin Demote Page
![demote](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/e02f3125-0aa8-473e-942d-7f9c9e0c105e)


Admin Promote Page
![promote](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/9b4c4a41-f355-4b73-b334-778c91a80b9c)


Admin Announcements Page
![announcements](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/c258027a-85bb-49ac-9344-1a5fba28aa59)

Admin Announcement Create Page
![createannouncement](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/088466eb-e682-4bc0-a21b-ec6eb5167027)

Admin Announcement Edit Page
![editannouncement](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/bc6e9b13-e1d2-4511-8d92-5def75df7148)

Admin Announcement Delete Page
![deleteannouncement](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/072b415c-ba5c-4d4f-91df-dbd2f8b7f5e7)
