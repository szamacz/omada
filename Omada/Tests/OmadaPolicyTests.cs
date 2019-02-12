using NUnit.Framework;

namespace Omada.Tests
{
    [TestFixture]
    public class OmadaPolicyTests : BaseTest
    {

        [Test]
        public void Closed_privacy_policy_shouldnt_be_visible_on_pages()
        {
            homePage.GoTo();

            var window = homePage.OpenReadPrivacyPolicyInNewTab();

            privacyStatementPage
                .VerifyIsAt()
                .CloseCookieAndPrivacyPolicy();
            privacyStatementPage.CloseTabAndSwitchBackTo(window);

            RefreshPage();

            homePage
                .VerifyIsAt()
                .VerifyPrivacyPolicyNotVisible();
        }
    }
}
