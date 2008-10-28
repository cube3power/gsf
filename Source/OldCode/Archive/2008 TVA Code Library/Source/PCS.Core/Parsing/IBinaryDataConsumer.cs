//*******************************************************************************************************
//  IBinaryDataConsumer.cs
//  Copyright © 2008 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: James R Carroll
//      Office: PSO TRAN & REL, CHATTANOOGA - MR 2W-C
//       Phone: 423/751-2827
//       Email: jrcarrol@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  03/01/2007 - Pinal C. Patel
//       Original version of source code generated.
//  09/10/2008 - J. Ritchie Carroll
//      Converted to C#.
//  10/28/2008 - Pinal C. Patel
//      Edited code comments.
//
//*******************************************************************************************************

namespace PCS.Parsing
{
    /// <summary>
    /// Specifies that this <see cref="System.Type"/> can initialize objects from a binary image.
    /// </summary>
    public interface IBinaryDataConsumer
    {
        /// <summary>
        /// Initializes object from the specified binary image.
        /// </summary>
        /// <param name="binaryImage">Binary image to be used for initialization.</param>
        /// <param name="startIndex">0-based starting index in the <paramref name="binaryImage"/> to be used for initialization.</param>
        /// <returns>The number of bytes used for initialization.</returns>
        int Initialize(byte[] binaryImage, int startIndex);
    }
}