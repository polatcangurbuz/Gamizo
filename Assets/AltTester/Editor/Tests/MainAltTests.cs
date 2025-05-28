using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;
using System.Threading;

public class MainAltTests
{   //Important! If your test file is inside a folder that contains an .asmdef file, please make sure that the assembly definition references NUnit.
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        //altDriver =new AltDriver();
        altDriver = new AltDriver();

        altDriver.LoadScene("GameScene");
        System.Threading.Thread.Sleep(1000);
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void MainStartButtonTest()
    {
        altDriver.LoadScene("GameScene");
        System.Threading.Thread.Sleep(1000); // 1 saniye bekle, sahnenin yüklenmesi için
        var startButton = altDriver.FindObject(By.NAME, "StartButton");
        Assert.IsNotNull(startButton, "StartButton bulunamadý!");
        startButton.Click();
        System.Threading.Thread.Sleep(1000); // 1 saniye bekle, sahnenin yüklenmesi için
    }

    [Test]
    public void MainSettingsButtonTest()
    {
        altDriver.LoadScene("GameScene");
        System.Threading.Thread.Sleep(1000); // 1 saniye bekle, sahnenin yüklenmesi için
        var mainButton = altDriver.FindObject(By.NAME, "MainOptionsButton");
        Assert.IsNotNull(mainButton, "MainOptionsButton bulunamadý!");
        mainButton.Click();
        Thread.Sleep(1000);
    }

}