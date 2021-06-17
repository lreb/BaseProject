
Unit tests. This type of test focuses on testing a unit of code: a building block of a software application, such as a function or a class. Unit tests ensure that an isolated component of a software application works as expected.
Integration tests. Unlike unit tests, integration tests help to discover any issues when the units of code are assembled together to create more complex components. In fact, even if each unit of code is working correctly in an isolated environment, you may discover some issues when you put them together to build your application.
End-to-end (E2E) tests. This is a type of test ensuring that a user-level function works as expected. To an extent, they are similar to integration tests. However, here the focus is on the functions that are directly accessible by the software's user or somehow from outside the application. An E2E test may involve many systems and aims to simulate a production scenario.
Mocking: use a mocking framework (e.g. Moq) to mock external dependencies that you shouldn’t need to test from your own code.
Integration Tests: use integration tests to go beyond isolated unit tests, to ensure that multiple components of your application are working correctly. This includes databases and file systems.
UI Tests: test your UI components using a tool such as Selenium WebDriver or IDE in the language of your choice, e.g. C#. For browser support, you may use Chrome or Firefox extensions, so this includes the new Chromium-based Edge browser.


![alt text](https://blog.octo.com/wp-content/uploads/2018/10/integration-tests-1024x634.png)


https://xunit.net/
https://fluentassertions.com
https://fluentassertions.com/introduction
https://github.com/jbogard/Respawn
https://github.com/sandord/Respawn.Postgres


https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api

https://jimmybogard.com/how-respawn-works/
https://www.codeproject.com/Articles/5250190/Integration-Tests-in-ASP-NET-Core-A-DBContext