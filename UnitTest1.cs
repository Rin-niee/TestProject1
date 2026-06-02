using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject1.Helpers;


namespace TestProject1
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }
        //Проверка добавления элемента с главной страницы
        [Test]
        public void AddFirstBookToFavoritesFromIndexPage()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru");
            var firstProduct = driver.FindElements(By.CssSelector(".genres-carousel__item .product"))[0];
            var bookTitle = firstProduct
                .FindElement(By.CssSelector(".product-title"));
                
            BooksHelper.AddToFavoritesAndAssert(driver, firstProduct, bookTitle);
        }
        //Проверка добавления элемента из каталога + сохранение при обновлении
        [Test]
        public void AddFirstBookToFavoritesFromCategory()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru");
            driver.FindElement(By.LinkText("Книги")).Click();
            var firstProduct = driver.FindElements(By.CssSelector(".products-row-outer .genres-carousel__container .genres-carousel__item"))[0];
            var bookTitle = firstProduct
             .FindElement(By.CssSelector(".product-title-link"));
            BooksHelper.AddToFavoritesAndAssert(driver, firstProduct, bookTitle);
        }


        //Проверка добавления элемента с страницы товара в избранное
        [Test]
        public void AddFirstBookToFavoritesFromBookPage()
        {
            driver.Navigate().GoToUrl("https://www.labirint.ru");
            var firstProduct = driver.FindElements(By.CssSelector(".genres-carousel__item .product"))[0];
            var bookTitle = firstProduct
                .FindElement(By.CssSelector(".product-title"));
            bookTitle.Click();
            var favoriteButton = driver.FindElement(
                By.XPath("//section[contains(@class,'area-price')]//button[@alt='Добавить в избранное']")
            );
            var bookTitle_new = driver.FindElement(By.TagName("h1"));
            var bookTitle_str = bookTitle_new.Text.Trim();

            favoriteButton.Click();
            BooksHelper.CheckBookInFavorites(driver, bookTitle_str);
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}
