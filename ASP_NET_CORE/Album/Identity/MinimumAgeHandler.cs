using System;
using System.Threading.Tasks;
using Album.Data;
using Album.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Album.Identity {
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement> {

        // UserManager được Inject qua khởi tạo
        private readonly UserManager<AppUser> _userManager;
        // Có Inject Logger để ghi log
        private readonly ILogger<MinimumAgeHandler> _logger;

        public MinimumAgeHandler (
            ILogger<MinimumAgeHandler> logger,
            AppDbContext appDbContext,
            UserManager<AppUser> userManager) {
            _userManager = userManager;
            _logger = logger;
        }
        protected override Task HandleRequirementAsync (AuthorizationHandlerContext context, MinimumAgeRequirement requirement) {
             

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
                    _logger.LogInformation (now.ToString() + " Không trong khung giờ được truy cập");
                    return Task.CompletedTask;
                }
            // start is after end, so do the inverse comparison
            if (end < now && now < start) {
                _logger.LogInformation (now.ToString() + " Không trong khung giờ được truy cập");
                return Task.CompletedTask;
            }

            // Thiết lập chứng thực thành công
            context.Succeed (requirement);
            return Task.CompletedTask;
        }
    }
}