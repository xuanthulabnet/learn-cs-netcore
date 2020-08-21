using System;
using Microsoft.Extensions.DependencyInjection;

namespace di {

    // Tạo một lớp cơ sở, cung cấp phương thức tin thông tin của lớp
    abstract class BaseInformation {
        // In kiểu và mã Hash (mã duy nhất) của đối tượng
        public void ShowInfo () => Console.WriteLine ($"{this.GetType().Name}-{this.GetHashCode()}");
        // In thông báo - khi đối tượng được tạo
        public void NotifyCreate () => Console.WriteLine ($"{this.GetType().Name} created");
    }

    class A : BaseInformation {
        public A () => NotifyCreate ();
    }

    class B : BaseInformation {
        A dependency;
        // Inject bằng hàm tạo
        public B (A dependency) {
            this.dependency = dependency;
            NotifyCreate ();
        }
    }

    class C : BaseInformation {
        public C () => NotifyCreate ();
    }
    class Program {
        static void Main (string[] args) {
            // Tạo và cấu hình ServiceCollection
            var services = new ServiceCollection ();
            services.AddSingleton<A> (); // Đăng ký dịch vụ A,  kiểu singleton (một bản đơn nhất)
            services.AddTransient<B> (); // Đăng ký dịch vụ B, kiểu transient (tạo mới mỗi khi yêu cầu)
            services.AddScoped<C> ();    // Đăng ký dịch vụ C, kiểu scoped (tạo mới trong một phạm vi)

            var serviceprovider = services.BuildServiceProvider (); // Tạo serviceprovider

            // SỬ DỤNG

            
            // Yêu cầu dịch vụ B, DI tự động tạo A và Inject vào B khi B khởi tạo
            Console.WriteLine("Yêu cầu lấy dịch B lần đầu, lưu vào b1:");
            B b1 = serviceprovider.GetService<B> ();
            // Output:
            //    A created
            //    B created
            
            // Yêu cầu lại dịch vụ B: B đăng ký là transient (tạm), nên đối tượng thực sự tạo lại
            // Dịch vụ A do là singleton, nên nó không tạo lại (có sẵn trong ServiceCollection đã tạo lần trước)
            // nó sẽ Inject vào dịch vụ B mới
            Console.WriteLine("Yêu cầu lấy dịch B lần hai, lưu vào b2:");
            B b2 = serviceprovider.GetService<B> ();
            // Output:
            //    B created
            
            // b1 và b2 là hai đối tượng khác nhau
            Console.WriteLine("Thông tin về b1 và b2:");
            b1.ShowInfo();
            b2.ShowInfo();

            
            // Yêu  cầu  A, A (singleton) đã tạo nên nó trả về đối dịch vụ 
            // đã từng tạo khi tạo B (Inject vào B)
            Console.WriteLine("Yêu cầu lấy dịch A, lưu vào a:");
            A a = serviceprovider.GetService<A> ();


            // Yêu cầu dịch vụ C, C là loại scoped (1 phiên bản trong một phạm vi, scoped)
            Console.WriteLine("Yêu cầu lấy dịch C, lưu vào c1:");
            C c1 = serviceprovider.GetService<C> ();
            // Out: C created

            // Yêu cầu C, C không tạo mới vì scoped không đổi, nó chính là c1
            Console.WriteLine("Yêu cầu lấy dịch C lần 2, lưu vào c2:");
            C c2 = serviceprovider.GetService<C> ();

            Console.WriteLine("Thông tin về c1 và c2:");
            c1.ShowInfo();
            c2.ShowInfo();

            // Tạo scope mới
            Console.WriteLine ("-----------New scope---------");
            using (var scope = serviceprovider.CreateScope ()) {
                // Yêu  cầu  A, A đã tạo nên nó trả về đối dịch vụ mà đã Inject vào B, cho dù là scope mới
                a = scope.ServiceProvider.GetService<A> ();
                // Yêu cầu C, C tạo mới vì scope mới (C kiểu scoped)
                C c = scope.ServiceProvider.GetService<C> ();
            }
        }

    }
}