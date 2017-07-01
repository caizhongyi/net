package org.cross.sms.msg;

/**
 */
public class SMSGSMEncoding {
    public static final int GSM7BITDEFAULT = 1;


    private static final String alphabet = "@?$\u00A5\u00E8\u00E9\u00F9\u00EC\u00F2\u00C7\n\u00D8\u00F8\r\u00C5\u00E5ƒ_÷√ÀŸ–ÿ”»Œ@\u00C6\u00E6\u00DF\u00C9 !\"#\u00A4%&\'()*+,-./0123456789:;<=>?\u00A1ABCDEFGHIJKLMNOPQRSTUVWXYZ\u00C4\u00D6\u00D1\u00DCß\u00BFabcdefghijklmnopqrstuvwxyz\u00E4\u00F6\u00F1\u00FC\u00E0";

    /**
     Converts an ASCII character to its hexadecimal pair.

     @param	c	the ASCII character.
     @param	charSet	the target character set for the conversion.
     @return	the two hex digits which represent the character in the
       specific character set.
     */
    private static String char2Hex(char c, int charSet) {
        switch (charSet) {
        case GSM7BITDEFAULT:
            for (int i = 0; i < alphabet.length(); i++) {
                if (alphabet.charAt(i) == c) {
                    return (i <= 15 ? "0" + Integer.toHexString(i) :
                            Integer.toHexString(i));
                }
            }
            break;
        }
        return (Integer.toHexString((int) c).length() < 2) ?
                "0" + Integer.toHexString((int) c) :
                Integer.toHexString((int) c);
    }

    /**
     Converts a hexadecimal value to the ASCII character it represents.

     @param	index	 the hexadecimal value.
     @param	charSet	the character set in which "index" is represented.
     @return  the ASCII character which is represented by the hexadecimal value.
     */
    public static char hex2Char(int index, int charSet) {
        switch (charSet) {
        case GSM7BITDEFAULT:
            if (index < alphabet.length()) {
                return alphabet.charAt(index);
            } else {
                return '?';
            }
        }
        return '?';
    }

    /**
     Converts a int value to the extended ASCII character it represents.
     @author George Karadimas
     @param	ch	 the int value.
     @param	charSet	the character set in which "ch" is represented.
     @return  the extended ASCII character which is represented by the int value.
     */
    public static char hex2ExtChar(int ch, int charSet) {
        switch (charSet) {
        case GSM7BITDEFAULT:
            switch (ch) {
            case 10:
                return '\f';
            case 20:
                return '^';
            case 40:
                return '{';
            case 41:
                return '}';
            case 47:
                return '\\';
            case 60:
                return '[';
            case 61:
                return '~';
            case 62:
                return ']';
            case 64:
                return '|';
            case 101:
                return '\u20AC';
            default:
                return '?';
            }
        default:
            return '?';
        }
    }

    /**
     Converts the given ASCII string to a string of hexadecimal pairs.

     @param	text	the ASCII string.
     @param	charSet	the target character set for the conversion.
     @return	the string of hexadecimals pairs which represent the "text"
       parameter in the specified "charSet".
     */
    public static String text2Hex(String text, int charSet) {
        String outText = "";

        for (int i = 0; i < text.length(); i++) {
            switch (text.charAt(i)) {

            default:
                outText = outText + char2Hex(text.charAt(i), charSet);
                break;

            }

        }

        return outText;
    }

    /**
     Converts the given string of hexadecimal pairs to its ASCII equivalent string.

     @param	text	the hexadecimal pair string.
     @param	charSet	the target character set for the conversion.
     @return	the ASCII string.
     */
    public static String hex2Text(String text, int charSet) {
        String outText = "";

        for (int i = 0; i < text.length(); i += 2) {
            String hexChar = "" + text.charAt(i) + text.charAt(i + 1);
            int c = Integer.parseInt(hexChar, 16);
            if (c == 27) {
                i++;
                outText = outText + hex2ExtChar((char) c, charSet);
            } else {
                outText = outText + hex2Char((char) c, charSet);
            }
        }
        return outText;
    }
}
