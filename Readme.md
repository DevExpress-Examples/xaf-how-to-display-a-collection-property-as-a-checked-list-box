<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592642/21.2.8%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1807)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [WebCheckedListBoxPropertyEditor.cs](./CS/DXExample.Module.Web/WebCheckedListBoxPropertyEditor.cs) (VB: [WebCheckedListBoxPropertyEditor.vb](./VB/DXExample.Module.Web/WebCheckedListBoxPropertyEditor.vb))
* **[WinCheckedListBoxPropertyEditor.cs](./CS/DXExample.Module.Win/WinCheckedListBoxPropertyEditor.cs) (VB: [WinCheckedListBoxPropertyEditor.vb](./VB/DXExample.Module.Win/WinCheckedListBoxPropertyEditor.vb))**
<!-- default file list end -->
# How to represent a collection property using a checked list box


<p><strong>Scenario:</strong></p>
<p><br /> 1. It is required to show a check list. It should be possible to add and remove items for this list dynamically.</p>
<p>2. There are a couple of child records, and it is required to show all available records in a compact manner, and link and unlink them from the master object quickly with check boxes. When an item is checked, this means that this record is associated with the master object.</p>
<p> </p>
<p><strong>Steps to implement:</strong></p>
<p><br /> This functionality is implemented via a custom property editor that can be used to edit XPCollection properties. There are two separate editors for WinForms and ASP.NET: <strong>WinCheckedListBoxPropertyEditor</strong> and <strong>WebCheckedListBoxPropertyEditor</strong>.</p>
<p><u>Common i</u><u>mple</u><u>m</u><u>entation details:</u></p>
<p>1. Create a custom property editor and specify that it should be used for collection properties via the <strong>PropertyEditor</strong><strong>A</strong><strong>ttribute</strong>.<br /> 2. Use a control that can populate the check boxes list based on the passed data source - <strong>CheckedListBoxControl</strong> in WinForms and <strong>ASPxCheckBoxList</strong> in ASP.NET.<br /> 3. Since an Object Space instance is required to populate the control's data source, implement the <strong>IComplexViewItem</strong> interface to pass this instance to the property editor.<br /> 4. Since control's settings depend on the property value, it is required to configure the control when the value is written to the property editor. An appropriate method is <strong>ReadValueCore</strong>.<br /> 5. Assign the control's <strong>DataSource</strong> based on the collection's items type, and check the generated list box items that present in the collection displayed via the property editor (it can be accessed via the PropertyValue property).<br /> 6. Modify the associated collection when the list box item's checked state is changed. This can be done by handling the <strong>CheckedListBoxControl</strong><strong>.</strong><strong>ItemCheck</strong> event in WinForms and <strong>ASPxCheckBoxList</strong><strong>.</strong><strong>SelectedIndexChanged</strong> in ASP.NET.<br /> 7. Open the Model Editor and assign the created property editor to the <strong>PropertyEditorType</strong> property of the required view item.</p>
<p><u>ASP</u><u>.NET</u><u> implementation's</u><u> specifics</u><u>:</u></p>
<p>1. Since the ASP.NET property editor (WebCheckedListBoxPropertyEditor) works with the ASPxEditBase descendant (ASPxCheckBoxList), inherit it from the <strong>ASPxPropertyEditor</strong> class to use the existing code to configure the property editor's settings.<br /> 2. ASPxPropertyEditor provides separate methods to create controls - <strong>CreateEditModeControlCore</strong> to create a control for the Edit mode, and <strong>CreateViewModeControlCore</strong> for the View mode. Since in both cases it is required to show a check box list, return the ASPxCheckBoxList control in both methods, but disable it in the CreateViewModeControlCore method.</p>
<p>3. Call the <strong>ASPxCheckBoxList.DataBin</strong>d method after assigning the control's data source to generate items.</p>
<p>4. Store object keys instead of entire objects in the editor to avoid issues with transferring data between requests.</p>
<p>5. Override the <strong>SetImmediatePostDataScript</strong> method to support the ImmediatePostData functionality. It is required to specify what client-side event should be used to raise an XAF callback that passes the new value to the server application. Use the <strong>SelectedIndexChanged</strong> event.</p>
<p>6. Return False in the overridden <strong>IsMemberSetterRequired</strong> method to specify that the editor should not be read-only if the bound property is read-only (because collection properties are read-only).</p>
<p><strong>See Also:</strong><br /> <a href="https://www.devexpress.com/Support/Center/p/S30847">S30847</a><br /> <a href="http://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraEditorsCheckedListBoxControltopic"><u>CheckedListBoxControl Class</u></a><br /> <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3097"><u>Implement Custom Property Editors</u></a><br /> <a href="https://www.devexpress.com/Support/Center/p/E1806">How to create a DetailView with a custom set of properties</a><br /> <a href="https://supportcenter.devexpress.com/ticket/details/t1011723">XAF Blazor - How to use a TagBox to view and edit a collection property in Detail Views</a></p>

<br/>


