using System.Security.Cryptography.X509Certificates;
using ControlBS.Facade;
using ControlBS.BusinessObjects;

namespace ControlBS.TestApi;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestGetList_ShouldList()
    {
        CTPERSFacade oFacade = new CTPERSFacade();
        int expectedResult = 5;
        Response<List<CTPERS>> oResponse = oFacade.List();
        int result = oResponse.value != null ? oResponse.value.Count : 0;
        Assert.AreEqual(expectedResult, result);
    }
}