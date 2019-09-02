using FakeItEasy;
using GitRemote.Domain;
using GitRemote.Domain.Operations;
using GitRemote.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GitRemoteTests
{
    [TestClass]
    public class UiTests
    {
        [TestMethod]
        public void PassingUrlAsArgument_InitializesGitUrl()
        {
            var expectedContent = "https://remoteGitRepo.com";

            var ui = new UI() { Arguments = new string[] { $"-GitApiUrl:{expectedContent}" } };
            Parameters parameters = ui.ProcessArguments();

            Assert.AreEqual(expectedContent, parameters.GitApiUrl);
        }

        [DataTestMethod]
        [DataRow("GitApiUrl")]
        [DataRow("GitToken")]
        [DataRow("GroupName")]
        public void PassingAPropertyArgument_InitializesCorrespondingProperty(string parameterName)
        {
            var expectedContent = "argumentContent";

            var ui = new UI() { Arguments = new string[] { $"-{parameterName}:{expectedContent}" } };
            Parameters parameters = ui.ProcessArguments();

            string propertyValue = (string)parameters.GetType().GetProperty(parameterName).GetValue(parameters, null);

            Assert.AreEqual(expectedContent, propertyValue);
        }

        [DataTestMethod]
        [DataRow("ListProjects")]
        public void PassingAnOperationArgument_InitializesOperationProperty(string parameterName)
        {
            var expectedOperation = (Operation)Enum.Parse(typeof(GitRemote.Domain.Operation), parameterName);

            var ui = new UI() { Arguments = new string[] { $"-{parameterName}" } };
            Parameters parameters = ui.ProcessArguments();

            Assert.AreEqual(expectedOperation.ToString(), parameters.Operation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OperationListProjects_ThrowsExceptionIfMissingOneParameter()
        {
            var ui = new UI()
            {
                Arguments = new string[] 
                {
                    $"-GitApiUrl:https://remoteGitRepo.com",
                    $"-GitToken:1234",
                    //$"-GroupName:Area91",
                    $"-ListProjects"
                }
            };
            Parameters parameters = ui.ProcessArguments();

            var expectedOperation = (Operation)Enum.Parse(typeof(GitRemote.Domain.Operation), parameters.Operation);

            var git = new GitClient();
            git.Run(expectedOperation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OperationListProjects_RunsCorrespondingOperation()
        {
            var ui = new UI()
            {
                Arguments = new string[]
                {
                    $"-GitApiUrl:https://remoteGitRepo.com",
                    $"-GitToken:1234",
                    $"-GroupName:Area91",
                    $"-ListProjects"
                }
            };

            var fakeOperation = A.Fake<IGitOperation>();

            var fakeOperationFactory = A.Fake<IOperationFactory>();
            A.CallTo(() => fakeOperationFactory.CreateOperation(Operation.ListProjects)).Returns(fakeOperation);

            Parameters parameters = ui.ProcessArguments();
            var expectedOperation = (Operation)Enum.Parse(typeof(GitRemote.Domain.Operation), parameters.Operation);

            var git = new GitClient();

            git.Run(expectedOperation);

            A.CallTo(() => fakeOperation.Run()).MustHaveHappenedOnceExactly();
        }
    }
}
