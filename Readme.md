# XAF - How to display a collection property as a checked list box

This example illustrates how to display a [collection property](https://docs.devexpress.com/eXpressAppFramework/113568/business-model-design-orm/data-types-supported-by-built-in-editors/collection-properties) as a checked list box in XAF applications.

![image](https://user-images.githubusercontent.com/14300209/229557843-6b3586a5-319f-45a2-b4aa-37935dda28ed.png)

The created application:

* Shows detail data as a checked list box.
* Allows you to dynamically add/remove detail records to/from the list box.
* Allows you to link/unlink child records to/from the master object using checkboxes. Check a record to associate it with the master object. Unchecked items are unlinked from the corresponding object.

## Implementation Details

1. Create master and detail objects.
2. Implement a [custom property editor](https://docs.devexpress.com/eXpressAppFramework/113097/ui-construction/view-items-and-property-editors/property-editors?p=netframework#custom-property-editors) depending on your application's platform. This example creates the [CheckedListBoxEditor](./CS/EFCore/CheckedListEF/CheckedListEF.Blazor.Server/Editors/CheckedListBoxEditor) editor for Blazor apps and [WinCheckedListBoxPropertyEditor.cs](./CS/EFCore/CheckedListEF/CheckedListEF.Win/Editors/WinCheckedListBoxPropertyEditor.cs) for Windows Forms.
3. Run the application.

## Files to Review

* Blazor - [CheckedListBoxEditor.cs](./CS/EFCore/CheckedListEF/CheckedListEF.Blazor.Server/Editors/CheckedListBoxEditor/CheckedListBoxEditor.cs)
* Windows Forms - [WinCheckedListBoxPropertyEditor.cs](./CS/EFCore/CheckedListEF/CheckedListEF.Win/Editors/WinCheckedListBoxPropertyEditor.cs) 

## Documentation

* [Collection Properties](https://docs.devexpress.com/eXpressAppFramework/113568/business-model-design-orm/data-types-supported-by-built-in-editors/collection-properties)
* [Property Editors](https://docs.devexpress.com/eXpressAppFramework/113097/ui-construction/view-items-and-property-editors/property-editors)

## More Examples

* [XAF Blazor - How to use a TagBox to view and edit a collection property in Detail Views](https://github.com/DevExpress-Examples/XAF-Blazor-How-to-use-a-TagBox-to-view-and-edit-a-collection-property-in-Detail-Views)
