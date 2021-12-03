using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System.Text.RegularExpressions;

namespace DevFreela.Application.Validators
{
    public class CreateCommandUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateCommandUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("E-mail não válido!");

            RuleFor(u => u.Password)
                .Must(ValidPassword)
                .WithMessage("Senha deve conter pelo menos 8 caracteres, um número uma letra minúscula, uma maiúscula e um caracter especial");

            RuleFor(u => u.FullName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome é obrigatório!");
                
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}


/*
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }

 */