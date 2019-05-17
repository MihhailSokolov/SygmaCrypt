Public Class Form1
    Dim key As String
    Dim encrypt As Boolean = False

    Function KeySort(ByVal AscKey1() As Integer) As Integer
        Dim m, c, i, n As Integer
        n = AscKey1.Length - 1
        For m = n To 1 Step -1
            For i = 0 To m - 1
                If AscKey1(i) > AscKey1(i + 1) Then
                    c = AscKey1(i)
                    AscKey1(i) = AscKey1(i + 1)
                    AscKey1(i + 1) = c
                End If
            Next
        Next
    End Function

    Sub Encryption()
        Try
            Dim EncTxt As String = ""
            Dim TxtLen As Decimal
            Dim EncKey As String = ""
            Dim keyLen As Decimal
            Dim TxtKey As String
            TxtLen = TextBox1.Text.Length
            keyLen = TextBox2.Text.Length
            Do Until TextBox1.Text.Length Mod TextBox2.Text.Length = 0
                TextBox1.Text = TextBox1.Text & " "
            Loop
            TxtKey = TextBox2.Text
            If TextBox1.Text.Length / TextBox2.Text.Length > 1 Then
                For i = 2 To TextBox1.Text.Length / TextBox2.Text.Length
                    key = key & TxtKey
                Next
            End If
            Dim TxtChar(TextBox1.Text.Length - 1)
            Dim KeyChar(TextBox1.Text.Length - 1)
            Dim EncryptedText(TextBox1.Text.Length - 1)
            For i = 0 To TextBox1.Text.Length - 1
                TxtChar(i) = TextBox1.Text(i)
                KeyChar(i) = key(i)
            Next
            Dim AscTxt(TextBox1.Text.Length - 1)
            Dim AscKey(TextBox1.Text.Length - 1) As Integer
            For i = 0 To TextBox1.Text.Length - 1
                AscTxt(i) = Asc(TxtChar(i))
                AscKey(i) = Asc(KeyChar(i))
            Next
            KeySort(AscKey)
            For i = 0 To TextBox1.Text.Length - 1
                EncryptedText(i) = Chr(AscTxt(i) + AscKey(i))
            Next
            For i = 0 To TextBox1.Text.Length - 1
                EncTxt = EncTxt & EncryptedText(i)
            Next
            TextBox1.Text = EncTxt
        Catch ex As Exception
            MsgBox("Error! Maybe your text is already encrypted.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Sub Decryption()
        Try
            Dim EncTxt As String = ""
            Dim EncKey As String = ""
            Dim TxtKey As String
            Dim DecTxt As String = ""
            Dim DecryptedText(TextBox1.Text.Length - 1)
            Dim TxtChar(TextBox1.Text.Length - 1)
            Dim KeyChar(TextBox1.Text.Length - 1)
            Dim EncryptedText(TextBox1.Text.Length - 1)
            Dim AscTxt(TextBox1.Text.Length - 1)
            Dim AscKey(TextBox1.Text.Length - 1) As Integer
            TxtKey = TextBox2.Text
            If TextBox1.Text.Length / TextBox2.Text.Length > 1 Then
                For i = 2 To TextBox1.Text.Length / TextBox2.Text.Length
                    key = key & TxtKey
                Next
            End If
            For i = 0 To TextBox1.Text.Length - 1
                TxtChar(i) = TextBox1.Text(i)
                KeyChar(i) = key(i)
            Next
            For i = 0 To TextBox1.Text.Length - 1
                AscTxt(i) = AscW(TxtChar(i))
                AscKey(i) = AscW(KeyChar(i))
            Next
            KeySort(AscKey)
            For i = 0 To TextBox1.Text.Length - 1
                DecryptedText(i) = ChrW(AscTxt(i) - AscKey(i))
            Next
            For i = 0 To TextBox1.Text.Length - 1
                DecTxt = DecTxt & DecryptedText(i)
            Next
            TextBox1.Text = DecTxt
        Catch ex As Exception
            MsgBox("Error! Maybe your text is already decrypted.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        key = TextBox2.Text
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
            TextBox2.Text = key
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.SelectAll()
        TextBox1.Copy()
        TextBox1.DeselectAll()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Paste()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Text field is empty.")
            Exit Sub
        Else
            SaveFileDialog1.Filter = "Text files|*.txt"
            If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
            Dim sigmacrypt_text As String = SaveFileDialog1.FileName
            Dim record As New System.IO.StreamWriter(sigmacrypt_text)
            record.Write(TextBox1.Text)
            record.Close()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = False Then Button5.Text = "Encrypt" : encrypt = True
        If RadioButton2.Checked = True Then Button5.Text = "Decrypt" : encrypt = False
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then Button5.Text = "Encrypt" : encrypt = True
        If RadioButton1.Checked = False Then Button5.Text = "Decrypt" : encrypt = False
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox1.Text = "" And TextBox2.Text = "" Then
            MsgBox("Both fields are empty.")
            Exit Sub
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Then
            If TextBox1.Text = "" Then
                MsgBox("Text field is empty.")
                Exit Sub
            ElseIf TextBox2.Text = "" Then
                MsgBox("Key field is empty.")
                Exit Sub
            End If
        Else
            If encrypt = False Then Decryption()
            If encrypt = True Then Encryption()
        End If
    End Sub
End Class
