using System.Configuration;

namespace Omada.Utils
{
    public static class Constant
    {
        public static readonly long TakedaCaseFileSize = 866748L;
        public static readonly string TakedaCaseFileName = "Omada+Case+Takeda.pdf";
        public static readonly string TakedaCasePageUrl = ConfigurationManager.AppSettings["AppUrl"] + 
            "en-us/more/customers/cases/takeda-case/download-case--takeda";
        public static readonly string MoreSectionName = "More";
        internal static readonly string PrivacyPolicyHeader = "WEBSITE PRIVACY POLICY";
    }
}
