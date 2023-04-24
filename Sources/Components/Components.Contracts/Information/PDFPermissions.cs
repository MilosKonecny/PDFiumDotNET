namespace PDFiumDotNET.Components.Contracts.Information
{
    using System;

    /// <summary>
    /// Enumeration defines all possible PDF document permissions.
    /// </summary>
    [Flags]
    public enum PDFPermissions
    {
        /// <summary>
        /// No permission is available.
        /// </summary>
        None = 0,

        /// <summary>
        /// Permission bit 1 - dummy.
        /// </summary>
        Bit1 = 0x01,

        /// <summary>
        /// Permission bit 2 - dummy.
        /// </summary>
        Bit2 = 0x02,

        /// <summary>
        /// (Revision 2) Print the document.
        /// (Revision 3 or greater) Print the document (possibly not at the highest quality level, depending on whether bit 12 is also set).
        /// (Bit 3)
        /// </summary>
        PrintDocument = 0b100,

        /// <summary>
        /// Modify the contents of the document by operations other than those controlled by bits 6, 9 and 11.
        /// (Bit 4)
        /// </summary>
        ModifyContents = 0b1000,

        /// <summary>
        /// (Revision 2) Copy or otherwise extract text and graphics from the document, including extracting text and graphics
        /// (in support of accessibility to users with disabilities or for other purposes).
        /// (Revision 3 or greater) Copy or otherwise extract text and graphics from the document
        /// by operations other than that controlled by bit 10.
        /// (Bit 5)
        /// </summary>
        ExtractTextAndGraphics = 0b10000,

        /// <summary>
        /// Add or modify text annotations, fill in interactive form fields, and, if bit 4 is also set,
        /// create or modify interactive from fields (including signature fields).
        /// (Bit 6)
        /// </summary>
        AddOrModifyTextAnnotations = 0b100000,

        /// <summary>
        /// Permission bit 7 - dummy.
        /// </summary>
        Bit7 = 0x40,

        /// <summary>
        /// Permission bit 8 - dummy.
        /// </summary>
        Bit8 = 0x80,

        /// <summary>
        /// (Revision 3 or greater) Fill in existing interactive form fields (including signature fields),
        /// even if bit 6 is clear.
        /// (Bit 9)
        /// </summary>
        FillFormFields = 0b100000000,

        /// <summary>
        /// (Revision 3 or greater) Extract text and graphics
        /// (in support of accessibility to users with disabilities or for other purposes).
        /// (Bit 10)
        /// </summary>
        ExtractTextAndGraphicsDisabilities = 0b1000000000,

        /// <summary>
        /// (Revision 3 or greater) Assemble the document (insert, rotate, or delete pages and create bookmarks or thumbnail images),
        /// even if bit 4 is clear.
        /// (Bit 11)
        /// </summary>
        AssembleDocument = 0b10000000000,

        /// <summary>
        /// (Revision 3 or greater) Print the document to a representation from which a faithful digital copy
        /// of the PDF content could be generated.
        /// When this bit is clear (and bit 3 is set), printing is limited to a low-level representation
        /// of the appearance, possibly of degraded quality.
        /// (Bit 12)
        /// </summary>
        PrintDocumentHighQuality = 0b100000000000,

        /// <summary>
        /// Mask for all flags.
        /// </summary>
        AllFlags = 0xFFF,
    }
}
