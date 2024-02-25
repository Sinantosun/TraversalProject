using Microsoft.AspNetCore.Identity;

namespace TraversalProject.Models
{
    public class CustomIdentiyValidator : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = "PasswordTooShort", Description = $"Parola Minumum {length} karakter olmalıdır." };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "DuplicateEmail", Description = $" {email} Bu mail adresi zaten kullanılıyor." };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DuplicateUserName", Description = $" {userName} Bu kullanıcı zaten kullanılıyor." };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = "PasswordRequiresDigit", Description = "Şifrede sayı bulunmalıdır." };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = "PasswordRequiresLower", Description = "Şifrede en az 1 tane kücük harf bulunmalıdır." };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = "PasswordRequiresUpper", Description = "Şifrede en az 1 tane büyük harf bulunmalıdır." };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError { Code = "PasswordRequiresNonAlphanumeric", Description = "Şifrede özel karakter bulunmalıdır." };
        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError { Code = "InvalidUserName", Description = $"{userName} geçersiz kullanıcı adı lütfen tekrar deneyiniz." };
        }
    }
}
