using Regulus.Remote;
using System;

namespace Regulus.Game.Tennis1.User
{
    public static class GPIHelper
    {
        public static Value<bool> Connect(this IConnect gpi, string ip , int port)
        {

            return gpi.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip) , port));
        }

        public static void Move(this Protocol.IControll gpi, float x ,float y)
        {

            gpi.Move(new CustomType.Vector2(x,y));
        }
    }
}