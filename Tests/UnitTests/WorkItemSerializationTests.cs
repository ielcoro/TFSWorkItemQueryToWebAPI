using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.TeamFoundation.WorkItemTracking.Client.Fakes;
using TFSWorkItemQueryService.Repository;
using TFSWorkItemQueryService.Repository.Model;

namespace UnitTests
{
    [TestClass]
    public class WorkItemSerializationTests
    {
        IDisposable shimContext;

        [TestInitialize]
        public void Initialize()
        {
            shimContext = ShimsContext.Create();
        }

        [TestCleanup]
        public void CleanUp()
        {
            shimContext.Dispose();
        }

        [TestMethod]
        public void WorkItemNativePropertiesShouldBeAccessedAsSuch()
        {
            var workItem = new ShimWorkItem();

            workItem.TitleGet = () => "Title";
            workItem.DescriptionGet = () => "Description";

            dynamic workItemWrapper = new WorkItemModel(workItem);

            Assert.AreEqual("Title", workItemWrapper.Title);
            Assert.AreEqual("Description", workItemWrapper.Description);
        }

        [TestMethod]
        public void WorkItemTemplatePropertiesShouldBeAccessedAsNativeProperties()
        {
            var workItem = new ShimWorkItem();

            var fieldCollectionFake = new ShimFieldCollection();

            fieldCollectionFake.Bind(new Field[] 
            {
                new ShimField() 
                {
                    NameGet = () => "CustomField", 
                    ValueGet = () => "CustomValue"
                }
            });

            workItem.FieldsGet = () => fieldCollectionFake;

            dynamic workItemWrapper = new WorkItemModel(workItem);

            Assert.AreEqual("CustomValue", workItemWrapper.CustomField);
        }

    }
}
