using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CustomerManagement.API.Attributes;
using CustomerManagement.API.Dtos;
using CustomerManagement.API.Dtos.Interfaces;
using CustomerManagement.API.Services.Interfaces;
using CustomerManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public abstract class CustomControllerBase<TEntity, TDto, TCreationDto, TFilterDto>: ControllerBase
        where TEntity:class, IEntityBase
        where TDto: class, IDto
        where TCreationDto: class, IUpsertDto
        where TFilterDto: class, IFilterDto

    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IServiceBase<TEntity> _service;

        public CustomControllerBase(
            ILogger logger,
            IMapper mapper, 
            IServiceBase<TEntity> service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }
        
       
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Get(PaginationDto paginationDto)
        {
            var _id = HttpContext.Request.Query["id"].ToString();
            if (_id != "")
            {
                return Get(long.Parse(_id));
            }
            var _start = HttpContext.Request.Query["_start"].ToString();
            var _end = HttpContext.Request.Query["_end"].ToString();
            var _sort = HttpContext.Request.Query["_sort"].ToString();
            if (_start != "" && _end != "" && _sort != "") 
            {
                paginationDto = new PaginationDto();
                
                paginationDto.RecordsPerPage = int.Parse(_end) - int.Parse(_start);
                paginationDto.Page = int.Parse(_end) / paginationDto.RecordsPerPage;

            }
           
            Response.Headers.Add("Access-Control-Expose-Headers","X-Total-Count");
           
            
            
            if (paginationDto == null)
            {
                var list = _service.Read();

                var dtoList = _mapper.Map<List<TDto>>(list);


                Response.Headers.Add("total",list.Count().ToString());
                
                
                return Ok(dtoList);
            }
            else
            {
                
                var list = _service.Read(paginationDto.Page, paginationDto.RecordsPerPage);

                var dtoList = _mapper.Map<List<TDto>>(list);
                
                Response.Headers.Add("total",_service.Count().ToString());
                
                Response.Headers.Add("X-Total-Count", _service.Count().ToString());

                return Ok(dtoList);
            }
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult Get(long id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Read(id);
            
            if (item == null)
            {
                _logger.LogWarning($"There is not record according to this id. Id is {id}");
                return NotFound();
            }
           
            var dtoItem = _mapper.Map<TDto>(item);
            
            return Ok(dtoItem);
        }
        

        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Post(TCreationDto creationDto)
        {

            TEntity item = _service.Create(_mapper.Map<TEntity>(creationDto));
            
            var itemDto = _mapper.Map<TDto>(item);
            
            return new CustomCreatedAtRouteResult(item.Id, itemDto);
        }
        
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public  ActionResult<TDto> Put(long id,  TCreationDto updateDTO)
        {

            var item = _mapper.Map<TEntity>(updateDTO);
            item.Id = id;

            _service.Update(item);

            return _mapper.Map<TDto>(item);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<TDto> Delete(long id)
        {

          var item =  _service.Delete(id);
          return _mapper.Map<TDto>(item);
        }
        
        
    }
}