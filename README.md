# AzureFunctions-TheLongRun-Leagues
Demo code for CQRS on Azure using Event Grid and Durable Functions

This project contains the Azure Functions App used to demonstrate the concept of running a *CQRS* / *Event Sourcing* backed system on a serverless Azure Functions backbone.

The libraries of the actual performing of *Event Sourcing* functions over event streams come from the related *CQRS Azure* project under this same root account.

The command and query handlers are implemented over a version of the *Azure Durable Functions* orchestration such that each can call lower level orchestrations (projections, classifiers, identifier groups and event submitters) to do the work of their long-running process.

Each orchestration (command or query) is itself backed by an event stream so that it can be diagnosed and the state can be recreated as at any given point in time.  These event streams are stored in Azure AppendBlob storage which means they can easily be copied down to a development machine for analysis.

![Overview of CQRS](Images/eventsourcing_use_with_cqrs.png)
