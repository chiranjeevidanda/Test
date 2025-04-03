# nec-Fulf3Pl-solution
Testing.
 // Pseudo code
        // 1. Create DBconext
        // 2. Create Repository
        // 3. Create UnitOfWork
        // 4. Create Service
        // 5. Create Controller
        // 6. Create Test

        // How to learn xunit with MOQ advance
        // 1. Create a simple class with 2 methods
        // 2. Create a test class for that class
        // 3. Create a mock object of that class
        // 4. Create a mock object of that class with constructor
        // 5. Create a mock object of that class with constructor and method
        // 6. Create a mock object of that class with constructor and method and return value
        // 7. Create a mock object of that class with constructor and method and return value and parameter
        // 8. Create a mock object of that class with constructor and method and return value and parameter and exception
        // 9. Create a mock object of that class with constructor and method and return value and parameter and exception and verify
        // 10. Create a mock object of that class with constructor and method and return value and parameter and exception and verify and setup
        // 11. Create a mock object of that class with constructor and method and return value and parameter and exception and verify and setup and callback
        // 12. Create a mock object of that class with constructor and method and return value and parameter and exception and verify and setup and callback and property
        // 13. Create a mock object of that class with constructor and method and return value and parameter and exception and verify and setup and callback and property and event
        // 14. Create a mock object of that class with constructor and method and return value and parameter and exception and verify and setup and callback and property and event and async

        // comands for generate unit test using xunit and MOQ
        // 1. dotnet new xunit -n NEC.Fulf3PL.Application.Implementation.Tests
        // 2. dotnet add NEC.Fulf3PL.Application.Implementation.Tests reference NEC.Fulf3PL.Application.Implementation
        // 3. dotnet add NEC.Fulf3PL.Application.Implementation.Tests package Moq
        // 4. dotnet add NEC.Fulf3PL.Application.Implementation.Tests package Microsoft.NET.Test.Sdk
        // 5. dotnet add NEC.Fulf3PL.Application.Implementation.Tests package xunit
        // 6. dotnet add NEC.Fulf3PL.Application.Implementation.Tests package xunit.runner.visualstudio
        // 7. dotnet add NEC.Fulf3PL.Application.Implementation.Tests package coverlet.collector
        // 8. dotnet test NEC.Fulf3PL.Application.Implementation.Tests
        // 9. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true
        // 10. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
        // 11. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/
        // 12. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*"
        // 13. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs"
        // 14. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs" /p:ExcludeByAttribute="Obsolete"
        // 15. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs" /p:ExcludeByAttribute="Obsolete" /p:Threshold=80
        // 16. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs" /p:ExcludeByAttribute="Obsolete" /p:Threshold=80 /p:ThresholdType=line
        // 17. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs" /p:ExcludeByAttribute="Obsolete" /p:Threshold=80 /p:ThresholdType=line /p:MergeWith=TestResults/coverage/coverage.json
        // 18. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs" /p:ExcludeByAttribute="Obsolete" /p:Threshold=80 /p:ThresholdType=line /p:MergeWith=TestResults/coverage/coverage.json /p:UseSourceLink=true
        // 19. dotnet test NEC.Fulf3PL.Application.Implementation.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=TestResults/coverage/ /p:Exclude="[xunit*]*" /p:ExcludeByFile="**/Migrations/*.cs" /p:ExcludeByAttribute="Obsolete" /p:Threshold=80 /p:ThresholdType=line /p:MergeWith=TestResults/coverage/coverage.json /p:UseSourceLink=true /p:ExcludeByFile="**/Program.cs"   


        // How to generate unit test cases for PersonService class
        // 1. Create a mock object of PersonService class with constructor
        // 2. Create a mock object of PersonService class with constructor and method and return value
        // 3. Create a mock object of PersonService class with constructor and method and return value and parameter
        // 4. Create a mock object of PersonService class with constructor and method and return value and parameter and exception
        // 5. Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify
        // 6. Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify and setup
        // 7. Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify and setup and callback
        // 8. Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify and setup and callback and property
        // 9. Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify and setup and callback and property and event
        // 10. Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify and setup and callback and property and event and async

        public class PersonServiceTests
    {
        [Fact]
        public void Add_Should_Return_Person()
        {
            // Arrange
           
            var person = new Person()
            {
                Name = "John Doe",
                Age = 30,
                Address = new Address()
                { Id = Guid.NewGuid(),
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001"
                },
                Emails = new List<Email>()
                {
                   new Email()
                    {
                        Id = Guid.NewGuid(),
                        EmailAdress = "example@example.com"
                    }
                }
            };
            var personServiceMock = new Mock<PersonService>();
            var personService = personServiceMock.Object;
            personServiceMock.Setup(x => x.Add(person)).Returns(person);

            // create Person object with static data

            // var person = new Person(/* initialize person object */);
            // create person object with dynamic data


            // Act
            var result = personService.Add(person);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Person>(result);
        }

        [Fact]
        public void Find_Should_Return_Persons()
        {
            //set up mock object of PersonService with constructor
            // Arrange

            //Create a mock object of PersonService class with constructor and method and return value

            var mockPerson = new Mock<Person>();
            mockPerson.Setup(x => x.Name).Returns("John Doe");
            mockPerson.Setup(x => x.Age).Returns(30);
            mockPerson.Setup(x => x.Address).Returns(new Address()
            {
                Id = Guid.NewGuid(),
                City = "New York",
                State = "NY",
                ZipCode = "10001"
            });
            mockPerson.Setup(x => x.Emails).Returns(new List<Email>()
            {
                new Email()
                {
                    Id = Guid.NewGuid(),
                    EmailAdress = ""
                }
            });
            var person = mockPerson.Object;

            //var mockPersonService = new Mock<PersonService>();
            //mockPersonService.Setup(x => x.Add(person)).Returns(person);
            //var personService = mockPersonService.Object;
            var mockPersonRepository = new Mock<IGenericRepository<Person>>();
            mockPersonRepository.Setup(x => x.Add(person)).Returns(person);
            var personRepository = mockPersonRepository.Object;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.GetRepository<Person>()).Returns(personRepository);
            var unitOfWork = mockUnitOfWork.Object;
            var mockLogger = new Mock<ILogger<PersonService>>();
           // mockLogger.Setup(x => x.LogError(eventId: LogEvent.ErrorOccured, exception: ex, LogEvent.ErrorOccured.ToString()));
            var logger = mockLogger.Object;
            var mockMediator = new Mock<IMediator>();

            var request = new CreatePersonCommand(
                    name: person.Name, age: person.Age, address: person.Address, emails: person.Emails);


            mockMediator.Setup(x => x.Send(request, default(System.Threading.CancellationToken)
                ).Wait());
            var mediator = mockMediator.Object;
            var personService = new PersonService(unitOfWork, logger, mediator);
        

            //var mockUnitOfWork = new Mock<IUnitOfWork>();
            //mockUnitOfWork.Setup(x => x.GetRepository<Person>()).Returns(personRepository);
            //var unitOfWork = mockUnitOfWork.Object;
            //var mockLogger = new Mock<ILogger<PersonService>>();
            //mockLogger.Setup(x => x.LogError(eventId: LogEvent.ErrorOccured, exception: ex, LogEvent.ErrorOccured.ToString()));
            //var logger = mockLogger.Object;
            //var mockMediator = new Mock<IMediator>();
            //mockMediator.Setup(x => x.Send(
            //    new CreatePersonCommand(
            //        name: entity.Name, age: entity.Age, address: entity.Address, emails: entity.Emails)
            //    ).Wait());
            //var mediator = mockMediator.Object;
            //var personService = new PersonService(unitOfWork, logger, mediator);
            //var mockPersonService = new Mock<PersonService>();
            //mockPersonService.Setup(x => x.Add(person)).Returns(person);
            //var personService = mockPersonService.Object;
            //var mockUnitOfWork = new Mock<IUnitOfWork>();
            //mockUnitOfWork.Setup(x => x.GetRepository<Person>()).Returns(personRepository);
            //var unitOfWork = mockUnitOfWork.Object;
            //var mockLogger = new Mock<ILogger<PersonService>>();
            //mockLogger.Setup(x => x.LogError(eventId: LogEvent.ErrorOccured, exception: ex, LogEvent.ErrorOccured.ToString()));



            //var mockCrm = Mock.Of<IUnitOfWork>(x => x.() == crmPickLists);
            //    var mockCache = Mock.Of<ICacheProvider>(x => x.GetPickLists() == cachePickLists);
            //    var mockLogger = Mock.Of<ILoggingProvider>();
            //    var personServiceMock = new Mock<PersonService>();
            //    // create mock object of PersonService instance

            //    var personService = personServiceMock.Object;
            //var expression = /* initialize expression */;
            Expression<Func<Person, bool>> expression = x => x.Age > 20;
            // Act
            var result = personService.Find(expression);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
        }
        [Fact]
        public void GetAll_Should_Return_Persons_VJ()
        {
            // Arrange
            //Create a mock object of PersonService class with constructor and method and return value and parameter and exception and verify and setup
           

            // Act
            var result = personService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
        }
    [Fact]
    public void GetAll_Should_Return_Persons()
    {
        // Arrange
            var personServiceMock = new Mock<PersonService>();
            var personService = personServiceMock.Object;

            // Act
            var result = personService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Person>>(result);
        }

        [Fact]
        public void GetById_Should_Return_Person()
        {
            // Arrange
            var personServiceMock = new Mock<PersonService>();
            var personService = personServiceMock.Object;
            var id =  Guid.NewGuid();

            // Act
            var result = personService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Person>(result);

            return List.Fir().ID
        }
    }