using Common;
using System;
using System.IO;
using System.Reflection;

namespace Server
{
    class Program
    {
        public static void Main(String[] args)
        { 
            SockerManager.Instance.StartListening();
        }

    }
}
