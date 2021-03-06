﻿using System;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace BidMessages
{
    /// <summary>
    /// Models the login request sent to server.
    /// </summary>
    public class LoginRequestMessage : ControlRequestMessage
    {
        private int m_bodyLength;
        private string m_username;
        private string m_password;
        private uint m_sessionKey;
        private string m_verificatonCode;

        /// <summary>
        /// Initializes a new instance of the <c>LoginRequest</c> class with the given verification code.
        /// </summary>
        /// <param name="username">the username for the remote server.</param>
        /// <param name="password">the password to the account.</param>
        /// <param name="sessionKey">the session key for the current login session.</param>
        /// <exception cref="System.ArgumentNullException">The input username or password is null.</exception>
        public LoginRequestMessage(string username, string password, uint sessionKey)
        {
            if (username == null || password == null)
            {
                throw new ArgumentNullException("username or password cannot be null.");
            }

            m_username = username;
            m_password = password;
            m_sessionKey = sessionKey;

            m_verificatonCode = CreateVerificationCode();
            m_bodyLength = TextEncoding.GetByteCount(m_verificatonCode);
        }

        /// <summary>
        /// The message's function code.
        /// </summary>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.LoginRequest;
            }
        }

        /// <summary>
        /// The verification code used in login request.
        /// </summary>
        public string VerificationCode
        {
            get { return m_verificatonCode; }
        }

        /// <summary>
        /// Creates a verification code with the given username, password, and session key.
        /// </summary>
        /// <returns>The verification code.</returns>
        private string CreateVerificationCode()
        {
            byte[] input, output;

            input = TextEncoding.GetBytes(m_username + m_sessionKey.ToString("x8") + m_password);

            using (MD5 md5 = MD5.Create())
            {
                output = md5.ComputeHash(input);
                Debug.Assert(output != null && output.Length > 0);
            }

            StringBuilder builder = new StringBuilder(32);
            foreach (byte b in output)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Encodes the body of a <c>LoginRequestMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override int GetBodyBytes(byte[] target, int offset)
        {
            string body = m_verificatonCode;
            return TextEncoding.GetBytes(body, 0, body.Length, target, offset);
        }

        /// <summary>
        /// Gets the length of the body of this message.
        /// </summary>
        /// <returns>The length of the body of this message.</returns>
        protected override int GetBodyLength()
        {
            return m_bodyLength;
        }

        /// <summary>
        /// Gets a string representation for the specified <c>LoginRequestMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and username.</returns>
        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, m_username);
        }
    }
}
