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

![Alt text](./diagrams/DomainModelGroup9-Sketch.png)

Here comes a description of our domain model.

![Illustration of the _Chirp!_ data model as UML class diagram.](docs/images/domain_model.png)

## Architecture — In the small

![Illustration of the _Chirp!_ data model as UML class diagram.](docs/images/../../diagrams/OnionDiagramSmallArchitectureG9.jpg)

Mangler deployment diagram - næste skridt

## Architecture of deployed application

![Illustration of the _Chirp!_ deployed application](docs/images/../../diagrams/DeploymentDiagram.png)

## User activities

We will outline a few different user journeys to showcase the capabilities of Chirp! users. This includes showcasing actions for an unauthorized user, guiding through registration and login processes, and demonstrating a typical user journey within the Chirp! app when logged in.

#### Un-authorised user-journey

For an unauthorized user, typical actions might involve viewing cheeps on the public timeline or accessing specific details about an author, such as past cheeps, total cheeps, and other information, which would be accessible through the author's private timeline. This process is depicted in a simple User Activity diagram, where the unauthorized user navigates the webpage to view public cheeps and then explores a specific author's timeline on Chirp! for more details.
![Illustration of Unauthorised user journey](docs/images/../../diagrams/ActivityDiagramNotAuthorised.png)

#### Registration and Login processes:

##### Log-in:

For a complete Chirp! experience, authorized users can personalize their interaction by posting, liking cheeps, and following authors. To log in, users click the login button and choose either GitHub or enter their password, username, and email. Upon successful login, users are redirected to their private timeline if following someone or to the public timeline if not following anyone yet.

![Illustration of Unauthorised user journey](docs/images/../../diagrams/ActivityDiagramLogin.png)

##### Registration:

![Illustration of Unauthorised user journey](docs/images/../../diagrams/ActivityDiagramRegister.png)

#### Cheeping and Following Authors

##### Cheeping:

![Illustration of Unauthorised user journey](docs/images/../../diagrams/ActivityDiagramCheep.png)

##### Following:

![Illustration of Unauthorised user journey](docs/images/../../diagrams/ActivityDiagramFollowAuthor.png)

## Sequence of functionality/calls trough _Chirp!_

# Process

## Build, test, release, and deployment

## Team work

We set up a KanBan board to handle our issues and give us an overview of the process an issue had to go through.

-   The issue is created and put into the 'Backlog'.
-   When the issue is ready with a description, acceptance criteria and has no dependencies pending it is moved to then coloumn 'Ready'.
-   When we start working on the issues it is moved to the 'In progress' coloumn.
-   If the issue relates to the code-base and we have determined it is done, it is moved to 'Create tests'.
-   When tests have been made (if necessary) a PR is created and the issues is moved to the coloumn 'In review'.
-   If the PR for the issues gets approved, it is merged into the main-branch. If this is the case the issue can be closed and moved to the 'Done' coloumn.

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License

We have chosen the MIT License for our application.

## LLMs, ChatGPT, CoPilot, and others

In this report we have only made use of the LLM's: ChatGPT and CoPilot.

**ChatGPT**: has been used to understand the theory behind some of the features which we have implemented, and generally not to generate code, unless implictly specified as co-writer.

**CoPilot**: was only used in the later stages of the process to speed up code-writing-proces when writing generic code. (We decided in the beginning that we would follow Rasmus' reccomendation of writing code without CoPilot, when we were learning the basics of C#. - kan slettes)
