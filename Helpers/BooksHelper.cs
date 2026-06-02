using OpenQA.Selenium;
using System;

namespace TestProject1.Helpers
{
    internal static class BooksHelper
    {
        public static void AddToFavoritesAndAssert(IWebDriver driver, IWebElement firstProduct, IWebElement titleElement)
        {
            var bookTitle = titleElement.Text.Trim();

            firstProduct
                .FindElement(By.CssSelector(".icon-fave"))
                .Click();
            driver.Navigate().Refresh();

            driver.FindElement(By.XPath("//a[.//span[text()='Отложено']]"))
                .Click();

            var savedFirstItem = driver
                .FindElements(By.CssSelector(".products-row .product"))[0];

            var savedTitle = savedFirstItem
                .FindElement(By.CssSelector(".product-title"))
                .Text
                .Trim();

            if (savedTitle == bookTitle)
            {
                Console.WriteLine("OK: книга успешно добавлена в отложенные");
            }
            else
            {
                Console.WriteLine($"FAIL: ожидали '{bookTitle}', но нашли '{savedTitle}'");
            }
        }
        public static void CheckBookInFavorites(
        IWebDriver driver,
        string expectedTitle)
            {
                driver.FindElement(
                    By.XPath("//a[.//span[text()='Отложено']]"))
                    .Click();

                var savedFirstItem = driver
                    .FindElements(By.CssSelector(".products-row .product"))[0];

                var savedTitle = savedFirstItem
                    .FindElement(By.CssSelector(".product-title"))
                    .Text
                    .Trim();

                Assert.That(savedTitle, Is.EqualTo(expectedTitle));
            }
    }
}