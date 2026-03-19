using Application.DTOs.Pacientes;
using FluentValidation;

namespace Application.Validators.Pacientes;

public class CrearPacienteValidator : AbstractValidator<CrearPacienteRequestDto>
{
    public CrearPacienteValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(100);
    }
}