using System;

namespace API.DTOs;

public class CartDto
{
    public required string CartId { get; set; }
    public List<CartItemDto> Items { get; set; } = [];
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
}
