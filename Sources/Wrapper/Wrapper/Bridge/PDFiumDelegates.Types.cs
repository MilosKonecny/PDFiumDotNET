﻿namespace PDFiumDotNET.Wrapper.Bridge
{
    using System.Runtime.InteropServices;

    // Disable "Field 'xxxx' should not contain an underscore" to preserve PDFium names.
#pragma warning disable SA1310

    /// <summary>
    /// The class contains all delegates of methods in pdfium dll.
    /// </summary>
    internal partial class PDFiumDelegates
    {
        /// <summary>
        /// Error value - no error.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_SUCCESS = 0;

        /// <summary>
        /// Error value - unknown error.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_UNKNOWN = 1;

        /// <summary>
        /// Error value - file not found or could not be opened.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_FILE = 2;

        /// <summary>
        /// Error value - file not in PDF format or corrupted.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_FORMAT = 3;

        /// <summary>
        /// Error value - password required or incorrect password.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_PASSWORD = 4;

        /// <summary>
        /// Error value - unsupported security scheme.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_SECURITY = 5;

        /// <summary>
        /// Error value - page not found or content error.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_PAGE = 6;

        /// <summary>
        /// Error value - load XFA error.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_XFALOAD = 7;

        /// <summary>
        /// Error value - layout XFA error.
        /// Return value of <see cref="FPDF_GetLastError"/>.
        /// </summary>
        internal const int FPDF_ERR_XFALAYOUT = 8;

        /// <summary>
        /// Permission bit - allow print - 3. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWPRINT = 0b100;

        /// <summary>
        /// Permission bit - allow content change - 4. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWCONTENTCHANGE = 0b1000;

        /// <summary>
        /// Permission bit - allow extract text and graphics 1 - 5. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWEXTRACTTEXTANDGRAPHICS1 = 0b10000;

        /// <summary>
        /// Permission bit - allow anotations - 6. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWANNOTATIONS = 0b100000;

        /// <summary>
        /// Permission bit - allow fill form fields - 9. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWFILLFORMFIELDS = 0b100000000;

        /// <summary>
        /// Permission bit - allow extract text and graphics 2 - 10. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWEXTRACTTEXTANDGRAPHICS2 = 0b1000000000;

        /// <summary>
        /// Permission bit - allow assemble document - 11. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWASSEMBLEDOCUMENT = 0b10000000000;

        /// <summary>
        /// Permission bit - allow print in high quality - 12. bit of permission value.
        /// Return value of <see cref="FPDF_GetDocPermissions"/>.
        /// </summary>
        internal const uint FPDF_PERMISSION_ALLOWPRINTHIGHQUALITY = 0b1000000000;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set if annotations are to be rendered.
        /// </summary>
        internal const int FPDF_ANNOT = 0x01;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set if using text rendering optimized for LCD display. This flag will only take effect if anti-aliasing is enabled for text.
        /// </summary>
        internal const int FPDF_LCD_TEXT = 0x02;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Don't use the native text output available on some platforms.
        /// </summary>
        internal const int FPDF_NO_NATIVETEXT = 0x04;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Grayscale output.
        /// </summary>
        internal const int FPDF_GRAYSCALE = 0x08;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Obsolete, has no effect, retained for compatibility.
        /// </summary>
        internal const int FPDF_DEBUG_INFO = 0x80;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Obsolete, has no effect, retained for compatibility.
        /// </summary>
        internal const int FPDF_NO_CATCH = 0x100;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Limit image cache size.
        /// </summary>
        internal const int FPDF_RENDER_LIMITEDIMAGECACHE = 0x200;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Always use halftone for image stretching.
        /// </summary>
        internal const int FPDF_RENDER_FORCEHALFTONE = 0x400;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Render for printing.
        /// </summary>
        internal const int FPDF_PRINTING = 0x800;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set to disable anti-aliasing on text. This flag will also disable LCD optimization for text rendering.
        /// </summary>
        internal const int FPDF_RENDER_NO_SMOOTHTEXT = 0x1000;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set to disable anti-aliasing on images.
        /// </summary>
        internal const int FPDF_RENDER_NO_SMOOTHIMAGE = 0x2000;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set to disable anti-aliasing on paths.
        /// </summary>
        internal const int FPDF_RENDER_NO_SMOOTHPATH = 0x4000;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set whether to render in a reverse Byte order, this flag is only used when rendering to a bitmap.
        /// </summary>
        internal const int FPDF_REVERSE_BYTE_ORDER = 0x10;

        /// <summary>
        /// Page rendering flags. They can be combined with bit-wise OR.
        /// Used in <see cref="FPDF_RenderPageBitmap"/> or <see cref="FPDF_RenderPageBitmapWithMatrix"/>.
        /// Set whether fill paths need to be stroked. This flag is only used when FPDF_COLORSCHEME is passed in,
        /// since with a single fill color for paths the boundaries of adjacent fill paths are less visible.
        /// </summary>
        internal const int FPDF_CONVERT_FILL_TO_STROKE = 0x20;

        /// <summary>
        /// Action type: Unsupported action type.
        /// </summary>
        public const int PDFACTION_UNSUPPORTED = 0;

        /// <summary>
        /// Action type: Go to a destination within current document.
        /// </summary>
        public const int PDFACTION_GOTO = 1;

        /// <summary>
        /// Action type: Go to a destination within another document.
        /// </summary>
        public const int PDFACTION_REMOTEGOTO = 2;

        /// <summary>
        /// Action type: URI, including web pages and other Internet resources.
        /// </summary>
        public const int PDFACTION_URI = 3;

        /// <summary>
        /// Action type: Launch an application or open a file.
        /// </summary>
        public const int PDFACTION_LAUNCH = 4;

        /// <summary>
        /// The file identifier entry type. See section 14.4 "File Identifiers" of the ISO 32000-1 standard.
        /// </summary>
        public enum FPDF_FILEIDTYPE
        {
            /// <summary>
            /// Permanent identifier based on the contents of the file at the time
            /// it was originally created and does not change when the file is incrementally updated.
            /// </summary>
            FILEIDTYPE_PERMANENT = 0,

            /// <summary>
            /// Changing identifier based on the file's contents at the time it was last updated.
            /// </summary>
            FILEIDTYPE_CHANGING = 1,
        }

        /// <summary>
        /// Enumeration defines all possible values defined for paper handling when printing.
        /// </summary>
        internal enum FPDF_DUPLEXTYPE : int
        {
            /// <summary>
            /// Undefined duplex type.
            /// </summary>
            DuplexUndefined = 0,

            /// <summary>
            /// Simplex duplex type.
            /// </summary>
            Simplex,

            /// <summary>
            /// Flip shor edge duplex type.
            /// </summary>
            DuplexFlipShortEdge,

            /// <summary>
            /// Flip long edge duplex type.
            /// </summary>
            DuplexFlipLongEdge,
        }

        /// <summary>
        /// Enumeration defines format values for <see cref="FPDFBitmap_CreateEx"/>.
        /// </summary>
        internal enum FPDFBitmapFormat : int
        {
            /// <summary>
            /// Unknown or unsupported format.
            /// </summary>
            FPDFBitmap_Unknown = 0,

            /// <summary>
            /// Gray scale bitmap, one byte per pixel.
            /// </summary>
            FPDFBitmap_Gray = 1,

            /// <summary>
            /// 3 bytes per pixel, byte order: blue, green, red.
            /// </summary>
            FPDFBitmap_BGR = 2,

            /// <summary>
            /// 4 bytes per pixel, byte order: blue, green, red, unused.
            /// </summary>
            FPDFBitmap_BGRx = 3,

            /// <summary>
            /// 4 bytes per pixel, byte order: blue, green, red, alpha.
            /// </summary>
            FPDFBitmap_BGRA = 4,
        }

        /// <summary>
        /// Color struct represents a 32-bit value specifing the color, in 8888 ARGB format.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct FPDF_COLOR
        {
            [FieldOffset(0)]
            private readonly byte _alpha;
            [FieldOffset(1)]
            private readonly byte _red;
            [FieldOffset(2)]
            private readonly byte _green;
            [FieldOffset(3)]
            private readonly byte _blue;
            [FieldOffset(0)]
            private readonly uint _argb;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_COLOR"/> struct.
            /// </summary>
            /// <param name="red">The red component.</param>
            /// <param name="green">The green component.</param>
            /// <param name="blue">The blue component.</param>
            /// <param name="alpha">The alpha component.</param>
            public FPDF_COLOR(byte red, byte green, byte blue, byte alpha = 255)
            {
                _argb = 0;
                _alpha = alpha;
                _red = red;
                _green = green;
                _blue = blue;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_COLOR"/> struct.
            /// </summary>
            /// <param name="argb">The 32 bit ARGB value of color.</param>
            public FPDF_COLOR(int argb)
            {
                _alpha = 0;
                _red = 0;
                _green = 0;
                _blue = 0;
                _argb = unchecked((uint)argb);
            }

            private FPDF_COLOR(uint argb)
            {
                _alpha = 0;
                _red = 0;
                _green = 0;
                _blue = 0;
                _argb = argb;
            }

            /// <summary>
            /// Gets the alpha component of color.
            /// </summary>
            public byte Alpha => _alpha;

            /// <summary>
            /// Gets the red component of color.
            /// </summary>
            public byte Red => _red;

            /// <summary>
            /// Gets the green component of color.
            /// </summary>
            public byte Green => _green;

            /// <summary>
            /// Gets the blue component of color.
            /// </summary>
            public byte Blue => _blue;

            /// <summary>
            /// Gets the 32 bit ARGB value of color.
            /// </summary>
            public int ARGB => unchecked((int)_argb);

            /// <summary>
            /// Implicit operator.
            /// </summary>
            /// <param name="argb">The 32 bit ARGB value of color to use.</param>
            public static implicit operator FPDF_COLOR(uint argb) => new FPDF_COLOR(argb);
        }

        /// <summary>
        /// Rectangle structure.
        /// </summary>
        internal struct FS_RECTF
        {
            /// <summary>
            /// The x-coordinate of the left-top corner.
            /// </summary>
            public float Left;

            /// <summary>
            /// The y-coordinate of the left-top corner.
            /// </summary>
            public float Top;

            /// <summary>
            /// The x-coordinate of the right-bottom corner.
            /// </summary>
            public float Right;

            /// <summary>
            /// The y-coordinate of the right-bottom corner.
            /// </summary>
            public float Bottom;
        }

        /// <summary>
        /// Size structure.
        /// </summary>
        internal struct FS_SIZEF
        {
            /// <summary>
            /// Width of size.
            /// </summary>
            public float Width;

            /// <summary>
            /// Height of size.
            /// </summary>
            public float Height;
        }

        /// <summary>
        /// Matrix for transformation, in the form [a b c d e f], equivalent to:
        /// | a  b  0 |
        /// | c  d  0 |
        /// | e  f  1 |
        /// Translation is performed with [1 0 0 1 tx ty].
        /// Scaling is performed with [sx 0 0 sy 0 0].
        /// See PDF Reference 1.7, 4.2.2 Common Transformations for more.
        /// </summary>
        internal struct FS_MATRIX
        {
            /// <summary>
            /// A value of matrix.
            /// </summary>
            public float A;

            /// <summary>
            /// Bavalue of matrix.
            /// </summary>
            public float B;

            /// <summary>
            /// C value of matrix.
            /// </summary>
            public float C;

            /// <summary>
            /// D value of matrix.
            /// </summary>
            public float D;

            /// <summary>
            /// E value of matrix.
            /// </summary>
            public float E;

            /// <summary>
            /// F value of matrix.
            /// </summary>
            public float F;
        }

        /// <summary>
        /// Container struct for quadrilateral points.
        /// </summary>
        public struct FS_QUADPOINTSF
        {
            /// <summary>
            /// X position of point 1.
            /// </summary>
            public float X1;

            /// <summary>
            /// Y position of point 1.
            /// </summary>
            public float Y1;

            /// <summary>
            /// X position of point 2.
            /// </summary>
            public float X2;

            /// <summary>
            /// Y position of point 2.
            /// </summary>
            public float Y2;

            /// <summary>
            /// X position of point 3.
            /// </summary>
            public float X3;

            /// <summary>
            /// Y position of point 3.
            /// </summary>
            public float Y3;

            /// <summary>
            /// X position of point 4.
            /// </summary>
            public float X4;

            /// <summary>
            /// Y position of point 4.
            /// </summary>
            public float Y4;
        }
    }
}
