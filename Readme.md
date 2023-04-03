<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592642/22.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1807)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CheckedListBoxEditor](./CS/EFCore/CheckedListEF/CheckedListEF.Blazor.Server/Editors/CheckedListBoxEditor)
* [WinCheckedListBoxPropertyEditor.cs](./CS/EFCore/CheckedListEF/CheckedListEF.Win/Editors/WinCheckedListBoxPropertyEditor.cs) 
<!-- default file list end -->
# How to represent a collection property using a checked list box


<Scenario:


1. It is required to show a check list. It should be possible to add and remove items for this list dynamically.

2. There are a couple of child records, and it is required to show all available records in a compact manner, and link and unlink them from the master object quickly with check boxes. When an item is checked, this means that this record is associated with the master object.

 

Steps to implement:


This functionality is implemented via a custom property editor that can be used to edit collection properties. There are two separate editors for Blazor ([CheckedListBoxEditor](./CS/EFCore/CheckedListEF/CheckedListEF.Blazor.Server/Editors/CheckedListBoxEditor)) and WinForms ( [WinCheckedListBoxPropertyEditor.cs](./CS/EFCore/CheckedListEF/CheckedListEF.Win/Editors/WinCheckedListBoxPropertyEditor.cs)).


See Also:

[Property Editors](https://docs.devexpress.com/eXpressAppFramework/113097/ui-construction/view-items-and-property-editors/property-editors)
[XAF Blazor - How to use a TagBox to view and edit a collection property in Detail Views](https://supportcenter.devexpress.com/ticket/details/t1011723/xaf-blazor-how-to-use-a-tagbox-to-view-and-edit-a-collection-property-in-detail-views)

