using GestionDocente.Infrastructure.Persistences.Context;
using GestionDocente.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using AutoMapper;
using GestionDocente.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionDocente.Infrastructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private bool disposedValue;
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly RoleManager<ApplicationRoleModel> _roleManager;

        public IApplicationUserRepository ApplicationUsers { get; }

        public IApplicationRoleRepository ApplicationRoles { get; }

        public IInstitucionRepository Instituciones { get; }

        public UnitOfWork(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUserModel> userManager, RoleManager<ApplicationRoleModel> roleManager)

        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;

            ApplicationUsers = new ApplicationUserRepository(_userManager, _mapper);
            ApplicationRoles = new ApplicationRoleRepository(_roleManager, _mapper);
            Instituciones = new InstitucionesRepository(_context, _mapper);
        }

        public void BeginTransaction()
        {
           _transaction = _context.Database.BeginTransaction();
        }
        
        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction?.CommitAsync();
                
                return true;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Eliminar el estado administrado (objetos administrados)
                    _transaction?.Dispose();
                    _context.Dispose();
                }

                // Liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // Establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // Reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~UnitOfWork()

#pragma warning disable S125 // Sections of code should not be commented out
                            // {
                            //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
                            //     Dispose(disposing: false);
                            // }

        public void Dispose()
#pragma warning restore S125 // Sections of code should not be commented out
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
    }
}
