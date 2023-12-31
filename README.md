# Table of Contents
- [Overview](#overview)
- [User manual](#user-manual)
  - [How to build the program](#how-to-build-the-program)
  - [How to run the program](#how-to-run-the-program)
  - [How to test the program](#how-to-test-the-program)
- [Using the program](#using-the-program)
- [Team members](#team-members)
- [Credits](#credits)
- [License](#license)

## Overview
This project constitutes Group 9's attempt at creating the Chirp! web application in the course Analysis, Design and Software Architecture at the IT University of Copenhagen. The Chirp! application is an X-like (formerly known as Twitter) application that encompasses much of the same functionality. As such, it is possible for a user of the application to - among other things - send cheeps (this applications version of tweets) to other users, like other users cheeps, and to view their individual timelines. To create the application, the group used a number of different technologies such as ASP.NET Core and SQLite.

## User manual

### How to build the program
In the root folder of the project, run the command: `dotnet build`

### How to run the program
Navigate to src/Chirp.Web and run the command: `dotnet run`

### How to test the program
- Running all tests: In the root folder of the project, run the command: `dotnet test`
- Running individual tests: If, for whatever reason, one wants to run test for individual parts of the application, navigate to the folder, for example test/Chirp.Web.Tests, and run the command: `dotnet test` 

## Using the program
When someone first visits the application, they are unauthorized and will therefore only be able to view the public timeline, but not interact with it in any other way. From here, one should navigate to the 'Register' section, where a new account can be created in one of two ways. Either an account can be created by simply choosing a username, email and password. Alternatively, an account can be created through the use of GitHub authorization, thus using the credentials of ones GitHub account. After an account is created, the new user is able to fully interact with the application. The user can send a new cheep by inputting text in the input form above the timeline and then clicking on the 'Share' button. Additionally, the user can choose to follow other users and/or like their cheeps by clicking the 'Follow' and 'Like' buttons respectively. In the header of the application, additional buttons are located giving the user freedom to navigate the site. The 'Home' button simply redirects to the homepage, thereby showing the public timeline. The '[username]'s Timeline' button redirects to a users private timeline. The 'Your Cheeps' button shows the cheeps authored by the user currently existing in the application. The 'About me' button shows the information, for example the name and email, that the application currently stores about the user. Finally, by clicking the 'Logout' button, the user logs out of the application.

## Team members
- [Anton](https://github.com/AntonFriis)
- [Clara](https://github.com/ClaraWJ)
- [Oline](https://github.com/olinesk)
- [Johan](https://github.com/JohanSandager)
- [Jonas](https://github.com/JKramer91)
- [Lauritz](https://github.com/lanaitu)

## Credits
A vast amount of sources were used in the creation of this project, and appreciation goes out to all of those. A special thanks should be given to [Andrew Lock](https://github.com/andrewlock), the author of [ASP.NET Core in Action, Third Edition](https://www.manning.com/books/asp-net-core-in-action-third-edition). This book proved an invaluable resource in understanding the intricacies of ASP.NET Core and many of the technologies used in this project.

## License
MIT License

Copyright (c) 2023 ITU-BDSA23-GROUP9

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.