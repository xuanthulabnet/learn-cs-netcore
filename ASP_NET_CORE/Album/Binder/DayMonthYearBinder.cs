using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Album.Binder {
    public class DayMonthYearBinder : IModelBinder {
        public Task BindModelAsync (ModelBindingContext bindingContext) {
            if (bindingContext == null) {
                throw new ArgumentNullException (nameof (bindingContext));
            }
            // Lấy tên Model (Object, thuộc tính ...)
            string modelName = bindingContext.ModelName;

            // Lấy giá trị truyền đến tương ứng với modelName
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue (modelName);

            // Không có giá trị
            if (valueProviderResult == ValueProviderResult.None) {
                return Task.CompletedTask;
            }

            // Thiết lập và lấy giá trị (đầu tiên)
            bindingContext.ModelState.SetModelValue (modelName, valueProviderResult);
            string valueStrinng = valueProviderResult.FirstValue;

            // Xử lý nếu giá trị truyền đến là null
            if (string.IsNullOrEmpty (valueStrinng)) {
                return Task.CompletedTask;
            }

            // Chuyển chuỗi giá trị thành DateTime
            DateTime? date = null;
            try {
                date = DateTime.ParseExact (valueStrinng, "dd/MM/yyyy", null);
            } catch {

                bindingContext.ModelState.TryAddModelError (modelName, "Nhập ngày tháng bị sai - yêu cầu định dạng dd/MM/yyyy (ví dụ 20/11/2020)");
                return Task.CompletedTask;
            }
            if (date < DateTime.Parse ("1/1/1945")) {
                bindingContext.ModelState.TryAddModelError (modelName, "Abcs");
                return Task.CompletedTask;

            }
            // Gán giá trị thành công
            bindingContext.Result = ModelBindingResult.Success (date);
            return Task.CompletedTask;

        }
    }
}