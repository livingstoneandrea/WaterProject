
Public Class Bill_calcfrm
    Public total As Double
    Dim price As Array = {33, 30}
    Dim login_form As New login_form()
    Dim pay_method As String


    Private Sub Bill_calcfrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Randomize()
        Dim randomBill As Double = Int(Rnd() * 1000)


        MessageBox.Show("random genereted is " & randomBill)

        lblwater_unit.Text = randomBill.ToString
        lblunits_used.Text = randomBill.ToString
        ' pnlElectrical_payment.Hide()
        pnlmanual_payment.Hide()
        pnluser_account.Visible = True
        pnlPay_confirm.Visible = False
        Panel7.Visible = False

    End Sub
    Private Sub btnuser_account_Click(sender As Object, e As EventArgs) Handles btnuser_account.Click
        pnluser_account.Visible = True
        pnlcheck_waterBill.Visible = False
        pnlReceipt_page.Visible = False
        pnlpayment_page.Visible = False
        PnlTotal_due.Hide()

    End Sub

    Private Sub btnWater_bill_Click(sender As Object, e As EventArgs) Handles btnWater_bill.Click
        pnluser_account.Visible = False
        pnlReceipt_page.Visible = False
        pnlpayment_page.Visible = False
        PnlTotal_due.Hide()
        pnlcheck_waterBill.Visible = True

    End Sub

    Private Sub btnPay_waterBill_Click(sender As Object, e As EventArgs) Handles btnPay_waterBill.Click
        pnlpayment_page.Visible = True
        pnluser_account.Visible = False
        pnlcheck_waterBill.Visible = False
        pnlReceipt_page.Visible = False
        PnlTotal_due.Hide()


    End Sub

    Private Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
        pnluser_account.Visible = False
        pnlpayment_page.Visible = False
        pnlcheck_waterBill.Visible = False
        PnlTotal_due.Hide()
        pnlReceipt_page.Visible = True

    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs)
        'Dim clientType As String = cmbclient_type.Text
        'Dim pay_method As String = cmbpayment_method.SelectedItem
        ' payMethod(pay_method)

    End Sub


    Public Sub calculateWater_Bill(ByVal client_type)
        Dim waterUnits As Double = Val(lblwater_unit.Text)
        Dim amount_paid As Integer


        MsgBox("paymethod is " & pay_method)
        Dim meter_rent As Double
        Dim princ_amount As Double
        Dim discount As Double

        Dim totalamount As Double
        Dim subTotal As Double
        Dim total_discount As Double
        Dim p As Double

        Select Case client_type
            Case "metered"
                meter_rent = 500.0
                lblmeter_rent.Text = meter_rent
                discount = 10
                If waterUnits > 100 Then
                    p = price(1)
                    discount += 9

                Else
                    p = price(0)

                End If

                If pay_method Is "Electronic" Then
                    discount += 5
                    ' pnlElectrical_payment.Show()
                Else
                    discount += 0.00
                    ' pnlmanual_payment.Show()
                End If
                total_discount = discount
                princ_amount = waterUnits * p

                'MessageBox.Show(total_discount.ToString)
                ' MessageBox.Show(princ_amount)


                subTotal = (princ_amount - (princ_amount * discount / 100))
                lbl_subtotal.Text = subTotal
                totalamount = subTotal + meter_rent

                ' MessageBox.Show("total calculated is :" & totalamount)

                total = princ_amount
            Case "unmetered"
                meter_rent = 0.0
                lblmeter_rent.Text = meter_rent
                If waterUnits > 100 Then
                    p = price(1)
                    discount += 9
                Else
                    p = price(0)

                End If

                If pay_method Is "Electronic" Then
                    discount += 5

                Else
                    discount += 0.0

                End If
                total_discount = discount
                princ_amount = waterUnits * p
                subTotal = (princ_amount - (princ_amount * discount / 100))

                totalamount = subTotal
                totalamount = subTotal + meter_rent
                total = princ_amount
                ' MessageBox.Show("total calculated is :" & totalamount)
        End Select
        ' MessageBox.Show(pay_method)
        lbldiscount.Text = total_discount

        lbdiscount_amount.Text = (princ_amount * discount / 100)
        lbl_amount.Text = princ_amount
        lbl_subtotal.Text = subTotal
        lblTotal.Text = totalamount.ToString
        lblprice_per_unit.Text = p
        lbInst_checkAmount.Text = totalamount.ToString

        Dim credit As Double
        Dim debit As Double
        If Val(txtAmount_sub.Text) > totalamount Then
            credit = Val(txtAmount_sub.Text) - totalamount
        ElseIf Val(txtAmount_sub.Text) < totalamount Then
            debit = totalamount - Val(txtAmount_sub.Text)
        Else
            credit = 0.0
            debit = 0.0
        End If


        lblcredit.Text = credit
        lbldebit.Text = debit
        Panel7.Visible = True
        'MessageBox.Show("credit is :" & credit)

    End Sub

    Private Sub btnInstant_pay_Click(sender As Object, e As EventArgs) Handles btnInstant_pay.Click
        Dim p As Integer
        PnlTotal_due.Show()
        If CType(lblwater_unit.Text, Integer) > 100 Then
            p = price(1)
        Else
            p = price(0)
        End If

        total = CType(lblwater_unit.Text, Integer) * p
        lblTotalDue.Text = total.ToString

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        PnlTotal_due.Hide()
    End Sub

    Private Sub pic_closeBtn_Click(sender As Object, e As EventArgs) Handles pic_closeBtn.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub picElect_pay_closeBtn_Click(sender As Object, e As EventArgs) Handles picElect_pay_closeBtn.Click

        pnlElectrical_payment.Hide()
    End Sub

    Private Sub picCancelManual_pay_Click(sender As Object, e As EventArgs) Handles picCancelManual_pay.Click
        pnlmanual_payment.Hide()
    End Sub

    Private Sub btnElectricall_pay_Click(sender As Object, e As EventArgs) Handles btnElectricall_pay.Click
        Dim clientType = lblClient.Text

        calculateWater_Bill(clientType)
        pnlPay_confirm.Visible = True
        MessageBox.Show("Thank your for your payment, confirmed payment was  succeful!!!")

    End Sub

    Private Sub btnElectronic_pay_Click(sender As Object, e As EventArgs) Handles btnElectronic_pay.Click
        pnlElectrical_payment.Visible = True
        pay_method = "Electronic"

    End Sub

    Private Sub btnManual_pay_Click(sender As Object, e As EventArgs) Handles btnManual_pay.Click
        pnlmanual_payment.Show()
        pay_method = "Manual/cash"

    End Sub

    Private Sub btnmanual_payment_Click(sender As Object, e As EventArgs) Handles btnmanual_payment.Click
        Dim clientType = lblClient.Text

        calculateWater_Bill(clientType)
        pnlPay_confirm.Visible = True
    End Sub

    Private Sub btnlog_out_Click(sender As Object, e As EventArgs) Handles btnlog_out.Click
        Me.Dispose()
        Me.Close()
        login_form.Visible = True

    End Sub

    Private Sub btnTrans_receipt_Click(sender As Object, e As EventArgs) Handles btnTrans_receipt.Click
        pnlReceipt_page.Visible = True
        pnlpayment_page.Visible = False

    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        Me.WindowState = WindowState.Minimized
    End Sub
End Class