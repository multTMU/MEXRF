Partial Class KED_base1DataSet
    Partial Public Class Constants_for_fitDataTable
        Private Sub Constants_for_fitDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.IndexColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

        Private Sub Constants_for_fitDataTable_Constants_for_fitRowChanging(sender As Object, e As Constants_for_fitRowChangeEvent) Handles Me.Constants_for_fitRowChanging


        End Sub

    End Class
End Class
