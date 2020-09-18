using System;
using System.Threading.Tasks;
using Album.Data;
using Album.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Album.Identity {
    public class CanUpdatePostAgeHandler : AuthorizationHandler<CanUpdatePostRequirement, Post> {

        private readonly ILogger<MinimumAgeHandler> _logger;
        private readonly UserManager<AppUser> _userManager;

        public CanUpdatePostAgeHandler (ILogger<MinimumAgeHandler> logger,
            UserManager<AppUser> userManager) {
            _logger = logger;
            _userManager = userManager;
        }
        protected Task HandleRequirementAsync (AuthorizationHandlerContext context, MinimumAgeRequirement requirement) {

            var user = _userManager.GetUserAsync (context.User).Result;
            if (user == null)
                return Task.CompletedTask;

            var dateOfBirth = user.Birthday;
            if (dateOfBirth == null) {
                _logger.LogInformation ("Không có ngày sinh");
                // Trả về mà chưa chứng thực thành công
                return Task.CompletedTask;
            }

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Value.Year;
            if (dateOfBirth > DateTime.Today.AddYears (-calculatedAge)) {
                calculatedAge--;
            }

            if (calculatedAge < requirement.MinimumAge) {
                _logger.LogInformation (calculatedAge + ": Không đủ tuổi truy cập");
                return Task.CompletedTask;
            }

            // https://stackoverflow.com/a/12998855/4776710
            TimeSpan start = new TimeSpan (requirement.OpenTime, 0, 0);
            TimeSpan end = new TimeSpan (requirement.CloseTime, 0, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;
            // see if start comes before end
            if (start < end)
                if (!(start <= now && now <= end)) {
                    _logger.LogInformation (now.ToString () + " Không trong khung giờ được truy cập");
                    return Task.CompletedTask;
                }
            // start is after end, so do the inverse comparison
            if (end < now && now < start) {
                _logger.LogInformation (now.ToString () + " Không trong khung giờ được truy cập");
                return Task.CompletedTask;
            }

            // Thiết lập chứng thực thành công
            context.Succeed (requirement);
            return Task.CompletedTask;
        }

        protected override Task HandleRequirementAsync (AuthorizationHandlerContext context, CanUpdatePostRequirement requirement, Post resource) {
            // Nếu cho Admin cập nhật thì kiểm tra User có RoleClaim Admin
            if (requirement.AdminCanUpdate) {
                if (context.User.IsInRole ("Admin")) {
                    // Cho phép
                    _logger.LogInformation ("Admin được cập nhật");
                    context.Succeed (requirement);
                    return Task.CompletedTask;
                }
            }

            if (context.User.Identity?.IsAuthenticated != true) {
                _logger.LogInformation ("User không đăng nhập");
                return Task.CompletedTask;
            }

            if (requirement.OwnerCanUpdate) {
                var user = _userManager.GetUserAsync (context.User).Result;
                if (user.Id == resource.UserID) {
                    _logger.LogInformation ("Được phép cập nhật");
                    context.Succeed (requirement);
                    return Task.CompletedTask;
                }
            }

            _logger.LogInformation ("Không được phép cập nhật");
            return Task.CompletedTask;
        }
    }
}