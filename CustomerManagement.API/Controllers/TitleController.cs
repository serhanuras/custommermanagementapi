using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CustomerManagement.API.Attributes;
using CustomerManagement.API.Dtos;
using CustomerManagement.API.Dtos.Title;
using CustomerManagement.API.Exceptions;
using CustomerManagement.API.Services;
using CustomerManagement.API.Services.Interfaces;
using CustomerManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class TitleController: CustomControllerBase<Title, TitleDto, TitleUpsertDto, TitleFilterDto>
    {
        private readonly TitleService _titleService;
        private readonly CustomerService _customerService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TitleController(ILogger<TitleController> logger,
            IMapper mapper,
            TitleService titleService,
            CustomerService customerService)
            : base(logger, mapper, titleService)
        {
            _titleService = titleService;
            _customerService = customerService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        public new ActionResult Get(
            [FromQuery] PaginationDto paginationDto
        )
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(TitleDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<TitleDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(TitleUpsertDto upsertDto)
        {
            return base.Post(upsertDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        public new ActionResult<TitleDto> Put(long id, TitleUpsertDto updateDTO)
        {
            return base.Put(id, updateDTO);
        }
        


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        public new ActionResult<TitleDto> Delete(long id)
        {
            if (this._customerService.Read().FirstOrDefault(x => x.TitleId == id) != null)
            {
                throw new ConflictException("There are records in Customers that are mapped to this record so please unbind them first.");
            }
            
            return base.Delete(id);
        }


    }
}