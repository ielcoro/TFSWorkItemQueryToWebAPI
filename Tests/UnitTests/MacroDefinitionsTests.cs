using FakeItEasy;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
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
        IDisposable shimContext;

        [TestInitialize]
        public void Initialize()
        {
            shimContext = ShimsContext.Create();

            var projectShim = new ShimProject();

            projectShim.NameGet = () => "TestProject";

            ShimQueryItem.AllInstances.ProjectGet = q => projectShim;

            var tfsContextInstance = new FakeTfsContext(shimContext);

            tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContextInstance));
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimContext.Dispose();
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
            string value = projectMacro.GetValue(new ShimQueryDefinition());

            //Assert

            A.CallTo(() => tfsContextMock.CurrentProject).MustHaveHappened();
            Assert.AreEqual("\"TestProject\"", value);
        }

        [TestMethod]
        public void UserMacroDefinitionShouldSetMacroName()
        {
            var userMacro = new MeMacro(A.Dummy<ITfsContext>());

            Assert.AreEqual("Me", userMacro.Name);
        }

        [TestMethod]
        public void UserMacroDefinitionShouldGetValueFromContext()
        {
            //Arrange
            var userMacro = new MeMacro(tfsContextMock);

            //Act
            string value = userMacro.GetValue(new ShimQueryDefinition());

            //Assert

            A.CallTo(() => tfsContextMock.CurrentUser).MustHaveHappened();
            Assert.AreEqual("\"Iñaki Elcoro\"", value);
        }

    }
}
