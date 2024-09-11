using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportApi.Data;
using PassportApi.Dtos.Notification;
using PassportApi.interfaces;
using PassportApi.Models;

namespace PassportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IRootRepository<Notification> _rootRepo;
        private readonly IMapper _mapper;

        public NotificationsController(IRootRepository<Notification> rootRepo, IMapper mapper)
        {
            _rootRepo = rootRepo;
            _mapper = mapper;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotifications()
        {
            try
            {
                var notifications = await _rootRepo.GetAllAsync();
                var notificationDtos = _mapper.Map<IEnumerable<NotificationDTO>>(notifications);
                return Ok(notificationDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching notifications.");
            }
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationDTO>> GetNotification(int id)
        {
            try
            {
                var notification = await _rootRepo.GetByIdAsync(id);
                if (notification == null)
                {
                    return NotFound($"Notification with ID {id} not found.");
                }

                var notificationDto = _mapper.Map<NotificationDTO>(notification);
                return Ok(notificationDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the notification.");
            }
        }

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, NotificationDTO notificationDto)
        {
            if (id != notificationDto.NotificationID)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var notification = _mapper.Map<Notification>(notificationDto);
                await _rootRepo.UpdateAsync(id, notification);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Notification with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the notification.");
            }
        }

        // POST: api/Notifications
        [HttpPost]
        public async Task<ActionResult<NotificationDTO>> PostNotification(NotificationDTO notificationDto)
        {
            try
            {
                var notification = _mapper.Map<Notification>(notificationDto);
                var newNotification = await _rootRepo.AddAsync(notification);
                var newNotificationDto = _mapper.Map<NotificationDTO>(newNotification);

                return CreatedAtAction(nameof(GetNotification), new { id = newNotificationDto.NotificationID }, newNotificationDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the notification.");
            }
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                var notification = await _rootRepo.GetByIdAsync(id);
                if (notification == null)
                {
                    return NotFound($"Notification with ID {id} not found.");
                }

                await _rootRepo.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the notification.");
            }
        }
    }
}
