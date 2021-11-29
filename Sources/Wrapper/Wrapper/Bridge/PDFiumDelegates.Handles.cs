namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;

    /// <summary>
    /// The class contains all delegates of methods in pdfium dll.
    /// </summary>
    internal partial class PDFiumDelegates
    {
        /// <summary>
        /// Document handle struct.
        /// </summary>
        internal struct FPDF_DOCUMENT
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_DOCUMENT InvalidHandle => new FPDF_DOCUMENT();

            /// <summary>
            /// Document handle.
            /// </summary>
            private readonly IntPtr _document;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_DOCUMENT"/> struct.
            /// </summary>
            /// <param name="document">Document handle to use.</param>
            public FPDF_DOCUMENT(IntPtr document)
            {
                _document = document;
            }

            /// <summary>
            /// Gets a value indicating whether the document handle is valid.
            /// </summary>
            public bool IsValid => _document != IntPtr.Zero;
        }

        /// <summary>
        /// Page handle struct.
        /// </summary>
        internal struct FPDF_PAGE
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_PAGE InvalidHandle => new FPDF_PAGE();

            /// <summary>
            /// Page handle.
            /// </summary>
            private readonly IntPtr _page;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_PAGE"/> struct.
            /// </summary>
            /// <param name="page">Page handle to use.</param>
            public FPDF_PAGE(IntPtr page)
            {
                _page = page;
            }

            /// <summary>
            /// Gets a value indicating whether the page handle is valid.
            /// </summary>
            public bool IsValid => _page != IntPtr.Zero;
        }

        /// <summary>
        /// Bitmap handle struct.
        /// </summary>
        internal struct FPDF_BITMAP
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_BITMAP InvalidHandle => new FPDF_BITMAP();

            /// <summary>
            /// Bitmap handle.
            /// </summary>
            private readonly IntPtr _bitmap;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_BITMAP"/> struct.
            /// </summary>
            /// <param name="bitmap">Bitmap handle to use.</param>
            public FPDF_BITMAP(IntPtr bitmap)
            {
                _bitmap = bitmap;
            }

            /// <summary>
            /// Gets a value indicating whether the bitmap handle is valid.
            /// </summary>
            public bool IsValid => _bitmap != IntPtr.Zero;
        }

        /// <summary>
        /// Page range handle struct.
        /// </summary>
        internal struct FPDF_PAGERANGE
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_PAGERANGE InvalidHandle => new FPDF_PAGERANGE();

            /// <summary>
            /// Page range handle.
            /// </summary>
            private readonly IntPtr _pageRange;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_PAGERANGE"/> struct.
            /// </summary>
            /// <param name="pageRange">Page range handle to use.</param>
            public FPDF_PAGERANGE(IntPtr pageRange)
            {
                _pageRange = pageRange;
            }

            /// <summary>
            /// Gets a value indicating whether the page range handle is valid.
            /// </summary>
            public bool IsValid => _pageRange != IntPtr.Zero;
        }

        /// <summary>
        /// Destination handle struct.
        /// </summary>
        internal struct FPDF_DEST
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_DEST InvalidHandle => new FPDF_DEST();

            /// <summary>
            /// Destination handle.
            /// </summary>
            private readonly IntPtr _dest;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_DEST"/> struct.
            /// </summary>
            /// <param name="dest">Destination handle to use.</param>
            public FPDF_DEST(IntPtr dest)
            {
                _dest = dest;
            }

            /// <summary>
            /// Gets a value indicating whether the destination handle is valid.
            /// </summary>
            public bool IsValid => _dest != IntPtr.Zero;
        }

        /// <summary>
        /// Bookmark handle struct.
        /// </summary>
        internal struct FPDF_BOOKMARK
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_BOOKMARK InvalidHandle => new FPDF_BOOKMARK();

            /// <summary>
            /// Bookmark handle.
            /// </summary>
            private readonly IntPtr _bookmark;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_BOOKMARK"/> struct.
            /// </summary>
            /// <param name="bookmark">Bookmark handle to use.</param>
            public FPDF_BOOKMARK(IntPtr bookmark)
            {
                _bookmark = bookmark;
            }

            /// <summary>
            /// Gets a value indicating whether the bookmark handle is valid.
            /// </summary>
            public bool IsValid => _bookmark != IntPtr.Zero;
        }

        /// <summary>
        /// Action handle struct.
        /// </summary>
        internal struct FPDF_ACTION
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_ACTION InvalidHandle => new FPDF_ACTION();

            /// <summary>
            /// Action handle.
            /// </summary>
            private readonly IntPtr _action;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_ACTION"/> struct.
            /// </summary>
            /// <param name="action">Action handle to use.</param>
            public FPDF_ACTION(IntPtr action)
            {
                _action = action;
            }

            /// <summary>
            /// Gets a value indicating whether the action handle is valid.
            /// </summary>
            public bool IsValid => _action != IntPtr.Zero;
        }

        /// <summary>
        /// Link handle struct.
        /// </summary>
        internal struct FPDF_LINK
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_LINK InvalidHandle => new FPDF_LINK();

            /// <summary>
            /// Link handle.
            /// </summary>
            private readonly IntPtr _link;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_LINK"/> struct.
            /// </summary>
            /// <param name="link">Link handle to use.</param>
            public FPDF_LINK(IntPtr link)
            {
                _link = link;
            }

            /// <summary>
            /// Gets a value indicating whether the link handle is valid.
            /// </summary>
            public bool IsValid => _link != IntPtr.Zero;
        }

        /// <summary>
        /// Annotation handle struct.
        /// </summary>
        internal struct FPDF_ANNOTATION
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_ANNOTATION InvalidHandle => new FPDF_ANNOTATION();

            /// <summary>
            /// Annotation handle.
            /// </summary>
            private readonly IntPtr _annotation;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_ANNOTATION"/> struct.
            /// </summary>
            /// <param name="annotation">Annotation handle to use.</param>
            public FPDF_ANNOTATION(IntPtr annotation)
            {
                _annotation = annotation;
            }

            /// <summary>
            /// Gets a value indicating whether the annotation handle is valid.
            /// </summary>
            public bool IsValid => _annotation != IntPtr.Zero;
        }

        /// <summary>
        /// Text page handle struct.
        /// </summary>
        internal struct FPDF_TEXTPAGE
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_TEXTPAGE InvalidHandle => new FPDF_TEXTPAGE();

            /// <summary>
            /// Annotation handle.
            /// </summary>
            private readonly IntPtr _textPage;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_TEXTPAGE"/> struct.
            /// </summary>
            /// <param name="textPage">Text page handle to use.</param>
            public FPDF_TEXTPAGE(IntPtr textPage)
            {
                _textPage = textPage;
            }

            /// <summary>
            /// Gets a value indicating whether the annotation handle is valid.
            /// </summary>
            public bool IsValid => _textPage != IntPtr.Zero;
        }

        /// <summary>
        /// Search context handle struct.
        /// </summary>
        internal struct FPDF_SCHHANDLE
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_SCHHANDLE InvalidHandle => new FPDF_SCHHANDLE();

            /// <summary>
            /// Search context handle.
            /// </summary>
            private readonly IntPtr _schHandle;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_SCHHANDLE"/> struct.
            /// </summary>
            /// <param name="schHandle">Search context handle to use.</param>
            public FPDF_SCHHANDLE(IntPtr schHandle)
            {
                _schHandle = schHandle;
            }

            /// <summary>
            /// Gets a value indicating whether the search context handle is valid.
            /// </summary>
            public bool IsValid => _schHandle != IntPtr.Zero;
        }

        /// <summary>
        /// Web link in page handle struct.
        /// </summary>
        internal struct FPDF_PAGELINK
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_PAGELINK InvalidHandle => new FPDF_PAGELINK();

            /// <summary>
            /// Web link in page handle.
            /// </summary>
            private readonly IntPtr _webLinkHandle;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_PAGELINK"/> struct.
            /// </summary>
            /// <param name="webLinkHandle">Web link in page handle to use.</param>
            public FPDF_PAGELINK(IntPtr webLinkHandle)
            {
                _webLinkHandle = webLinkHandle;
            }

            /// <summary>
            /// Gets a value indicating whether the web link in page handle is valid.
            /// </summary>
            public bool IsValid => _webLinkHandle != IntPtr.Zero;
        }

        /// <summary>
        /// Page object handle struct.
        /// </summary>
        internal struct FPDF_PAGEOBJECT
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_PAGEOBJECT InvalidHandle => new FPDF_PAGEOBJECT();

            /// <summary>
            /// Page object handle.
            /// </summary>
            private readonly IntPtr _pageObjectHandle;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_PAGEOBJECT"/> struct.
            /// </summary>
            /// <param name="pageObjectHandle">Page object handle to use.</param>
            public FPDF_PAGEOBJECT(IntPtr pageObjectHandle)
            {
                _pageObjectHandle = pageObjectHandle;
            }

            /// <summary>
            /// Gets a value indicating whether the page link handle is valid.
            /// </summary>
            public bool IsValid => _pageObjectHandle != IntPtr.Zero;
        }

        /// <summary>
        /// Form handle struct.
        /// </summary>
        internal struct FPDF_FORMHANDLE
        {
            /// <summary>
            /// Gets invalid handle.
            /// </summary>
            public static FPDF_FORMHANDLE InvalidHandle => new FPDF_FORMHANDLE();

            /// <summary>
            /// Form handle.
            /// </summary>
            private readonly IntPtr _formHandle;

            /// <summary>
            /// Initializes a new instance of the <see cref="FPDF_FORMHANDLE"/> struct.
            /// </summary>
            /// <param name="formHandle">Form handle to use.</param>
            public FPDF_FORMHANDLE(IntPtr formHandle)
            {
                _formHandle = formHandle;
            }

            /// <summary>
            /// Gets a value indicating whether the page link handle is valid.
            /// </summary>
            public bool IsValid => _formHandle != IntPtr.Zero;
        }
    }
}
