using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Discuz.Api.Methods;
using System.Diagnostics;

namespace Discuz.Api.Test {
    [TestClass]
    public class UnitTest1 {
        [TestInitialize]
        public void Init() {
            ApiClient.OnMessage += ApiClient_OnMessage;
        }

        [TestCleanup]
        public void CleanUp() {
            ApiClient.OnMessage -= ApiClient_OnMessage;
        }

        void ApiClient_OnMessage(object sender, MessageArgs e) {
            Debug.WriteLine(e.Message);
        }

        [TestMethod]
        public void TestIndex() {
            var method = new ForumIndex();
            var result = ApiClient.Execute(method).Result;
        }

        [TestMethod]
        public void ForumDisplayTest() {
            var method = new ForumDisplay() {
                ForumID = 23,
                PageSize = 10
            };
            var result = ApiClient.Execute(method).Result;
        }

        [TestMethod]
        public void ViewThreadTest() {
            var method = new ViewThread() {
                ThreadID = 3063982
            };
            var result = ApiClient.Execute(method).Result;
        }
    }
}
