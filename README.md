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

Before running the application, ensure you have the necessary configurations either in the secrets.json or appsettings.json file. Everywhere you see "Your" replace it with the actual value.
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

## Database Diagram
![database](https://github.com/KristiyanHristov04/SocialMedia-ASP.NET-Core-MVC/assets/92588334/5ee4476b-0839-4ffe-9e60-696b2dda4a7f)
