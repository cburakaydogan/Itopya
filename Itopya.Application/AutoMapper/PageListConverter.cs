using AutoMapper;
using Itopya.Domain.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itopya.Application.AutoMapper
{
    public class PageListConverter<TSource, TDest> : ITypeConverter<PagedList<TSource>, PagedList<TDest>>
    {
        public PagedList<TDest> Convert(PagedList<TSource> source, PagedList<TDest> destination, ResolutionContext context)
        {

            var items = context.Mapper.Map<List<TDest>>(source);


            destination = new PagedList<TDest>(items, source.MetaData.TotalCount, source.MetaData.CurrentPage, source.MetaData.PageSize);

            return destination;
        }
    }
}