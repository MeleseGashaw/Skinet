using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
    public class BasketController: BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
            {
            _basketRepository=basketRepository;
            _mapper=mapper;
            }

            [HttpGet]
            public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
            {
                var basket =await _basketRepository.GetBasketAsync(id);
                return Ok(basket?? new CustomerBasket(id));
            }
                [HttpPost]
            public async Task<ActionResult<CustomerBasket>> UpdateBasketById(Dtos.CustomerBasketDto basket)
            {
            var customerBusket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
                var updatedBasket =await _basketRepository.UpdateBasketAsync(customerBusket);
               return Ok (updatedBasket);
            }
              [HttpDelete]
            public async Task  DeleteBasketAsync(string id)
            {
                 await _basketRepository.DeleteBasketAsync(id);
               
            }
    }
}