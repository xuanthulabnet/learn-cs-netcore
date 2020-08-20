using System;

namespace CS026_Attribute {
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class MotaAttribute : Attribute // có thể đặt tên Mota thay cho MotaAttribute
    {
        public MotaAttribute(string v) => Description = v;

        public string Description {set; get;}
    }

}