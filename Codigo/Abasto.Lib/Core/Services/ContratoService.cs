using Microsoft.Extensions.Options;
using Abasto.Lib.Core.CustomEntities;
using Abasto.Lib.Core.Entities;
using Abasto.Lib.Core.Exceptions;
using Abasto.Lib.Core.Interfaces;
using Abasto.Lib.Core.QueryFilters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abasto.Lib.Core.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public ContratoService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Contrato> GetContrato(string id)
        {
            return await _unitOfWork.ContratoRepository.GetById(id);
        }

        public PagedList<Contrato> GetContratos(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var posts = _unitOfWork.ContratoRepository.GetAll();
            var pagedPosts = PagedList<Contrato>.Create(posts, filters.PageNumber, filters.PageSize);
            return pagedPosts;
        }

        public async Task InsertPost(Contrato post)
        {            
            var userPost = await _unitOfWork.ContratoRepository.GetById(post.CodigoContrato);
            //if (userPost.Count() < 10)
            //{
            //    var lastPost = userPost.FirstOrDefault();
            //    if ((DateTime.Now - lastPost.Date).TotalDays < 7)
            //    {
            //        throw new BusinessException("You are not able to publish the post");
            //    }
            //}          

            await _unitOfWork.ContratoRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Contrato obj)
        {
            var existingPost = await _unitOfWork.ContratoRepository.GetById(obj.CodigoContrato);

            _unitOfWork.ContratoRepository.Update(existingPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.ContratoRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
