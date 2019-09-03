using FakeItEasy;
using GitRemote.Domain;
using GitRemote.Domain.Operations;
using GitRemote.Entities;
using GitRemote.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GitRemoteTests
{
    [TestClass]
    public class GitClientTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OperationListProjects_ThrowsExceptionIfMissingOneParameter()
        {
            Parameters parameters = new Parameters
            {
                GitApiUrl = "GitApi",
                GitToken = "GitToken",
                //GroupName = "GitProjectGroup",
                Operation = "ListProjects"
            };

            var expectedOperation = (OperationType)Enum.Parse(typeof(OperationType), parameters.Operation);

            var git = new GitClient();
            git.OperationParameters = parameters;
            git.Run(expectedOperation);
        }

        [TestMethod]
        public void OperationListProjects_RunsCorrespondingOperation()
        {
            var fakeOperation = A.Fake<IGitOperation>();

            var fakeOperationFactory = A.Fake<IOperationFactory>();
            A.CallTo(() => fakeOperationFactory.CreateOperation(OperationType.ListProjects)).Returns(fakeOperation);

            Parameters parameters = new Parameters { Operation = "ListProjects" };
            var expectedOperation = (OperationType)Enum.Parse(typeof(OperationType), parameters.Operation);

            var git = new GitClient();
            git.OperationFactory = fakeOperationFactory;
            git.OperationParameters = parameters;
            git.Run(expectedOperation);

            A.CallTo(() => fakeOperation.Run()).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void OperationListProjects_PrintsResultsToTraceListeners()
        {
            var fakeOperation = A.Fake<IGitOperation>();

            var fakeOperationFactory = A.Fake<IOperationFactory>();
            A.CallTo(() => fakeOperationFactory.CreateOperation(OperationType.ListProjects)).Returns(fakeOperation);

            Parameters parameters = new Parameters
            {
                Operation = "ListProjects"
            };
            var expectedOperation = (OperationType)Enum.Parse(typeof(OperationType), parameters.Operation);

            var git = new GitClient();
            git.OperationFactory = fakeOperationFactory;
            git.OperationParameters = parameters;
            git.Run(expectedOperation);

            A.CallTo(() => fakeOperation.PrintResultsToTraceListeners()).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void WhenOperationListProjectsReturnsOneProject_TraceListenersReceiveOneLine()
        {
            var groupInformation = new GroupInformation { Id = 20 };
            var projectInformation = new ProjectInformation { WebUrl = "expectedProject" };

            var fakeGitConnector = A.Fake<IGitConnector>();

            A.CallTo(() => fakeGitConnector.GetGroups(A<string>.Ignored))
                .Returns(new List<GroupInformation> { groupInformation });

            A.CallTo(() => fakeGitConnector.GetProjects(groupInformation))
                .Returns(new List<ProjectInformation> { projectInformation });

            Parameters parameters = new Parameters
            {
                GitApiUrl = "GitApi",
                GitToken = "GitToken",
                GroupName = "GitProjectGroup",
                Operation = "ListProjects"
            };
            var expectedOperation = (OperationType)Enum.Parse(typeof(OperationType), parameters.Operation);

            var testTraceListener = new TestTraceListener();
            Trace.Listeners.Clear();
            Trace.Listeners.Add(testTraceListener);

            var git = new GitClient();
            git.OperationParameters = parameters;
            git.GitConnector = fakeGitConnector;
            git.Run(expectedOperation);

            Assert.AreEqual(1, testTraceListener.WritenMessages.Count);
            Assert.IsTrue(testTraceListener.WritenMessages.Contains(projectInformation.WebUrl));
        }
    }
}
