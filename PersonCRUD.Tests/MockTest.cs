using System.Collections.Generic;
using Moq;
using Autofac.Extras.Moq;
using Xunit;
using PersonCRUD.Library;

namespace PersonCRUD.Tests
{
    public class MockTest
    {
        [Theory]
        [InlineData("Nicholas","Browning","nicholas.browning@email.com","0402893048")]
        public void PersonCreate_Successful(string personFirstName, string personLastName, string personEmail, string personPhone)
        {
            PersonDAL personDAL = new PersonDAL();

            Person expectedPerson = new Person
            {
                PersonID = 0,
                PersonFirstName = personFirstName,
                PersonLastName = personLastName,
                PersonEmail = personEmail,
                PersonPhone = personPhone
            };

            var actual = personDAL.PersonCreate_Mock(personFirstName, personLastName, personEmail, personPhone);

            Assert.Equal(expectedPerson.PersonID, actual.PersonID);
            Assert.Equal(expectedPerson.PersonFirstName, actual.PersonFirstName);
            Assert.Equal(expectedPerson.PersonLastName, actual.PersonLastName);
            Assert.Equal(expectedPerson.PersonEmail, actual.PersonEmail);
            Assert.Equal(expectedPerson.PersonPhone, actual.PersonPhone);
        }

        [Fact]
        public void PersonCreate_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string PersonFirstName = "Nicholas";
                string PersonLastName = "Browning";
                string PersonEmail = "nicholas.browning@email.com";
                string PersonPhone = "0402893048";

                mock.Mock<IPersonDAL>().Setup(x => x.PersonCreate(PersonFirstName, PersonLastName, PersonEmail, PersonPhone));

                var cls = mock.Create<PersonORM>();

                cls.PersonCreate(PersonFirstName, PersonLastName, PersonEmail, PersonPhone);

                mock.Mock<IPersonDAL>().Verify(x => x.PersonCreate(PersonFirstName, PersonLastName, PersonEmail, PersonPhone), Times.Exactly(0));
            }
        }

        [Fact]
        public void ReadAllPeople_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IPersonDAL>().Setup(x => x.ReadAllPeople()).Returns(GetSamplePeople());

                var cls = mock.Create<PersonORM>();
                var expected = GetSamplePeople();
                var actual = cls.ReadAllPeople();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].PersonFirstName, actual[i].PersonFirstName);
                    Assert.Equal(expected[i].PersonLastName, actual[i].PersonLastName);
                }
            }
        }

        private List<Person> GetSamplePeople()
        {
            List<Person> output = new List<Person>
            {
                new Person
                {
                    PersonID = 0,
                    PersonFirstName = "grewgre",
                    PersonLastName = "rewgre",
                    PersonEmail = "grewgre",
                    PersonPhone = "ewgrewger"
                },
                new Person
                {
                    PersonID = 1,
                    PersonFirstName = "John",
                    PersonLastName = "Lennon",
                    PersonEmail = "john.lennon@email.com",
                    PersonPhone = "0498189432"
                },
                new Person
                {
                    PersonID = 2,
                    PersonFirstName = "Nick",
                    PersonLastName = "Browning",
                    PersonEmail = "nick.browning@email.com",
                    PersonPhone = "0408298493"
                },
                new Person
                {
                    PersonID = 3,
                    PersonFirstName = "Nick",
                    PersonLastName = "Jonas",
                    PersonEmail = "nick.jonas@eami.com",
                    PersonPhone = "09490384"
                }
            };

            return output;
        }
    }
}
