using System;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController(PaymentsService paymentsService, StoreContext context) : BaseApiController
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CartDto>> CreateOrUpdatePaymentIntent()
    {
        var cart = await context.Carts.GetCartWithItems(Request.Cookies["cartId"]);

        if (cart == null) return BadRequest("Problem with the cart");

        var intent = await paymentsService.CreateOrUpdatePaymentIntent(cart);

        if (intent == null) return BadRequest("Problem creating payment intent");

        cart.PaymentIntentId ??= intent.Id;
        cart.ClientSecret ??= intent.ClientSecret;

        if (context.ChangeTracker.HasChanges())
        {
            var result = await context.SaveChangesAsync() > 0;

            if (!result) return BadRequest("Problem updating cart with intent");

        }

        return cart.ToDto();
    }
}


