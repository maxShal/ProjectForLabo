namespace FirstProject;

public partial class Form1 : Form
{
    private TextBox m1 = null!;
    private TextBox m2 = null!;
    private TextBox V = null!;
    private TextBox txtResult = null!;
    private Button calculate = null!;
    public Form1()
    {
        InitializeComponent();

        InitializeMyComponent();
    }

    private void InitializeMyComponent()
    {

        m1 = new TextBox(){Location = new Point(20, 20), Width = 100, PlaceholderText = "Введите m1"};
        this.Controls.Add(m1);


        m2 = new TextBox(){Location = new Point(20, 40), Width = 100, PlaceholderText = "Введите m2"};
        this.Controls.Add(m2);

        V = new TextBox(){Location = new Point(20, 80), Width = 100, PlaceholderText = "Введите V"};
        this.Controls.Add(V);

        calculate = new Button(){Location = new Point(20, 100), Text = "Расчёт"};
        calculate.Click += BtnCalculate;
        this.Controls.Add(calculate);

        txtResult = new TextBox(){Location = new Point(20, 140), Width = 150, ReadOnly = true};
        this.Controls.Add(txtResult);
    }

    private void BtnCalculate(object? sender, EventArgs e)
    {
        if(!double.TryParse(m1.Text, out double param1)|| param1<= 0)
        {
            MessageBox.Show("Некорректные данные для m1");
            return;
        }

        if(!double.TryParse(m2.Text, out double param2)|| param2<= 0)
        {
            MessageBox.Show("Некорректные данные для m2");
            return;
        }

        if(param2<param1)
        {
            MessageBox.Show("m2 < m1");
            return;
        }

        if(!double.TryParse(V.Text, out double param3) || param3 <= 0)
        {
            MessageBox.Show("Некорректные данные для V");
            return;
        }

        double result = ((param2-param1)*100)/param3;

        txtResult.Text = result.ToString();



        SaveFile(param1, param2, param3, result);

    }

    private void SaveFile(double m1, double m2, double V, double result)
    {
        string filePath = "result.txt";
        string line = DateTime.Now +": m1=" + m1 + ", m2=" + m2 + ", V=" + V + ", result=" + result;
        File.AppendAllText(filePath, line + Environment.NewLine);

        MessageBox.Show("Данные сохранены");
    }
}
