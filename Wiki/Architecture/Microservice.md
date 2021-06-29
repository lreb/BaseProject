# Microservice Architecture

It is an architectural style that structures an application as a collection of services that are

- Highly maintainable and testable
- Loosely coupled
- Independently deployable
- Organized around business capabilities
- Owned by a small team

![alt text](https://microservices.io/i/Microservice_Architecture.png)

![alt text](https://www.researchgate.net/profile/Edwin-Forero-Garcia/publication/333432166/figure/fig3/AS:763582178398208@1559063586294/Microservices-architecture-Image-taken-from.ppm)

![alt text](https://feras.blog/wp-content/uploads/Microservices-Architecture.png)

![alt text](https://res.cloudinary.com/practicaldev/image/fetch/s--seen3BGm--/c_limit%2Cf_auto%2Cfl_progressive%2Cq_auto%2Cw_880/https://user-images.githubusercontent.com/2697570/49395813-cd094980-f737-11e8-9e9a-6c20db5720c4.jpg)

![alt text](https://docs.microsoft.com/es-es/azure/architecture/solution-ideas/media/dev-test-microservice.png)

![alt text](https://cloudmeloncom.files.wordpress.com/2019/12/ref-app.png?w=656&h=337)

## Benefits

- Microservice architecture gives developers the freedom to independently develop and deploy services
- A microservice can be developed by a fairly small team
- Code for different services can be written in different languages (though many practitioners discourage it)
- Easy integration and automatic deployment (using open-source continuous integration tools such as Jenkins, Hudson, etc.)
- Easy to understand and modify for developers, thus can help a new team member become productive quickly
- The developers can make use of the latest technologies
- The code is organized around business capabilities
Starts the web container more quickly, so the deployment is also faster
- When change is required in a certain part of the application, only the related service can be modified and redeployed—no need to modify and redeploy the entire application
- Better fault isolation: if one microservice fails, the other will continue to work (although one problematic area of a monolith application can jeopardize the entire system)
- Easy to scale and integrate with third-party services
- No long-term commitment to technology stack

## Drawbacks

- Due to distributed deployment, testing can become complicated and tedious
- Increasing number of services can result in information barriers
- The architecture brings additional complexity as the developers have to mitigate fault tolerance, network latency, and deal with a variety of message formats as well as load balancing
- Being a distributed system, it can result in duplication of effort
- When number of services increases, integration and managing whole products can become complicated
- In addition to several complexities of monolithic architecture, the developers have to deal with the additional complexity of a distributed system
- Developers have to put additional effort into implementing the mechanism of communication between the services
- Handling use cases that span more than one service without using distributed transactions is not only tough but also requires communication and cooperation between different teams

## Keys

### Subdomains

Split bussiness logic into little subdomains, for example

- Order Service
- Inventory Service
- Sales Service

### Independent Database

There are a few different ways to keep a service’s persistent data private. You do not need to provision a database server for each service. For example, if you are using a relational database then the options are:

Private-tables-per-service – each service owns a set of tables that must only be accessed by that service
Schema-per-service – each service has a database schema that’s private to that service
Database-server-per-service – each service has it’s own database server.
Private-tables-per-service and schema-per-service have the lowest overhead. Using a schema per service is appealing since it makes ownership clearer. Some high throughput services might need their own database server.

It is a good idea to create barriers that enforce this modularity. You could, for example, assign a different database user id to each service and use a database access control mechanism such as grants. Without some kind of barrier to enforce encapsulation, developers will always be tempted to bypass a service’s API and access it’s data directly.

### Event Sourcing

An operation related to a table may affect many parties. Here, we can solve these and similar problems with event-based approaches. Event-based approaches can be explained as informing people who want to listen to you when you do something. We call these as domain events. For example, when you create a user, it’s like sending a “welcome email” to this user.

![alt text](https://miro.medium.com/max/700/1*2EOA4TPAxSbxfah1FCXtDg.png)

Solutions like RabbitMQ, Kafka, and ActiveMQ.

[CQRS and Event Sourcing](https://dzone.com/articles/microservices-with-cqrs-and-event-sourcing)
