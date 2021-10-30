﻿using Abasto.Negocio.Core.Entities;
using Abasto.Negocio.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abasto.Negocio.Infrastructure.Repositories
{
    public class IngPublicacionRepository:IIngPublicacionRepository
    {
        public async Task<IEnumerable<IngPublicacion>> ToListAsync()
        {
            var lista = Enumerable.Range(1, 10).Select(x => new IngPublicacion
            {
                PubId = x,
                PubDescripcion = $"Descripcion {x}",
                PubFecha = DateTime.Now,
                PubImage = "Esto es una Imagen",
                UsuId = x * 2
            });
            await Task.Delay(19);
            return lista;
        }
    }
}