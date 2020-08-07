using System;
using System.Collections.Generic;
using AutoMapper;
using CustomerManagement.API.Attributes;
using CustomerManagement.API.Dtos;
using CustomerManagement.API.Dtos.Company;
using CustomerManagement.API.Dtos.Customer;
using CustomerManagement.API.Exceptions;
using CustomerManagement.API.Services;
using CustomerManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class CustomerController: CustomControllerBase<Customer, CustomerDto, CustomerUpsertDto, CustomerFilterDto>
    {
        private readonly CustomerService _customerService;
        private readonly TitleService _titleService;
        private readonly CompanyService _companyService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger,
            IMapper mapper,
            CustomerService customerService,
            TitleService titleService,
            CompanyService companyService)
            : base(logger, mapper, customerService)
        {
            _customerService = customerService;
            _companyService = companyService;
            _titleService = titleService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Getting all items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpGet("list")]
        public new ActionResult Get([FromQuery] PaginationDto paginationDto)
        {
            return base.Get(paginationDto);
        }

        /// <summary>
        /// Get Item Type By Id
        /// </summary>
        /// <param name="id">Id of the item to get</param>
        /// <returns></returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<CustomerDto> Get(long id)
        {

            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(CustomerUpsertDto upsertDto)
        {
            if (!_companyService.Exists(upsertDto.CompanyId) || !_titleService.Exists(upsertDto.TitleId))
                throw new NotFoundException("Company id or title id is not found.");

            return base.Post(upsertDto);
        }


        /// <summary>
        /// Updating a Item
        /// </summary>
        /// <param name="id">Id of the order type to update</param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:long}")]
        public new ActionResult<CustomerDto> Put(long id, CustomerUpsertDto updateDTO)
        {
            return base.Put(id, updateDTO);
        }
        


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        public new ActionResult<CustomerDto> Delete(long id)
        {
            return base.Delete(id);
        }
    }
}