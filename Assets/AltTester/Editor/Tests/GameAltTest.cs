using NUnit.Framework;
using AltTester.AltTesterSDK.Driver;

public class GameAltTest
{   //Important! If your test file is inside a folder that contains an .asmdef file, please make sure that the assembly definition references NUnit.
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver =new AltDriver();

        altDriver.LoadScene("GameScene");
        System.Threading.Thread.Sleep(1000);

        // StartButton'a basýp GameCanvas'ý aç
        var startButton = altDriver.FindObject(By.NAME, "StartButton");
        Assert.IsNotNull(startButton, "StartButton bulunamadý!");
        startButton.Click();
        System.Threading.Thread.Sleep(1000);
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void OptionsButtonTest()
    {
        var optionsButton = altDriver.FindObject(By.NAME, "Options Button").Click();
        Assert.IsNotNull(optionsButton, "optionsButton bulunamadý!");
        System.Threading.Thread.Sleep(1000);
    }
    [Test]
    public void ChatButtonTest()
    {
        var chatButton = altDriver.FindObject(By.NAME, "ChatButton").Click();
        Assert.IsNotNull(chatButton, "chatButton bulunamadý!");
        System.Threading.Thread.Sleep(1000);
    }
    [Test]
    public void GoogleAdsButtonTest()
    {
        var GoogleAdsButton = altDriver.FindObject(By.NAME, "GoogleAdsButton").Click();
        Assert.IsNotNull(GoogleAdsButton, "GoogleAdsButton bulunamadý!");
        System.Threading.Thread.Sleep(1000);
    }
  
    [Test]
    public void InterAdsButtonTest()
    {
        var InterAds = altDriver.FindObject(By.NAME, "InterAds").Click();
        Assert.IsNotNull(InterAds, "InterAds bulunamadý!");
        System.Threading.Thread.Sleep(1000);
    }

    [Test]
    public void RewardedAdsButtonTest()
    {
        var RewardedAds = altDriver.FindObject(By.NAME, "RewardedAds").Click();
        Assert.IsNotNull(RewardedAds, "RewardedAds bulunamadý!");
        System.Threading.Thread.Sleep(1000);
    }

}