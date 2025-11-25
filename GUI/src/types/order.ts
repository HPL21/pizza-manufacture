export interface Order {
    id: number
    placed_at: string
    completed_at: string
    total_price: number
    total_weight: number
    total_calories: number
    recipient_address: string
    recipient_phone: string
    recipient_email: string
    payment_method: string
}