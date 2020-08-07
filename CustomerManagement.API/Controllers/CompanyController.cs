using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CustomerManagement.API.Attributes;
using CustomerManagement.API.Dtos;
using CustomerManagement.API.Dtos.Company;
using CustomerManagement.API.Dtos.Title;
using CustomerManagement.API.Exceptions;
using CustomerManagement.API.Services;
using CustomerManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.API.Controllers
{
    [ApiController]
    [LowerCaseRoute()]
    public class CompanyController: CustomControllerBase<Company, CompanyDto, CompanyUpsertDto, CompanyFilterDto>
    {
        private readonly CompanyService _companyService;
        private readonly CustomerService _customerService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CompanyController(ILogger<CompanyController> logger,
            IMapper mapper,
            CompanyService companyService,
            CustomerService customerService)
            : base(logger, mapper, companyService)
        {
            _companyService = companyService;
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
        [ProducesResponseType(typeof(CompanyDto), 200)]
        [HttpGet("{id}")]
        public new ActionResult<CompanyDto> Get(long id)
        {
            return base.Get(id);
        }


        /// <summary>
        /// Adding Item Type
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public new ActionResult Post(CompanyUpsertDto upsertDto)
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
        public new ActionResult<CompanyDto> Put(long id, CompanyUpsertDto updateDTO)
        {
            return base.Put(id, updateDTO);
        }
        


        /// <summary>
        /// Delete a item
        /// </summary>
        /// <param name="id">Id of the item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:long}")]
        public new ActionResult<CompanyDto> Delete(long id)
        {
            if (this._customerService.Read().FirstOrDefault(x => x.CompanyId == id) != null)
            {
                throw new ConflictException("There are records in Customers that are mapped to this record so please unbind them first.");
            }
            
            return base.Delete(id);
        }

        
    }
}