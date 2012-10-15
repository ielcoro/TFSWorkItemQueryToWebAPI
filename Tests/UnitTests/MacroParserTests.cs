using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService.Repository;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using FakeItEasy;

namespace UnitTests
{
    //TFS Macro info here: http://msdn.microsoft.com/en-us/library/dd286638.aspx#qvariables
    [TestClass]
    public class MacroParserTests
    {
        [TestMethod]
        public void AllMacrosMustHaveAName()
        {
            string name = "Project";

            var macro = new Macro(name);

            Assert.AreEqual(macro.Name, name);
        }

        [TestMethod]
        public void MacroNameCannotBeNullOrEmpty()
        {
            string emptyName = "";
            string nullName = null;

            ExtAssert.Throws<ArgumentException>(() => new Macro(emptyName));
            ExtAssert.Throws<ArgumentException>(() => new Macro(nullName));
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

            var macroMock = A.Fake<IMacro>();

            A.CallTo(() => macroMock.Name).Returns("Project");
            A.CallTo(() => macroMock.GetValue()).Returns("\"TestProject\"");

            //Act
            var parser = new MacroParser(new List<IMacro>() { macroMock });

            QueryDefinition parsedQuery = parser.Replace(queryDefinition);

            //Assert
            A.CallTo(() => macroMock.GetValue()).MustHaveHappened();
            A.CallTo(() => macroMock.Name).MustHaveHappened();
            Assert.AreEqual(expectedQuery, parsedQuery.QueryText);

        }

        [TestMethod]
        public void MacroParserWithTwoMatchingMacrosBothTokensShouldBeReplaced()
        {
            //Arrange
            string query = "SELECT System.ID, System.Title from workitems WHERE Project = @Project AND CreatedBy = @Me";
            var queryDefinition = new QueryDefinition("test", query);

            string expectedQuery = "SELECT System.ID, System.Title from workitems WHERE Project = \"TestProject\" AND CreatedBy = \"Iñaki Elcoro\"";

            var projectMacroMock = A.Fake<IMacro>();

            A.CallTo(() => projectMacroMock.Name).Returns("Project");
            A.CallTo(() => projectMacroMock.GetValue()).Returns("\"TestProject\"");

            var userMacroMock = A.Fake<IMacro>();

            A.CallTo(() => userMacroMock.Name).Returns("Me");
            A.CallTo(() => userMacroMock.GetValue()).Returns("\"Iñaki Elcoro\"");

            //Act
            var parser = new MacroParser(new List<IMacro>() { projectMacroMock, userMacroMock });

            QueryDefinition parsedQuery = parser.Replace(queryDefinition);

            //Assert
            A.CallTo(() => projectMacroMock.GetValue()).MustHaveHappened();
            A.CallTo(() => projectMacroMock.Name).MustHaveHappened();
            A.CallTo(() => userMacroMock.GetValue()).MustHaveHappened();
            A.CallTo(() => userMacroMock.Name).MustHaveHappened();

            Assert.AreEqual(expectedQuery, parsedQuery.QueryText);
        }

    }
}
