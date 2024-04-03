/*
	@Title		: Algorithms > CRC
	@Website	: https://github.com/Aetherinox/XSum-shahash-utility
	@Authors	: Aetherinox
                : Benton Stark

	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.
*/

using System;
using System.ComponentModel;

namespace Starksoft.Aspen.GnuPG
{
    /// <summary>
    /// Event arguments class for the DecryptAsyncCompleted event.
    /// </summary>
    public class DecryptAsyncCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="error">Exception information generated by the event.</param>
        /// <param name="cancelled">Cancelled event flag.  This flag is set to true if the event was cancelled.</param>
        public DecryptAsyncCompletedEventArgs(Exception error, bool cancelled)
            : base(error, cancelled, null)
        {
        }
    }

} 
