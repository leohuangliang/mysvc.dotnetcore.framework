using MySvc.DotNetCore.Framework.Domain.Core.Impl;

namespace MySvc.DotNetCore.Framework.IS4.Domain.Common
{
    public class VerificationCodeInfo : ValueObject<VerificationCodeInfo>
    {
        public VerificationCodeInfo(
            string userId, 
            VerificationCodeType verificationCodeType,
            string verificationCode)
        {
            this.UserId = userId;
            this.VerificationCodeType = verificationCodeType;
            this.VerificationCode = verificationCode;
        }

        public string UserId { get; private set; }
        public VerificationCodeType VerificationCodeType { get; private set; }
        public string VerificationCode { get; private set; }
    }

    public enum VerificationCodeType
    {
        Email,
        PhoneNumber,
    }
}
