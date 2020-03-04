using System;
using System.Text;
using Xunit;

namespace util.common.test
{
    public class StringTest
    {
        [Fact]
        public void GetLast()
        {
            Assert.True("UnoDosTres".Last(4) == "Tres");
            Assert.True("".Last(4) == "");
        }

        [Fact]
        public void Random()
        {
            var text = "UnoDosTres";
            var newTxt = "UnoDosTres".Random();
            Assert.True(text.Length == newTxt.Length && text != newTxt);
        }

        [Fact]
        public void IsEmail()
        {
            Assert.True("mail@corre.com".Check().IsEmail());
            Assert.False("mail@corre".Check().IsEmail());
            Assert.False("".Check().IsEmail());
            Assert.False("mail @corre.com".Check().IsEmail());
            Assert.False("mailcorre.com".Check().IsEmail());
        }

        [Fact]
        public void IsPhone()
        {
            Assert.False("9999".Check().IsPhone());
            Assert.True("915555555".Check().IsPhone());
        }

        [Fact]
        public void IsMobile()
        {
            Assert.False("9999".Check().IsMobile());
            Assert.False("915555555".Check().IsMobile());
            Assert.True("616451328".Check().IsMobile());

        }

        [Fact]
        public void FormatWithMask()
        {
            Assert.True("holacomoestas".Check().IsFormatWithMask("#### #### #####") == "hola como estas");
            Assert.True("".Check().IsFormatWithMask("#### #### #####") == "");
            Assert.True("holacomoestas".Check().IsFormatWithMask("#### #### ##########") == "hola como estas");
            Assert.True("holacomoestas".Check().IsFormatWithMask("FORMATEADO: #### #### ##########") == "FORMATEADO: hola como estas");
        }

        [Fact]
        public void IsIsin()
        {
            Assert.True("AU0000XVGZA3".Check().IsIsin());
            Assert.False("AU0000XVGZD3".Check().IsIsin());
            Assert.False("".Check().IsIsin());
        }

        [Fact]
        public void IsValidUrl()
        {
            Assert.True("http://www.google.es".Check().IsUrl());
            Assert.True("http://www.google".Check().IsUrl());
            Assert.False("httpas://www.google".Check().IsUrl());
        }

        [Fact]
        public void Repeat()
        {
            Assert.True("HOLA".Repeat(3) == "HOLAHOLAHOLA");
            Assert.True("".Repeat(4) == "");
        }
    }
}
