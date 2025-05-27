using EmployeeHierachy12345.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeHierachy12345.Service
{
    public interface IPaymentService
    {
        Task<MerchantOrder> ProcessMerchantOrder(PaymentRequest payRequest);
        Task<string> CompleteOrderProcess(IHttpContextAccessor httpContextAccessor);
    }

    public class PaymentService : IPaymentService
    {
        private readonly string _razorpayKey = "rzp_test_f10WA3OlIkQgII";
        private readonly string _razorpaySecret = "pCNbV6YvWNDPkSxi7Q2ODLjr";

        public async Task<MerchantOrder> ProcessMerchantOrder(PaymentRequest payRequest)
        {
            try
            {
                // Generate unique transaction ID using GUID
                string transactionId = Guid.NewGuid().ToString();
                var client = new Razorpay.Api.RazorpayClient(_razorpayKey, _razorpaySecret);

                var options = new Dictionary<string, object>
                {
                    { "amount", payRequest.Amount * 100 }, // Amount in paise
                    { "receipt", transactionId },
                    { "currency", "INR" },
                    { "payment_capture", "0" } // 0 for manual capture
                };

                var orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();

                return new MerchantOrder
                {
                    OrderId = orderId,
                    RazorpayKey = _razorpayKey,
                    Amount = payRequest.Amount * 100,
                    Currency = "INR",
                    Name = payRequest.Name,
                    Email = payRequest.Email,
                    PhoneNo = payRequest.PhoneNumber,
                    Address = payRequest.Address,
                    Description = "Order by Merchant"
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new InvalidOperationException("Error processing merchant order", ex);
            }
        }

        public async Task<string> CompleteOrderProcess(IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                var paymentId = httpContextAccessor.HttpContext?.Request.Form["rzp_paymentid"].ToString();
                var orderId = httpContextAccessor.HttpContext?.Request.Form["rzp_orderid"].ToString();

                if (string.IsNullOrEmpty(paymentId) || string.IsNullOrEmpty(orderId))
                {
                    throw new InvalidOperationException("Payment ID or Order ID is missing.");
                }

                var client = new Razorpay.Api.RazorpayClient(_razorpayKey, _razorpaySecret);
                var payment = client.Payment.Fetch(paymentId);

                // Capture the payment
                var options = new Dictionary<string, object>
                {
                    { "amount", payment.Attributes["amount"] }
                };

                var paymentCaptured = payment.Capture(options);
                return paymentCaptured.Attributes["status"].ToString();
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new InvalidOperationException("Error completing the order process", ex);
            }
        }
    }
}
