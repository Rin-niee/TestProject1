using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using TestProject1.Helpers;


namespace TestProject1
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    [TestFixture("edge")]
    [TestFixture("safari")]
    public class Tests
    {
        private IWebDriver driver;
        private readonly string browser;

        public Tests(string browser)
        {
            this.browser = browser;
        }

        [SetUp]
        public void Setup()
        {
            switch (browser.ToLower())
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                case "edge":
                    driver = new EdgeDriver();
                    break;
                case "safari":
                    driver = new SafariDriver();
                    break;

                default:
                    driver = new ChromeDriver();
                    break;
            }
        }
        //Проверка добавления элемента с главной страницы + перезагрузка страницы
        [Test]
        public void AddFirstBookToFavoritesFromIndexPage()
        {
            driver.Navigate().GoToUrl("https://www.vl.ru/afisha/");
            var firstProduct = driver.FindElements(By.CssSelector(".popular-in-last-time__main .event-list__item"))[0];
            var eventId = firstProduct
                .FindElement(By.CssSelector("[data-event-id]"))
                .GetAttribute("data-event-id");
            BooksHelper.AddToFavoritesAndAssert(driver, firstProduct, eventId);
        }

        //Проверка добавления элемента с главной страницы + перезагрузка страницы
        [Test]
        public void AddFirstBookToFavoritesFromBookPage()
        {
            driver.Navigate().GoToUrl("https://www.vl.ru/afisha/");
            var firstProduct = driver.FindElements(By.CssSelector(".popular-in-last-time__main .event-list__item"))[0];
            firstProduct
                .FindElement(By.CssSelector(".event-list__item-title")).Click();
            driver.FindElement(By.CssSelector(".event__favorite-text")).Click();

            var eventId = driver
                .FindElement(By.CssSelector(".event__favorite-link[data-event-id]"))
                .GetAttribute("data-event-id");
            BooksHelper.CheckBookInFavorites(driver, eventId);
        }

        //добавление и удаления афиши из избранного с проверкой
        [Test]
        public void AddRemoveBookFromFavorites()
        {
            driver.Navigate().GoToUrl("https://www.vl.ru/afisha/");

            var firstProduct = driver.FindElements(
                By.CssSelector(".popular-in-last-time__main .event-list__item")
            )[0];

            var eventId = firstProduct
                .FindElement(By.CssSelector("[data-event-id]"))
                .GetAttribute("data-event-id");

            firstProduct
                .FindElement(By.CssSelector(".favorite__list-item-link"))
                .Click();

            BooksHelper.CheckBookInFavorites(driver, eventId);

            driver.Navigate().Back();

           firstProduct = driver.FindElements(
                By.CssSelector(".event-list__items .event-list__item")
            )[0];

            firstProduct
                .FindElement(By.CssSelector(".favorite__list-item-link"))
                .Click();

            BooksHelper.CheckBookNotInFavorites(driver, eventId);
        }

        //удаление афиши со страницы избранного
        [Test]
        public void AddRemoveBookFromFavoritesPages()
        {
            driver.Navigate().GoToUrl("https://www.vl.ru/afisha/");

            var firstProduct = driver.FindElements(By.CssSelector(".popular-in-last-time__main .event-list__item"))[0];
            var eventId = firstProduct
                .FindElement(By.CssSelector("[data-event-id]"))
                .GetAttribute("data-event-id");
            BooksHelper.AddToFavoritesAndAssert(driver, firstProduct, eventId);
            firstProduct = driver.FindElements(By.CssSelector(".event-list__items .event-list__item"))[0];
            eventId = firstProduct
                .FindElement(By.CssSelector("[data-event-id]"))
                .GetAttribute("data-event-id");
            firstProduct
                .FindElement(By.CssSelector(".favorite__list-item-link"))
                .Click();
            BooksHelper.CheckBookNotInFavorites(driver, eventId);
        }


        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
