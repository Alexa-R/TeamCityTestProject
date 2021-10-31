using System.Configuration;
using AutomationPracticeProject.Helpers;
using AutomationPracticeProject.PageObjects;
using NUnit.Framework;

namespace AutomationPracticeProject.TestCases.UiTestCases.PDP
{
    public class AddProduct : BaseTest
    {
        [Test, Category("High")]
        public void AddProductToCartFromPdp()
        {
            Pages.BasePage.EnterItemInSearchInputField("Printed Dress");
            Pages.BasePage.ClickItemFromSearchResultPopup(1);
            var productTitle =  Pages.ProductPage.GetProductTitleText();
            Pages.ProductPage.ClickAddToCartButton();
            Pages.ProductCartPopup.ClickProceedToCheckoutButton();
            Assert.AreEqual(productTitle, Pages.CheckoutPage.GetLastProductTitleText());
        }
    }
}