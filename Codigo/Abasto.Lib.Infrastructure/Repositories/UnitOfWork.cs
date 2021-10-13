﻿using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.Interfaces;
using Abasto.Lib.Infrastructure.Data;
using System.Threading.Tasks;

namespace Abasto.Lib.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NegocioContext _context;
        private readonly IContratoRepository _contratoRepository;
        //private readonly IRepository<User> _userRepository;
        //private readonly IRepository<Comment> _commentRepository;
        private readonly ISecurityRepository _securityRepository;

        public UnitOfWork(NegocioContext context)
        {
            _context = context;
        }

        public IContratoRepository PostRepository => _contratoRepository ?? new ContratoRepository(_context);

        //public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        //public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
