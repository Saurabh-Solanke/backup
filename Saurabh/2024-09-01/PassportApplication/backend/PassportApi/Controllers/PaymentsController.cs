using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.Dtos.Payment;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IRootRepository<Payment> _rootRepo;
        private readonly IMapper _mapper;

        public PaymentsController(IRootRepository<Payment> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            try
            {
                var payments = await _rootRepo.GetAllAsync();
                var paymentDtos = _mapper.Map<IEnumerable<PaymentDTO>>(payments);
                return Ok(paymentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching payments.");
            }
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(int id)
        {
            try
            {
                var payment = await _rootRepo.GetByIdAsync(id);
                if (payment == null)
                {
                    return NotFound($"Payment with ID {id} not found.");
                }

                var paymentDto = _mapper.Map<PaymentDTO>(payment);
                return Ok(paymentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the payment.");
            }
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDTO paymentDto)
        {
            if (id != paymentDto.PaymentId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var payment = _mapper.Map<Payment>(paymentDto);
                await _rootRepo.UpdateAsync(id, payment);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the payment.");
            }
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> PostPayment(PaymentDTO paymentDto)
        {
            try
            {
                var payment = _mapper.Map<Payment>(paymentDto);
                var newPayment = await _rootRepo.AddAsync(payment);
                var newPaymentDto = _mapper.Map<PaymentDTO>(newPayment);

                return CreatedAtAction(nameof(GetPayment), new { id = newPaymentDto.PaymentId }, newPaymentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the payment.");
            }
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var payment = await _rootRepo.GetByIdAsync(id);
                if (payment == null)
                {
                    return NotFound($"Payment with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the payment.");
            }
        }
    }
}
