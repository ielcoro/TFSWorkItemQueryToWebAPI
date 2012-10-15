using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService.Repository;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using FakeItEasy;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;

namespace UnitTests
{
    //TFS Macro info here: http://msdn.microsoft.com/en-us/library/dd286638.aspx#qvariables
    [TestClass]
    public class MacroParserTests
    {
        class FakeMacro : Macro
        {
            public FakeMacro(string name)
                : base(name)
            {

            }

            public override string GetValue(QueryDefinition definition)
            {
                return null;
            }
        }

        IMacro projectMacroMock;
        IMacro userMacroMock;
        MacroParser parserWithMacros;
        IDisposable shimContext;

        [TestInitialize]
        public void Initialize()
        {
            shimContext = ShimsContext.Create();
            
            var project = new ShimProject();

            project.NameGet = () => "TestProject";

            ShimQueryItem.AllInstances.ProjectGet = (q) => project;
            var definition = new ShimQueryDefinition();

            projectMacroMock = A.Fake<IMacro>();

            A.CallTo(() => projectMacroMock.Name).Returns("Project");
            A.CallTo(() => projectMacroMock.GetValue(A<QueryDefinition>.Ignored)).Returns("\"TestProject\"");

            userMacroMock = A.Fake<IMacro>();

            A.CallTo(() => userMacroMock.Name).Returns("Me");
            A.CallTo(() => userMacroMock.GetValue(A<QueryDefinition>.Ignored)).Returns("\"Iñaki Elcoro\"");

            parserWithMacros = new MacroParser(new IMacro[] { projectMacroMock, userMacroMock });
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimContext.Dispose();
        }

        [TestMethod]
        public void AllMacrosMustHaveAName()
        {
            string name = "Project";

            var macro = new FakeMacro(name);

            Assert.AreEqual(macro.Name, name);
        }

        [TestMethod]
        public void MacroNameCannotBeNullOrEmpty()
        {
            string emptyName = "";
            string nullName = null;

            ExtAssert.Throws<ArgumentException>(() => new FakeMacro(emptyName));
            ExtAssert.Throws<ArgumentException>(() => new FakeMacro(nullName));
        }

        [TestMethod]
        public void MacroParserWithNoMacroDefinitionShouldNotReplaceAnyToken()
        {
            string expectedQuery = "SELECT System.ID, System.Title from workitems WHERE Project = @Project";
            var queryDefinition = new QueryDefinition("test", expectedQuery);

            var parser = new MacroParser(Enumerable.Empty<IMacro>());

            QueryDefinition parsedDefinition = parser.Replace(queryDefinition);

            Assert.AreEqual(expectedQuery, parsedDefinition.QueryText);
        }

        [TestMethod]
        public void MacroParserWithMatchingMacroShouldReplaceToken()
        {
            //Arrange
            string query = "SELECT System.ID, System.Title from workitems WHERE Project = @Project";
            var queryDefinition = new QueryDefinition("test", query);

            string expectedQuery = "SELECT System.ID, System.Title from workitems WHERE Project = \"TestProject\"";

            //Act
            QueryDefinition parsedQuery = parserWithMacros.Replace(queryDefinition);

            //Assert
            A.CallTo(() => projectMacroMock.GetValue(queryDefinition)).MustHaveHappened();
            A.CallTo(() => projectMacroMock.Name).MustHaveHappened();

            Assert.AreEqual(expectedQuery, parsedQuery.QueryText);

        }

        [TestMethod]
        public void MacroParserWithTwoMatchingMacrosBothTokensShouldBeReplaced()
        {
            //Arrange
            string query = "SELECT System.ID, System.Title from workitems WHERE Project = @Project AND CreatedBy = @Me";
            var queryDefinition = new QueryDefinition("test", query);

            string expectedQuery = "SELECT System.ID, System.Title from workitems WHERE Project = \"TestProject\" AND CreatedBy = \"Iñaki Elcoro\"";

            //Act
            QueryDefinition parsedQuery = parserWithMacros.Replace(queryDefinition);

            //Assert
            A.CallTo(() => projectMacroMock.GetValue(queryDefinition)).MustHaveHappened();
            A.CallTo(() => projectMacroMock.Name).MustHaveHappened();
            A.CallTo(() => userMacroMock.GetValue(queryDefinition)).MustHaveHappened();
            A.CallTo(() => userMacroMock.Name).MustHaveHappened();

            Assert.AreEqual(expectedQuery, parsedQuery.QueryText);
        }

    }
}
