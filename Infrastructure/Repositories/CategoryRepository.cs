﻿using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Params;
using Infrastructure.Data;
using Infrastructure.Extensions;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Category>> GetAllAsync(PaginationParam paginationParam, string? search = null)
        {
            var query = _context.Categories.AsQueryable().Search(search).OrderBy(c => c.Name);
            var result = await query.ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);

            return result;
        }

        public async Task<PagedList<Category>> GetAllHierarchyAsync(PaginationParam paginationParam)
        {
            var query = _context.Categories.AsQueryable().GetHierarchicalCategories();
            var result = await query.ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);

            return result;
        }
    }
}
