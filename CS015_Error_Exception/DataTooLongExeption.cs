using System;
namespace CS015_Error_Exception
{
    public class DataTooLongExeption : Exception
    {
        const string erroMessage = "Dữ liệu quá dài";
        public DataTooLongExeption() : base(erroMessage) {
        }
    }
}