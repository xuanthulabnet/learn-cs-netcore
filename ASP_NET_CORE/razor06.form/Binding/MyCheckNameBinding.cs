using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace razor06.form.Binding {

    // Liên kết dữ liệu (binding) - chuỗi dữ liệu gửi đến
    // phải không có chữ xxx - dữ liệu được filter thành chữ IN HOA
    public class MyCheckNameBinding : IModelBinder {
        private readonly ILogger<MyCheckNameBinding> _logger;

        public MyCheckNameBinding (ILogger<MyCheckNameBinding> logger) {
            _logger = logger;
        }
        public Task BindModelAsync (ModelBindingContext bindingContext) {

            if (bindingContext == null) {
                throw new ArgumentNullException (nameof (bindingContext));
            }

            // Lấy ModelName - tên thuộc tính binding
            string modelName = bindingContext.ModelName;

            // Lấy giá trị gửi đến
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue (modelName);

            // Không có giá trị gửi đến (không thiết lập giá trị cho thuộc  tính)
            if (valueProviderResult == ValueProviderResult.None) {
                return Task.CompletedTask;
            }

            // Thiết lập cho ModelState giá trị bindinng
            bindingContext.ModelState.SetModelValue (modelName, valueProviderResult);

            // Đọc giá trị đầu tiên gửi đêns
            string value = valueProviderResult.FirstValue;

            // Xử lý nếu chuỗi giá trị gửi đến null
            if (string.IsNullOrEmpty (value)) {
                return Task.CompletedTask;
            }

            var s = value.ToUpper();

            if (s.Contains ("XXX")) {
                // chứa ký tự không được phép, thiết lập báo lỗi (không binding) 
                bindingContext.ModelState.TryAddModelError (
                    modelName, "Không được phép chứa xxx.");
                return Task.CompletedTask;

            }

            // Gán giá trị vào thuộc tính (có loại bỏ khoảng trắng)
            bindingContext.Result = ModelBindingResult.Success(s.Trim());

            return Task.CompletedTask;

        }
    }
}