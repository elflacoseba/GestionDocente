﻿namespace GestionDocente.Application.Dtos.Response
{
    public class ApplicationRoleResponseDto
    {
        /// <summary>
        /// Gets or sets the primary key for this role.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name for this role.
        /// </summary>
        public string? Name { get; set; }       

        public string? Description { get; set; }
    }
}
