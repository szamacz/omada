using NUnit.Framework;
using Omada.Tests;

namespace Omada.Tests
{
    [TestFixture]
    public class OmadaGeneralTests : BaseTest
    {
        [SetUp]
        public void Before()
        {
            homePage.GoTo();
        }

        [Test]
        public void Can_search_and_open_news()
        {
            var articleName = "Gartner IAM Summit 2016 - London";

            homePage
                .VerifyIsAt()
                .SearchFor("gartner");

            searchPage
                .VerifyIsAt()
                .VerifyNumberOfResulstGreaterThanOne()
                //.VerifyListContainsArticle("There is Safety in Numbers")  //such article doesn't exist
                .OpenArticle(articleName);

            newsDetailsPage
                .VerifyNewsArticle(articleName)
                .GoToNewsSubsection();

            newsPage
                .VerifyNewsContainsArticle(articleName);
        }

        [Test]
        public void Can_change_location_in_contact_page()
        {
            homePage.GoToContactPage();

            contactPage
                .SelectLocation("U.S. West")
                .HoverOverLocation("Germany");
        }

        [Test]
        public void Can_download_pdf_case_file()
        {
            homePage.GoToCasesPage();

            casesPage.OpenTakedDownloadPage();

            caseDetailsPage.FillFormWithFakeData();

            caseTakedaDownloadPage
                .GoTo()
                .DownloadFileAndVerify();
        }
    }
}
