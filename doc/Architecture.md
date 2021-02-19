# 1. Architecture

## **Repository convention**

I always use the **convention for GitHub repositories** you can know more [here](https://github.com/kriasoft/Folder-Structure-Conventions/blob/master/README.md)
There are another more specific for the .NET projects [here](https://gist.github.com/davidfowl/ed7564297c61fe9ab814)

## **Estructure**

The architecture follow some of the DDD principles but it is not a fully DDD architecture.

![DDD](images/DDDLayers.png)

It's some similar to this (I got the picture from internet and it isn't really that) Where the UI layer is the Host + API, disgregated in the project for separate the par od the API for using in another host (like the TestHost).

![DDD](images/Mediator.png)

The mediator pattern is useful for separate the queries of the request and also useful for CQRS architectures (Which they are fucking difficult sometimes). Thanks to this pattern you can separate the flows of the CRUD, the Read and the creation, modification and deleting and use different strategies for each one. I use Entity framework and Dapper, which is faster than EF for the queries.

[Go back](Index.md)
