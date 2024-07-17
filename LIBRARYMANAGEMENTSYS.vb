Imports System.Data.SqlClient
Public Class Form1
    Private connectionString As String = "Data Source=desktop-0ul0q6c\msql;Initial Catalog=LibraryDB;Integrated Security=True"
    Private connection As SqlConnection

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection = New SqlConnection(connectionString)
        LoadBooks()
    End Sub

    Private Sub LoadBooks()
        Dim query As String = "SELECT * FROM Books"
        Dim adapter As New SqlDataAdapter(query, connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        dgvBooks.DataSource = table
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim query As String = "INSERT INTO Books (Title, Author, YearPublished, Genre) VALUES (@Title, @Author, @YearPublished, @Genre)"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@Title", txttitle.Text)
        command.Parameters.AddWithValue("@Author", txtAuthor.Text)
        command.Parameters.AddWithValue("@YearPublished", Convert.ToInt32(txtYearPublished.Text))
        command.Parameters.AddWithValue("@Genre", txtGenre.Text)

        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
        LoadBooks()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvBooks.SelectedRows.Count > 0 Then
            Dim id As Integer = Convert.ToInt32(dgvBooks.SelectedRows(0).Cells("ID").Value)
            Dim query As String = "UPDATE Books SET Title=@Title, Author=@Author, YearPublished=@YearPublished, Genre=@Genre WHERE ID=@ID"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ID", id)
            command.Parameters.AddWithValue("@Title", txttitle.Text)
            command.Parameters.AddWithValue("@Author", txtAuthor.Text)
            command.Parameters.AddWithValue("@YearPublished", Convert.ToInt32(txtYearpublished.Text))
            command.Parameters.AddWithValue("@Genre", txtGenre.Text)

            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()
            LoadBooks()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If dgvBooks.SelectedRows.Count > 0 Then
            Dim id As Integer = Convert.ToInt32(dgvBooks.SelectedRows(0).Cells("ID").Value)
            Dim query As String = "DELETE FROM Books WHERE ID=@ID"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ID", id)
            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()
            LoadBooks()
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        txttitle.Clear()
        txtAuthor.Clear()
        txtYearpublished.Clear()
        txtGenre.Clear()


    End Sub

    Private Sub dgvBooks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooks.CellContentClick

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvBooks.Rows(e.RowIndex)
            txttitle.Text = row.Cells("Title").Value.ToString()
            txtAuthor.Text = row.Cells("Author").Value.ToString()
            txtYearpublished.Text = row.Cells("YearPublished").Value.ToString()
            txtGenre.Text = row.Cells("Genre").Value.ToString()
        End If


    End Sub
End Class