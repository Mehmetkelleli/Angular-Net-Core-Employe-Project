using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Backend.Application.IdentityErrorHandling
{
    public class IdentityCustomError : IdentityErrorDescriber
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public IdentityCustomError(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(_localizer["email-dublicate"], email)
            };
        }
        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = _localizer["error"] }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = _localizer["error"] }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = _localizer["password-false"] }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = _localizer["invalid-token"] }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = _localizer["user-already-login"] }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = string.Format(_localizer["user-name-error"]) }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = string.Format(_localizer["invalid-mail"], email) }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = string.Format(_localizer["user-name-already-taken"], userName) }; }
        //public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(_localizer["Email {0} is already taken."] , email) }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format(_localizer["Role name {0} is invalid."], role) }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format(_localizer["Role name {0} is already taken."], role) }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = _localizer["User already has a password set"] }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = _localizer["user-lock"] }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format(_localizer["User already in role {0}."], role) }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = string.Format(_localizer["User is not in role {0}."], role) }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format(_localizer["password-lenght"], length) }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _localizer["password-not-alphanumeric"] }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = _localizer["passwords-0-9."] }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = _localizer["password-a-z"] }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = _localizer["password-A-Z"] }; }
        // DuplicateUserName, InvalidEmail, DuplicateUserName etc
    }
}
