namespace Game.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        int test = 6;
        Assert.IsTrue(test == 6);
    }

    [TestMethod]
    public void TestMethod2()
    {
        int test = 5;
        Assert.IsTrue(test == 6);
    }
}