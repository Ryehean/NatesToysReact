import type { Item } from "../../app/models/cart";
import { useClearCartMutation, useFetchCartQuery } from "../../features/cart/cartApi";

export const useCart = () => {
    const { data: cart } = useFetchCartQuery();
    const [clearCart] = useClearCartMutation();
    const subtotal = cart?.items.reduce((sum: number, item: Item) => sum + item.quantity * item.price, 0) ?? 0;
    const deliveryFee = subtotal > 10000 ? 0 : 500;
    const total = subtotal + deliveryFee;

    return { cart, subtotal, deliveryFee, total, clearCart }
}