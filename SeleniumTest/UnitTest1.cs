using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver driver;
        public IWebElement firstname;
        public SelectElement state;
        public IWebElement password;
        public IWebElement confirm_password;
        public IWebElement dance_chk;
        public IWebElement btn_toNew;
        public Actions actions;
        public UnitTest1()
        {
            driver = new ChromeDriver("D:\\WebDevV2\\chromedriver_win32");
            driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");
            firstname = driver.FindElement(By.Id("fname"));
            state = new SelectElement(driver.FindElement(By.Id("state")));
            password = driver.FindElement(By.Name("Password"));
            confirm_password = driver.FindElement(By.Name("Confirm Password"));
            dance_chk = driver.FindElement(By.Id("Dance"));
            btn_toNew = driver.FindElement(By.ClassName("caret"));
            actions = new Actions(driver);
        }

        [TestMethod]
        public void NameTypedOrNot()
        {
            firstname.SendKeys("John");
            Assert.AreEqual(firstname.GetAttribute("value"), "John");
        }

        [TestMethod]
        public void StateSelectedOrNot()
        {
            state.SelectByValue("Australia");
            Assert.AreEqual(state.SelectedOption.GetAttribute("value"), "Australia");
        }

        [TestMethod]
        public void VerifyPassword()
        {
            password.SendKeys("123");
            confirm_password.SendKeys("abc");
            Assert.AreNotEqual(password.GetAttribute("value"), confirm_password.GetAttribute("value"));
        }

        [TestMethod]
        public void CheckDance()
        {
            dance_chk.Click();
            Assert.IsTrue(dance_chk.Selected);
        }

        [TestMethod]
        public void CheckGotoNewPage()
        {
            actions.MoveToElement(btn_toNew).Click().Perform();
            WebDriverWait wait = new WebDriverWait(driver , new TimeSpan(10));

            IList<IWebElement> arr = driver.FindElements(By.TagName("a"));
            actions.MoveToElement(arr[2]).Click().Perform();
            
            var browserTabs = driver.WindowHandles;
            driver.SwitchTo().Window(browserTabs[1]);
            Assert.AreEqual(driver.Url, "https://cloudqa.io/");
            driver.Close();
            driver.SwitchTo().Window(browserTabs[0]);
            
        }
    }
}