using FakeItEasy;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFSWorkItemQueryService.Repository;
using UnitTests.Fakes;

namespace UnitTests
{
    [TestClass]
    public class MacroDefinitionsTests
    {
        ITfsContext tfsContextMock;

        [TestInitialize]
        public void Initialize()
        {
            var shimContext = ShimsContext.Create();

            var tfsContextInstance = new FakeTfsContext(shimContext);

            tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContextInstance));
        }

        [TestMethod]
        public void ProjectMacroDefinitionShouldSetMacroName()
        {
            var projectMacro = new ProjectMacro(A.Dummy<ITfsContext>());

            Assert.AreEqual("Project", projectMacro.Name);
        }

        [TestMethod]
        public void ProjectMacroDefinitionShouldGetValueFromContext()
        {
            //Arrange
            var projectMacro = new ProjectMacro(tfsContextMock);
            //Act
            string value = projectMacro.GetValue();

            //Assert

            A.CallTo(() => tfsContextMock.CurrentProject).MustHaveHappened();
            Assert.AreEqual("\"TestProject\"", value);
        }

        [TestMethod]
        public void UserMacroDefinitionShouldSetMacroName()
        {
            var userMacro = new UserMacro(A.Dummy<ITfsContext>());

            Assert.AreEqual("User", userMacro.Name);
        }

        [TestMethod]
        public void UserMacroDefinitionShouldGetValueFromContext()
        {
            //Arrange
            var userMacro = new UserMacro(tfsContextMock);

            //Act
            string value = userMacro.GetValue();

            //Assert

            A.CallTo(() => tfsContextMock.CurrentUser).MustHaveHappened();
            Assert.AreEqual("\"Iñaki Elcoro\"", value);
        }

    }
}
