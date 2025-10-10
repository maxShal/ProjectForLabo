namespace FirstProject;

using System.Text;

public partial class Form1 : Form
{
    private TextBox m1 = null!;
    private TextBox m2 = null!;
    private TextBox V = null!;
    private TextBox txtResult = null!;

    private TextBox m1_ = null!;
    private TextBox m2_ = null!;
    private TextBox V_ = null!;
    private TextBox txtResult_ = null!;

    private TextBox resPrint = null!;

    private TextBox discPrint = null!;

    private Button calculate = null!;
    public Form1()
    {
        InitializeComponent();

        InitializeMyComponent();
    }

    private void InitializeMyComponent()
    {

        m1 = new TextBox() { Location = new Point(20, 20), Width = 100, PlaceholderText = "Введите m1" };
        this.Controls.Add(m1);


        m2 = new TextBox() { Location = new Point(20, 40), Width = 100, PlaceholderText = "Введите m2" };
        this.Controls.Add(m2);

        V = new TextBox() { Location = new Point(20, 80), Width = 100, PlaceholderText = "Введите V" };
        this.Controls.Add(V);

        txtResult = new TextBox() { Location = new Point(20, 140), Width = 150, ReadOnly = true, Text = "X1" };
        this.Controls.Add(txtResult);



        m1_ = new TextBox() { Location = new Point(200, 20), Width = 100, PlaceholderText = "Введите m1" };
        this.Controls.Add(m1_);


        m2_ = new TextBox() { Location = new Point(200, 40), Width = 100, PlaceholderText = "Введите m2" };
        this.Controls.Add(m2_);

        V_ = new TextBox() { Location = new Point(200, 80), Width = 100, PlaceholderText = "Введите V" };
        this.Controls.Add(V_);

        txtResult_ = new TextBox() { Location = new Point(200, 140), Width = 150, ReadOnly = true, Text = "X2" };
        this.Controls.Add(txtResult_);



        calculate = new Button() { Location = new Point(123, 110), Text = "Расчёт" };
        calculate.Click += BtnCalculate;
        this.Controls.Add(calculate);

        resPrint = new TextBox() { Location = new Point(20, 160), Width = 150, ReadOnly = true, Text = "Результат" };
        this.Controls.Add(resPrint);

        discPrint = new TextBox() { Location = new Point(200, 160), Width = 150, ReadOnly = true, Text = "Расхождение" };
        this.Controls.Add(discPrint);
        
        
    }

    private void BtnCalculate(object? sender, EventArgs e)
    {
        if (!ExepHandler(m1, m2 ,V, out double m1v, out double m2v, out double Vv)) return;
        if (!ExepHandler(m1_, m2_ ,V_, out double m1v_, out double m2v_, out double Vv_)) return;

        double X1 = (m2v-m1v)*100/Vv;
        double X2 = (m2v_-m1v_)*100/Vv_;

        txtResult.Text ="X1 = " + X1.ToString("F3");
        txtResult_.Text = "X2 = " + X2.ToString("F3");

        double disc = Math.Abs(X1 - X2);
        double res = (X1 + X2) / 2;

        discPrint.Text = "Расхождение = " + disc.ToString("F3");
        resPrint.Text ="Результат = " + res.ToString("F3");

        if (disc > 0.05)
        {
            MessageBox.Show("Слишко бльшое расхождение.\n"+
            "Расхождение не должно привышать 0,05");
            return;
        }



        SaveFile(m1v, m2v, Vv, X1, m1v_, m2v_, Vv_, X2, res, disc);

    }

    private bool ExepHandler(TextBox t1, TextBox t2, TextBox t3, out double m1, out double m2, out double V)
    {
        m1 = 0; m2 = 0; V = 0;

        if (!double.TryParse(t1.Text, out m1) || m1 <= 0)
        {
            MessageBox.Show("Некорректные данные для m1 \n" +
            "Значение должно быть больше 0, дробные числа вводятся через запятую.");
            return false;
        }

        if (!double.TryParse(t2.Text, out m2) || m2 <= 0)
        {
            MessageBox.Show("Некорректные данные для m2 \n" +
            "Значение должно быть больше 0, дробные числа вводятся через запятую.");
            return false;
        }

        if (m2 < m1)
        {
            MessageBox.Show("m2 < m1");
            return false;
        }

        if (!double.TryParse(t3.Text, out V) || V <= 0)
        {
            MessageBox.Show("Некорректные данные для V \n" +
            "Значение должно быть больше 0, дробные числа вводятся через запятую.");
            return false;
        }

        return true;

    }

    private void SaveFile(double m1, double m2, double V, double X1, double m1_, double m2_, double V_, double X2, double res, double disc)
    {
        string filePath = "result.txt";
        var sb = new StringBuilder();

        sb.AppendLine(DateTime.Now.ToString());
        sb.AppendLine("m1 = "+m1+" m2 = "+m2+" V1 = "+V+" X1 = "+ X1.ToString("F3"));
        sb.AppendLine("m1 = "+m1_+" m2 = "+m2_+" V1 = "+V_+" X2 = "+ X2.ToString("F3"));
        sb.AppendLine("Расхождение: "+ disc.ToString("F3"));
        sb.AppendLine("Результат испытания: " + res.ToString("F3"));
        sb.AppendLine("");

        File.AppendAllText(filePath, sb.ToString());

        MessageBox.Show("Данные сохранены в файл result.txt");
    }
}
