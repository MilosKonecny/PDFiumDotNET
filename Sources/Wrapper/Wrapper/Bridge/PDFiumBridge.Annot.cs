namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;
    using PDFiumDotNET.Wrapper.Exceptions;

    // Disable "Member 'xxxx' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsSupportedSubtype"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_IsSupportedSubtype FPDFAnnot_IsSupportedSubtypeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsSupportedSubtype"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_IsSupportedSubtype FPDFAnnot_IsSupportedSubtype => FPDFAnnot_IsSupportedSubtypeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFPage_CreateAnnot"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFPage_CreateAnnot FPDFPage_CreateAnnotStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFPage_CreateAnnot"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFPage_CreateAnnot FPDFPage_CreateAnnot => FPDFPage_CreateAnnotStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFPage_GetAnnotCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFPage_GetAnnotCount FPDFPage_GetAnnotCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFPage_GetAnnotCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFPage_GetAnnotCount FPDFPage_GetAnnotCount => FPDFPage_GetAnnotCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFPage_GetAnnot"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFPage_GetAnnot FPDFPage_GetAnnotStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFPage_GetAnnot"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFPage_GetAnnot FPDFPage_GetAnnot => FPDFPage_GetAnnotStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFPage_GetAnnotIndex"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFPage_GetAnnotIndex FPDFPage_GetAnnotIndexStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFPage_GetAnnotIndex"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFPage_GetAnnotIndex FPDFPage_GetAnnotIndex => FPDFPage_GetAnnotIndexStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFPage_CloseAnnot"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFPage_CloseAnnot FPDFPage_CloseAnnotStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFPage_CloseAnnot"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFPage_CloseAnnot FPDFPage_CloseAnnot => FPDFPage_CloseAnnotStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFPage_RemoveAnnot"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFPage_RemoveAnnot FPDFPage_RemoveAnnotStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFPage_RemoveAnnot"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFPage_RemoveAnnot FPDFPage_RemoveAnnot => FPDFPage_RemoveAnnotStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetSubtype"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetSubtype FPDFAnnot_GetSubtypeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetSubtype"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetSubtype FPDFAnnot_GetSubtype => FPDFAnnot_GetSubtypeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype FPDFAnnot_IsObjectSupportedSubtypeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype FPDFAnnot_IsObjectSupportedSubtype => FPDFAnnot_IsObjectSupportedSubtypeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_UpdateObject"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_UpdateObject FPDFAnnot_UpdateObjectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_UpdateObject"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_UpdateObject FPDFAnnot_UpdateObject => FPDFAnnot_UpdateObjectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_AddInkStroke"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_AddInkStroke FPDFAnnot_AddInkStrokeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_AddInkStroke"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_AddInkStroke FPDFAnnot_AddInkStroke => FPDFAnnot_AddInkStrokeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_RemoveInkList"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_RemoveInkList FPDFAnnot_RemoveInkListStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_RemoveInkList"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_RemoveInkList FPDFAnnot_RemoveInkList => FPDFAnnot_RemoveInkListStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_AppendObject"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_AppendObject FPDFAnnot_AppendObjectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_AppendObject"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_AppendObject FPDFAnnot_AppendObject => FPDFAnnot_AppendObjectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetObjectCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetObjectCount FPDFAnnot_GetObjectCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetObjectCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetObjectCount FPDFAnnot_GetObjectCount => FPDFAnnot_GetObjectCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetObject"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetObject FPDFAnnot_GetObjectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetObject"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetObject FPDFAnnot_GetObject => FPDFAnnot_GetObjectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_RemoveObject"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_RemoveObject FPDFAnnot_RemoveObjectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_RemoveObject"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_RemoveObject FPDFAnnot_RemoveObject => FPDFAnnot_RemoveObjectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetColor"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetColor FPDFAnnot_SetColorStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetColor"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetColor FPDFAnnot_SetColor => FPDFAnnot_SetColorStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetColor"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetColor FPDFAnnot_GetColorStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetColor"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetColor FPDFAnnot_GetColor => FPDFAnnot_GetColorStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_HasAttachmentPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_HasAttachmentPoints FPDFAnnot_HasAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_HasAttachmentPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_HasAttachmentPoints FPDFAnnot_HasAttachmentPoints => FPDFAnnot_HasAttachmentPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetAttachmentPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetAttachmentPoints FPDFAnnot_SetAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetAttachmentPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetAttachmentPoints FPDFAnnot_SetAttachmentPoints => FPDFAnnot_SetAttachmentPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints FPDFAnnot_AppendAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints FPDFAnnot_AppendAttachmentPoints => FPDFAnnot_AppendAttachmentPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_CountAttachmentPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_CountAttachmentPoints FPDFAnnot_CountAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_CountAttachmentPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_CountAttachmentPoints FPDFAnnot_CountAttachmentPoints => FPDFAnnot_CountAttachmentPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetAttachmentPoints"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetAttachmentPoints FPDFAnnot_GetAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetAttachmentPoints"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetAttachmentPoints FPDFAnnot_GetAttachmentPoints => FPDFAnnot_GetAttachmentPointsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetRect"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetRect FPDFAnnot_SetRectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetRect"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetRect FPDFAnnot_SetRect => FPDFAnnot_SetRectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetRect"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetRect FPDFAnnot_GetRectStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetRect"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetRect FPDFAnnot_GetRect => FPDFAnnot_GetRectStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetVertices"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetVertices FPDFAnnot_GetVerticesStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetVertices"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetVertices FPDFAnnot_GetVertices => FPDFAnnot_GetVerticesStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetInkListCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetInkListCount FPDFAnnot_GetInkListCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetInkListCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetInkListCount FPDFAnnot_GetInkListCount => FPDFAnnot_GetInkListCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetInkListPath"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetInkListPath FPDFAnnot_GetInkListPathStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetInkListPath"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetInkListPath FPDFAnnot_GetInkListPath => FPDFAnnot_GetInkListPathStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetLine"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetLine FPDFAnnot_GetLineStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetLine"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetLine FPDFAnnot_GetLine => FPDFAnnot_GetLineStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetBorder"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetBorder FPDFAnnot_SetBorderStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetBorder"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetBorder FPDFAnnot_SetBorder => FPDFAnnot_SetBorderStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetBorder"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetBorder FPDFAnnot_GetBorderStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetBorder"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetBorder FPDFAnnot_GetBorder => FPDFAnnot_GetBorderStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_HasKey"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_HasKey FPDFAnnot_HasKeyStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_HasKey"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_HasKey FPDFAnnot_HasKey => FPDFAnnot_HasKeyStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetValueType"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetValueType FPDFAnnot_GetValueTypeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetValueType"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetValueType FPDFAnnot_GetValueType => FPDFAnnot_GetValueTypeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetStringValue"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetStringValue FPDFAnnot_SetStringValueStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetStringValue"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetStringValue FPDFAnnot_SetStringValue => FPDFAnnot_SetStringValueStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetStringValue"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetStringValue FPDFAnnot_GetStringValueStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetStringValue"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetStringValue FPDFAnnot_GetStringValue => FPDFAnnot_GetStringValueStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetNumberValue"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetNumberValue FPDFAnnot_GetNumberValueStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetNumberValue"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetNumberValue FPDFAnnot_GetNumberValue => FPDFAnnot_GetNumberValueStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetAP"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetAP FPDFAnnot_SetAPStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetAP"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetAP FPDFAnnot_SetAP => FPDFAnnot_SetAPStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetAP"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetAP FPDFAnnot_GetAPStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetAP"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetAP FPDFAnnot_GetAP => FPDFAnnot_GetAPStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetLinkedAnnot"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetLinkedAnnot FPDFAnnot_GetLinkedAnnotStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetLinkedAnnot"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetLinkedAnnot FPDFAnnot_GetLinkedAnnot => FPDFAnnot_GetLinkedAnnotStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFlags"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFlags FPDFAnnot_GetFlagsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFlags"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFlags FPDFAnnot_GetFlags => FPDFAnnot_GetFlagsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetFlags"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetFlags FPDFAnnot_SetFlagsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetFlags"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetFlags FPDFAnnot_SetFlags => FPDFAnnot_SetFlagsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldFlags"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormFieldFlags FPDFAnnot_GetFormFieldFlagsStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldFlags"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormFieldFlags FPDFAnnot_GetFormFieldFlags => FPDFAnnot_GetFormFieldFlagsStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint FPDFAnnot_GetFormFieldAtPointStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint FPDFAnnot_GetFormFieldAtPoint => FPDFAnnot_GetFormFieldAtPointStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldName"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormFieldName FPDFAnnot_GetFormFieldNameStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldName"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormFieldName FPDFAnnot_GetFormFieldName => FPDFAnnot_GetFormFieldNameStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldType"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormFieldType FPDFAnnot_GetFormFieldTypeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldType"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormFieldType FPDFAnnot_GetFormFieldType => FPDFAnnot_GetFormFieldTypeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldValue"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormFieldValue FPDFAnnot_GetFormFieldValueStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldValue"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormFieldValue FPDFAnnot_GetFormFieldValue => FPDFAnnot_GetFormFieldValueStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetOptionCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetOptionCount FPDFAnnot_GetOptionCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetOptionCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetOptionCount FPDFAnnot_GetOptionCount => FPDFAnnot_GetOptionCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetOptionLabel"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetOptionLabel FPDFAnnot_GetOptionLabelStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetOptionLabel"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetOptionLabel FPDFAnnot_GetOptionLabel => FPDFAnnot_GetOptionLabelStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsOptionSelected"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_IsOptionSelected FPDFAnnot_IsOptionSelectedStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsOptionSelected"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_IsOptionSelected FPDFAnnot_IsOptionSelected => FPDFAnnot_IsOptionSelectedStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFontSize"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFontSize FPDFAnnot_GetFontSizeStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFontSize"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFontSize FPDFAnnot_GetFontSize => FPDFAnnot_GetFontSizeStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsChecked"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_IsChecked FPDFAnnot_IsCheckedStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_IsChecked"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_IsChecked FPDFAnnot_IsChecked => FPDFAnnot_IsCheckedStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes FPDFAnnot_SetFocusableSubtypesStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes FPDFAnnot_SetFocusableSubtypes => FPDFAnnot_SetFocusableSubtypesStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount FPDFAnnot_GetFocusableSubtypesCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount FPDFAnnot_GetFocusableSubtypesCount => FPDFAnnot_GetFocusableSubtypesCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes FPDFAnnot_GetFocusableSubtypesStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes FPDFAnnot_GetFocusableSubtypes => FPDFAnnot_GetFocusableSubtypesStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetLink"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetLink FPDFAnnot_GetLinkStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetLink"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetLink FPDFAnnot_GetLink => FPDFAnnot_GetLinkStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormControlCount"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormControlCount FPDFAnnot_GetFormControlCountStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormControlCount"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormControlCount FPDFAnnot_GetFormControlCount => FPDFAnnot_GetFormControlCountStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormControlIndex"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormControlIndex FPDFAnnot_GetFormControlIndexStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormControlIndex"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormControlIndex FPDFAnnot_GetFormControlIndex => FPDFAnnot_GetFormControlIndexStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue FPDFAnnot_GetFormFieldExportValueStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue FPDFAnnot_GetFormFieldExportValue => FPDFAnnot_GetFormFieldExportValueStatic;

        /// <summary>
        /// Gets or sets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetURI"/>.
        /// </summary>
        private static PDFiumDelegates.FPDFAnnot_SetURI FPDFAnnot_SetURIStatic { get; set; }

        /// <summary>
        /// Gets the loaded method <see cref="PDFiumDelegates.FPDFAnnot_SetURI"/>.
        /// </summary>
        internal PDFiumDelegates.FPDFAnnot_SetURI FPDFAnnot_SetURI => FPDFAnnot_SetURIStatic;

        private static void LoadDllAnnotPart(string libraryName)
        {
            LoadDllAnnotPart1(libraryName);
            LoadDllAnnotPart2(libraryName);
            LoadDllAnnotPart3(libraryName);
            LoadDllAnnotPart4(libraryName);
        }

        private static void LoadDllAnnotPart1(string libraryName)
        {
            // FPDFAnnot_IsSupportedSubtype
            var functionName = nameof(PDFiumDelegates.FPDFAnnot_IsSupportedSubtype);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_IsSupportedSubtypeStatic = (PDFiumDelegates.FPDFAnnot_IsSupportedSubtype)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_IsSupportedSubtype));

            // FPDFPage_CreateAnnot
            functionName = nameof(PDFiumDelegates.FPDFPage_CreateAnnot);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFPage_CreateAnnotStatic = (PDFiumDelegates.FPDFPage_CreateAnnot)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFPage_CreateAnnot));

            // FPDFPage_GetAnnotCount
            functionName = nameof(PDFiumDelegates.FPDFPage_GetAnnotCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFPage_GetAnnotCountStatic = (PDFiumDelegates.FPDFPage_GetAnnotCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFPage_GetAnnotCount));

            // FPDFPage_GetAnnot
            functionName = nameof(PDFiumDelegates.FPDFPage_GetAnnot);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFPage_GetAnnotStatic = (PDFiumDelegates.FPDFPage_GetAnnot)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFPage_GetAnnot));

            // FPDFPage_GetAnnotIndex
            functionName = nameof(PDFiumDelegates.FPDFPage_GetAnnotIndex);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFPage_GetAnnotIndexStatic = (PDFiumDelegates.FPDFPage_GetAnnotIndex)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFPage_GetAnnotIndex));

            // FPDFPage_CloseAnnot
            functionName = nameof(PDFiumDelegates.FPDFPage_CloseAnnot);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFPage_CloseAnnotStatic = (PDFiumDelegates.FPDFPage_CloseAnnot)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFPage_CloseAnnot));

            // FPDFPage_RemoveAnnot
            functionName = nameof(PDFiumDelegates.FPDFPage_RemoveAnnot);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFPage_RemoveAnnotStatic = (PDFiumDelegates.FPDFPage_RemoveAnnot)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFPage_RemoveAnnot));

            // FPDFAnnot_GetSubtype
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetSubtype);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetSubtypeStatic = (PDFiumDelegates.FPDFAnnot_GetSubtype)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetSubtype));

            // FPDFAnnot_IsObjectSupportedSubtype
            functionName = nameof(PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_IsObjectSupportedSubtypeStatic = (PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_IsObjectSupportedSubtype));

            // FPDFAnnot_UpdateObject
            functionName = nameof(PDFiumDelegates.FPDFAnnot_UpdateObject);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_UpdateObjectStatic = (PDFiumDelegates.FPDFAnnot_UpdateObject)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_UpdateObject));

            // FPDFAnnot_AddInkStroke
            functionName = nameof(PDFiumDelegates.FPDFAnnot_AddInkStroke);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_AddInkStrokeStatic = (PDFiumDelegates.FPDFAnnot_AddInkStroke)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_AddInkStroke));

            // FPDFAnnot_RemoveInkList
            functionName = nameof(PDFiumDelegates.FPDFAnnot_RemoveInkList);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_RemoveInkListStatic = (PDFiumDelegates.FPDFAnnot_RemoveInkList)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_RemoveInkList));

            // FPDFAnnot_AppendObject
            functionName = nameof(PDFiumDelegates.FPDFAnnot_AppendObject);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_AppendObjectStatic = (PDFiumDelegates.FPDFAnnot_AppendObject)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_AppendObject));

            // FPDFAnnot_GetObjectCount
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetObjectCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetObjectCountStatic = (PDFiumDelegates.FPDFAnnot_GetObjectCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetObjectCount));

            // FPDFAnnot_GetObject
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetObject);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetObjectStatic = (PDFiumDelegates.FPDFAnnot_GetObject)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetObject));
        }

        private static void LoadDllAnnotPart2(string libraryName)
        {
            // FPDFAnnot_RemoveObject
            var functionName = nameof(PDFiumDelegates.FPDFAnnot_RemoveObject);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_RemoveObjectStatic = (PDFiumDelegates.FPDFAnnot_RemoveObject)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_RemoveObject));

            // FPDFAnnot_SetColor
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetColor);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetColorStatic = (PDFiumDelegates.FPDFAnnot_SetColor)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetColor));

            // FPDFAnnot_GetColor
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetColor);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetColorStatic = (PDFiumDelegates.FPDFAnnot_GetColor)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetColor));

            // FPDFAnnot_HasAttachmentPoints
            functionName = nameof(PDFiumDelegates.FPDFAnnot_HasAttachmentPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_HasAttachmentPointsStatic = (PDFiumDelegates.FPDFAnnot_HasAttachmentPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_HasAttachmentPoints));

            // FPDFAnnot_SetAttachmentPoints
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetAttachmentPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetAttachmentPointsStatic = (PDFiumDelegates.FPDFAnnot_SetAttachmentPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetAttachmentPoints));

            // FPDFAnnot_AppendAttachmentPoints
            functionName = nameof(PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_AppendAttachmentPointsStatic = (PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_AppendAttachmentPoints));

            // FPDFAnnot_CountAttachmentPoints
            functionName = nameof(PDFiumDelegates.FPDFAnnot_CountAttachmentPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_CountAttachmentPointsStatic = (PDFiumDelegates.FPDFAnnot_CountAttachmentPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_CountAttachmentPoints));

            // FPDFAnnot_GetAttachmentPoints
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetAttachmentPoints);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetAttachmentPointsStatic = (PDFiumDelegates.FPDFAnnot_GetAttachmentPoints)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetAttachmentPoints));

            // FPDFAnnot_SetRect
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetRect);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetRectStatic = (PDFiumDelegates.FPDFAnnot_SetRect)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetRect));

            // FPDFAnnot_GetRect
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetRect);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetRectStatic = (PDFiumDelegates.FPDFAnnot_GetRect)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetRect));

            // FPDFAnnot_GetVertices
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetVertices);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetVerticesStatic = (PDFiumDelegates.FPDFAnnot_GetVertices)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetVertices));

            // FPDFAnnot_GetInkListCount
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetInkListCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetInkListCountStatic = (PDFiumDelegates.FPDFAnnot_GetInkListCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetInkListCount));

            // FPDFAnnot_GetInkListPath
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetInkListPath);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetInkListPathStatic = (PDFiumDelegates.FPDFAnnot_GetInkListPath)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetInkListPath));

            // FPDFAnnot_GetLine
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetLine);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetLineStatic = (PDFiumDelegates.FPDFAnnot_GetLine)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetLine));

            // FPDFAnnot_SetBorder
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetBorder);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetBorderStatic = (PDFiumDelegates.FPDFAnnot_SetBorder)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetBorder));
        }

        private static void LoadDllAnnotPart3(string libraryName)
        {
            // FPDFAnnot_GetBorder
            var functionName = nameof(PDFiumDelegates.FPDFAnnot_GetBorder);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetBorderStatic = (PDFiumDelegates.FPDFAnnot_GetBorder)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetBorder));

            // FPDFAnnot_HasKey
            functionName = nameof(PDFiumDelegates.FPDFAnnot_HasKey);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_HasKeyStatic = (PDFiumDelegates.FPDFAnnot_HasKey)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_HasKey));

            // FPDFAnnot_GetValueType
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetValueType);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetValueTypeStatic = (PDFiumDelegates.FPDFAnnot_GetValueType)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetValueType));

            // FPDFAnnot_SetStringValue
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetStringValue);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetStringValueStatic = (PDFiumDelegates.FPDFAnnot_SetStringValue)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetStringValue));

            // FPDFAnnot_GetStringValue
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetStringValue);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetStringValueStatic = (PDFiumDelegates.FPDFAnnot_GetStringValue)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetStringValue));

            // FPDFAnnot_GetNumberValue
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetNumberValue);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetNumberValueStatic = (PDFiumDelegates.FPDFAnnot_GetNumberValue)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetNumberValue));

            // FPDFAnnot_SetAP
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetAP);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetAPStatic = (PDFiumDelegates.FPDFAnnot_SetAP)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetAP));

            // FPDFAnnot_GetAP
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetAP);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetAPStatic = (PDFiumDelegates.FPDFAnnot_GetAP)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetAP));

            // FPDFAnnot_GetLinkedAnnot
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetLinkedAnnot);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetLinkedAnnotStatic = (PDFiumDelegates.FPDFAnnot_GetLinkedAnnot)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetLinkedAnnot));

            // FPDFAnnot_GetFlags
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFlags);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFlagsStatic = (PDFiumDelegates.FPDFAnnot_GetFlags)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFlags));

            // FPDFAnnot_SetFlags
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetFlags);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetFlagsStatic = (PDFiumDelegates.FPDFAnnot_SetFlags)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetFlags));

            // FPDFAnnot_GetFormFieldFlags
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormFieldFlags);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormFieldFlagsStatic = (PDFiumDelegates.FPDFAnnot_GetFormFieldFlags)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormFieldFlags));

            // FPDFAnnot_GetFormFieldAtPoint
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormFieldAtPointStatic = (PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormFieldAtPoint));

            // FPDFAnnot_GetFormFieldName
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormFieldName);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormFieldNameStatic = (PDFiumDelegates.FPDFAnnot_GetFormFieldName)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormFieldName));

            // FPDFAnnot_GetFormFieldType
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormFieldType);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormFieldTypeStatic = (PDFiumDelegates.FPDFAnnot_GetFormFieldType)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormFieldType));
        }

        private static void LoadDllAnnotPart4(string libraryName)
        {
            // FPDFAnnot_GetFormFieldValue
            var functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormFieldValue);
            var address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormFieldValueStatic = (PDFiumDelegates.FPDFAnnot_GetFormFieldValue)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormFieldValue));

            // FPDFAnnot_GetOptionCount
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetOptionCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetOptionCountStatic = (PDFiumDelegates.FPDFAnnot_GetOptionCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetOptionCount));

            // FPDFAnnot_GetOptionLabel
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetOptionLabel);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetOptionLabelStatic = (PDFiumDelegates.FPDFAnnot_GetOptionLabel)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetOptionLabel));

            // FPDFAnnot_IsOptionSelected
            functionName = nameof(PDFiumDelegates.FPDFAnnot_IsOptionSelected);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_IsOptionSelectedStatic = (PDFiumDelegates.FPDFAnnot_IsOptionSelected)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_IsOptionSelected));

            // FPDFAnnot_GetFontSize
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFontSize);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFontSizeStatic = (PDFiumDelegates.FPDFAnnot_GetFontSize)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFontSize));

            // FPDFAnnot_IsChecked
            functionName = nameof(PDFiumDelegates.FPDFAnnot_IsChecked);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_IsCheckedStatic = (PDFiumDelegates.FPDFAnnot_IsChecked)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_IsChecked));

            // FPDFAnnot_SetFocusableSubtypes
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetFocusableSubtypesStatic = (PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetFocusableSubtypes));

            // FPDFAnnot_GetFocusableSubtypesCount
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFocusableSubtypesCountStatic = (PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFocusableSubtypesCount));

            // FPDFAnnot_GetFocusableSubtypes
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFocusableSubtypesStatic = (PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFocusableSubtypes));

            // FPDFAnnot_GetLink
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetLink);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetLinkStatic = (PDFiumDelegates.FPDFAnnot_GetLink)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetLink));

            // FPDFAnnot_GetFormControlCount
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormControlCount);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormControlCountStatic = (PDFiumDelegates.FPDFAnnot_GetFormControlCount)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormControlCount));

            // FPDFAnnot_GetFormControlIndex
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormControlIndex);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormControlIndexStatic = (PDFiumDelegates.FPDFAnnot_GetFormControlIndex)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormControlIndex));

            // FPDFAnnot_GetFormFieldExportValue
            functionName = nameof(PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_GetFormFieldExportValueStatic = (PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_GetFormFieldExportValue));

            // FPDFAnnot_SetURI
            functionName = nameof(PDFiumDelegates.FPDFAnnot_SetURI);
            address = NativeMethods.GetProcAddressAnsi(_libraryHandle, functionName);
            if (address == IntPtr.Zero)
            {
                throw PDFiumFunctionNotFoundException.CreateException(libraryName, functionName, Marshal.GetLastWin32Error());
            }

            FPDFAnnot_SetURIStatic = (PDFiumDelegates.FPDFAnnot_SetURI)Marshal.GetDelegateForFunctionPointer(address, typeof(PDFiumDelegates.FPDFAnnot_SetURI));
        }
    }
}
