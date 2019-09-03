using FakeItEasy;
using GitRemote.Domain;
using GitRemote.Domain.Operations;
using GitRemote.Entities;
using GitRemote.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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

            var fakeGitConnector = A.Fake<IGitConnector>();
            A.CallTo(() => fakeGitConnector.GetGroups(A<string>.Ignored))
                .Returns(new List<GroupInformation> { new GroupInformation { Id = 20 } });

            Parameters parameters = new Parameters
            {
                GitApiUrl = "GitApi",
                GitToken = "GitToken",
                GroupName = "GitProjectGroup",
                Operation = "ListProjects"
            };
            var expectedOperation = (OperationType)Enum.Parse(typeof(GitRemote.Domain.OperationType), parameters.Operation);

            var git = new GitClient();
            git.OperationFactory = fakeOperationFactory;
            git.OperationParameters = parameters;
            git.GitConnector = fakeGitConnector;
            git.Run(expectedOperation);

            A.CallTo(() => fakeOperation.Run()).MustHaveHappenedOnceExactly();
        }
    }
}
