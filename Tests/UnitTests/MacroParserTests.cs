using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSWorkItemQueryService.Repository;

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
    }
}
