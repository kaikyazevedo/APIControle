using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanca.Api.Domain.Service.Interfaces
{

    /// <summary>
    /// Interface generica para criação de srviços do tipo CRUD
    /// </summary>
    /// <typeparam name="RQ">Contrato de Request</typeparam>
    /// <typeparam name="RS">Contrato de Response</typeparam>
    /// <typeparam name="I">Tipo do Id</typeparam>
    public interface IService<RQ, RS, I> where RQ : class
    {
        Task<IEnumerable<RS>> Obter(I idUsuario);
        Task<RS> Obter(I id, I idUsuario);
        Task<RS> Adicionar(RQ entidade, I idUsuario);
        Task<RS> Atualizar(I id, RQ entidade, I idUsuario);
        Task Inativar(I id, I idUsuario);
    }
}