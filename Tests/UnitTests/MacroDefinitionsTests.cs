using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFSWorkItemQueryService.Repository;

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
            var projectMacro = new ProjectMacro();

            
        }

    }
}
