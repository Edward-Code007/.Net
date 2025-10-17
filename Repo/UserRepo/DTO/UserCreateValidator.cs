using FluentValidation;
using tests.Data;
using tests.Models;
namespace tests.Repo.UserRepo.DTO

{
    public class UserCreateValidator: AbstractValidator<UserCreateDTO>
    {
        private readonly AppDbContext _context;
        public UserCreateValidator(AppDbContext context) {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El Nombre no puede estar Vacio")
                .Length(4,20)
                .WithMessage("La Longitud del Nombre debe estar entre 4 y 20 caracteres");
            RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Email No Valido")
                    .Must(ValdiateAveilableEmail)
                    .WithMessage("Email no Disponible");
                    ;
            RuleFor(x => x.Password)
                    .NotEmpty();

        }

        public bool ValdiateAveilableEmail(string email)
        {
            bool user = _context.User.Any(x => x.Email == email);
            if (user is false)
            {
                return true;
            }
            return false;
        }
    }
}
