//using NUnit.Framework;
//using Altom.AltUnityDriver;
//using AltTester.AltTesterSDK.Driver;
//using UnityEngine.Assertions;

//namespace AltUnityTestStarter
//{
//    public class MainMenuTests
//    {
//        AltUnityDriver driver;

//        [OneTimeSetUp]
//        public void Setup()
//        {
//            // Unity oyununa baðlan (localhost:13000 default port)
//            driver = new AltUnityDriver();
//        }

//        [Test]
//        public void ClickPlayButton_And_CheckGameHUD()
//        {
//            // 1. Play butonunu bul ve týkla
//            var playButton = driver.FindObject(By.NAME, "PlayButton");
//            playButton.Tap();

//            // 2. GameHUD adlý objeyi ara
//            var gameUI = driver.FindObject(By.NAME, "GameHUD");

//            // 3. Varsa test baþarýlýdýr
//            Assert.IsNotNull(gameUI);
//        }

//        [OneTimeTearDown]
//        public void TearDown()
//        {
//            driver.Stop();
//        }
//    }
//}