using ApplicationGenerator.General.Loaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ApplicationGenerator.General.Tests
{
    [TestClass]
    public class LoadingTest
    {
        [TestMethod]
        public void LoadAndDeserializeSampleFile()
        {
            string file = File.ReadAllText("./TestObjectDefinition.json");
            Assert.IsNotNull(JSONObjectModelLoader.Load(file));
        }
    }
}
