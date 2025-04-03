namespace FSH.Starter.Shared.Authorization;
public static class TenantConstants
{
    public static class Root
    {
        public const string Id = "root";
        public const string Name = "Root";
        public const string EmailAddress = "admin@root.com";
        public const string Address = "admin@root.com";
        public const string DefaultProfilePicture = "assets/defaults/profile-picture.webp";
        public const string PhoneNumber = "";
        public const string PrimaryColor = "#1d8cc1";
        public const string SecondaryColor = "#7d858c";
        public const string Logo = "/logo-light.svg";        
        public const string ReportFooter = "";
        public const string ReportHeader = "";
    }
    public static class DefaultTenantOrganization
    {
        public const int Id = 1;
        public const string CommonName = "TST-886431145-399999999900003";
        public const string SerialNumber = "1-TST|2-TST|3-ed22f1d8-e6a2-1118-9b58-d9a8f11e445f";
        public const string OrganizationIdentifier = "399999999900003";
        public const string IdentifierType = "TST";
        public const string OrganizationUnitName = "Riyadh Branch";
        public const string OrganizationName = "Qlines";  
        public const string CountryName = "SA";  
        public const string InvoiceType = "1100";  
        public const string LocationAddress = "RRRD2929";  
        public const string IndustryBusinessCategory = "IT Services";  
        public const string TenantId = "1";  
    }

    public const string DefaultPassword = "123Pa$$word!";

    public const string Identifier = "tenant";

}
