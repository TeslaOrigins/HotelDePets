using System;

namespace HDP.Application.Exceptions
{
    public class BusinessException<Object> : Exception
    {
        public string[] messages { get; set; }
        public Object obj { get; set; }

        public BusinessException()
        {
        }

        public BusinessException(string[] messages, Object obj)
        {
            this.messages = messages;
        }
    }
}