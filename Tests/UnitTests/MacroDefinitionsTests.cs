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
        [TestMethod]
        public void ProjectMacroDefinitionShouldSetMacroName()
        {
            var projectMacro = new ProjectMacro();

            Assert.AreEqual("Project", projectMacro.Name);
        }

        [TestMethod]
        public void ProjectMacroDefinitionShouldGetValueFromContext()
        {
            //Arrange
            var shimContext = ShimsContext.Create();

            var tfsContextInstance = new FakeTfsContext(shimContext);

            var tfsContextMock = A.Fake<ITfsContext>(o => o.Wrapping(tfsContextInstance));

            //Act
            var projectMacro = new ProjectMacro();

            string value = projectMacro.GetValue();

            //Assert

            A.CallTo(() => tfsContextMock.CurrentProject).MustHaveHappened();
            Assert.AreEqual("\"TestProject\"", value);
        }

    }
}
