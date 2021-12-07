namespace PDFiumDotNET.Wrapper.Bridge
{
    using System;
    using System.Runtime.InteropServices;

    // Disable "Member 'xxxx' does not access instance data and can be marked as static."
#pragma warning disable CA1822

    /// <summary>
    /// The class contains all pdfium methods currently supported in this project.
    /// </summary>
    internal sealed partial class PDFiumBridge
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_IsSupportedSubtype_Delegate(FPDF_ANNOTATION_SUBTYPE subtype);

        private static FPDFAnnot_IsSupportedSubtype_Delegate FPDFAnnot_IsSupportedSubtypeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Check if an annotation subtype is currently supported for creation.
        /// Currently supported subtypes:
        ///     - circle
        ///     - freetext
        ///     - highlight
        ///     - ink
        ///     - link
        ///     - popup
        ///     - square,
        ///     - squiggly
        ///     - stamp
        ///     - strikeout
        ///     - text
        ///     - underline
        ///     .
        /// </summary>
        /// <param name="subtype">the subtype to be checked.</param>
        /// <returns>Returns true if this subtype supported.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_IsSupportedSubtype(FPDF_ANNOTATION_SUBTYPE subtype);.
        /// </remarks>
        public bool FPDFAnnot_IsSupportedSubtype(FPDF_ANNOTATION_SUBTYPE subtype)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_IsSupportedSubtypeStatic(subtype);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_ANNOTATION FPDFPage_CreateAnnot_Delegate(FPDF_PAGE page, FPDF_ANNOTATION_SUBTYPE subtype);

        private static FPDFPage_CreateAnnot_Delegate FPDFPage_CreateAnnotStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Create an annotation in |page| of the subtype |subtype|. If the specified subtype is illegal or unsupported,
        /// then a new annotation will not be created.
        /// Must call FPDFPage_CloseAnnot() when the annotation returned by this function is no longer needed.
        /// </summary>
        /// <param name="page">Handle to a page.</param>
        /// <param name="subtype">The subtype of the new annotation.</param>
        /// <returns>Returns a handle to the new annotation object, or NULL on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_ANNOTATION FPDF_CALLCONV FPDFPage_CreateAnnot(FPDF_PAGE page, FPDF_ANNOTATION_SUBTYPE subtype);.
        /// </remarks>
        public FPDF_ANNOTATION FPDFPage_CreateAnnot(FPDF_PAGE page, FPDF_ANNOTATION_SUBTYPE subtype)
        {
            lock (_syncObject)
            {
                return FPDFPage_CreateAnnotStatic(page, subtype);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFPage_GetAnnotCount_Delegate(FPDF_PAGE page);

        private static FPDFPage_GetAnnotCount_Delegate FPDFPage_GetAnnotCountStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the number of annotations in |page|.
        /// </summary>
        /// <param name="page">Handle to a page.</param>
        /// <returns>Returns the number of annotations in |page|.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFPage_GetAnnotCount(FPDF_PAGE page);.
        /// </remarks>
        public int FPDFPage_GetAnnotCount(FPDF_PAGE page)
        {
            lock (_syncObject)
            {
                return FPDFPage_GetAnnotCountStatic(page);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_ANNOTATION FPDFPage_GetAnnot_Delegate(FPDF_PAGE page, int index);

        private static FPDFPage_GetAnnot_Delegate FPDFPage_GetAnnotStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get annotation in |page| at |index|. Must call FPDFPage_CloseAnnot() when the annotation returned by this function is no longer needed.
        /// </summary>
        /// <param name="page">Handle to a page.</param>
        /// <param name="index">The index of the annotation.</param>
        /// <returns>Returns a handle to the annotation object, or NULL on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_ANNOTATION FPDF_CALLCONV FPDFPage_GetAnnot(FPDF_PAGE page, int index);.
        /// </remarks>
        public FPDF_ANNOTATION FPDFPage_GetAnnot(FPDF_PAGE page, int index)
        {
            lock (_syncObject)
            {
                return FPDFPage_GetAnnotStatic(page, index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFPage_GetAnnotIndex_Delegate(FPDF_PAGE page, FPDF_ANNOTATION annot);

        private static FPDFPage_GetAnnotIndex_Delegate FPDFPage_GetAnnotIndexStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the index of |annot| in |page|. This is the opposite of FPDFPage_GetAnnot().
        /// </summary>
        /// <param name="page">Handle to the page that the annotation is on.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns the index of |annot|, or -1 on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFPage_GetAnnotIndex(FPDF_PAGE page, FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFPage_GetAnnotIndex(FPDF_PAGE page, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFPage_GetAnnotIndexStatic(page, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void FPDFPage_CloseAnnot_Delegate(FPDF_ANNOTATION annot);

        private static FPDFPage_CloseAnnot_Delegate FPDFPage_CloseAnnotStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Close an annotation. Must be called when the annotation returned by FPDFPage_CreateAnnot() or FPDFPage_GetAnnot() is no longer needed.
        /// This function does not remove the annotation from the document.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <remarks>
        /// FPDF_EXPORT void FPDF_CALLCONV FPDFPage_CloseAnnot(FPDF_ANNOTATION annot);.
        /// </remarks>
        public void FPDFPage_CloseAnnot(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                FPDFPage_CloseAnnotStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFPage_RemoveAnnot_Delegate(FPDF_PAGE page, int index);

        private static FPDFPage_RemoveAnnot_Delegate FPDFPage_RemoveAnnotStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Remove the annotation in |page| at |index|.
        /// </summary>
        /// <param name="page">Handle to a page.</param>
        /// <param name="index">The index of the annotation.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFPage_RemoveAnnot(FPDF_PAGE page, int index);.
        /// </remarks>
        public bool FPDFPage_RemoveAnnot(FPDF_PAGE page, int index)
        {
            lock (_syncObject)
            {
                return FPDFPage_RemoveAnnotStatic(page, index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_ANNOTATION_SUBTYPE FPDFAnnot_GetSubtype_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetSubtype_Delegate FPDFAnnot_GetSubtypeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the subtype of an annotation.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns the annotation subtype.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_ANNOTATION_SUBTYPE FPDF_CALLCONV FPDFAnnot_GetSubtype(FPDF_ANNOTATION annot);.
        /// </remarks>
        public FPDF_ANNOTATION_SUBTYPE FPDFAnnot_GetSubtype(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetSubtypeStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_IsObjectSupportedSubtype_Delegate(FPDF_ANNOTATION_SUBTYPE subtype);

        private static FPDFAnnot_IsObjectSupportedSubtype_Delegate FPDFAnnot_IsObjectSupportedSubtypeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Check if an annotation subtype is currently supported for object extraction, update, and removal.
        /// Currently supported subtypes: ink and stamp.
        /// </summary>
        /// <param name="subtype">The subtype to be checked.</param>
        /// <returns>Returns true if this subtype supported.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_IsObjectSupportedSubtype(FPDF_ANNOTATION_SUBTYPE subtype);.
        /// </remarks>
        public bool FPDFAnnot_IsObjectSupportedSubtype(FPDF_ANNOTATION_SUBTYPE subtype)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_IsObjectSupportedSubtypeStatic(subtype);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_UpdateObject_Delegate(FPDF_ANNOTATION annot, FPDF_PAGEOBJECT obj);

        private static FPDFAnnot_UpdateObject_Delegate FPDFAnnot_UpdateObjectStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Update |obj| in |annot|. |obj| must be in |annot| already and must have been retrieved by FPDFAnnot_GetObject().
        /// Currently, only ink and stamp annotations are supported by this API.
        /// Also note that only path, image, and text objects have APIs for modification; see FPDFPath_*(), FPDFText_*(), and FPDFImageObj_*().
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="obj">Handle to the object that |annot| needs to update.</param>
        /// <returns>Return true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_UpdateObject(FPDF_ANNOTATION annot, FPDF_PAGEOBJECT obj);.
        /// </remarks>
        public bool FPDFAnnot_UpdateObject(FPDF_ANNOTATION annot, FPDF_PAGEOBJECT obj)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_UpdateObjectStatic(annot, obj);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_AddInkStroke_Delegate(FPDF_ANNOTATION annot, ref FS_POINTF points, ulong point_count);

        private static FPDFAnnot_AddInkStroke_Delegate FPDFAnnot_AddInkStrokeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Add a new InkStroke, represented by an array of points, to the InkList of |annot|.
        /// The API creates an InkList if one doesn't already exist in |annot|.
        /// This API works only for ink annotations. Please refer to ISO 32000-1:2008 spec, section 12.5.6.13.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="points">Pointer to a FS_POINTF array representing input points.</param>
        /// <param name="point_count">Number of elements in |points| array. This should not exceed the maximum value that can be represented by an int32_t).</param>
        /// <returns>Returns the 0-based index at which the new InkStroke is added in the InkList of the |annot|. Returns -1 on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_AddInkStroke(FPDF_ANNOTATION annot, const FS_POINTF* points, size_t point_count);.
        /// </remarks>
        public int FPDFAnnot_AddInkStroke(FPDF_ANNOTATION annot, ref FS_POINTF points, ulong point_count)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_AddInkStrokeStatic(annot, ref points, point_count);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_RemoveInkList_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_RemoveInkList_Delegate FPDFAnnot_RemoveInkListStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Removes an InkList in |annot|. This API works only for ink annotations.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Return true on successful removal of /InkList entry from context of the non-null ink |annot|. Returns false on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_RemoveInkList(FPDF_ANNOTATION annot);.
        /// </remarks>
        public bool FPDFAnnot_RemoveInkList(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_RemoveInkListStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_AppendObject_Delegate(FPDF_ANNOTATION annot, FPDF_PAGEOBJECT obj);

        private static FPDFAnnot_AppendObject_Delegate FPDFAnnot_AppendObjectStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Add |obj| to |annot|. |obj| must have been created by FPDFPageObj_CreateNew{Path|Rect}() or FPDFPageObj_New{Text|Image}Obj(),
        /// and will be owned by |annot|. Note that an |obj| cannot belong to more than one |annot|.
        /// Currently, only ink and stamp annotations are supported by this API. Also note that only path, image, and text objects have APIs for creation.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="obj">Handle to the object that is to be added to |annot|.</param>
        /// <returns>Return true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_AppendObject(FPDF_ANNOTATION annot, FPDF_PAGEOBJECT obj);.
        /// </remarks>
        public bool FPDFAnnot_AppendObject(FPDF_ANNOTATION annot, FPDF_PAGEOBJECT obj)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_AppendObjectStatic(annot, obj);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetObjectCount_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetObjectCount_Delegate FPDFAnnot_GetObjectCountStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the total number of objects in |annot|, including path objects, text objects, external objects, image objects, and shading objects.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns the number of objects in |annot|.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetObjectCount(FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetObjectCount(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetObjectCountStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_PAGEOBJECT FPDFAnnot_GetObject_Delegate(FPDF_ANNOTATION annot, int index);

        private static FPDFAnnot_GetObject_Delegate FPDFAnnot_GetObjectStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the object in |annot| at |index|.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="index">The index of the object.</param>
        /// <returns>Return a handle to the object, or NULL on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_PAGEOBJECT FPDF_CALLCONV FPDFAnnot_GetObject(FPDF_ANNOTATION annot, int index);.
        /// </remarks>
        public FPDF_PAGEOBJECT FPDFAnnot_GetObject(FPDF_ANNOTATION annot, int index)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetObjectStatic(annot, index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_RemoveObject_Delegate(FPDF_ANNOTATION annot, int index);

        private static FPDFAnnot_RemoveObject_Delegate FPDFAnnot_RemoveObjectStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Remove the object in |annot| at |index|.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="index">The index of the object to be removed.</param>
        /// <returns>Return true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_RemoveObject(FPDF_ANNOTATION annot, int index);.
        /// </remarks>
        public bool FPDFAnnot_RemoveObject(FPDF_ANNOTATION annot, int index)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_RemoveObjectStatic(annot, index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetColor_Delegate(FPDF_ANNOTATION annot, FPDFANNOT_COLORTYPES type, uint r, uint g, uint b, uint a);

        private static FPDFAnnot_SetColor_Delegate FPDFAnnot_SetColorStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the color of an annotation. Fails when called on annotations with appearance streams already defined; instead use FPDFPath_Set{Stroke|Fill}Color().
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="type">Type of the color to be set.</param>
        /// <param name="r">Buffer to hold the R value of the color. Ranges from 0 to 255.</param>
        /// <param name="g">Buffer to hold the G value of the color. Ranges from 0 to 255.</param>
        /// <param name="b">Buffer to hold the B value of the color. Ranges from 0 to 255.</param>
        /// <param name="a">Buffer to hold the opacity. Ranges from 0 to 255.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetColor(FPDF_ANNOTATION annot, FPDFANNOT_COLORTYPE type, unsigned int R, unsigned int G, unsigned int B, unsigned int A);.
        /// </remarks>
        public bool FPDFAnnot_SetColor(FPDF_ANNOTATION annot, FPDFANNOT_COLORTYPES type, uint r, uint g, uint b, uint a)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetColorStatic(annot, type, r, g, b, a);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetColor_Delegate(FPDF_ANNOTATION annot, FPDFANNOT_COLORTYPES type, ref uint r, ref uint g, ref uint b, ref uint a);

        private static FPDFAnnot_GetColor_Delegate FPDFAnnot_GetColorStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the color of an annotation. If no color is specified, default to yellow for highlight annotation, black for all else.
        /// Fails when called on annotations with appearance streams already defined; instead use FPDFPath_Get{Stroke|Fill}Color().
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="type">Type of the color requested.</param>
        /// <param name="r">Buffer to hold the R value of the color. Ranges from 0 to 255.</param>
        /// <param name="g">Buffer to hold the G value of the color. Ranges from 0 to 255.</param>
        /// <param name="b">Buffer to hold the B value of the color. Ranges from 0 to 255.</param>
        /// <param name="a">Buffer to hold the opacity. Ranges from 0 to 255.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetColor(FPDF_ANNOTATION annot, FPDFANNOT_COLORTYPE type, unsigned int* R, unsigned int* G, unsigned int* B, unsigned int* A);.
        /// </remarks>
        public bool FPDFAnnot_GetColor(FPDF_ANNOTATION annot, FPDFANNOT_COLORTYPES type, ref uint r, ref uint g, ref uint b, ref uint a)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetColorStatic(annot, type, ref r, ref g, ref b, ref a);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_HasAttachmentPoints_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_HasAttachmentPoints_Delegate FPDFAnnot_HasAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Check if the annotation is of a type that has attachment points (i.e. quadpoints).
        /// Quadpoints are the vertices of the rectangle that encompasses the texts affected by the annotation.
        /// They provide the coordinates in the page where the annotation is attached.
        /// Only text markup annotations (i.e. highlight, strikeout, squiggly, and underline) and link annotations have quadpoints.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns true if the annotation is of a type that has quadpoints, false otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_HasAttachmentPoints(FPDF_ANNOTATION annot);.
        /// </remarks>
        public bool FPDFAnnot_HasAttachmentPoints(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_HasAttachmentPointsStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetAttachmentPoints_Delegate(FPDF_ANNOTATION annot, ulong quad_index, ref FS_QUADPOINTSF quad_points);

        private static FPDFAnnot_SetAttachmentPoints_Delegate FPDFAnnot_SetAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Replace the attachment points (i.e. quadpoints) set of an annotation at |quad_index|.
        /// This index needs to be within the result of FPDFAnnot_CountAttachmentPoints().
        /// If the annotation's appearance stream is defined and this annotation is of a type with quadpoints,
        /// then update the bounding box too if the new quadpoints define a bigger one.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="quad_index">Index of the set of quadpoints.</param>
        /// <param name="quad_points">The quadpoints to be set.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetAttachmentPoints(FPDF_ANNOTATION annot, size_t quad_index, const FS_QUADPOINTSF* quad_points);.
        /// </remarks>
        public bool FPDFAnnot_SetAttachmentPoints(FPDF_ANNOTATION annot, ulong quad_index, ref FS_QUADPOINTSF quad_points)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetAttachmentPointsStatic(annot, quad_index, ref quad_points);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_AppendAttachmentPoints_Delegate(FPDF_ANNOTATION annot, ref FS_QUADPOINTSF quad_points);

        private static FPDFAnnot_AppendAttachmentPoints_Delegate FPDFAnnot_AppendAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Append to the list of attachment points (i.e. quadpoints) of an annotation.
        /// If the annotation's appearance stream is defined and this annotation is of a type with quadpoints,
        /// then update the bounding box too if the new quadpoints define a bigger one.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="quad_points">The quadpoints to be set.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_AppendAttachmentPoints(FPDF_ANNOTATION annot, const FS_QUADPOINTSF* quad_points);.
        /// </remarks>
        public bool FPDFAnnot_AppendAttachmentPoints(FPDF_ANNOTATION annot, ref FS_QUADPOINTSF quad_points)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_AppendAttachmentPointsStatic(annot, ref quad_points);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_CountAttachmentPoints_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_CountAttachmentPoints_Delegate FPDFAnnot_CountAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the number of sets of quadpoints of an annotation.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns the number of sets of quadpoints, or 0 on failure.</returns>
        /// <remarks>FPDF_EXPORT size_t FPDF_CALLCONV FPDFAnnot_CountAttachmentPoints(FPDF_ANNOTATION annot);.
        /// </remarks>
        public ulong FPDFAnnot_CountAttachmentPoints(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_CountAttachmentPointsStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetAttachmentPoints_Delegate(FPDF_ANNOTATION annot, ulong quad_index, ref FS_QUADPOINTSF quad_points);

        private static FPDFAnnot_GetAttachmentPoints_Delegate FPDFAnnot_GetAttachmentPointsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the attachment points (i.e. quadpoints) of an annotation.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="quad_index">Index of the set of quadpoints.</param>
        /// <param name="quad_points">Receives the quadpoints; must not be NULL.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetAttachmentPoints(FPDF_ANNOTATION annot, size_t quad_index, FS_QUADPOINTSF* quad_points);.
        /// </remarks>
        public bool FPDFAnnot_GetAttachmentPoints(FPDF_ANNOTATION annot, ulong quad_index, ref FS_QUADPOINTSF quad_points)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetAttachmentPointsStatic(annot, quad_index, ref quad_points);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetRect_Delegate(FPDF_ANNOTATION annot, ref FS_RECTF rect);

        private static FPDFAnnot_SetRect_Delegate FPDFAnnot_SetRectStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the annotation rectangle defining the location of the annotation. If the annotation's appearance stream is defined
        /// and this annotation is of a type without quadpoints, then update the bounding box too if the new rectangle defines a bigger one.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="rect">The annotation rectangle to be set.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetRect(FPDF_ANNOTATION annot, const FS_RECTF* rect);.
        /// </remarks>
        public bool FPDFAnnot_SetRect(FPDF_ANNOTATION annot, ref FS_RECTF rect)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetRectStatic(annot, ref rect);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetRect_Delegate(FPDF_ANNOTATION annot, ref FS_RECTF rect);

        private static FPDFAnnot_GetRect_Delegate FPDFAnnot_GetRectStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the annotation rectangle defining the location of the annotation.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="rect">Receives the rectangle; must not be NULL.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetRect(FPDF_ANNOTATION annot, FS_RECTF* rect);.
        /// </remarks>
        public bool FPDFAnnot_GetRect(FPDF_ANNOTATION annot, ref FS_RECTF rect)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetRectStatic(annot, ref rect);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetVertices_Delegate(FPDF_ANNOTATION annot, IntPtr buffer, ulong length);

        private static FPDFAnnot_GetVertices_Delegate FPDFAnnot_GetVerticesStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the vertices of a polygon or polyline annotation. |buffer| is an array of points of the annotation.
        /// If |length| is less than the returned length, or |annot| or |buffer| is NULL, |buffer| will not be modified.
        /// </summary>
        /// <param name="annot">Handle to an annotation, as returned by e.g. FPDFPage_GetAnnot().</param>
        /// <param name="buffer">Buffer for holding the points.</param>
        /// <param name="length">Length of the buffer in points.</param>
        /// <returns>Returns the number of points if the annotation is of type polygon or polyline, 0 otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetVertices(FPDF_ANNOTATION annot, FS_POINTF* buffer, unsigned long length);.
        /// </remarks>
        public ulong FPDFAnnot_GetVertices(FPDF_ANNOTATION annot, IntPtr buffer, ulong length)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetVerticesStatic(annot, buffer, length);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetInkListCount_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetInkListCount_Delegate FPDFAnnot_GetInkListCountStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the number of paths in the ink list of an ink annotation.
        /// </summary>
        /// <param name="annot">Handle to an annotation, as returned by e.g. FPDFPage_GetAnnot().</param>
        /// <returns>Returns the number of paths in the ink list if the annotation is of type ink, 0 otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetInkListCount(FPDF_ANNOTATION annot);.
        /// </remarks>
        public ulong FPDFAnnot_GetInkListCount(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetInkListCountStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetInkListPath_Delegate(FPDF_ANNOTATION annot, ulong path_index, IntPtr buffer, ulong length);

        private static FPDFAnnot_GetInkListPath_Delegate FPDFAnnot_GetInkListPathStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get a path in the ink list of an ink annotation. |buffer| is an array of points of the path.
        /// If |length| is less than the returned length, or |annot| or |buffer| is NULL, |buffer| will not be modified.
        /// </summary>
        /// <param name="annot">Handle to an annotation, as returned by e.g. FPDFPage_GetAnnot().</param>
        /// <param name="path_index">Index of the path.</param>
        /// <param name="buffer">Buffer for holding the points.</param>
        /// <param name="length">Length of the buffer in points.</param>
        /// <returns>Returns the number of points of the path if the annotation is of type ink, 0 otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetInkListPath(FPDF_ANNOTATION annot, unsigned long path_index, FS_POINTF* buffer, unsigned long length);.
        /// </remarks>
        public ulong FPDFAnnot_GetInkListPath(FPDF_ANNOTATION annot, ulong path_index, IntPtr buffer, ulong length)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetInkListPathStatic(annot, path_index, buffer, length);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetLine_Delegate(FPDF_ANNOTATION annot, ref FS_POINTF start, ref FS_POINTF end);

        private static FPDFAnnot_GetLine_Delegate FPDFAnnot_GetLineStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the starting and ending coordinates of a line annotation.
        /// </summary>
        /// <param name="annot">Handle to an annotation, as returned by e.g. FPDFPage_GetAnnot().</param>
        /// <param name="start">Starting point.</param>
        /// <param name="end">Ending point.</param>
        /// <returns>Returns true if the annotation is of type line, |start| and |end| are not NULL, false otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetLine(FPDF_ANNOTATION annot, FS_POINTF* start, FS_POINTF* end);.
        /// </remarks>
        public bool FPDFAnnot_GetLine(FPDF_ANNOTATION annot, ref FS_POINTF start, ref FS_POINTF end)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetLineStatic(annot, ref start, ref end);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetBorder_Delegate(FPDF_ANNOTATION annot, float horizontal_radius, float vertical_radius, float border_width);

        private static FPDFAnnot_SetBorder_Delegate FPDFAnnot_SetBorderStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the characteristics of the annotation's border (rounded rectangle).
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="horizontal_radius">Horizontal corner radius, in default user space units.</param>
        /// <param name="vertical_radius">Vertical corner radius, in default user space units.</param>
        /// <param name="border_width">Border width, in default user space units.</param>
        /// <returns>Returns true if setting the border for |annot| succeeds, false otherwise.</returns>
        /// <remarks>
        /// If |annot| contains an appearance stream that overrides the border values, then the appearance stream will be removed on success.
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetBorder(FPDF_ANNOTATION annot, float horizontal_radius, float vertical_radius, float border_width);.
        /// </remarks>
        public bool FPDFAnnot_SetBorder(FPDF_ANNOTATION annot, float horizontal_radius, float vertical_radius, float border_width)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetBorderStatic(annot, horizontal_radius, vertical_radius, border_width);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetBorder_Delegate(FPDF_ANNOTATION annot, ref float horizontal_radius, ref float vertical_radius, ref float border_width);

        private static FPDFAnnot_GetBorder_Delegate FPDFAnnot_GetBorderStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the characteristics of the annotation's border (rounded rectangle).
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="horizontal_radius">Horizontal corner radius, in default user space units.</param>
        /// <param name="vertical_radius">Vertical corner radius, in default user space units.</param>
        /// <param name="border_width">Border width, in default user space units.</param>
        /// <returns>Returns true if |horizontal_radius|, |vertical_radius| and |border_width| are not NULL, false otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetBorder(FPDF_ANNOTATION annot, float* horizontal_radius, float* vertical_radius, float* border_width);.
        /// </remarks>
        public bool FPDFAnnot_GetBorder(FPDF_ANNOTATION annot, ref float horizontal_radius, ref float vertical_radius, ref float border_width)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetBorderStatic(annot, ref horizontal_radius, ref vertical_radius, ref border_width);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_HasKey_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key);

        private static FPDFAnnot_HasKey_Delegate FPDFAnnot_HasKeyStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Check if |annot|'s dictionary has |key| as a key.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="key">The key to look for, encoded in UTF-8.</param>
        /// <returns>Returns true if |key| exists.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_HasKey(FPDF_ANNOTATION annot, FPDF_BYTESTRING key);.
        /// </remarks>
        public bool FPDFAnnot_HasKey(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_HasKeyStatic(annot, key);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetValueType_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key);

        private static FPDFAnnot_GetValueType_Delegate FPDFAnnot_GetValueTypeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the type of the value corresponding to |key| in |annot|'s dictionary.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="key">The key to look for, encoded in UTF-8.</param>
        /// <returns>Returns the type of the dictionary value.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_OBJECT_TYPE FPDF_CALLCONV FPDFAnnot_GetValueType(FPDF_ANNOTATION annot, FPDF_BYTESTRING key);.
        /// </remarks>
        public int FPDFAnnot_GetValueType(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetValueTypeStatic(annot, key);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetStringValue_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key, IntPtr value);

        private static FPDFAnnot_SetStringValue_Delegate FPDFAnnot_SetStringValueStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the string value corresponding to |key| in |annot|'s dictionary, overwriting the existing value if any.
        /// The value type would be FPDF_OBJECT_STRING after this function call succeeds.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="key">The key to the dictionary entry to be set, encoded in UTF-8.</param>
        /// <param name="value">The string value to be set, encoded in UTF-16LE.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetStringValue(FPDF_ANNOTATION annot, FPDF_BYTESTRING key, FPDF_WIDESTRING value);.
        /// </remarks>
        public bool FPDFAnnot_SetStringValue(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key, IntPtr value)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetStringValueStatic(annot, key, value);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetStringValue_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key, IntPtr buffer, ulong buflen);

        private static FPDFAnnot_GetStringValue_Delegate FPDFAnnot_GetStringValueStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the string value corresponding to |key| in |annot|'s dictionary. |buffer| is only modified
        /// if |buflen| is longer than the length of contents. Note that if |key| does not exist in the dictionary
        /// or if |key|'s corresponding value in the dictionary is not a string (i.e. the value is not of type FPDF_OBJECT_STRING or FPDF_OBJECT_NAME),
        /// then an empty string would be copied to |buffer| and the return value would be 2. On other errors,
        /// nothing would be added to |buffer| and the return value would be 0.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="key">The key to the requested dictionary entry, encoded in UTF-8.</param>
        /// <param name="buffer">Buffer for holding the value string, encoded in UTF-16LE.</param>
        /// <param name="buflen">Length of the buffer in bytes.</param>
        /// <returns>Returns the length of the string value in bytes.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetStringValue(FPDF_ANNOTATION annot, FPDF_BYTESTRING key, FPDF_WCHAR* buffer, unsigned long buflen);.
        /// </remarks>
        public ulong FPDFAnnot_GetStringValue(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key, IntPtr buffer, ulong buflen)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetStringValueStatic(annot, key, buffer, buflen);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetNumberValue_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key, ref float value);

        private static FPDFAnnot_GetNumberValue_Delegate FPDFAnnot_GetNumberValueStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the float value corresponding to |key| in |annot|'s dictionary. Writes value to |value| and returns True
        /// if |key| exists in the dictionary and |key|'s corresponding value is a number (FPDF_OBJECT_NUMBER), False otherwise.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="key">The key to the requested dictionary entry, encoded in UTF-8.</param>
        /// <param name="value">Receives the value, must not be NULL.</param>
        /// <returns>Returns True if value found, False otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetNumberValue(FPDF_ANNOTATION annot, FPDF_BYTESTRING key, float* value);.
        /// </remarks>
        public bool FPDFAnnot_GetNumberValue(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key, ref float value)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetNumberValueStatic(annot, key, ref value);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetAP_Delegate(FPDF_ANNOTATION annot, FPDF_ANNOT_APPEARANCEMODES appearanceMode, IntPtr value);

        private static FPDFAnnot_SetAP_Delegate FPDFAnnot_SetAPStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the AP (appearance string) in |annot|'s dictionary for a given |appearanceMode|.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="appearanceMode">The appearance mode (normal, rollover or down) for which to get the AP.</param>
        /// <param name="value">The string value to be set, encoded in UTF-16LE. If nullptr is passed, the AP is cleared for that mode.
        /// If the mode is Normal, APs for all modes are cleared.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetAP(FPDF_ANNOTATION annot, FPDF_ANNOT_APPEARANCEMODE appearanceMode, FPDF_WIDESTRING value);.
        /// </remarks>
        public bool FPDFAnnot_SetAP(FPDF_ANNOTATION annot, FPDF_ANNOT_APPEARANCEMODES appearanceMode, IntPtr value)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetAPStatic(annot, appearanceMode, value);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetAP_Delegate(FPDF_ANNOTATION annot, FPDF_ANNOT_APPEARANCEMODES appearanceMode, IntPtr buffer, ulong buflen);

        private static FPDFAnnot_GetAP_Delegate FPDFAnnot_GetAPStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the AP (appearance string) from |annot|'s dictionary for a given |appearanceMode|.
        /// |buffer| is only modified if |buflen| is large enough to hold the whole AP string.
        /// If |buflen| is smaller, the total size of the AP is still returned, but nothing is copied.
        /// If there is no appearance stream for |annot| in |appearanceMode|, an empty string is written to |buf| and 2 is returned.
        /// On other errors, nothing is written to |buffer| and 0 is returned.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="appearanceMode">The appearance mode (normal, rollover or down) for which to get the AP.</param>
        /// <param name="buffer">Buffer for holding the value string, encoded in UTF-16LE.</param>
        /// <param name="buflen">Length of the buffer in bytes.</param>
        /// <returns>Returns the length of the string value in bytes.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetAP(FPDF_ANNOTATION annot, FPDF_ANNOT_APPEARANCEMODE appearanceMode, FPDF_WCHAR* buffer, unsigned long buflen);.
        /// </remarks>
        public ulong FPDFAnnot_GetAP(FPDF_ANNOTATION annot, FPDF_ANNOT_APPEARANCEMODES appearanceMode, IntPtr buffer, ulong buflen)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetAPStatic(annot, appearanceMode, buffer, buflen);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_ANNOTATION FPDFAnnot_GetLinkedAnnot_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key);

        private static FPDFAnnot_GetLinkedAnnot_Delegate FPDFAnnot_GetLinkedAnnotStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the annotation corresponding to |key| in |annot|'s dictionary. Common keys for linking annotations include "IRT" and "Popup".
        /// Must call FPDFPage_CloseAnnot() when the annotation returned by this function is no longer needed.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="key">The key to the requested dictionary entry, encoded in UTF-8.</param>
        /// <returns>Returns a handle to the linked annotation object, or NULL on failure.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_ANNOTATION FPDF_CALLCONV FPDFAnnot_GetLinkedAnnot(FPDF_ANNOTATION annot, FPDF_BYTESTRING key);.
        /// </remarks>
        public FPDF_ANNOTATION FPDFAnnot_GetLinkedAnnot(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string key)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetLinkedAnnotStatic(annot, key);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetFlags_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetFlags_Delegate FPDFAnnot_GetFlagsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the annotation flags of |annot|.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns the annotation flags.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetFlags(FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetFlags(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFlagsStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetFlags_Delegate(FPDF_ANNOTATION annot, int flags);

        private static FPDFAnnot_SetFlags_Delegate FPDFAnnot_SetFlagsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the |annot|'s flags to be of the value |flags|.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="flags">The flag values to be set.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetFlags(FPDF_ANNOTATION annot, int flags);.
        /// </remarks>
        public bool FPDFAnnot_SetFlags(FPDF_ANNOTATION annot, int flags)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetFlagsStatic(annot, flags);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetFormFieldFlags_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetFormFieldFlags_Delegate FPDFAnnot_GetFormFieldFlagsStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the annotation flags of |annot|.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment().</param>
        /// <param name="annot">Handle to an interactive form annotation.</param>
        /// <returns>Returns the annotation flags specific to interactive forms.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetFormFieldFlags(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetFormFieldFlags(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormFieldFlagsStatic(handle, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_ANNOTATION FPDFAnnot_GetFormFieldAtPoint_Delegate(FPDF_FORMHANDLE handle, FPDF_PAGE page, ref FS_POINTF point);

        private static FPDFAnnot_GetFormFieldAtPoint_Delegate FPDFAnnot_GetFormFieldAtPointStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Retrieves an interactive form annotation whose rectangle contains a given point on a page.
        /// Must call FPDFPage_CloseAnnot() when the annotation returned is no longer needed.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment().</param>
        /// <param name="page">Handle to the page, returned by FPDF_LoadPage function.</param>
        /// <param name="point">Position in PDF "user space".</param>
        /// <returns>Returns the interactive form annotation whose rectangle contains the given coordinates on the page.
        /// If there is no such annotation, return NULL.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_ANNOTATION FPDF_CALLCONV FPDFAnnot_GetFormFieldAtPoint(FPDF_FORMHANDLE hHandle, FPDF_PAGE page, const FS_POINTF* point);.
        /// </remarks>
        public FPDF_ANNOTATION FPDFAnnot_GetFormFieldAtPoint(FPDF_FORMHANDLE handle, FPDF_PAGE page, ref FS_POINTF point)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormFieldAtPointStatic(handle, page, ref point);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetFormFieldName_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, IntPtr buffer, ulong buflen);

        private static FPDFAnnot_GetFormFieldName_Delegate FPDFAnnot_GetFormFieldNameStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets the name of |annot|, which is an interactive form annotation.
        /// |buffer| is only modified if |buflen| is longer than the length of contents.
        /// In case of error, nothing will be added to |buffer| and the return value will be 0.
        /// Note that return value of empty string is 2 for "\0\0".
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment().</param>
        /// <param name="annot">Handle to an interactive form annotation.</param>
        /// <param name="buffer">Buffer for holding the name string, encoded in UTF-16LE.</param>
        /// <param name="buflen">Length of the buffer in bytes.</param>
        /// <returns>Returns the length of the string value in bytes.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetFormFieldName(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot, FPDF_WCHAR* buffer, unsigned long buflen);.
        /// </remarks>
        public ulong FPDFAnnot_GetFormFieldName(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, IntPtr buffer, ulong buflen)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormFieldNameStatic(handle, annot, buffer, buflen);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetFormFieldType_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetFormFieldType_Delegate FPDFAnnot_GetFormFieldTypeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets the form field type of |annot|, which is an interactive form annotation.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment().</param>
        /// <param name="annot">Handle to an interactive form annotation.</param>
        /// <returns>Returns the type of the form field (one of the FPDF_FORMFIELD_* values) on success. Returns -1 on error.
        /// See field types in fpdf_formfill.h.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetFormFieldType(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetFormFieldType(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormFieldTypeStatic(handle, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetFormFieldValue_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, IntPtr buffer, ulong buflen);

        private static FPDFAnnot_GetFormFieldValue_Delegate FPDFAnnot_GetFormFieldValueStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets the value of |annot|, which is an interactive form annotation.
        /// |buffer| is only modified if |buflen| is longer than the length of contents.
        /// In case of error, nothing will be added to |buffer| and the return value will be 0.
        /// Note that return value of empty string is 2 for "\0\0".
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment().</param>
        /// <param name="annot">Handle to an interactive form annotation.</param>
        /// <param name="buffer">Buffer for holding the value string, encoded in UTF-16LE.</param>
        /// <param name="buflen">Length of the buffer in bytes.</param>
        /// <returns>Returns the length of the string value in bytes.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetFormFieldValue(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot, FPDF_WCHAR* buffer, unsigned long buflen);.
        /// </remarks>
        public ulong FPDFAnnot_GetFormFieldValue(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, IntPtr buffer, ulong buflen)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormFieldValueStatic(handle, annot, buffer, buflen);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetOptionCount_Delegate(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetOptionCount_Delegate FPDFAnnot_GetOptionCountStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the number of options in the |annot|'s "Opt" dictionary. Intended for use with listbox and combobox widget annotations.
        /// </summary>
        /// <param name="hHandle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns the number of options in "Opt" dictionary on success.
        /// Return value will be -1 if annotation does not have an "Opt" dictionary or other error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetOptionCount(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetOptionCount(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetOptionCountStatic(hHandle, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetOptionLabel_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, int index, IntPtr buffer, ulong buflen);

        private static FPDFAnnot_GetOptionLabel_Delegate FPDFAnnot_GetOptionLabelStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the string value for the label of the option at |index| in |annot|'s "Opt" dictionary.
        /// Intended for use with listbox and combobox widget annotations.
        /// |buffer| is only modified if |buflen| is longer than the length of contents.
        /// If index is out of range or in case of other error, nothing will be added to |buffer| and the return value will be 0.
        /// Note that return value of empty string is 2 for "\0\0".
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="index">Numeric index of the option in the "Opt" array.</param>
        /// <param name="buffer">Buffer for holding the value string, encoded in UTF-16LE.</param>
        /// <param name="buflen">Length of the buffer in bytes.</param>
        /// <returns>Returns the length of the string value in bytes.
        /// If |annot| does not have an "Opt" array, |index| is out of range or if any other error occurs, returns 0.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetOptionLabel(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot, int index, FPDF_WCHAR* buffer, unsigned long buflen);.
        /// </remarks>
        public ulong FPDFAnnot_GetOptionLabel(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, int index, IntPtr buffer, ulong buflen)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetOptionLabelStatic(handle, annot, index, buffer, buflen);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_IsOptionSelected_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, int index);

        private static FPDFAnnot_IsOptionSelected_Delegate FPDFAnnot_IsOptionSelectedStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Determine whether or not the option at |index| in |annot|'s "Opt" dictionary is selected.
        /// Intended for use with listbox and combobox widget annotations.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="index">Numeric index of the option in the "Opt" array.</param>
        /// <returns>Returns true if the option at |index| in |annot|'s "Opt" dictionary is selected, false otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_IsOptionSelected(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, int index);.
        /// </remarks>
        public bool FPDFAnnot_IsOptionSelected(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, int index)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_IsOptionSelectedStatic(handle, annot, index);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetFontSize_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, ref float value);

        private static FPDFAnnot_GetFontSize_Delegate FPDFAnnot_GetFontSizeStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the float value of the font size for an |annot| with variable text.
        /// If 0, the font is to be auto-sized: its size is computed as a function of the height of the annotation rectangle.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <param name="value">Required. Float which will be set to font size on success.</param>
        /// <returns>Returns true if the font size was set in |value|, false on error or if |value| not provided.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetFontSize(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot, float* value);.
        /// </remarks>
        public bool FPDFAnnot_GetFontSize(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, ref float value)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFontSizeStatic(handle, annot, ref value);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_IsChecked_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot);

        private static FPDFAnnot_IsChecked_Delegate FPDFAnnot_IsCheckedStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Determine if |annot| is a form widget that is checked. Intended for use with checkbox and radio button widgets.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns true if |annot| is a form widget and is checked, false otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_IsChecked(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot);.
        /// </remarks>
        public bool FPDFAnnot_IsChecked(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_IsCheckedStatic(handle, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetFocusableSubtypes_Delegate(FPDF_FORMHANDLE handle, IntPtr subtypes, ulong count);

        private static FPDFAnnot_SetFocusableSubtypes_Delegate FPDFAnnot_SetFocusableSubtypesStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Set the list of focusable annotation subtypes. Annotations of subtype FPDF_ANNOT_WIDGET are by default focusable.
        /// New subtypes set using this API will override the existing subtypes.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="subtypes">List of annotation subtype which can be tabbed over.</param>
        /// <param name="count">Total number of annotation subtype in list.</param>
        /// <returns>Returns true if list of annotation subtype is set successfully, false otherwise.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetFocusableSubtypes(FPDF_FORMHANDLE hHandle, const FPDF_ANNOTATION_SUBTYPE* subtypes, size_t count);.
        /// </remarks>
        public bool FPDFAnnot_SetFocusableSubtypes(FPDF_FORMHANDLE handle, IntPtr subtypes, ulong count)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetFocusableSubtypesStatic(handle, subtypes, count);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetFocusableSubtypesCount_Delegate(FPDF_FORMHANDLE handle);

        private static FPDFAnnot_GetFocusableSubtypesCount_Delegate FPDFAnnot_GetFocusableSubtypesCountStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the count of focusable annotation subtypes as set by host for a |hHandle|.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <returns>Returns the count of focusable annotation subtypes or -1 on error.
        /// Note : Annotations of type FPDF_ANNOT_WIDGET are by default focusable.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetFocusableSubtypesCount(FPDF_FORMHANDLE hHandle);.
        /// </remarks>
        public int FPDFAnnot_GetFocusableSubtypesCount(FPDF_FORMHANDLE handle)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFocusableSubtypesCountStatic(handle);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_GetFocusableSubtypes_Delegate(FPDF_FORMHANDLE handle, IntPtr subtypes, ulong count);

        private static FPDFAnnot_GetFocusableSubtypes_Delegate FPDFAnnot_GetFocusableSubtypesStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Get the list of focusable annotation subtype as set by host.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="subtypes">Receives the list of annotation subtype which can be tabbed over.
        /// Caller must have allocated |subtypes| more than or equal to the count obtained from FPDFAnnot_GetFocusableSubtypesCount() API.</param>
        /// <param name="count">Size of |subtypes|.</param>
        /// <returns>Returns true on success and set list of annotation subtype to |subtypes|, false otherwise.
        /// Note : Annotations of type FPDF_ANNOT_WIDGET are by default focusable.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_GetFocusableSubtypes(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION_SUBTYPE* subtypes, size_t count);.
        /// </remarks>
        public bool FPDFAnnot_GetFocusableSubtypes(FPDF_FORMHANDLE handle, IntPtr subtypes, ulong count)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFocusableSubtypesStatic(handle, subtypes, count);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate FPDF_LINK FPDFAnnot_GetLink_Delegate(FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetLink_Delegate FPDFAnnot_GetLinkStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets FPDF_LINK object for |annot|. Intended to use for link annotations.
        /// </summary>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns FPDF_LINK from the FPDF_ANNOTATION and NULL on failure, if the input annot is NULL or input annot's subtype is not link.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_LINK FPDF_CALLCONV FPDFAnnot_GetLink(FPDF_ANNOTATION annot);.
        /// </remarks>
        public FPDF_LINK FPDFAnnot_GetLink(FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetLinkStatic(annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetFormControlCount_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetFormControlCount_Delegate FPDFAnnot_GetFormControlCountStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets the count of annotations in the |annot|'s control group.
        /// A group of interactive form annotations is collectively called a form control group.
        /// Here, |annot|, an interactive form annotation, should be either a radio button or a checkbox.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns number of controls in its control group or -1 on error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetFormControlCount(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetFormControlCount(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormControlCountStatic(handle, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int FPDFAnnot_GetFormControlIndex_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot);

        private static FPDFAnnot_GetFormControlIndex_Delegate FPDFAnnot_GetFormControlIndexStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets the index of |annot| in |annot|'s control group.
        /// A group of interactive form annotations is collectively called a form control group.
        /// Here, |annot|, an interactive form annotation, should be either a radio button or a checkbox.
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment.</param>
        /// <param name="annot">Handle to an annotation.</param>
        /// <returns>Returns index of a given |annot| in its control group or -1 on error.</returns>
        /// <remarks>
        /// FPDF_EXPORT int FPDF_CALLCONV FPDFAnnot_GetFormControlIndex(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot);.
        /// </remarks>
        public int FPDFAnnot_GetFormControlIndex(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormControlIndexStatic(handle, annot);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong FPDFAnnot_GetFormFieldExportValue_Delegate(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, IntPtr buffer, ulong buflen);

        private static FPDFAnnot_GetFormFieldExportValue_Delegate FPDFAnnot_GetFormFieldExportValueStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Gets the export value of |annot| which is an interactive form annotation.
        /// Intended for use with radio button and checkbox widget annotations.
        /// |buffer| is only modified if |buflen| is longer than the length of contents.
        /// In case of error, nothing will be added to |buffer| and the return value will be 0.
        /// Note that return value of empty string is 2 for "\0\0".
        /// </summary>
        /// <param name="handle">Handle to the form fill module, returned by FPDFDOC_InitFormFillEnvironment().</param>
        /// <param name="annot">Handle to an interactive form annotation.</param>
        /// <param name="buffer">Buffer for holding the value string, encoded in UTF-16LE.</param>
        /// <param name="buflen">Length of the buffer in bytes.</param>
        /// <returns>Returns the length of the string value in bytes.</returns>
        /// <remarks>
        /// FPDF_EXPORT unsigned long FPDF_CALLCONV FPDFAnnot_GetFormFieldExportValue(FPDF_FORMHANDLE hHandle, FPDF_ANNOTATION annot, FPDF_WCHAR* buffer, unsigned long buflen);.
        /// </remarks>
        public ulong FPDFAnnot_GetFormFieldExportValue(FPDF_FORMHANDLE handle, FPDF_ANNOTATION annot, IntPtr buffer, ulong buflen)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_GetFormFieldExportValueStatic(handle, annot, buffer, buflen);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool FPDFAnnot_SetURI_Delegate(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string uri);

        private static FPDFAnnot_SetURI_Delegate FPDFAnnot_SetURIStatic { get; set; }

        /// <summary>
        /// Experimental API.
        /// Add a URI action to |annot|, overwriting the existing action, if any.
        /// </summary>
        /// <param name="annot">Handle to a link annotation.</param>
        /// <param name="uri">The URI to be set, encoded in 7-bit ASCII.</param>
        /// <returns>Returns true if successful.</returns>
        /// <remarks>
        /// FPDF_EXPORT FPDF_BOOL FPDF_CALLCONV FPDFAnnot_SetURI(FPDF_ANNOTATION annot, const char* uri);.
        /// </remarks>
        public bool FPDFAnnot_SetURI(FPDF_ANNOTATION annot, [MarshalAs(UnmanagedType.LPStr)] string uri)
        {
            lock (_syncObject)
            {
                return FPDFAnnot_SetURIStatic(annot, uri);
            }
        }

        private static void LoadDllAnnotPart()
        {
            LoadDllAnnotPart1();
            LoadDllAnnotPart2();
        }

        private static void LoadDllAnnotPart1()
        {
            FPDFAnnot_IsSupportedSubtypeStatic = GetPDFiumFunction<FPDFAnnot_IsSupportedSubtype_Delegate>(nameof(FPDFAnnot_IsSupportedSubtype));
            FPDFPage_CreateAnnotStatic = GetPDFiumFunction<FPDFPage_CreateAnnot_Delegate>(nameof(FPDFPage_CreateAnnot));
            FPDFPage_GetAnnotCountStatic = GetPDFiumFunction<FPDFPage_GetAnnotCount_Delegate>(nameof(FPDFPage_GetAnnotCount));
            FPDFPage_GetAnnotStatic = GetPDFiumFunction<FPDFPage_GetAnnot_Delegate>(nameof(FPDFPage_GetAnnot));
            FPDFPage_GetAnnotIndexStatic = GetPDFiumFunction<FPDFPage_GetAnnotIndex_Delegate>(nameof(FPDFPage_GetAnnotIndex));
            FPDFPage_CloseAnnotStatic = GetPDFiumFunction<FPDFPage_CloseAnnot_Delegate>(nameof(FPDFPage_CloseAnnot));
            FPDFPage_RemoveAnnotStatic = GetPDFiumFunction<FPDFPage_RemoveAnnot_Delegate>(nameof(FPDFPage_RemoveAnnot));
            FPDFAnnot_GetSubtypeStatic = GetPDFiumFunction<FPDFAnnot_GetSubtype_Delegate>(nameof(FPDFAnnot_GetSubtype));
            FPDFAnnot_IsObjectSupportedSubtypeStatic = GetPDFiumFunction<FPDFAnnot_IsObjectSupportedSubtype_Delegate>(nameof(FPDFAnnot_IsObjectSupportedSubtype));
            FPDFAnnot_UpdateObjectStatic = GetPDFiumFunction<FPDFAnnot_UpdateObject_Delegate>(nameof(FPDFAnnot_UpdateObject));
            FPDFAnnot_AddInkStrokeStatic = GetPDFiumFunction<FPDFAnnot_AddInkStroke_Delegate>(nameof(FPDFAnnot_AddInkStroke));
            FPDFAnnot_RemoveInkListStatic = GetPDFiumFunction<FPDFAnnot_RemoveInkList_Delegate>(nameof(FPDFAnnot_RemoveInkList));
            FPDFAnnot_AppendObjectStatic = GetPDFiumFunction<FPDFAnnot_AppendObject_Delegate>(nameof(FPDFAnnot_AppendObject));
            FPDFAnnot_GetObjectCountStatic = GetPDFiumFunction<FPDFAnnot_GetObjectCount_Delegate>(nameof(FPDFAnnot_GetObjectCount));
            FPDFAnnot_GetObjectStatic = GetPDFiumFunction<FPDFAnnot_GetObject_Delegate>(nameof(FPDFAnnot_GetObject));
            FPDFAnnot_RemoveObjectStatic = GetPDFiumFunction<FPDFAnnot_RemoveObject_Delegate>(nameof(FPDFAnnot_RemoveObject));
            FPDFAnnot_SetColorStatic = GetPDFiumFunction<FPDFAnnot_SetColor_Delegate>(nameof(FPDFAnnot_SetColor));
            FPDFAnnot_GetColorStatic = GetPDFiumFunction<FPDFAnnot_GetColor_Delegate>(nameof(FPDFAnnot_GetColor));
            FPDFAnnot_HasAttachmentPointsStatic = GetPDFiumFunction<FPDFAnnot_HasAttachmentPoints_Delegate>(nameof(FPDFAnnot_HasAttachmentPoints));
            FPDFAnnot_SetAttachmentPointsStatic = GetPDFiumFunction<FPDFAnnot_SetAttachmentPoints_Delegate>(nameof(FPDFAnnot_SetAttachmentPoints));
            FPDFAnnot_AppendAttachmentPointsStatic = GetPDFiumFunction<FPDFAnnot_AppendAttachmentPoints_Delegate>(nameof(FPDFAnnot_AppendAttachmentPoints));
            FPDFAnnot_CountAttachmentPointsStatic = GetPDFiumFunction<FPDFAnnot_CountAttachmentPoints_Delegate>(nameof(FPDFAnnot_CountAttachmentPoints));
            FPDFAnnot_GetAttachmentPointsStatic = GetPDFiumFunction<FPDFAnnot_GetAttachmentPoints_Delegate>(nameof(FPDFAnnot_GetAttachmentPoints));
            FPDFAnnot_SetRectStatic = GetPDFiumFunction<FPDFAnnot_SetRect_Delegate>(nameof(FPDFAnnot_SetRect));
            FPDFAnnot_GetRectStatic = GetPDFiumFunction<FPDFAnnot_GetRect_Delegate>(nameof(FPDFAnnot_GetRect));
            FPDFAnnot_GetVerticesStatic = GetPDFiumFunction<FPDFAnnot_GetVertices_Delegate>(nameof(FPDFAnnot_GetVertices));
            FPDFAnnot_GetInkListCountStatic = GetPDFiumFunction<FPDFAnnot_GetInkListCount_Delegate>(nameof(FPDFAnnot_GetInkListCount));
            FPDFAnnot_GetInkListPathStatic = GetPDFiumFunction<FPDFAnnot_GetInkListPath_Delegate>(nameof(FPDFAnnot_GetInkListPath));
            FPDFAnnot_GetLineStatic = GetPDFiumFunction<FPDFAnnot_GetLine_Delegate>(nameof(FPDFAnnot_GetLine));
            FPDFAnnot_SetBorderStatic = GetPDFiumFunction<FPDFAnnot_SetBorder_Delegate>(nameof(FPDFAnnot_SetBorder));
        }

        private static void LoadDllAnnotPart2()
        {
            FPDFAnnot_GetBorderStatic = GetPDFiumFunction<FPDFAnnot_GetBorder_Delegate>(nameof(FPDFAnnot_GetBorder));
            FPDFAnnot_HasKeyStatic = GetPDFiumFunction<FPDFAnnot_HasKey_Delegate>(nameof(FPDFAnnot_HasKey));
            FPDFAnnot_GetValueTypeStatic = GetPDFiumFunction<FPDFAnnot_GetValueType_Delegate>(nameof(FPDFAnnot_GetValueType));
            FPDFAnnot_SetStringValueStatic = GetPDFiumFunction<FPDFAnnot_SetStringValue_Delegate>(nameof(FPDFAnnot_SetStringValue));
            FPDFAnnot_GetStringValueStatic = GetPDFiumFunction<FPDFAnnot_GetStringValue_Delegate>(nameof(FPDFAnnot_GetStringValue));
            FPDFAnnot_GetNumberValueStatic = GetPDFiumFunction<FPDFAnnot_GetNumberValue_Delegate>(nameof(FPDFAnnot_GetNumberValue));
            FPDFAnnot_SetAPStatic = GetPDFiumFunction<FPDFAnnot_SetAP_Delegate>(nameof(FPDFAnnot_SetAP));
            FPDFAnnot_GetAPStatic = GetPDFiumFunction<FPDFAnnot_GetAP_Delegate>(nameof(FPDFAnnot_GetAP));
            FPDFAnnot_GetLinkedAnnotStatic = GetPDFiumFunction<FPDFAnnot_GetLinkedAnnot_Delegate>(nameof(FPDFAnnot_GetLinkedAnnot));
            FPDFAnnot_GetFlagsStatic = GetPDFiumFunction<FPDFAnnot_GetFlags_Delegate>(nameof(FPDFAnnot_GetFlags));
            FPDFAnnot_SetFlagsStatic = GetPDFiumFunction<FPDFAnnot_SetFlags_Delegate>(nameof(FPDFAnnot_SetFlags));
            FPDFAnnot_GetFormFieldFlagsStatic = GetPDFiumFunction<FPDFAnnot_GetFormFieldFlags_Delegate>(nameof(FPDFAnnot_GetFormFieldFlags));
            FPDFAnnot_GetFormFieldAtPointStatic = GetPDFiumFunction<FPDFAnnot_GetFormFieldAtPoint_Delegate>(nameof(FPDFAnnot_GetFormFieldAtPoint));
            FPDFAnnot_GetFormFieldNameStatic = GetPDFiumFunction<FPDFAnnot_GetFormFieldName_Delegate>(nameof(FPDFAnnot_GetFormFieldName));
            FPDFAnnot_GetFormFieldTypeStatic = GetPDFiumFunction<FPDFAnnot_GetFormFieldType_Delegate>(nameof(FPDFAnnot_GetFormFieldType));
            FPDFAnnot_GetFormFieldValueStatic = GetPDFiumFunction<FPDFAnnot_GetFormFieldValue_Delegate>(nameof(FPDFAnnot_GetFormFieldValue));
            FPDFAnnot_GetOptionCountStatic = GetPDFiumFunction<FPDFAnnot_GetOptionCount_Delegate>(nameof(FPDFAnnot_GetOptionCount));
            FPDFAnnot_GetOptionLabelStatic = GetPDFiumFunction<FPDFAnnot_GetOptionLabel_Delegate>(nameof(FPDFAnnot_GetOptionLabel));
            FPDFAnnot_IsOptionSelectedStatic = GetPDFiumFunction<FPDFAnnot_IsOptionSelected_Delegate>(nameof(FPDFAnnot_IsOptionSelected));
            FPDFAnnot_GetFontSizeStatic = GetPDFiumFunction<FPDFAnnot_GetFontSize_Delegate>(nameof(FPDFAnnot_GetFontSize));
            FPDFAnnot_IsCheckedStatic = GetPDFiumFunction<FPDFAnnot_IsChecked_Delegate>(nameof(FPDFAnnot_IsChecked));
            FPDFAnnot_SetFocusableSubtypesStatic = GetPDFiumFunction<FPDFAnnot_SetFocusableSubtypes_Delegate>(nameof(FPDFAnnot_SetFocusableSubtypes));
            FPDFAnnot_GetFocusableSubtypesCountStatic = GetPDFiumFunction<FPDFAnnot_GetFocusableSubtypesCount_Delegate>(nameof(FPDFAnnot_GetFocusableSubtypesCount));
            FPDFAnnot_GetFocusableSubtypesStatic = GetPDFiumFunction<FPDFAnnot_GetFocusableSubtypes_Delegate>(nameof(FPDFAnnot_GetFocusableSubtypes));
            FPDFAnnot_GetLinkStatic = GetPDFiumFunction<FPDFAnnot_GetLink_Delegate>(nameof(FPDFAnnot_GetLink));
            FPDFAnnot_GetFormControlCountStatic = GetPDFiumFunction<FPDFAnnot_GetFormControlCount_Delegate>(nameof(FPDFAnnot_GetFormControlCount));
            FPDFAnnot_GetFormControlIndexStatic = GetPDFiumFunction<FPDFAnnot_GetFormControlIndex_Delegate>(nameof(FPDFAnnot_GetFormControlIndex));
            FPDFAnnot_GetFormFieldExportValueStatic = GetPDFiumFunction<FPDFAnnot_GetFormFieldExportValue_Delegate>(nameof(FPDFAnnot_GetFormFieldExportValue));
            FPDFAnnot_SetURIStatic = GetPDFiumFunction<FPDFAnnot_SetURI_Delegate>(nameof(FPDFAnnot_SetURI));
        }
    }
}
