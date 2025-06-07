Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim cb As MySqlCommandBuilder

    Sub koneksi()
        conn = New MySqlConnection
        conn.ConnectionString = "server=127.0.0.1; userid=root; password=''; database=db_latihan"
        Try
            conn.Open()
            MessageBox.Show("Koneksi Berhasil")
            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            conn.Close()
        End Try

        Dim path As New Drawing2D.GraphicsPath()
        path.StartFigure()
        path.AddArc(New Rectangle(0, 0, 20, 20), 180, 90) ' Sudut kiri atas
        path.AddArc(New Rectangle(btnTambah.Width - 20, 0, 20, 20), 270, 90) ' Sudut kanan atas
        path.AddArc(New Rectangle(btnTambah.Width - 20, btnTambah.Height - 20, 20, 20), 0, 90) ' Sudut kanan bawah
        path.AddArc(New Rectangle(0, btnTambah.Height - 20, 20, 20), 90, 90) ' Sudut kiri bawah
        path.CloseFigure()

        btnTambah.Region = New Region(path) ' Terapkan region ke tombol
        btnTambah.FlatStyle = FlatStyle.Flat
        btnTambah.FlatAppearance.BorderSize = 0

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ds = New DataSet()
        koneksi()

        da = New MySqlDataAdapter("SELECT * FROM tblbarang", conn)
        da.Fill(ds, "tblbarang")

        DataGridView1.DataSource = ds.Tables("tblbarang").DefaultView

        DataGridView1.Columns("Kode_Barang").HeaderText = "Kode Barang"
        DataGridView1.Columns("Nama_Barang").HeaderText = "Nama Barang"
        DataGridView1.Columns("Jenis").HeaderText = "Jenis"
        DataGridView1.Columns("Satuan").HeaderText = "Satuan"
        DataGridView1.Columns("Harga_Beli").HeaderText = "Harga Beli"
        DataGridView1.Columns("Harga_Jual").HeaderText = "Harga Jual"
        DataGridView1.Columns("Stock").HeaderText = "Stock"

        DataGridView1.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10)
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            no.Text = selectedRow.Cells("Kode_Barang").Value.ToString()
            txtNamaBarang.Text = selectedRow.Cells("Nama_Barang").Value.ToString()
            txtJenis.Text = selectedRow.Cells("Jenis").Value.ToString()
            txtSatuan.Text = selectedRow.Cells("Satuan").Value.ToString()
            txtHargaBeli.Text = selectedRow.Cells("Harga_Beli").Value.ToString()
            txtHargaJual.Text = selectedRow.Cells("Harga_Jual").Value.ToString()
            txtStock.Text = selectedRow.Cells("Stock").Value.ToString()
        End If
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        ' Tambahkan data hanya ke DataSet tanpa menyimpannya ke database
        Dim newRow As DataRow = ds.Tables("tblbarang").NewRow()
        newRow("Kode_Barang") = no.Text
        newRow("Nama_Barang") = txtNamaBarang.Text
        newRow("Jenis") = txtJenis.Text
        newRow("Satuan") = txtSatuan.Text

        Try
            newRow("Harga_Beli") = Integer.Parse(txtHargaBeli.Text)
            newRow("Harga_Jual") = Integer.Parse(txtHargaJual.Text)
            newRow("Stock") = Integer.Parse(txtStock.Text)
        Catch ex As FormatException
            MsgBox("Input untuk Harga Beli, Harga Jual, atau Stock harus berupa angka.")
            Exit Sub
        End Try

        ds.Tables("tblbarang").Rows.Add(newRow)

        MsgBox("Data berhasil ditambahkan ke DataGridView (belum disimpan ke database)")

        ClearTextBoxes()
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        ' Simpan data dari DataSet ke database
        cb = New MySqlCommandBuilder(da)
        da.Update(ds, "tblbarang")

        MsgBox("Data berhasil disimpan ke database")
    End Sub

    Private Sub btnUbah_Click(sender As Object, e As EventArgs) Handles btnUbah.Click
        For Each row As DataRow In ds.Tables("tblbarang").Rows
            If row("Kode_Barang").ToString() = no.Text Then
                row("Nama_Barang") = txtNamaBarang.Text
                row("Jenis") = txtJenis.Text
                row("Satuan") = txtSatuan.Text

                Try
                    row("Harga_Beli") = Integer.Parse(txtHargaBeli.Text)
                    row("Harga_Jual") = Integer.Parse(txtHargaJual.Text)
                    row("Stock") = Integer.Parse(txtStock.Text)
                Catch ex As FormatException
                    MsgBox("Input untuk Harga Beli, Harga Jual, atau Stock harus berupa angka.")
                    Exit Sub
                End Try
            End If
        Next

        MsgBox("Data berhasil diubah di DataGridView (belum disimpan ke database)")
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        For Each row As DataRow In ds.Tables("tblbarang").Rows
            If row("Kode_Barang").ToString() = no.Text Then
                row.Delete()
            End If
        Next

        MsgBox("Data berhasil dihapus dari DataGridView (belum disimpan ke database)")

        ClearTextBoxes()
    End Sub

    Private Sub ClearTextBoxes()
        no.Text = ""
        txtNamaBarang.Text = ""
        txtJenis.Text = ""
        txtSatuan.Text = ""
        txtHargaBeli.Text = ""
        txtHargaJual.Text = ""
        txtStock.Text = ""
    End Sub
End Class
