﻿namespace GestionDocente.Application.Dtos.Request
{
    public class CreateInstitucionDto
    {     
        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }
    }
}
