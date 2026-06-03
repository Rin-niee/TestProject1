using OpenQA.Selenium;
using System;

namespace TestProject1.Helpers
{
    internal static class BooksHelper
    {
        public static void AddToFavoritesAndAssert(IWebDriver driver, IWebElement firstProduct, string expectedEventId)
        {

            firstProduct
                .FindElement(By.CssSelector(".favorite__list-item-link"))
                .Click();
            CheckBookInFavorites(driver, expectedEventId);
        }
        public static void CheckBookInFavorites(
        IWebDriver driver,
        string expectedEventId)
        {
            driver.Navigate().Refresh();
            driver.FindElement(By.CssSelector(".favorite__list-link.header-element")).Click();
            var items = driver.FindElements(By.CssSelector(".event-list__item"));
            var found = items.Any(item =>
            {
                var favoriteElement = item.FindElement(
                    By.CssSelector("[data-event-id]")
                );

                return favoriteElement.GetAttribute("data-event-id") == expectedEventId;
            });

            if (found)
            {
                Console.WriteLine("OK: событие найдено в избранном");
            }
            else
            {
                Console.WriteLine($"FAIL: событие с id {expectedEventId} не найдено в избранном");
            }
        }
        public static void CheckBookNotInFavorites(
        IWebDriver driver,
        string expectedEventId)
        {
            driver.Navigate().Refresh();

            driver.FindElement(
                By.CssSelector(".favorite__list-link.header-element")
            ).Click();

            var events = driver.FindElements(
                By.CssSelector("[data-event-id]")
            );

            var found = events.Any(x =>
                x.GetAttribute("data-event-id") == expectedEventId);

            Assert.That(found, Is.False, $"Событие {expectedEventId} всё ещё находится в избранном");
            TestContext.WriteLine(
                $"OK: событие {expectedEventId} успешно удалено из избранного"
            );
        }
    }
}