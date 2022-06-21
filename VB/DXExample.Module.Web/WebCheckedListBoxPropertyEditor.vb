Imports System
Imports DevExpress.Xpo
Imports System.Web.UI.WebControls
Imports DevExpress.ExpressApp.Web
Imports DevExpress.Web
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Web.Editors.ASPx

Namespace DXExample.Module.Web
    <PropertyEditor(GetType(XPBaseCollection), False)> _
    Public Class WebCheckedListBoxPropertyEditor
        Inherits ASPxPropertyEditor
        Implements IComplexViewItem
        Public Sub New(ByVal objectType As Type, ByVal model As IModelMemberViewItem)
            MyBase.New(objectType, model)
        End Sub
        Private application As XafApplication
        Private objectSpace As IObjectSpace
        Protected Overrides Function CreateEditModeControlCore() As WebControl
            Return New ASPxCheckBoxList()
        End Function
        Protected Overrides Function CreateViewModeControlCore() As WebControl
            Dim control As New ASPxCheckBoxList()
            control.ClientEnabled = False
            Return control
        End Function
        Private checkedItems As XPBaseCollection
        Protected Overrides Sub ReadValueCore()
            MyBase.ReadValueCore()
            If TypeOf PropertyValue Is XPBaseCollection Then
                Dim control As ASPxCheckBoxList = If(ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit, Editor, InplaceViewModeEditor)
                RemoveHandler control.SelectedIndexChanged, AddressOf Control_SelectedIndexChanged
                checkedItems = CType(PropertyValue, XPBaseCollection)
                Dim dataSource As New XPCollection(checkedItems.Session, MemberInfo.ListElementType)
                Dim classInfo As IModelClass = application.Model.BOModel.GetClass(MemberInfo.ListElementTypeInfo.Type)
                If checkedItems.Sorting.Count > 0 Then
                    dataSource.Sorting = checkedItems.Sorting
                ElseIf (Not String.IsNullOrEmpty(classInfo.DefaultProperty)) Then
                    dataSource.Sorting.Add(New SortProperty(classInfo.DefaultProperty, DevExpress.Xpo.DB.SortingDirection.Ascending))
                End If
                control.DataSource = dataSource
                control.TextField = classInfo.DefaultProperty
                control.ValueField = classInfo.KeyProperty
                control.ValueType = classInfo.TypeInfo.KeyMember.MemberType
                control.DataBind()
                control.UnselectAll()
                For Each obj As Object In checkedItems
                    control.Items.FindByValue(objectSpace.GetKeyValue(obj)).Selected = True
                Next obj
                AddHandler control.SelectedIndexChanged, AddressOf Control_SelectedIndexChanged
            End If
        End Sub
        Private Sub Control_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim control As ASPxCheckBoxList = CType(sender, ASPxCheckBoxList)
            For Each item As ListEditItem In control.Items
                Dim obj As Object = objectSpace.GetObjectByKey(MemberInfo.ListElementTypeInfo.Type, item.Value)
                If item.Selected Then
                    checkedItems.BaseAdd(obj)
                Else
                    checkedItems.BaseRemove(obj)
                End If
            Next item
            OnControlValueChanged()
            objectSpace.SetModified(CurrentObject)
        End Sub
        Public Shadows ReadOnly Property Editor() As ASPxCheckBoxList
            Get
                Return CType(MyBase.Editor, ASPxCheckBoxList)
            End Get
        End Property
        Public Shadows ReadOnly Property InplaceViewModeEditor() As ASPxCheckBoxList
            Get
                Return CType(MyBase.InplaceViewModeEditor, ASPxCheckBoxList)
            End Get
        End Property
        Protected Overrides Sub SetImmediatePostDataScript(ByVal script As String)
            Editor.ClientSideEvents.SelectedIndexChanged = script
        End Sub
        Protected Overrides Function IsMemberSetterRequired() As Boolean
            Return False
        End Function

        #Region "IComplexViewItem Members"

        Public Sub Setup(ByVal objectSpace As IObjectSpace, ByVal application As XafApplication) Implements IComplexViewItem.Setup
            Me.application = application
            Me.objectSpace = objectSpace
        End Sub

        #End Region
    End Class
End Namespace