using System;

namespace WebDAVClient.Helpers
{
    public class WebDAVException : Exception
    {
        private int m_nCode = -1;
        private string m_strMessage = "";
        private Exception m_exception = null;

        public WebDAVException()
        {
        }

        public WebDAVException(string message)
        {
            m_strMessage = message;
        }

        public WebDAVException(string message, Exception innerException) 
        {
            m_strMessage = message;
            m_exception = innerException;
        }

        public WebDAVException(int httpCode, string message, Exception innerException) 
        {
            m_nCode = httpCode;
            m_strMessage = message;
            m_exception = innerException;
        }

        public WebDAVException(int httpCode, string message) 
        {
            m_nCode = httpCode;
            m_strMessage = message;
        }

        public override string ToString()
        {
            var s = string.Format("HttpStatusCode: {0}", m_nCode);
            s += Environment.NewLine + string.Format("Message: {0}", m_strMessage);
            return s;
        }
    }
}