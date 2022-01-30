using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Accounting.API.Entities;
using Accounting.API.Repositories.Interfaces;
using AutoMapper;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;

        public AccountingController(ICartRepository cartRepository, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet("{userName}", Name = "GetCart")]
        [ProducesResponseType(typeof(Cart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> GetCart(string userName)
        {
            var cart = await _cartRepository.GetCartByUserName(userName);

            return Ok(cart ?? new Cart(userName));
        }

        [HttpPost(Name = "UpdateCart")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> UpdateCart([FromBody] Cart cart)
        {
            return Ok(await _cartRepository.UpdateCart(cart));
        }

        [HttpDelete("{userName}", Name = "DeleteCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string userName)
        {
            await _cartRepository.DeleteCart(userName);
            return Ok();
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] AccountingCheckout checkout)
        {
            var cart = await _cartRepository.GetCartByUserName(checkout.UserName);

            if (cart == null)
            {
                return null;
            }

            await _cartRepository.DeleteCart(checkout.UserName);

            var eventMessage = _mapper.Map<CartCheckoutEvent>(checkout);
            eventMessage.RequestId = new Guid();
            eventMessage.TotalPrice = checkout.TotalPrice;

            try
            {
                _eventBus.PublishCartCheckout(EventBusConstants.CartCheckoutQueue, eventMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Accepted();
        }
    }
}
