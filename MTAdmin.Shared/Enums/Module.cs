namespace MTAdmin.Shared.Enums
{
    public enum Module
    {
        Dashboard,
        Languages,
        Categories,
        Tags,
        Templates,
        Enquiries,
        EmailSubscriber,
        UserLogs
    }

    public enum ClientModule
    {
        Home,
        Categories,
        Tags,
        AboutUs,
        Contact,
        Disclaimer,
        PrivacyPolicy,
        Search
    }

    public enum TemplateTypeEnum
    {
        Image=1,
        Video=2,
        Undefined=3
    }

    public enum FilterByEnum
    {
        latest=1,
        popular=2, // top popular
        name = 3
    }
    public enum CategoryFilterEnum
    {
        all=1,
        trending=2
    }
}
