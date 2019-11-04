using FluentValidation;

namespace AspNetCoreApiStarter.ViewModels.Validators
{
    /// <summary>
    /// Defines validator for the <see cref="UserVm"/> Class.
    /// </summary>
    public class UserValidator : AbstractValidator<UserVm>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserValidator"/> class.
        /// </summary>
        public UserValidator()
        {
            this.RuleFor(vm => vm.UserName).NotEmpty().WithMessage("User Name cannot be empty");
            this.RuleFor(vm => vm.UserName).Length(0, 255);
            this.RuleFor(vm => vm.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid Email address");
            this.RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty");
            this.RuleFor(vm => vm.FirstName).NotEmpty().WithMessage("FirstName cannot be empty");
            this.RuleFor(vm => vm.LastName).NotEmpty().WithMessage("LastName cannot be empty");
        }
    }
}