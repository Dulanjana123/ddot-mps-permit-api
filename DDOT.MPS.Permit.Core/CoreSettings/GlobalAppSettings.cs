namespace DDOT.MPS.Permit.Core.CoreSettings
{
    public class GlobalAppSettings
    {
        public int OtpExpirationTime { get; set; }
        public int MaxOtpIncorrectCount { get; set; }
        public string JwtSecretKey { get; set; }
        public int ResetPasswordTokenExpiryHours { get; set; }
        public int LoginTokenExpiryHours { get; set; }
        public string MarApiKey { get; set; }
        public string LocationDetailsApiUrl { get; set; }
        public string SSLQueryApiUrl { get; set; }
    }
}
