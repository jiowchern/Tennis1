using Tennis1.Common;
using Regulus.Remote;
using System;

namespace Tennis1.User
{
    public static class GPIHelper
    {
        public static Value<bool> Connect(this IConnect gpi, string ip , int port)
        {

            return gpi.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip) , port));
        }

        public static void Move(this IControll gpi, float x ,float y)
        {

            gpi.Move(new Regulus.CustomType.Vector2(x,y));
        }

        internal static void SignUp(IPreparer gpi, string name, int count)
        {
            gpi.SignUp(new Registration() { Name=name,PlayerNumber = count});
        }
    }
}