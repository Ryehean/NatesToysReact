using System;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class CartExtensions
{
    public static CartDto ToDto(this Cart cart)
    {
        return new CartDto
        {
            CartId = cart.CartId,
            ClientSecret = cart.ClientSecret,
            PaymentIntentId = cart.PaymentIntentId,
            Items = cart.Items.Select(x => new CartItemDto
            {
                ProductId = x.ProductId,
                Name = x.Product.Name,
                Price = x.Product.Price,
                Brand = x.Product.Brand,
                Type = x.Product.Type,
                PictureUrl = x.Product.PictureUrl,
                Quantity = x.Quantity
            }).ToList()
        };
    }

    public static async Task<Cart> GetCartWithItems(this IQueryable<Cart> query,
        string? cartId)
    {
        return await query
        .Include(x => x.Items)
        .ThenInclude(x => x.Product)
        .FirstOrDefaultAsync(x => x.CartId == cartId)
            ?? throw new Exception("Cannot get cart");
    }
}