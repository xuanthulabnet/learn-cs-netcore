using System;

namespace mvcblog.Events
{
    public static class RenderEvents
    {
        public static event EventHandler render_head;

        public static string RenderHead(object sender) {
            render_head?.Invoke(sender, null);
            return null;
        }

    }
}