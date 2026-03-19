using Domain.Execptions;

public class Paciente
{
    public int IdPaciente { get; private set; }
    public string Nombre { get; private set; }
    public string Apellido { get; private set; }
    public DateTime FechaNacimiento { get; private set; }
    public string NumTelefono { get; private set; }
    public string Correo { get; private set; }
    public bool Estado { get; private set; }

    public Paciente(
        int idPaciente,
        string nombre,
        string apellido,
        DateTime fechaNacimiento,
        string numTelefono,
        string correo,
        bool estado)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del paciente es obligatorio");

        if (string.IsNullOrWhiteSpace(apellido))
            throw new DomainException("El apellido del paciente es obligatorio");

        if (string.IsNullOrWhiteSpace(numTelefono))
            throw new DomainException("El teléfono del paciente es obligatorio");

        if (string.IsNullOrWhiteSpace(correo))
            throw new DomainException("El correo del paciente es obligatorio");

        IdPaciente = idPaciente;
        Nombre = nombre.Trim();
        Apellido = apellido.Trim();
        FechaNacimiento = fechaNacimiento;
        NumTelefono = numTelefono.Trim();
        Correo = correo.Trim();
        Estado = estado;
    }
}