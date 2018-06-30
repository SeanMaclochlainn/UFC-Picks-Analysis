﻿using FightData.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor.Test.WebpageParsing
{
    [TestClass]
    public class TestHtmlDocumentGenerator
    {
        private EntityDataGenerator entityDataGenerator;

        public TestHtmlDocumentGenerator()
        {
            entityDataGenerator = new EntityDataGenerator(new Database().Context);
        }

        [TestMethod]
        public void GenerateHtmlDocument()
        {
            HtmlDocumentGenerator htmlDocumentGenerator = new HtmlDocumentGenerator(entityDataGenerator.GetWebpage());

            Assert.IsNotNull(htmlDocumentGenerator.HtmlDocument);
        }
    }
}
