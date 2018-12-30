using ApplicationGenerator.General.CodeGen.CSharp;
using ApplicationGenerator.General.Loaders;
using ApplicationGenerator.General.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ApplicationGenerator.General.Tests
{
    [TestClass]
    public class CodeGenTest
    {
        private ObjectModel objectModel;
        [TestInitialize]
        public void Init()
        {
            string file = File.ReadAllText("./TestObjectDefinition.json");
            objectModel = JSONObjectModelLoader.Load(file);
        }

        [TestMethod]
        public void CSharpCodeGenTest()
        {
            CSharpFromObjectModel cs = new CSharpFromObjectModel();
            var code = cs.Generate(objectModel);
            Assert.IsTrue(code.Length > 0);
        }
    }
}
