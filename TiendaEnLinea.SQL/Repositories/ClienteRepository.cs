﻿using Bitworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaEnLinea.Core.Model;
using TiendaEnLinea.Core.Repositories;

namespace TiendaEnLinea.SQL.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private TiendaEnLineaContext _db;
        public ClienteRepository(BaseContext context) : base(context)
        {
            _db = context as TiendaEnLineaContext;
        }

        public Cliente GuardarCliente(Cliente cliente)
        {

            _db.Cliente.Add(cliente);
            _db.SaveChanges();

            return cliente;
        }

        public void ModificarCliente(Cliente cliente)
        {
            Cliente cl = _db.Cliente.FirstOrDefault(x => x.Codigo == cliente.Codigo);

            if(cl != null)
            {
                cl.NombreCompleto = cliente.NombreCompleto;
                cl.Telefono = cliente.Telefono;
                cl.DUI = cliente.DUI;
                cl.Direccion = cliente.Direccion;

                _db.SaveChanges();

            }
            else
            {
                GuardarCliente(cliente);
            }
            
        }
    }
}
