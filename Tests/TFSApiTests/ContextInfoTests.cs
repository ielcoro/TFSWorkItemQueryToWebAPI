using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFSApiTests
{
    [TestClass]
    public class ContextInfoTests
    {

        [TestMethod]
        public void TFSCurrentUserAccess()
        {
            var tfsServer = new TfsTeamProjectCollection(new Uri(Properties.Settings.Default.TfsAddress));

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());

            Assert.AreEqual("Iñaki Elcoro-Iribe Mayo", tfsServer.AuthorizedIdentity.DisplayName);
        }

        [TestMethod]
        public void TFSCurrentTeamProject()
        {
            var tfsServer = new TfsTeamProjectCollection(new Uri(Properties.Settings.Default.TfsAddress));

            tfsServer.ClientCredentials = new TfsClientCredentials(new WindowsCredential());

            var workItemStore = tfsServer.GetService<WorkItemStore>();
            
            Project project = workItemStore.Projects[Properties.Settings.Default.TeamProject];

            Assert.AreEqual("EquipoIE", project.Name);
        }
    }
}
