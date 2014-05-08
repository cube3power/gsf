﻿//******************************************************************************************************
//  GuidExtensions.cs - Gbtc
//
//  Copyright © 2014, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  05/07/2014 - Steven E. Chisholm
//       Generated original version of source code.
//
//******************************************************************************************************

using System;

namespace GSF
{
    /// <summary>
    /// Extension methods for <see cref="Guid"/>.
    /// </summary>
    public unsafe static class GuidExtensions
    {

        /// <summary>
        /// Encodes a <see cref="Guid"/> following RFC 4122.
        /// </summary>
        /// <param name="guid">the <see cref="Guid"/> to serialize</param>
        /// <param name="buffer">where to store the <see cref="guid"/></param>
        /// <param name="startingIndex">the starting index in <see cref="buffer"/></param>
        public static int ToRfcBytes(this Guid guid, byte[] buffer, int startingIndex)
        {
            //Since Microsoft is not very clear how Guid.ToByteArray() performs on big endian processors
            //we are assuming that the internal structure of a Guid will always be the same. Reviewing mono source code
            //the internal stucture is also the same.

            buffer.ValidateParameters(startingIndex, 16);

            byte* src = (byte*)&guid;
            fixed (byte* dst = &buffer[startingIndex])
            {
                if (BitConverter.IsLittleEndian)
                {
                    //Guid._a (int)
                    dst[0] = src[3];
                    dst[1] = src[2];
                    dst[2] = src[1];
                    dst[3] = src[0];
                    //Guid._b (short)
                    dst[4] = src[5];
                    dst[5] = src[4];
                    //Guid._c (short)
                    dst[6] = src[7];
                    dst[7] = src[6];
                    //Guid._d - Guid._k (8 bytes)
                    //Since already encoded as big endian, just copy the data.
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }
                else
                {
                    //all fields are encoded big-endian. Just copy.
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }
            }
            return 16;
        }

        /// <summary>
        /// Encodes a <see cref="Guid"/> following RFC 4122.
        /// </summary>
        /// <param name="guid">the <see cref="Guid"/> to serialize</param>
        /// <returns>A <see cref="byte"/>[] that represents a big endian encoded Guid.</returns>
        public static byte[] ToRfcBytes(this Guid guid)
        {
            byte[] rv = new byte[16];
            guid.ToRfcBytes(rv, 0);
            return rv;
        }

        /// <summary>
        /// Decodes a <see cref="Guid"/> following RFC 4122
        /// </summary>
        /// <param name="buffer">where to read the <see cref="Guid"/>.</param>
        /// <returns></returns>
        public static Guid ToRfcGuid(this byte[] buffer)
        {
            return buffer.ToRfcGuid(0);
        }

        /// <summary>
        /// Decodes a <see cref="Guid"/> following RFC 4122
        /// </summary>
        /// <param name="buffer">where to read the <see cref="Guid"/>.</param>
        /// <param name="startingIndex">the starting index in <see cref="buffer"/>.</param>
        /// <returns></returns>
        public static Guid ToRfcGuid(this byte[] buffer, int startingIndex)
        {
            buffer.ValidateParameters(startingIndex, 16);

            //Since Microsoft is not very clear how Guid.ToByteArray() performs on big endian processors
            //we are assuming that the internal structure of a Guid will always be the same. Reviewing mono source code
            //the internal stucture is also the same.
            Guid rv;
            byte* dst = (byte*)&rv;
            fixed (byte* src = &buffer[startingIndex])
            {
                if (BitConverter.IsLittleEndian)
                {
                    //Guid._a (int)
                    dst[0] = src[3];
                    dst[1] = src[2];
                    dst[2] = src[1];
                    dst[3] = src[0];
                    //Guid._b (short)
                    dst[4] = src[5];
                    dst[5] = src[4];
                    //Guid._c (short)
                    dst[6] = src[7];
                    dst[7] = src[6];
                    //Guid._d - Guid._k (8 bytes)
                    //Since already encoded as big endian, just copy the data.
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }
                else
                {
                    //all fields are encoded big-endian. Just copy.
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }

                return rv;
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Obsolete methods to support backwards compatibility with a bug that existed in EndianOrder's Guid methods
        //---------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="guid">the <see cref="Guid"/> to serialize</param>
        /// <returns>A <see cref="byte"/>[] that represents a big endian encoded Guid.</returns>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcBytes from now on.", false)]
        public static byte[] __ToBigEndianOrderBytes(Guid guid)
        {
            byte[] rv = new byte[16];
            __ToBigEndianOrderBytes(guid, rv, 0);
            return rv;
        }

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="guid">the <see cref="Guid"/> to serialize</param>
        /// <param name="buffer">where to store the <see cref="guid"/></param>
        /// <param name="startingIndex">the starting index in <see cref="buffer"/></param>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcBytes from now on.", false)]
        public static int __ToBigEndianOrderBytes(Guid guid, byte[] buffer, int startingIndex)
        {
            //Since Microsoft is not very clear how Guid.ToByteArray() performs on big endian processors
            //we are assuming that the internal structure of a Guid will always be the same. Reviewing mono source code
            //the internal stucture is also the same.

            buffer.ValidateParameters(startingIndex, 16);

            byte* src = (byte*)&guid;
            fixed (byte* dst = &buffer[startingIndex])
            {
                if (BitConverter.IsLittleEndian)
                {
                    dst[15] = src[0];
                    dst[14] = src[1];
                    dst[13] = src[2];
                    dst[12] = src[3];
                    dst[11] = src[4];
                    dst[10] = src[5];
                    dst[9] = src[6];
                    dst[8] = src[7];
                    dst[7] = src[8];
                    dst[6] = src[9];
                    dst[5] = src[10];
                    dst[4] = src[11];
                    dst[3] = src[12];
                    dst[2] = src[13];
                    dst[1] = src[14];
                    dst[0] = src[15];
                }
                else
                {
                    //ToDo: Test this on a big endian architecture.

                    //Guid._a (int)  //swap endian
                    dst[15] = src[3];
                    dst[14] = src[2];
                    dst[13] = src[1];
                    dst[12] = src[0];
                    //Guid._b (short) //swap endian
                    dst[11] = src[5];
                    dst[10] = src[4];
                    //Guid._c (short) //swap endian
                    dst[9] = src[7];
                    dst[8] = src[6];
                    //Guid._d - Guid._k (8 bytes)
                    dst[7] = src[8];
                    dst[6] = src[9];
                    dst[5] = src[10];
                    dst[4] = src[11];
                    dst[3] = src[12];
                    dst[2] = src[13];
                    dst[1] = src[14];
                    dst[0] = src[15];
                }
            }
            return 16;
        }

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="buffer">where to read the <see cref="Guid"/>.</param>
        /// <returns></returns>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcGuid from now on.", false)]
        public static Guid __ToBigEndianOrderGuid(byte[] buffer)
        {
            return __ToBigEndianOrderGuid(buffer, 0);
        }

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="buffer">where to read the <see cref="Guid"/>.</param>
        /// <param name="startingIndex">the starting index in <see cref="buffer"/>.</param>
        /// <returns></returns>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcGuid from now on.", false)]
        public static Guid __ToBigEndianOrderGuid(byte[] buffer, int startingIndex)
        {
            buffer.ValidateParameters(startingIndex, 16);

            //BigEndianOrder was a reverse of byte ordering that microsoft used.

            Guid rv;
            byte* dst = (byte*)&rv;
            fixed (byte* src = &buffer[startingIndex])
            {
                if (BitConverter.IsLittleEndian)
                {
                    dst[15] = src[0];
                    dst[14] = src[1];
                    dst[13] = src[2];
                    dst[12] = src[3];
                    dst[11] = src[4];
                    dst[10] = src[5];
                    dst[9] = src[6];
                    dst[8] = src[7];
                    dst[7] = src[8];
                    dst[6] = src[9];
                    dst[5] = src[10];
                    dst[4] = src[11];
                    dst[3] = src[12];
                    dst[2] = src[13];
                    dst[1] = src[14];
                    dst[0] = src[15];
                }
                else
                {
                    //ToDo: Test this on a big endian architecture.

                    //Guid._a (int)  //swap endian
                    dst[15] = src[3];
                    dst[14] = src[2];
                    dst[13] = src[1];
                    dst[12] = src[0];
                    //Guid._b (short) //swap endian
                    dst[11] = src[5];
                    dst[10] = src[4];
                    //Guid._c (short) //swap endian
                    dst[9] = src[7];
                    dst[8] = src[6];
                    //Guid._d - Guid._k (8 bytes)
                    dst[7] = src[8];
                    dst[6] = src[9];
                    dst[5] = src[10];
                    dst[4] = src[11];
                    dst[3] = src[12];
                    dst[2] = src[13];
                    dst[1] = src[14];
                    dst[0] = src[15];
                }
                return rv;
            }
        }



        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="guid">the <see cref="Guid"/> to serialize</param>
        /// <returns>A <see cref="byte"/>[] that represents a big endian encoded Guid.</returns>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcBytes from now on.", false)]
        public static byte[] __ToLittleEndianOrderBytes(Guid guid)
        {
            byte[] rv = new byte[16];
            __ToLittleEndianOrderBytes(guid, rv, 0);
            return rv;
        }

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="guid">the <see cref="Guid"/> to serialize</param>
        /// <param name="buffer">where to store the <see cref="guid"/></param>
        /// <param name="startingIndex">the starting index in <see cref="buffer"/></param>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcBytes from now on.", false)]
        public static int __ToLittleEndianOrderBytes(Guid guid, byte[] buffer, int startingIndex)
        {
            //This encoding is the same as the default microsoft encoding. 
            //Which is each internal word of the guid is stored little endian.

            buffer.ValidateParameters(startingIndex, 16);

            byte* src = (byte*)&guid;
            fixed (byte* dst = &buffer[startingIndex])
            {
                if (BitConverter.IsLittleEndian)
                {
                    //just copy the data
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }
                else
                {
                    //ToDo: Test this on a big endian architecture.

                    //Guid._a (int)  //swap endian
                    dst[0] = src[3];
                    dst[1] = src[2];
                    dst[2] = src[1];
                    dst[3] = src[0];
                    //Guid._b (short) //swap endian
                    dst[4] = src[5];
                    dst[5] = src[4];
                    //Guid._c (short) //swap endian
                    dst[6] = src[7];
                    dst[7] = src[6];
                    //Guid._d - Guid._k (8 bytes)
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }
            }
            return 16;
        }

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="buffer">where to read the <see cref="Guid"/>.</param>
        /// <returns></returns>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcGuid from now on.", false)]
        public static Guid __ToLittleEndianOrderGuid(byte[] buffer)
        {
            return __ToLittleEndianOrderGuid(buffer, 0);
        }

        /// <summary>
        /// Mimicks the encoding that was present in BigEndianOrder. 
        /// </summary>
        /// <param name="buffer">where to read the <see cref="Guid"/>.</param>
        /// <param name="startingIndex">the starting index in <see cref="buffer"/>.</param>
        /// <returns></returns>
        [Obsolete("This method is for backwards compatibility only. Use ToRfcGuid from now on.", false)]
        public static Guid __ToLittleEndianOrderGuid(byte[] buffer, int startingIndex)
        {
            buffer.ValidateParameters(startingIndex, 16);

            //Since Microsoft is not very clear how Guid.ToByteArray() performs on big endian processors
            //we are assuming that the internal structure of a Guid will always be the same. Reviewing mono source code
            //the internal stucture is also the same.
            Guid rv;
            byte* dst = (byte*)&rv;
            fixed (byte* src = &buffer[startingIndex])
            {
                if (BitConverter.IsLittleEndian)
                {
                    //internal stucture is correct, just copy
                    *(long*)(dst + 0) = *(long*)(src + 0);
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }
                else
                {
                    //ToDo: Test this on a big endian architecture.

                    //Guid._a (int) //swap endian
                    dst[0] = src[3];
                    dst[1] = src[2];
                    dst[2] = src[1];
                    dst[3] = src[0];
                    //Guid._b (short) //swap endian
                    dst[4] = src[5];
                    dst[5] = src[4];
                    //Guid._c (short) //swap endian
                    dst[6] = src[7];
                    dst[7] = src[6];
                    //Guid._d - Guid._k (8 bytes)
                    *(long*)(dst + 8) = *(long*)(src + 8);
                }

                return rv;
            }
        }

    }
}