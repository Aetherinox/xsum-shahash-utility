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
using System.Text;
using System.Text.RegularExpressions;

namespace Starksoft.Aspen.GnuPG
{

    /*
        Class structure that proves a read-only view of the GnuPG keys. 
    */

    public class GpgKey
    {
        private string _key;
        private DateTime _keyCreation;
	    private DateTime _keyExpiration;
        private string _userId;
        private string _userName;
        private string _subKey;
        private DateTime _subKeyExpiration;
        private string _raw;

        /*
            GnuPGKey constructor.

            :   raw
                Raw output stream text data containing key information.
        */

        public GpgKey(string raw)
        {
            _raw = raw;
            ParseRaw();          
        }

        /*
            Key text information.
        */

        public string Key
        {
            get { return _key; }
        }

        /*
            Key creation date and time (if available otherwise DateTime.MinValue).
        */

        public DateTime KeyCreation
        {
            get { return _keyCreation; }
        }

        /*
            Key expiration date and time.
        */

        public DateTime KeyExpiration
        {
            get { return _keyExpiration; }
        }

        /*
            Key user identification.
        */

        public string UserId
        {
            get { return _userId; }
        }

        /*
            Key user name.
        */

        public string UserName
        {
            get { return _userName; }
        }

        /*
            Sub-key information.
        */

        public string SubKey
        {
            get { return _subKey; }
        }

        /*
            Sub-key expiration data and time.
        */

        public DateTime SubKeyExpiration
        {
            get { return _subKeyExpiration; }
        }

        /*
            Raw output key text generated by GPG.EXE.
        */

        public string Raw
        {
            get { return _raw; }
        }

        /*
            Parse the raw console data as provided by gpg output.
        */

        private void ParseRaw()
        {
            // split the lines either CR or LF and then remove the empty entries
            // this will allow the solution to work both Linux and Windows 
            string rawClean     = _raw;
            rawClean            = rawClean.Replace( "[", "" );
            rawClean            = rawClean.Replace( "]", "" );

            string[] lines      = rawClean.Split(  new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries );
            string[] pub        = SplitSpaces( lines[ 0 ] );
            string uid          = null;
            string[] sub        = null;

            if ( lines.Length > 1 )
                uid = lines[ 1 ];

            if ( lines.Length < 1 )
                sub = SplitSpaces( lines[ 2 ] );
                        
            _key = pub[ 1 ];

            if ( pub.Length >= 6 )
            {
                if ( pub[ 5 ].Length > 1 )
                    _keyExpiration  = DateTime.Parse( pub[ 5 ].Substring( 0, pub[ 5 ].Length ) );
                    _keyCreation    = DateTime.Parse( pub[ 2 ] );
                } 
                else 
                {
                    // try to parse it
                    DateTime.TryParse( pub[ 2 ], out _keyExpiration );
                }

                // test to see if there is a sub key
                if ( sub != null )
                {
                    _subKey = sub[ 1 ];
                    DateTime.TryParse( sub[ 2 ], out _subKeyExpiration );

                
            }

            // test to see if we have a uid and if so try to parse it
            if ( uid != null )
                ParseUid( uid );
        }

        /*
            Split Spaces
        */

        private string[] SplitSpaces(string input)
        {
            char[] splitChar = { ' ' };
            return input.Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
        }

        /*
            Parse UID
        */

        private void ParseUid( string uid )
        {
            Regex name      = new Regex( @"(?<=uid).*(?=<)" );
            Regex userId    = new Regex( @"(?<=<).*(?=>)" );

            _userName       = name.Match( uid ).ToString( ).Trim( );
            _userId         = userId.Match( uid ).ToString( );
        }

        /// <summary>
        /// Returns string reprentation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("Key: {0}{1}", _key, Environment.NewLine);
            s.AppendFormat("KeyCreation: {0}{1}", _keyCreation, Environment.NewLine);
            s.AppendFormat("KeyExpiration: {0}{1}", _keyExpiration, Environment.NewLine);
            s.AppendFormat("UserId: {0}{1}", _userId, Environment.NewLine);
            s.AppendFormat("SubKey: {0}{1}", _subKey, Environment.NewLine);
            s.AppendFormat("SubKeyExpiration: {0}{1}", _subKeyExpiration, Environment.NewLine);
            s.AppendFormat("Raw: {0}{1}", _raw, Environment.NewLine);

            return s.ToString();
        }

    }
}
