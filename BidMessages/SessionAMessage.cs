﻿using System;

namespace BidMessages
{
    /// <summary>
    /// Models <c>QuoteMessage</c>s of Session A.
    /// </summary>
    public class SessionAMessage : QuoteDataMessage
    {
        private static readonly int[] m_tagToIndex = new int[]
        {
            -1, // Undefined,
            0, // UpdateTimestamp,
            1, // AuctionSession,
            2, // InitialPriceFlag,
            3, // AuctionName,
            4, // BidSize,
            5, // LimitPrice,
            6, // InitialPrice,
            7, // AuctionBeginTime,
            8, // AuctionEndTime,
            9, // FirstBeginTime,
            10, // FirstEndTime,
            11, // SecondBeginTime,
            12, // SecondEndTime,
            13, // ServerTime,
            14, // BidQuantity
            15, // BidPrice,
            16, // BidTime,
            17, // ProcessedCount,
            18, // PendingCount,
            -1, // BidLower,
            -1, // BidUpper,
            -1, // ContentText,
        };

        /// <summary>
        /// Initializes a new instance of the <c>SessionA</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionAMessage</c>.</param>
        /// <param name="offset">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session A message.</exception>
        public SessionAMessage(byte[] message, int offset, int count)
            : base(message, offset, count)
        {
            if (PeekSession(message, offset) != AuctionSession)
            {
                throw new ArgumentException("Session mismatch.");
            }
        }

        /// <summary>
        /// The message's auction session.
        /// </summary>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionA;
            }
        }

        /// <summary>
        /// The message's price upper bound.
        /// </summary>
        public override int PriceUpperBound
        {
            get
            {
                return LimitPrice;
            }
        }

        /// <summary>
        /// The message's price lower bound.
        /// </summary>
        public override int PriceLowerBound
        {
            get
            {
                return InitialPrice;
            }
        }

        /// <summary>
        /// The message's limit price.
        /// </summary>
        public int LimitPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.LimitPrice));
            }
        }

        /// <summary>
        /// Gets the index to the fields array given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in the fields array or -1 if the field doesn't exist.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The input tag is out of range.</exception>
        public override int GetIndexFromTag(QuoteFieldTags tag)
        {
            int index = (int)tag;

            if (index < 0 || index >= m_tagToIndex.Length)
            {
                throw new ArgumentOutOfRangeException("tag out of range.");
            }

            return m_tagToIndex[index];
        }
    }
}
