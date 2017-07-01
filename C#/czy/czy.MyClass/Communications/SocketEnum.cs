using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyClass.Communications
{
    public class SocketEnum
    {
        public enum MessageType
        {
            String,
            File
        }

        public enum SendFileState
        {
            Start,
            Stop
        }
    }
}
