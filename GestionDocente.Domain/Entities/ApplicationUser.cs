﻿namespace GestionDocente.Domain.Entities
{
    public class ApplicationUser
    {
        /// <summary>
        /// Obtiene o establece la clave principal para este usuario.
        /// </summary>
        public string Id { get; set; } 

        /// <summary>
        /// Obtiene o establece el nombre de usuario para este usuario.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de usuario normalizado para este usuario.
        /// </summary>
        public string? NormalizedUserName { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección de correo electrónico para este usuario.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección de correo electrónico normalizada para este usuario.
        /// </summary>
        public string? NormalizedEmail { get; set; }

        /// <summary>
        /// Obtiene o establece una bandera que indica si un usuario ha confirmado su dirección de correo electrónico.
        /// </summary>
        /// <value>Verdadero si la dirección de correo electrónico ha sido confirmada, de lo contrario, falso.</value>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Obtiene o establece una representación salada y hasheada de la contraseña para este usuario.
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// Un valor aleatorio que debe cambiar cada vez que las credenciales de un usuario cambian (contraseña cambiada, inicio de sesión eliminado).
        /// </summary>
        public string? SecurityStamp { get; set; }

        /// <summary>
        /// Un valor aleatorio que debe cambiar cada vez que un usuario se persiste en el almacenamiento.
        /// </summary>
        public string? ConcurrencyStamp { get; set; } 

        /// <summary>
        /// Obtiene o establece un número de teléfono para el usuario.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Obtiene o establece una bandera que indica si un usuario ha confirmado su número de teléfono.
        /// </summary>
        /// <value>Verdadero si el número de teléfono ha sido confirmado, de lo contrario, falso.</value>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Obtiene o establece una bandera que indica si la autenticación de dos factores está habilitada para este usuario.
        /// </summary>
        /// <value>Verdadero si 2fa está habilitado, de lo contrario, falso.</value>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha y hora, en UTC, en que finaliza el bloqueo de cualquier usuario.
        /// </summary>
        /// <remarks>
        /// Un valor en el pasado significa que el usuario no está bloqueado.
        /// </remarks>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Obtiene o establece una bandera que indica si el usuario podría ser bloqueado.
        /// </summary>
        /// <value>Verdadero si el usuario podría ser bloqueado, de lo contrario, falso.</value>
        public bool LockoutEnabled { get; set; }

        public bool IsActive
        {
            get
            {
                if (LockoutEnd == null || LockoutEnd.Value.UtcDateTime <= DateTime.UtcNow)
                {
                    return true;
                }

                return false;
            }
        }
            
            
        /// <summary>
        /// Obtiene o establece el número de intentos fallidos de inicio de sesión para el usuario actual.
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Devuelve o establece el primer nombre del usuario.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Devuelve o establece el apellido del usuario.
        /// </summary>
        public string? LastName { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            SecurityStamp = Guid.NewGuid().ToString();
        }

    }
}
