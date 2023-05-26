using Xunit.Abstractions;
using Xunit.Sdk;

namespace Laboratorio.CRUD.Company.Tests
{
    public class OrderHelper : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases) where TTestCase : ITestCase =>
            testCases.OrderBy(testCase => testCase.TestMethod.Method.Name);
    }
}