using System.Collections.Generic;
using System.Collections;
public class PhoneName : IListProductName
{
    public PhoneName() => System.Console.WriteLine("PhoneName created");
    // Mảng tên các điện thoại
    private List<string> phone_names =  new List<string> {
        "Iphone 7", "Samsung Galaxy", "Nokia 123"
    };
    public IEnumerable<string> GetNames()
    {
        return phone_names;
    }
}
