﻿using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session F.
    /// </summary>
    public class SessionFMessage : SessionCEFHMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionFMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionFMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session F message.</exception>
        public SessionFMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
            if (PeekSession(message, startIndex) != AuctionSession)
            {
                throw new ArgumentException("Session mismatch.");
            }
        }

        /// <summary>
        /// Property <c>AuctionSession</c> represents the message's auction session.
        /// </summary>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionF;
            }
        }
    }
}
