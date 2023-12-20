---
title: _Chirp!_ Project Report
subtitle: ITU BDSA 2023 Group 9
author:
    - "Anton Dørge Friis <anlf@itu.dk>"
    - "Johan Sandager <jsbe@itu.dk>"
    - "Oline Scharling Krebs <okre@itu.dk>"
    - "Jonas Kramer <kram@itu.dk>"
    - "Clara Walther Jansen <clwj@itu.dk>"
    - "Lauritz Andersen <lana@itu.dk>"
numbersections: true
---

# Design and Architecture of _Chirp!_

## Domain model

In our domain model, we capture entities and relationships to provide a high-level abstraction of the system's static structure, thereby centering the focus on the business logic of our Chirp!-system. We provide a complete diagram below:

![Domain model UML](./images/DomainModelGroup9-Sketch.png)

At the highest level in this diagram, we have Chirp.Core, Chirp.Infrastructre, Chirp&#46;Web, wherein certain classes are contained. Apart from this, we have references to external libraries such as AspNetCoreIdentity and FluentValidation.

### Brief Description of Classes inside Chirp.Core

Chirp.Core contains all the core functionality. In Chirp.Core, we have Data Transfer Objects (DTOs) for Cheeps and Authors, as well as interfaces for Author and Cheep Repositories. There is an associative relationship between the DTOs and the repositories, since the repositories use the DTOs to create cheeps and authors, get cheeps and name of authors, follow authors etc. The interface repositories provide an interface contract for the repository implementation that will transfer structured data between different layers of the application and use DTOs as a standardized communication contract in this regard.

### Brief Description of Classes inside Chirp.Infrastructure

Key elements include CheepRepository and AuthorRepository, which are concrete implementations of the Repository interfaces. These repositories use ChirpContext to interact with the database, managing data for authors and cheeps. Each repository handles specific queries related to either authors or cheeps, working with Author and Cheep models. However, they return only Data Transfer Objects (DTOs) to maintain separation of concerns. The Author and Cheep models are queried from the dbsets of the dbcontext. The composition relationship between the Author and Cheep classes and ChirpContext ensures data integrity, as removing the context also removes the associated data models from the program.

To streamline authentication and authorization, a chirp author inherits from Identity User. There is a composite relationship between Author and Cheep, indicating that an Author can own 0 or more cheeps and that any existing cheep is owned by a unique Author.

### Brief Description of Classes inside Chirp&#46;Web

Inside of Chirp&#46;Web, we have our Program.cs, which is the class the program is run from. It has the fields WebApplicationBuilder builder and WebApplication app, which are used for encapsulating the app's services and middleware and for building the web application, setting up authentication and services necessary to make the application run.

## Architecture — In the small

The diagram below depicts the Onion-architecture of our code-base. The different layers and their position shows which code they have access to. 'Chirp.Core' only know about itself, whereas 'Chirp.Infrastucture' has access to the code in 'Chirp.Core' but not the outer layers - except for the database as depicted with the arrow in the diagram. In general, upper layers should depend on lower layers. The Onion Architecture organizes our software in a manner, where we can keep our main business rules separate from external details. This separation of concerns makes the software easier to understand and change. It is also good for testing and adapting to new requirements and technologies. It adheres to SOLID-principles like Dependency Inversion.

![Illustration of the _Chirp!_ data model as UML class diagram.](./images/OnionDiagramSmallArchitectureG9.jpg)

## Architecture of deployed application

We will briefly discuss the architecture of the deployed application, which is based on a client-server-model.

In the [deployment diagram](#deployedapp) the Web Server, hosted on Azure, manages user requests and serves the website, while the SQL Server, also on Azure, stores structured data like user information and cheeps. The Client browser communicates with the Web Server, which contains the webpage artifact. The Web server, in turn, interacts with the SQL Server, which contains the SQL_Database artifact, for database operations. In this manner, the Client does not directly connect to the SQL Server; but instead communicates with the Web Server, which handles the interaction with the database.

![Illustration of the _Chirp!_ deployed application](./diagrams/DeploymentDiagram.png){#deployedapp}

## User activities

We will outline a few different user journeys to showcase the capabilities of Chirp! users. This includes showcasing possible actions for an unauthorized user, guiding through registration and login processes, and demonstrating a typical user journey within the Chirp! app when logged in.

### Un-authorised User Journey

For an unauthorized user, typical actions might involve viewing cheeps on the public timeline or accessing specific details about an author, such as past cheeps, total cheeps, and other information, which would be accessible through the author's private timeline.
A user journey corresponding to this use of Chirp is described in the following User Activity diagram:

![Illustration of Unauthorised user journey](./diagrams/ActivityDiagramNotAuthorised.png)

### Registration and Login processes {#login-register}

For the full Chirp! experience, authorized users can personalize their interaction by posting, liking cheeps, and following authors. The registration and log-in processes are described in the following diagrams:

#### Registration

![Illustration of Registering to Chirp](./diagrams/ActivityDiagramRegister.png)

#### Log-in

![Illustration of Log-in process](./diagrams/ActivityDiagramLogin.png)

### Cheeping and Following Authors

When using Chirp, users primarily write cheeps or follow authors and like their cheeps. The upcoming user activity diagrams are centered around these actions. In both diagrams, we assume the user has already been through the [log-in or registration process](#login-register) to engage in these functionalities.

#### Following an Author

![Illustration of Following other users](./diagrams/ActivityDiagramFollowAuthor.png)

#### Cheeping a Cheep

In this diagram, we assume, we have a user, who is already following other users.

![Illustration of Cheeping](./diagrams/ActivityDiagramCheep.png)

## Sequence of functionality/calls through _Chirp!_

In the following, two sequence diagrams are shown. The first shows a general overview of some of the calls a user - authorized or not - might go through, while the second gives an idea of what a specific call looks like and the inner workings behind it.

### Sequence diagram 1

In this sequence diagram, we have 3 lifelines: User, Chirp.Web, and ChirpDb. Here we show an overview of the simple sequences both an unauthorized and authorized user will go through to access different parts of the our application.

![Simple Sequence Diagram](./images/SimpleSequenceDiagram.png)

### Sequence diagram 2

In this sequence, we have 5 lifelines: UnAuthorizedUser, Chirp.Web, CheepRepository, ChirpContext, and RemoteDB. Here, we see the sequence of calls that is made both internally by the program and externally, from an unauthorized user, sending a simple GET request to the root endpoint (acessing bdsagroup9chirprazor.azurewebsites.net).

![Sequence of calls thorugh Chirp for an unauthorized user to root](./diagrams/SeqDia.png)

# Process

This chapter gives a brief overview of our process, showcasing GitHub Actions workflows with UML activity diagrams. We highlight our team's project board's status and offer clear instructions for local setup and testing.

## Build, test, release, and deployment

We use Github Workflows to streamline and automate software development processes and ensure continuous integration and continuous delivery.

The illustration below shows our build and test workflow, which ensures that the code passes all tests before merging a pull request to main.

![Build and test github workflow](./diagrams/BuildAndTest.png)

In the illustration below, we see the workflow that creates a release of the program to Github. It is triggered when a tag of the format v\* is pushed to github.

![Release github workflow](./diagrams/ReleaseWorkflow.png)

This Github workflow is triggered after a push to main, and releases main to our production environment.

![Deployment gihub workflow](./diagrams/ReleaseToProduction.png)

## Team work

In this chapter, we will provide an overview of our collaboration by discussing the status of tasks on our project board and showing the general flow of activities from task creation to integration of features.

### Project Board

Show a screenshot of your project board right before hand-in. Briefly describe which tasks are still unresolved, i.e., which features are missing from your applications or which functionality is incomplete.

The image below shows our project board as is before handin of this report. The only issue we have left (issue#194), is making the ducplicate UI-elements into partial components as this would rid our application of some redundant code.

![Workflow from Task to Finish](./images/projectboard.png)

We set up a KanBan board to handle our issues and give us an overview of the process an issue had to go through.

-   The issue is created and put into the 'Backlog'.
-   When the issue is ready with a description, acceptance criteria and has no dependencies pending it is moved to then coloumn 'Ready'.
-   When we start working on the issues it is moved to the 'In progress' coloumn.
-   If the issue relates to the code-base and we hgiave determined it is done, it is moved to 'Create tests'.
-   When tests have been made (if necessary) a PR is created and the issues is moved to the coloumn 'In review'.
-   If the PR for the issues gets approved, it is merged into the main-branch. If this is the case the issue can be closed and moved to the 'Done' coloumn.

## Team work diagram

The diagram below shows the entire workflow - from receiving a task to creating an issue and the above-mentioned steps.

![Workflow from Task to Finish](./diagrams/TeamWorkDiagram.png)

## How to make _Chirp!_ work locally

Firstly, open a command prompt. From here, navigate to the folder in which you want the project to be, and run the command:

`git clone https://github.com/ITU-BDSA23-GROUP9/Chirp.git`

Then, navigate to the Chirp folder with the command:

`cd Chirp`

Navigate to src/Chirp.Web by running the command:

`cd src/Chirp.Web/`

Then start the application by running the command:

`dotnet run`

To be able to log in with GitHub, you will need to export client_secrets to the application.
Firstly, navigate to:

...

Then, ...:

`export GITHUB_CLIENT_ID=731d6c33e6157e4ffdcd`

and the secret:

`export GITHUB_CLIENT_SECRET=dc75bc058fa4f5c20eb6f930ffae5a7d30a5fd25`

## How to run test suite locally

Firstly, open a command prompt. Navigate to the folder in which the Chirp! application is. When standing in the root folder, to run all tests, simply run the command:

`dotnet test`

If you want to run tests for individidual parts of the system, first go to the test directory with the command:

`cd test`

Then go into an individual directory, for example by running:

`cd Chirp.Core.Tests/`

Then run the command:

`dotnet test`

# Ethics

In this chapter, we will discuss the software license we have chosen and explain how we have utilized AI/Large Language Models in our development process.

## License

We have chosen the MIT License for our application. Mainly due its simplicity, which makes it easy for students to collaborate and simplifies licensing issues. In addition, it allows for lots of people to use our webpage, and if we potentially wanted to commercialize Chirp!, the license is really flexible and allows this.

## LLMs, ChatGPT, CoPilot, and others

In this report we have only made use of the LLM's: ChatGPT and CoPilot.

**ChatGPT**: has been used to understand the theory behind some of the features which we have implemented, and generally not to generate code, unless explictly specified as co-writer. In some cases, it has been used for debugging purposes, indicated by including it as a co-writer in commit messages.

**CoPilot**: was only used in the later stages of the process to speed up code-writing-proces when writing generic code. (We decided in the beginning that we would follow Rasmus' reccomendation of writing code without CoPilot, when we were learning the basics of C#.)

### Usefulness and efficiency of LLMs

Language models (LLMs) helped us understand concepts and find bugs efficiently. However, directly using them to generate code for more complex architecture might lead to more debugging, which was why we only used it for generating very generic code. It is important to use LLMs in a way where you benefit from their strengths without slowing down the development process.
